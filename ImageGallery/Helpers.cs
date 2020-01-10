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


namespace ImageGallery
{
    public static class Helpers
    {
        public static readonly string[] ImageFileExtensions = new string[] { "jpg", "jpeg", "png", "gif", "bmp", "ico", "tif", "tiff" };
        public static readonly string[] TextFileExtensions = new string[] { "txt", "log", "nfo", "c", "cpp", "cc", "cxx", "h", "hpp", "hxx", "cs", "vb", "html", "htm", "xhtml", "xht", "xml", "css", "js", "php", "bat", "java", "lua", "py", "pl", "cfg", "ini", "dart", "go", "gohtml" };
        public static readonly string[] VideoFileExtensions = new string[] { "mp4", "webm", "mkv", "avi", "vob", "ogv", "ogg", "mov", "qt", "wmv", "m4p", "m4v", "mpg", "mp2", "mpeg", "mpe", "mpv", "m2v", "m4v", "flv", "f4v" };
        private static readonly Color thumbnailBgColor;

        static Helpers() {
            thumbnailBgColor = Color.FromArgb(255, 238, 238, 238);
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

        public static Image LoadImage(FileInfo file)
        {
            if (!file.Exists || !IsImageFile(file.FullName))
            {
                return null;
            }
            
            // http://stackoverflow.com/questions/788335/why-does-image-fromfile-keep-a-file-handle-open-sometimes
            Image img = Image.FromStream(new MemoryStream(File.ReadAllBytes(file.FullName)));

            return img;
        }

        public static Image CreateThumbnail(Image img, int width, int height)
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
                g.FillRectangle(new SolidBrush(thumbnailBgColor), 0, 0, width, height);
                g.DrawImage(img, new Rectangle(0, 0, width, height), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);
            }
            return bmp;
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


        [DllImport("rpcrt4.dll", SetLastError = true)]
        static extern int UuidCreateSequential(out Guid guid);

        public static Guid NewSequentialId()
        {
            Guid guid;
            UuidCreateSequential(out guid);
            var s = guid.ToByteArray();
            var t = new byte[16];
            t[3] = s[0];
            t[2] = s[1];
            t[1] = s[2];
            t[0] = s[3];
            t[5] = s[4];
            t[4] = s[5];
            t[7] = s[6];
            t[6] = s[7];
            t[8] = s[8];
            t[9] = s[9];
            t[10] = s[10];
            t[11] = s[11];
            t[12] = s[12];
            t[13] = s[13];
            t[14] = s[14];
            t[15] = s[15];
            return new Guid(t);
        }

        public static string Truncate(string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }

    }
}
