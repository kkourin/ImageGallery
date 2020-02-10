using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageGallery.Database
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Models;
    using System.Drawing;
    using System.Timers;

    public static class DBHelpers
    {
        private static readonly string[] fts_split_tokens = new string[] { "\\", "/", ".", " ", "-", "+", "-", "'", "%", "(", ")", "[", "]", "{", "}", "\"", "=", "\t", "_", "　" };
        private static string Tokenize(string name)
        {
            String[] tokens = name.Split(fts_split_tokens, StringSplitOptions.RemoveEmptyEntries);
            return String.Join("|", tokens);
        }

        public static string GetRelativePathFromFile(string insideFile, string outsideFolder)
        {
            if (!outsideFolder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                outsideFolder += Path.DirectorySeparatorChar;
            }

            Uri insideUri = new Uri(insideFile);
            Uri outsideUri = new Uri(outsideFolder);
            return Uri.UnescapeDataString(outsideUri.MakeRelativeUri(insideUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }
        public static string GetRelativePath(string insideFolder, string outsideFolder)
        {
            // Folders must end in a slash
            if (!insideFolder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                insideFolder += Path.DirectorySeparatorChar;
            }
            return GetRelativePathFromFile(insideFolder, outsideFolder);
        }
        public static string TokenizeDirectory(FileInfo fileInfo, Watcher watcher)
        {
            string fileName = fileInfo.Directory.FullName;
            string watcherName = watcher.Directory;
            return Tokenize(GetRelativePath(fileName, watcherName));
        }

        public static string TokenizeName(FileInfo fileInfo)
        {
            return Tokenize(Path.GetFileNameWithoutExtension(fileInfo.Name));
        }

        public static bool IsInDir(DirectoryInfo dir, string rootDir)
        {
            while (dir != null)
            {
                if (dir.FullName == rootDir)
                {
                    return true;
                }
                dir = dir.Parent;
            }
            return false;
        }

        private static byte[] GetImageThumbnail(FileInfo file, long fileSizeLimit = 100000000)
        {
            Image img;
            try
            {
                img = Helpers.LoadImage(file, fileSizeLimit);

            }
            catch (ArgumentException)
            {
                return null;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (Exception)
            {
                // TODO: remopve this catch all. I use it right now to not fail on open files.
                return null;
            }

            try
            {
                Image thumbnailImage = Helpers.CreateThumbnail(img, 150, 150);
                byte[] thumbnail = Helpers.ImageToJpegByteArray(thumbnailImage);
                return thumbnail;

            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        private static byte[] GetVideoThumbnail(FileInfo file, VideoThumbnailExtractor videoThumbnailExtractor)
        {
            Image img = videoThumbnailExtractor.GenerateFromPath(file.FullName);
            if (img == null)
            {
                return null;
            }
            try
            {
                Image thumbnailRawBytes = Helpers.CreateThumbnail(img, 320, 320, Color.Black);
                byte[] thumbnail = Helpers.ImageToJpegByteArray(thumbnailRawBytes);
                return thumbnail;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public static byte[] GetThumbnail(FileInfo file)
        {
            if (!file.Exists)
            {
                return null;
            }
            if (Helpers.IsImageFile(file.Name))
            {
                return GetImageThumbnail(file);
            }
            return null;

        }
        public static byte[] GetThumbnail(FileInfo file, VideoThumbnailExtractor videoThumbnailExtractor)
        {
            if (!file.Exists)
            {
                return null;
            }
            if (Helpers.IsImageFile(file.Name))
            {
                return GetImageThumbnail(file);
            }
            else if (Helpers.IsVideoFile(file.Name) && videoThumbnailExtractor != null)
            {
                return GetVideoThumbnail(file, videoThumbnailExtractor);
            }
            return null;

        }



        // If extensions is empty, returns ALL. Note: only counts white listed files.
        public static Dictionary<string, (DateTime, DateTime)> GetAllFileInfo(Watcher watcher)
        {
            var dir = watcher.Directory;
            return GetAllFileInfoInDirectory(watcher, dir, watcher.ScanSubdirectories.GetValueOrDefault());
        }

        // If extensions is empty, returns ALL. Note: only counts white listed files.
        public static Dictionary<string, (DateTime, DateTime)> GetAllFileInfoInDirectory(Watcher watcher, string dir, bool recursive)
        {
            if (!Directory.Exists(dir))
            {
                return null;
            }
            var files = from filename in Directory.EnumerateFiles(dir, "*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                        where watcher.WhitelistedFile(filename)
                        select new KeyValuePair<string, (DateTime, DateTime)>(
                            filename,
                            (System.IO.File.GetLastWriteTimeUtc(filename),
                            System.IO.File.GetCreationTimeUtc(filename))
                        );
            return files.ToDictionary(kv => kv.Key, kv => kv.Value);
        }



        // Extension
        public static void Reset(this Timer timer)
        {
            timer.Stop();
            timer.Start();
        }



    }
}
