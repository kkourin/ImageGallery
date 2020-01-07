﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageGallery.Database
{
    using Models;
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
        public static byte[] GetThumbnail(FileInfo file)
        {
            // If image...
            // REmember, very high chance of exception, file may be partially downloaded...
            return null;
        }

        public static HashSet<string> GetAllFilenames(string dir)
        {
            if (!Directory.Exists(dir))
            {
                return null;
            }
            HashSet<string> acc = new HashSet<string>();
            GetAllFilenamesAcc(dir, acc);
            return acc;
        }

        private static void GetAllFilenamesAcc(string dir, HashSet<string> acc)
        {
            foreach (string subDir in Directory.GetDirectories(dir))
            {
                GetAllFilenamesAcc(subDir, acc);
            }
            foreach (string filename in Directory.GetFiles(dir))
            {
                acc.Add(filename);
            }

        }

        // Extension
        public static void Reset(this Timer timer)
        {
            timer.Stop();
            timer.Start();
        }
    }
}
