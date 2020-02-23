using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manina.Windows.Forms;
using System.Threading;
using System.IO;

namespace ImageGallery
{
    using Database;
    using Database.Models;
    using System.Collections.Concurrent;
    using System.Runtime.Caching;

    class FileModelAdapter : ImageListView.ImageListViewItemAdaptor
    {
        private const int ThumbnailLoadTimeout = 4;
        private static readonly ImageConverter _imageConverter;
        private static readonly ConcurrentDictionary<string, byte[]> _iconCache;
        
        public static void RefreshIconCache()
        {
            _iconCache.Clear();
        }
        string ColumnGroup { get; set; }
        static FileModelAdapter()
        {
            _imageConverter = new ImageConverter();
            _iconCache = new ConcurrentDictionary<String, byte[]>();
        }


        private bool disposed;
        public override void Dispose()
        {
            disposed = true;
        }

        public override Utility.Tuple<ColumnType, string, object>[] GetDetails(object key)
        {
            List<Utility.Tuple<ColumnType, string, object>> details = new List<Utility.Tuple<ColumnType, string, object>>();
            return details.ToArray();
        }

        public override string GetSourceImage(object key)
        {
            if (disposed)
            {
                return null;
            }
            File file = (File)key;
            return file.FullName;
        }

        // TODO: factor this out of filemodeladaptor
        public static Image getThumbnailFromFile(File file)
        {
            if (file.Thumbnail == null)
            {
                try
                {
                    /*
                    if(!_iconCache.ContainsKey(file.Extension))
                    {
                        _iconCache.TryAdd(file.Extension, IconHelpers.GetExtraLargeIcon(file.Extension));
                    }
                    */
                    
                    byte[] imageBytes = null;
                    for (int retries = 0; retries < 3; ++retries)
                    {
                        try
                        {
                            imageBytes = _iconCache.GetOrAdd(file.Extension, ext => IconHelpers.GetExtraLargeIcon(ext));
                            return (Bitmap)_imageConverter.ConvertFrom(imageBytes);
                        }
                        catch (System.NullReferenceException)
                        {

                        }
                        Thread.Sleep(300);
                    }
                    return null;

                    
                    
                    //return _iconCache[file.Extension];
                    
                        //var task = Task.Run(() => IconHelpers.GetExtraLargeIcon(file.Extension));
                    //if (task.Wait(TimeSpan.FromSeconds(ThumbnailLoadTimeout)))
                    //{
                     //   return (Bitmap)_imageConverter.ConvertFrom(task.Result);
                    //}
                    
                    //return IconHelpers.GetExtraLargeIcon(file.Extension);
                    // TODO: add fallback icons instead of returning null
                }
                catch (ArgumentException)
                {
                    return null;
                }
                catch (FileNotFoundException)
                {
                    return null;
                }
            }
            try
            {
                return MakeImageFromJpegBlob(file.Thumbnail);
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }
        public override Image GetThumbnail(object key, Size size, UseEmbeddedThumbnails useEmbeddedThumbnails, bool useExifOrientation)
        {
            if (disposed)
            {
                return null;
            }
            File file = (File)key;

            return getThumbnailFromFile(file);
        }

        private static Image MakeImageFromJpegBlob(byte[] blob)
        {
            return (Bitmap)_imageConverter.ConvertFrom(blob);
        }

        public override string GetUniqueIdentifier(object key, Size size, UseEmbeddedThumbnails useEmbeddedThumbnails, bool useExifOrientation)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
