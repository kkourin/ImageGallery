using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using LibVLCSharp.Shared;
using System.Drawing.Imaging;

namespace ImageGallery
{
    public class VideoThumbnailExtractor
    {
        public class VLCHolder
        {
            public class VLCPackage
            {
                public VLCHolder holder;
            }
            public LibVLC LibVLC { get; }
            public MediaPlayer MediaPlayer { get; }
            public CancellationTokenSource stoppedTokenSource { get; set; }

            public VLCHolder()
            {
                LibVLC = new LibVLC();
                MediaPlayer = new MediaPlayer(LibVLC);
                MediaPlayer.Stopped += MediaPlayer_Stopped;
            }

            private void MediaPlayer_Stopped(object sender, EventArgs e)
            {
                Console.WriteLine("stopped");
                stoppedTokenSource.Cancel();
            }

            public static Task MakeVLCThread(VLCPackage package, ManualResetEvent e)
            {
                return Task.Factory.StartNew(() =>
                {
                    package.holder = new VLCHolder();
                    e.Set();
                    while (true)
                    {
                        Thread.Sleep(70);
                    }
                }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            }

        }

        private uint _width;
        private uint _height;

        /// <summary>
        /// RGBA is used, so 4 byte per pixel, or 32 bits.
        /// </summary>
        private uint BytePerPixel = 4;

        /// <summary>
        /// the number of bytes per "line"
        /// For performance reasons inside the core of VLC, it must be aligned to multiples of 32.
        /// </summary>
        private uint Pitch;

        /// <summary>
        /// The number of lines in the buffer.
        /// For performance reasons inside the core of VLC, it must be aligned to multiples of 32.
        /// </summary>
        private uint Lines;

        private CancellationTokenSource _cts = new CancellationTokenSource();


        private static uint Align(uint size)
        {
            if (size % 32 == 0)
            {
                return size;
            }

            return ((size / 32) + 1) * 32;// Align on the next multiple of 32
        }


        private MemoryMappedFile CurrentMappedFile;
        private MemoryMappedViewAccessor CurrentMappedViewAccessor;
        private readonly BlockingCollection<(MemoryMappedFile file, MemoryMappedViewAccessor accessor)?> firstCaptureFile = new BlockingCollection<(MemoryMappedFile file, MemoryMappedViewAccessor accessor)?>(1);

        private readonly BlockingCollection<(MemoryMappedFile file, MemoryMappedViewAccessor accessor)?> secondCaptureFile = new BlockingCollection<(MemoryMappedFile file, MemoryMappedViewAccessor accessor)?>(1);

        private const uint DefaultWidth = 320;
        private const uint DefaultHeight = 320;
        private const int SecondFrame = 12;
        private int _currentFrame = 0;

        private const int timeout = 10000;

        private LibVLC _libVLC;
        private MediaPlayer _mp;

        private bool _useSecondFrame = false;
        //private VLCTaskQueue _queue;
        private VLCHolder _vlcHolder;
        public void Reset()
        {
            // Empty file.
            firstCaptureFile.TryTake(out _);
            _currentFrame = 0;
            _cts = new CancellationTokenSource();
            _vlcHolder.stoppedTokenSource = _cts;
        }

        public Bitmap ResizeToDimensions(Bitmap bmp)
        {
            uint w = 0;
            uint h = 0;
            _mp.Size(0, ref w, ref h);
            if (w == 0 || h == 0)
            {
                return null;
            }
            double currentRatio = (double)w / h;
            double captureRatio = (double)bmp.Width / bmp.Height;
            if (currentRatio > captureRatio)
            {
                double scaleFactor = (double)bmp.Width / w;
                w = (uint)bmp.Width;
                h = (uint)Math.Round(h * scaleFactor);
            }
            else
            {
                double scaleFactor = (double)bmp.Height / h;
                h = (uint)bmp.Height;
                w = (uint)Math.Round(w * scaleFactor);
            }
            Bitmap resizedBitmap;
            try
            {
                resizedBitmap = new Bitmap((int)w, (int)h, PixelFormat.Format32bppArgb);
                Graphics resizedGraphic = Graphics.FromImage(resizedBitmap);
                resizedGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                resizedGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                resizedGraphic.DrawImage(bmp, new Rectangle(0, 0, (int)w, (int)h));
            }
            catch (ArgumentException)
            {
                return null;
            }
            return resizedBitmap;
        }

