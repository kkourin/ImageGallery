using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageGallery
{
    public static class Helpers
    {
        public static readonly string[] ImageFileExtensions = new string[] { "jpg", "jpeg", "png", "gif", "bmp", "ico", "tif", "tiff" };
        public static readonly string[] TextFileExtensions = new string[] { "txt", "log", "nfo", "c", "cpp", "cc", "cxx", "h", "hpp", "hxx", "cs", "vb", "html", "htm", "xhtml", "xht", "xml", "css", "js", "php", "bat", "java", "lua", "py", "pl", "cfg", "ini", "dart", "go", "gohtml" };
        public static readonly string[] VideoFileExtensions = new string[] { "mp4", "webm", "mkv", "avi", "vob", "ogv", "ogg", "mov", "qt", "wmv", "m4p", "m4v", "mpg", "mp2", "mpeg", "mpe", "mpv", "m2v", "m4v", "flv", "f4v" };
        public static readonly string[] AudioFileExtensions = new string[] { "3gp", "aac", "ac3", "m4a", "caf", "xm", "flac", "mod", "mp3", "ape", "pls", "opus", "ra", "ram", "spx", "tta", "ogg", "wav" };
        private static readonly Color DefaultBgColour;

        static Helpers()
        {
            DefaultBgColour = Color.FromArgb(255, 238, 238, 238);
        }

        public static string GetFilenameExtension(string filePath, bool includeDot = false)
        {
            string extension = "";

            if (!string.IsNullOrEmpty(filePath))
            {
                int pos = filePath.LastIndexOf('.');

                if (pos >= 0)
                {
                    extension = filePath.Substring(pos + 1);

                    if (includeDot)
                    {
                        extension = "." + extension;
                    }
                }
            }

            return extension;
        }
        public static bool CheckExtension(string filePath, IEnumerable<string> extensions)
        {
            string ext = GetFilenameExtension(filePath);

            if (!string.IsNullOrEmpty(ext))
            {
                return extensions.Any(x => ext.Equals(x, StringComparison.OrdinalIgnoreCase));
            }

            return false;
        }
        public static bool IsImageFile(string filePath)
        {
            return CheckExtension(filePath, ImageFileExtensions);
        }

        public static bool IsVideoFile(string filePath)
        {
            return CheckExtension(filePath, VideoFileExtensions);
        }

        public static bool IsAudioFile(string filePath)
        {
            return CheckExtension(filePath, AudioFileExtensions);
        }

        // Throws argument exception. Warning: this does not check if the file has a image file extension by default.
        public static Image LoadImage(FileInfo file, long fileSizeLimit, bool checkExtension = false)
        {
            Console.WriteLine($"Loading image {file.FullName}");
            if (checkExtension && !IsImageFile(file.Name))
            {
                throw new ArgumentException($"File is not an image: {file.FullName}");
            }
            if (!file.Exists)
            {
                throw new ArgumentException($"File does not exist: {file.FullName}");
            }
            if (file.Length > fileSizeLimit)
            {
                throw new ArgumentException($"File is too large: {file.FullName}");
            }

            // http://stackoverflow.com/questions/788335/why-does-image-fromfile-keep-a-file-handle-open-sometimes
            Image img = Image.FromStream(new MemoryStream(File.ReadAllBytes(file.FullName)));

            return img;
        }

        public static Image CreateThumbnail(Image img, int width, int height, Color backgroundColor)
        {
            double srcRatio = (double)img.Width / img.Height;
            double dstRatio = (double)width / height;
            int w, h;

            if (srcRatio <= dstRatio)
            {
                if (srcRatio >= 1)
                {
                    w = (int)(img.Height * dstRatio);
                }
                else
                {
                    w = (int)(img.Width / srcRatio * dstRatio);
                }

                h = img.Height;
            }
            else
            {
                w = img.Width;

                if (srcRatio >= 1)
                {
                    h = (int)(img.Height / dstRatio * srcRatio);
                }
                else
                {
                    h = (int)(img.Height * srcRatio / dstRatio);
                }
            }

            int x = (img.Width - w) / 2;
            int y = (img.Height - h) / 2;

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                SetHighQuality(g);
                g.FillRectangle(new SolidBrush(backgroundColor), 0, 0, width, height);
                g.DrawImage(img, new Rectangle(0, 0, width, height), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);
            }
            return bmp;
        }

        public static Image CreateThumbnail(Image img, int width, int height)
        {
            return CreateThumbnail(img, width, height, DefaultBgColour);
        }

        private static void SetHighQuality(Graphics g)
        {
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
        }

        public static byte[] ImageToJpegByteArray(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }


        public static string Truncate(string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }

        /// <summary>
        /// Returns an icon representation of an image contained in the specified file.
        /// This function is identical to System.Drawing.Icon.ExtractAssociatedIcon, xcept this version works.
        /// </summary>
        /// <param name="filePath">The path to the file that contains an image.</param>
        /// <returns>The System.Drawing.Icon representation of the image contained in the specified file.</returns>
        /// <exception cref="System.ArgumentException">filePath does not indicate a valid file.</exception>
        public static Icon ExtractAssociatedIcon(String filePath)
        {
            int index = 0;

            Uri uri;
            if (filePath == null)
            {
                throw new ArgumentException(String.Format("'{0}' is not valid for '{1}'", "null", "filePath"), "filePath");
            }
            try
            {
                uri = new Uri(filePath);
            }
            catch (UriFormatException)
            {
                filePath = Path.GetFullPath(filePath);
                uri = new Uri(filePath);
            }

            if (uri.IsFile)
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException(filePath);
                }

                StringBuilder iconPath = new StringBuilder(260);
                iconPath.Append(filePath);

                IntPtr handle = SafeNativeMethods.ExtractAssociatedIcon(new HandleRef(null, IntPtr.Zero), iconPath, ref index);
                if (handle != IntPtr.Zero)
                {
                    //IntSecurity.ObjectFromWin32Handle.Demand();
                    return Icon.FromHandle(handle);
                }
            }
            return null;
        }


        /// <summary>
        /// This class suppresses stack walks for unmanaged code permission. 
        /// (System.Security.SuppressUnmanagedCodeSecurityAttribute is applied to this class.) 
        /// This class is for methods that are safe for anyone to call. 
        /// Callers of these methods are not required to perform a full security review to make sure that the 
        /// usage is secure because the methods are harmless for any caller.
        /// </summary>
        internal static class SafeNativeMethods
        {
            [DllImport("shell32.dll", EntryPoint = "ExtractAssociatedIcon", CharSet = CharSet.Auto)]
            internal static extern IntPtr ExtractAssociatedIcon(HandleRef hInst, StringBuilder iconPath, ref int index);

            [DllImport("user32")]
            public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
            [DllImport("user32")]
            public static extern int RegisterWindowMessage(string message);

            public const int HWND_BROADCAST = 0xffff;
            public static readonly int WM_SHOWME = SafeNativeMethods.RegisterWindowMessage("WM_SHOWME");
        }


        // Disable transitions
        const int DWMWA_TRANSITIONS_FORCEDISABLED = 3;

        [DllImport("dwmapi", PreserveSig = true)]
        static extern int DwmSetWindowAttribute(IntPtr hWnd, int attr, ref int value, int attrLen);

        public static void DisableFormTransition(IntPtr handle)
        {
            // in the form's constructor:
            // (Note: in addition to checking the OS version for DWM support, you should also check
            // that DWM composition is enabled---or at least gracefully handle the function's
            // failure when it is not. Instead of S_OK, it will return DWM_E_COMPOSITIONDISABLED.)
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int value = 1;  // TRUE to disable
                DwmSetWindowAttribute(handle,
                                      DWMWA_TRANSITIONS_FORCEDISABLED,
                                      ref value,
                                      Marshal.SizeOf(value));
            }
        }


        public static readonly int WM_SHOWME = SafeNativeMethods.RegisterWindowMessage("WM_SHOWME");

        public static void DoShowMe()
        {
            SafeNativeMethods.PostMessage(
                (IntPtr)SafeNativeMethods.HWND_BROADCAST,
                SafeNativeMethods.WM_SHOWME,
                IntPtr.Zero,
                IntPtr.Zero);
        }

        public static DateTime MaxTime(DateTime t1, DateTime t2)
        {
           return t1 > t2 ? t1 : t2;
        }
        

        public static DateTime? LastChangeTime(FileInfo file)
        {
            if (!file.Exists)
            {
                return null;
            }
         
            
            return MaxTime(file.LastWriteTimeUtc, file.CreationTimeUtc);
        }

        public static ObservableHashSet<string> TagStringToHash(string tagString)
        {
            return tagString
                .Split(new char[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Where(t => t.Any())
                .ToObservableHashSet();
        }

        public static string HashToTagString(ObservableHashSet<string> tags)
        {
            var tagList = tags.ToList();
            tagList.Sort();
            return String.Join("\r\n", tagList);
        }

        public static ObservableHashSet<T> ToObservableHashSet<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableHashSet<T>(enumerable);
        }
    }

}
