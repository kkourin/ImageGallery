using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageGallery.Database
{
    using ImageGallery.Database.Models;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System.Windows.Forms;


    public class FilesContext : DbContext
    {
        public DbSet<File> Files { get; set; }

        public DbSet<Watcher> Watchers { get; set; }

        public enum SortColumn
        {
            DateChanged,
            Name,
            DateAccessed,
            TimesAccessed
        }

        public static VideoThumbnailExtractor videoThumbnailExtractor { get; set; }
        private static Comparer<File> NameComparer { get; }
        private static Comparer<File> LastUseTimeComparer { get; }
        private static Comparer<File> TimesAccessedComparer { get; }
        private static Comparer<File> LastChangeTimeComparer { get; }

        private static int CompareDateTime(DateTime? d1, DateTime? d2)
        {
            if (!d1.HasValue)
            {
                return -1;
            }
            if (!d2.HasValue)
            {
                return 1;
            }
            return DateTime.Compare(d1.Value, d2.Value);
        }
        static FilesContext()
        {
            NameComparer = Comparer<File>.Create((f1, f2) => f1.Name.CompareTo(f2.Name));
            LastUseTimeComparer = Comparer<File>.Create((f1, f2) => -CompareDateTime(f1.LastUseTime, f2.LastUseTime));
            TimesAccessedComparer = Comparer<File>.Create((f1, f2) => -f1.TimesAccessed.CompareTo(f2.TimesAccessed));
            LastChangeTimeComparer = Comparer<File>.Create((f1, f2) => -CompareDateTime(f1.LastChangeTime, f2.LastChangeTime));
        }

        public static Comparer<File> ComparerFromSort(SortColumn sortColumn)
        {
            switch (sortColumn)
            {
                case SortColumn.Name:
                    return NameComparer;
                case SortColumn.DateAccessed:
                    return LastUseTimeComparer;
                case SortColumn.TimesAccessed:
                    return TimesAccessedComparer;
                case SortColumn.DateChanged:
                    return LastChangeTimeComparer;
            }
            return NameComparer; 
        }

        public static string GenerateDatabasePath()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"kkkourin\ImageGallery");
            Directory.CreateDirectory(folder);
#if DEBUG
            return Path.Combine(folder, "gallery_data_DEBUG.db");
#else
            return Path.Combine(folder, "gallery_data.db");
#endif
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=" + GenerateDatabasePath());
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Watcher>()
                .Property(b => b.Whitelist)
                .HasDefaultValue(new HashSet<string>());

            modelBuilder.Entity<Watcher>()
                .Property(b => b.Enabled)
                .HasDefaultValue(true);

            modelBuilder.Entity<Watcher>()
                .Property(b => b.GenerateVideoThumbnails)
                .HasDefaultValue(true);

            modelBuilder.Entity<Watcher>()
                .Property(b => b.ScanSubdirectories)
                .HasDefaultValue(true);

            modelBuilder.Entity<File>()
                .Property(b => b.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<File>()
                .Property(b => b.TimesAccessed)
                .HasDefaultValue(0);

            modelBuilder.Entity<File>()
                .Property(b => b.Directory_fts)
                .HasDefaultValue("");

            modelBuilder.Entity<File>()
                .Property(b => b.Name_fts)
                .HasDefaultValue("");

            modelBuilder.Entity<File>()
                .Property(b => b.Name_tokenized_fts)
                .HasDefaultValue("");

            modelBuilder.Entity<File>()
                .Property(b => b.Extension_fts)
                .HasDefaultValue("");

            modelBuilder.Entity<File>()
                .Property(b => b.Custom_fts)
                .HasDefaultValue(new ObservableHashSet<string>())
                .HasConversion(
                    h => File.HashToTagString(h),
                    s => File.TagStringToHash(s));

            modelBuilder.Entity<File>()
                .Property(b => b.Comment)
                .HasDefaultValue("");

            modelBuilder.Entity<File>()
                .HasIndex(b => b.FullName);

            modelBuilder.Entity<File>()
                .HasIndex(b => new { b.FullName, b.WatcherId })
                .IsUnique();

            modelBuilder.Entity<File>()
                .HasIndex(b => b.Directory);

            modelBuilder.Entity<File>()
                .HasIndex(b => b.CreatedTime);

            modelBuilder.Entity<File>()
                .HasIndex(b => b.FileCreatedTime);

            modelBuilder.Entity<File>()
               .HasIndex(b => b.FileModifiedTime);
            modelBuilder.Entity<File>()
               .HasIndex(b => b.LastChangeTime);

            modelBuilder.Entity<File>()
                .HasIndex(b => b.LastUseTime);

            modelBuilder.Entity<File>()
                .HasIndex(b => b.TimesAccessed);

            modelBuilder.Entity<Watcher>()
                .Property(b => b.Whitelist)
                .HasConversion(
                    h => Watcher.HashToExtensionString(h),
                    s => Watcher.ExtensionStringToHash(s));

            modelBuilder.Entity<Watcher>()
                .Property(b => b.ShortcutKeys)
                .HasConversion(
                    v => (int)v,
                    v => (Keys)v);
        }



        public IQueryable<File> Search(string Query, HashSet<int> watchers, SortColumn order)
        {
            var result = from file in
                      Files.FromSqlRaw(
                       "SELECT *" +
                       "FROM \"Files\" WHERE \"Id\" IN (" +
                           "SELECT RowID from FileFTS WHERE FileFTS MATCH {0}" +
                       ")"
                       , Query).AsNoTracking()
                         where watchers.Contains(file.WatcherId)
                         select file;
            return OrderBySort(result, order);
        }



        public static IQueryable<File> OrderBySort(IQueryable<File> files, SortColumn order)
        {
            switch (order)
            {
                case SortColumn.Name:
                    return files.OrderBy(t => t.Name);
                case SortColumn.DateAccessed:
                    return files.OrderByDescending(t => t.LastUseTime);
                case SortColumn.TimesAccessed:
                    return files.OrderByDescending(t => t.TimesAccessed);
                case SortColumn.DateChanged:
                    return files.OrderByDescending(t => t.LastChangeTime);
                default:
                    return files;
            }
        }

        public static string MakeQuery(string textInput)
        {
            var terms = from term in textInput.Split()
                        select String.Format("\"{0}\" *", term);
            return String.Join(" AND ", terms);
        }

        public void RecordUse(IEnumerable<File> files)
        {
            foreach(var file in files)
            {
                var found = Files.Find(file.Id);
                if (found != null)
                {
                    found.RecordUse();
                }
            }
            SaveChanges();
        }
        public void RecordUse(File file)
        {
            RecordUse(new List<File> { file });
        }

        public IEnumerable<File> FilesInDirectory(string directory, Watcher watcher)
        {
            var files = Files.FromSqlRaw(
                "SELECT * FROM Files WHERE WatcherId == {0} AND (Directory == {1} OR Directory GLOB {2})",
                watcher.Id,
                directory,
                directory + Path.DirectorySeparatorChar + '*'
            ).ToList();
            return from file in files
                   where DBHelpers.IsInDir(new DirectoryInfo(file.Directory), directory)
                   select file;
        }

        public IQueryable<File> FilesWithName(string filename, Watcher watcher)
        {
            return from file in Files
                   where file.FullName == filename && file.WatcherId == watcher.Id
                   select file;
        }

        public void DeleteFile(string name, Watcher watcher)
        {
            var files = FilesInDirectory(name, watcher);
            if (files.Any())
            {
                Console.WriteLine($"{name} was a non-empty directory. Deleting {files.Count()} files.");
                Files.RemoveRange(files);
                SaveChanges();
                return;
            }
            Files.RemoveRange(FilesWithName(name, watcher));
            SaveChanges();
        }


        public void RenameFile(string oldName, string newName, Watcher watcher)
        {
            var fileInfo = new FileInfo(newName);
            var files = (from file in Files
                         where file.FullName == oldName
                         select file).ToList();
            if (files.Count == 0)
            {
                return;
            }
            else if (files.Count == 1)
            {
                var file = files.First();
                file.FullName = fileInfo.FullName;
                file.Extension = fileInfo.Extension;
                file.Extension_fts = fileInfo.Extension.Trim('.');
                file.Name = fileInfo.Name;
                file.Name_fts = fileInfo.Name;
                file.Name_tokenized_fts = DBHelpers.TokenizeName(fileInfo);
                file.Thumbnail = MaybeMakeThumbnail(fileInfo, watcher);
                SaveChanges();
            }
            else
            {
                // Should not be reached.
                throw new Exception("Unique constraint violated.");
            }
        }

        public void RenameFilesInDirectory(string oldPath, string newPath, Watcher watcher)
        {
            var filesInOldPath = FilesInDirectory(oldPath, watcher);
            foreach (File file in filesInOldPath)
            {
                var newFullName = newPath + Path.DirectorySeparatorChar + DBHelpers.GetRelativePathFromFile(file.FullName, oldPath);
                FileInfo newInfo = new FileInfo(newFullName);
                file.FullName = newInfo.FullName;
                file.Directory = newInfo.DirectoryName;
                file.Directory_fts = DBHelpers.TokenizeDirectory(newInfo, watcher);
            }
            SaveChanges();
        }

        public void AddFile(string filename, Watcher watcher)
        {
            var fileInfo = new FileInfo(filename);
            var files = FilesWithName(filename, watcher).ToList();
            if (files.Any())
            {
                UpdateFileContents(files.First(), fileInfo, watcher);
                return;
            }
            Console.WriteLine($"Adding file {fileInfo.FullName} to watcher {watcher.Id}.");
            Files.Add(MakeFileModel(fileInfo, watcher));

            SaveChanges();
        }

        private static File MakeFileModel(FileInfo fileInfo, Watcher watcher)
        {
            File newFile = new File
            {
                FullName = fileInfo.FullName,
                Directory = fileInfo.DirectoryName,
                Name = fileInfo.Name,
                FileCreatedTime = fileInfo.CreationTimeUtc,
                FileModifiedTime = fileInfo.LastWriteTimeUtc,
                LastChangeTime = Helpers.LastChangeTime(fileInfo),
                Extension = fileInfo.Extension,
                Directory_fts = DBHelpers.TokenizeDirectory(fileInfo, watcher),
                Name_fts = fileInfo.Name,
                Name_tokenized_fts = DBHelpers.TokenizeName(fileInfo),
                Extension_fts = fileInfo.Extension.Trim('.'),
                WatcherId = watcher.Id
            };
            // Fix these when they actually get exceptions.
            newFile.Thumbnail = MaybeMakeThumbnail(fileInfo, watcher);
            return newFile;

        }

        public void UpdateFile(string filename, Watcher watcher)
        {
            var fileInfo = new FileInfo(filename);
            var files = FilesWithName(filename, watcher).ToList();
            // TODO: Try clause?
            if (!files.Any())
            {
                return;
            }
            else if (files.Count() <= 1)
            {
                UpdateFileContents(files.First(), fileInfo, watcher);
            }
            else
            {
                throw new Exception("Unique constraint violated.");
            }
        }



        private void UpdateFileContents(File file, FileInfo fileInfo, Watcher watcher)
        {
            file.CreatedTime = fileInfo.CreationTimeUtc;
            file.FileModifiedTime = fileInfo.LastWriteTimeUtc;
            file.LastChangeTime = Helpers.LastChangeTime(fileInfo);
            file.Thumbnail = MaybeMakeThumbnail(fileInfo, watcher); // TODO: get new thumbnail
            SaveChanges();
        }
        
        private IQueryable<File> GetAllFilesInWatcher(Watcher watcher)
        {
            return from file in Files
                   where file.WatcherId == watcher.Id
                   select file;
        }

        private static byte[] MaybeMakeThumbnail(FileInfo file, Watcher watcher)
        {
            if (Helpers.IsImageFile(file.FullName))
            {
                return DBHelpers.GetThumbnail(file);
            }
            else if (Helpers.IsVideoFile(file.FullName) && videoThumbnailExtractor != null && watcher.GenerateVideoThumbnails.GetValueOrDefault())
            {
                Console.WriteLine($"Generating video thumbnail for file {file.Name}.");
                return DBHelpers.GetThumbnail(file, videoThumbnailExtractor);
            }
            return null;
        }
        public bool Sync(Watcher watcher)
        {
            bool changed = false;
            var filesInDir = DBHelpers.GetAllFileInfo(watcher);
            if (filesInDir == null)
            {
                return false;
            }
            // Remove files no longer in folder.
            var toRemove = from file in GetAllFilesInWatcher(watcher).ToList()
                           where !filesInDir.ContainsKey(file.FullName)
                           select file;
            changed |= toRemove.Any();
            Files.RemoveRange(toRemove);

            // Update updated files.
            // Tuples returned are of the form (Modified Time, Created Time)
            var toUpdate = from file in GetAllFilesInWatcher(watcher).ToList()
                           where filesInDir.ContainsKey(file.FullName) &&
                           (!filesInDir[file.FullName].Item1.Equals(file.FileModifiedTime) || !filesInDir[file.FullName].Item2.Equals(file.FileCreatedTime))
                           select file;

            changed |= toUpdate.Any();
            foreach (var file in toUpdate)
            {
                file.FileModifiedTime = filesInDir[file.FullName].Item1;
                file.FileCreatedTime = filesInDir[file.FullName].Item2;
                file.Thumbnail = MaybeMakeThumbnail(new FileInfo(file.FullName), watcher);
            }

            // Add files that are new.
            foreach (var file in GetAllFilesInWatcher(watcher))
            {
                filesInDir.Remove(file.FullName);
            }
            Console.WriteLine($"Adding {filesInDir.Count()} files during sync.");
            var toAdd = from filename in filesInDir.Keys
                        select MakeFileModel(new FileInfo(filename), watcher);
            changed |= toAdd.Any();
            Files.AddRange(toAdd);
            SaveChanges();
            return changed;
        }

        public void SyncCreatedDirectory(Watcher watcher, string dir)
        {
            var filesInDir = DBHelpers.GetAllFileInfoInDirectory(watcher, dir, watcher.ScanSubdirectories.GetValueOrDefault());
            if (filesInDir == null)
            {
                return;
            }

            foreach (var filename in filesInDir.Keys)
            {
                var files = FilesWithName(filename, watcher).ToList();
                var fileInfo = new FileInfo(filename);
                if (files.Any())
                {
                    var file = files.First();
                    if (!(fileInfo.Exists && fileInfo.LastWriteTimeUtc == file.FileModifiedTime && fileInfo.CreationTimeUtc == file.CreatedTime))
                    {
                        UpdateFileContents(file, fileInfo, watcher);
                    }
                    continue;
                }
                Console.WriteLine("Writing file for created sync.");
                Files.Add(MakeFileModel(new FileInfo(filename), watcher));
            }
            SaveChanges();
        }

        public Watcher AddWatcherForm(string name, string dir, string whitelist, bool generateVideoThumbnails, bool scanSubdirectories)
        {
            var watcher = new Watcher
            {
                Name = name,
                Directory = dir,
                Whitelist = Watcher.ExtensionStringToHash(whitelist),
                GenerateVideoThumbnails = generateVideoThumbnails,
                ScanSubdirectories = scanSubdirectories
            };

            Watchers.Add(watcher);
            SaveChanges();
            return watcher;
        }

        public void UpdateWatcherForm(int id, string name, string whitelist, bool generateVideoThumbnails, bool scanSubdirectories)
        {
            var foundWatcher = Watchers.Find(id);
            if (foundWatcher == null)
            {
                return;
            }
            foundWatcher.Name = name;
            foundWatcher.Whitelist = Watcher.ExtensionStringToHash(whitelist);
            foundWatcher.GenerateVideoThumbnails = generateVideoThumbnails;
            foundWatcher.ScanSubdirectories = scanSubdirectories;
            SaveChanges();
        }

        public bool UpdateFileTags(File file)
        {
            var foundFile = Files.Find(file.Id);
            if (foundFile == null)
            {
                return false;
            }
            foundFile.Custom_fts = file.Custom_fts;
            SaveChanges();
            return true;
        }
        public int UpdateFilesTags(List<File> editedFiles)
        {
            Dictionary<int, File> editedFileDict = editedFiles.ToDictionary(file => file.Id, file => file);
            HashSet<int> ids = editedFileDict.Keys.ToHashSet();
            var files = Files.Where(file => ids.Contains(file.Id));
            int count = 0;
            foreach (var file in files)
            {
                file.Custom_fts = editedFileDict[file.Id].Custom_fts;
                ++count;
            }
            SaveChanges();
            return count;
        }

        public Dictionary<int, (Keys ShortcutKeys, bool GlobalShortcut)> GetWatcherShortcutMap()
        {
            var watcherKeyMap = new Dictionary<int, (Keys ShortcutKeys, bool GlobalShortcut)>();
            foreach (var watcher in Watchers)
            {
                watcherKeyMap[watcher.Id] = (watcher.ShortcutKeys, watcher.GlobalShortcut);
            }
            return watcherKeyMap;
        }
        public void UpdateShortcuts(Watcher watcher, Keys keys, bool global)
        {
            var foundWatcher = Watchers.Find(watcher.Id);
            if (foundWatcher == null)
            {
                return;
            }
            foundWatcher.ShortcutKeys = keys;
            foundWatcher.GlobalShortcut = global;
            SaveChanges();
        }
    }

}
