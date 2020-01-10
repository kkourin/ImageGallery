using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageGallery.Database
{
    using Models;
    class FilesContext : DbContext
    {
        public DbSet<File> Files { get; set; }

        public DbSet<Watcher> Watchers { get; set; }

        public static DatabaseConfig Config { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DatabaseConfig config = Config ?? new DatabaseConfig();
            var path = config.DatabasePath;
            if (path.Length == 0)
            {
                throw new FileNotFoundException("Database parameter was empty.");
            }
            optionsBuilder.UseSqlite("Data Source=" + config.DatabasePath);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Watcher>()
                .Property(b => b.Whitelist)
                .HasDefaultValue(new HashSet<string>());

            modelBuilder.Entity<Watcher>()
                .Property(b => b.Enabled)
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
                .HasDefaultValue("");

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
                .HasIndex(b => b.LastUseTime);

            modelBuilder.Entity<File>()
                .HasIndex(b => b.TimesAccessed);

            modelBuilder.Entity<Watcher>()
                .Property(b => b.Whitelist)
                .HasConversion(
                    h => Watcher.HashToExtensionString(h),
                    s => Watcher.ExtensionStringToHash(s));
        }
        /*
        public IQueryable<FTSRow> Search(string Query)
        {
            return FTSRow.FromSql(
                "SELECT FileId, Text"
                + "FROM \"Text\" WHERE \"Text\" MATCH {0}"
                , Query
            );
        }
        */
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
                SaveChanges();
            }
            else
            {
                // Should not be reached.
                throw new Exception("Unique constraint violated.");
            }
        }

        public void RenameFilesInDirectory(string oldPath, string newPath, IEnumerable<File> filesInOldPath, Watcher watcher)
        {
            foreach (File file in filesInOldPath)
            {
                var newFullName = newPath + Path.DirectorySeparatorChar + DBHelpers.GetRelativePathFromFile(file.FullName, oldPath);
                FileInfo newInfo = new FileInfo(newFullName);
                file.FullName = newInfo.FullName;
                file.Directory = newInfo.DirectoryName;
                file.Directory_fts = DBHelpers.TokenizeDirectory(newInfo, watcher);
                Console.WriteLine(newFullName);
            }
            SaveChanges();
        }

        public void AddFile(string filename, Watcher watcher)
        {
            var fileInfo = new FileInfo(filename);
            var dirInfo = fileInfo.Directory;

            if (!DBHelpers.IsInDir(dirInfo, watcher.Directory))
            {
                Console.WriteLine($"File {filename} not in watcher {watcher.Name} | {watcher.Directory}");
                return;
            }
            var files = FilesWithName(filename, watcher);
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
                Extension = fileInfo.Extension,
                Directory_fts = DBHelpers.TokenizeDirectory(fileInfo, watcher),
                Name_fts = fileInfo.Name,
                Name_tokenized_fts = DBHelpers.TokenizeName(fileInfo),
                Extension_fts = fileInfo.Extension.Trim('.'),
                WatcherId = watcher.Id
            };
            // Fix these when they actually get exceptions.
            try
            {
                newFile.Thumbnail = DBHelpers.GetThumbnail(fileInfo);
            }
            catch
            {

            }
            return newFile;

        }

        public void UpdateFile(string filename, Watcher watcher)
        {
            var fileInfo = new FileInfo(filename);
            var files = FilesWithName(filename, watcher);
            // TODO: Try clause?
            if (!files.Any())
            {
                AddFile(filename, watcher);
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
            file.Thumbnail = DBHelpers.GetThumbnail(fileInfo); // TODO: get new thumbnail
            SaveChanges();
        }
        
        private IQueryable<File> GetAllFilesInWatcher(Watcher watcher)
        {
            return from file in Files
                   where file.WatcherId == watcher.Id
                   select file;
        }
        public void Sync(Watcher watcher)
        {
            var filesInDir = DBHelpers.GetAllFileInfo(watcher);
            // Remove files no longer in folder.
            var toRemove = from file in GetAllFilesInWatcher(watcher).ToList()
                           where !filesInDir.ContainsKey(file.FullName)
                           select file;
            Files.RemoveRange(toRemove);

            // Update updated files.
            var toUpdate = from file in GetAllFilesInWatcher(watcher).ToList()
                           where filesInDir.ContainsKey(file.FullName) && filesInDir[file.FullName].Equals(file.FileModifiedTime)
                           select file;
            foreach (var file in toUpdate)
            {
                file.FileModifiedTime = filesInDir[file.FullName];
            }

            // Add files that are new.
            foreach (var file in GetAllFilesInWatcher(watcher))
            {
                filesInDir.Remove(file.FullName);
            }
            Console.WriteLine($"Adding {filesInDir.Count()} files during sync.");
            var toAdd = from filename in filesInDir.Keys
                        select MakeFileModel(new FileInfo(filename), watcher);
            Files.AddRange(toAdd);
            SaveChanges();
        }

        public Watcher AddWatcherForm(string name, string dir, string whitelist)
        {
            var watcher = new Watcher{
                Name = name,
                Directory = dir,
                Whitelist = Watcher.ExtensionStringToHash(whitelist)
            };
            Watchers.Add(watcher);
            SaveChanges();
            return watcher;
        }
    }

}