        public VideoThumbnailExtractor() : this(DefaultWidth, DefaultHeight, false)
        {
            
        }
        public VideoThumbnailExtractor(uint width, uint height, bool useSecondFrame)
        {
            VLCHolder.VLCPackage pack = new VLCHolder.VLCPackage();
            ManualResetEvent get = new ManualResetEvent(false);
            VLCHolder.MakeVLCThread(pack, get);
            get.WaitOne();
            var holder = pack.holder;
            _libVLC = holder.LibVLC;
            _mp = holder.MediaPlayer;
            _vlcHolder = holder;
            _mp.EnableHardwareDecoding = true;
            Pitch = Align(width * BytePerPixel);
            Lines = Align(height);
            _mp.SetVideoFormat("RV32", width, height, Pitch);
            _mp.SetVideoCallbacks(Lock, null, Display);
            _width = width;
            _height = height;
            _useSecondFrame = useSecondFrame;

        }


        public Image GenerateFromPath(string path)
        {
            // Check file exists?
            Reset();
            var media = new Media(_libVLC, path, FromType.FromPath);
            media.AddOption(":no-audio");
            _mp.Media = media;
            _mp.SetSpu(100);
            _mp.Play();
            var task = GrabThumbnailTask(_cts.Token);
            _cts.CancelAfter(timeout);
            var imageBytes = task.Result;
            _mp.Stop();
            return imageBytes;
        }

        private Task<Image> GrabThumbnailTask(CancellationToken token)
        {
            return Task.Run(() => GrabThumbnail(token));
        }

        private Image GrabThumbnail(CancellationToken token)
        {
            Image output = null;
            (MemoryMappedFile file, MemoryMappedViewAccessor accessor)? firstFileTuple = null;
            (MemoryMappedFile file, MemoryMappedViewAccessor accessor)? secondFileTuple = null;
            try
            {
                firstCaptureFile.TryTake(out firstFileTuple, -1, token);
                if (_useSecondFrame)
                {
                    secondCaptureFile.TryTake(out secondFileTuple, -1, token);
                }
            }
            catch (OperationCanceledException)
            {

            }

            if (!_mp.IsSeekable)
            {
                return null;
            }

            (MemoryMappedFile file, MemoryMappedViewAccessor accessor) usedFileTuple;
            if (secondFileTuple.HasValue)
            {
                usedFileTuple = secondFileTuple.Value;
            }
            else if (firstFileTuple.HasValue)
            {
                usedFileTuple = firstFileTuple.Value;
            }
            else
            {
                return null;
            }
            var accessor = usedFileTuple.accessor;
            var file = usedFileTuple.file;

            var stream = file.CreateViewStream();
            byte[] streamBytes = new byte[stream.Length];
            stream.Read(streamBytes, 0, (int)stream.Length);
            GCHandle pinnedArray = GCHandle.Alloc(streamBytes, GCHandleType.Pinned);
            IntPtr bytesPtr = pinnedArray.AddrOfPinnedObject();
            using (var bmp = new Bitmap((int)(Pitch / BytePerPixel), (int)Lines, (int)(Pitch / BytePerPixel) * (int)BytePerPixel, PixelFormat.Format32bppArgb, bytesPtr))
            {
                output = ResizeToDimensions(bmp);
            }
            pinnedArray.Free();
            accessor.Dispose();
            file.Dispose();
            return output;

        }


        private IntPtr Lock(IntPtr opaque, IntPtr planes)
        {
            CurrentMappedFile = MemoryMappedFile.CreateNew(null, Pitch * Lines);
            CurrentMappedViewAccessor = CurrentMappedFile.CreateViewAccessor();
            Marshal.WriteIntPtr(planes, CurrentMappedViewAccessor.SafeMemoryMappedViewHandle.DangerousGetHandle());
            return IntPtr.Zero;
        }

        private void Display(IntPtr opaque, IntPtr picture)
        {
            if (_currentFrame == 0) //remove
            {

                Console.WriteLine("Grabbed");
                firstCaptureFile.Add((CurrentMappedFile, CurrentMappedViewAccessor));
            }
            else if (_useSecondFrame && _currentFrame == SecondFrame)
            {
                secondCaptureFile.Add((CurrentMappedFile, CurrentMappedViewAccessor));
            }
            else
            {
                CurrentMappedViewAccessor.Dispose();
                CurrentMappedFile.Dispose();

            }
            CurrentMappedFile = null;
            CurrentMappedViewAccessor = null;
            ++_currentFrame;
        }
    }

}