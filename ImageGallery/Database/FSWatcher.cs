using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
namespace ImageGallery.Database
{
    using Models;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Runtime.Caching;
    using System.Threading;
    class WaitResult
    {
        public Exception exception { get; set; }
    }
    class FileEventQueue
    {
        private enum EventType
        {
            Created,
            Deleted,
            Changed,
            Renamed,
            Error
        }

        private class FileEvent
        {
            public EventType Type { get; set; }
            public String FullPath { get; set; }
            public String OldFullPath { get; set; }
            public ErrorEventArgs Error { get; set; }
            public FileEvent(FileSystemEventArgs e)
            {
                switch (e.ChangeType)
                {
                    case WatcherChangeTypes.Changed:
                        Type = EventType.Changed;
                        break;
                    case WatcherChangeTypes.Created:
                        Type = EventType.Created;
                        break;
                    case WatcherChangeTypes.Deleted:
                        Type = EventType.Deleted;
                        break;
                    default:
                        break;
                }
                FullPath = e.FullPath;
            }

            public FileEvent(RenamedEventArgs e)
            {
                Type = EventType.Renamed;
                OldFullPath = e.OldFullPath;
                FullPath = e.FullPath;
            }

            public FileEvent(ErrorEventArgs e)
            {
                Type = EventType.Error;
                Error = e;
            }
            public override string ToString()
            {
                return $"FileEvent({Type}, {FullPath}, {OldFullPath})";
            }
        }

        private bool _closed = false;
        public bool Closed
        {
            get
            {
                return _closed;
            }
        }

        readonly CancellationTokenSource cts = new CancellationTokenSource();
        readonly BlockingCollection<FileEvent> queue = new BlockingCollection<FileEvent>();
        readonly Task loopTask;
        readonly Watcher watcher;
        readonly private Mutex SyncMutex;
        readonly System.Timers.Timer SyncTimer;
        private readonly WaitResult waitResult;
        readonly FSWatcher fsWatcher;

        private readonly MemoryCache _changedCache;
        private const int CacheTimeMilliseconds = 2500;


        public FileEventQueue(
            Watcher watcher,
            Mutex mutex,
            System.Timers.Timer timer,
            WaitResult waitResult,
            FSWatcher fsWatcher)
        {
            _changedCache = MemoryCache.Default;
            this.fsWatcher = fsWatcher;
            this.waitResult = waitResult;
            this.SyncTimer = timer;
            SyncMutex = mutex;
            this.watcher = watcher;
            loopTask = Task.Run(() => Loop(cts.Token));
        }
        
        private CacheItemPolicy ChangeCachePolicy()
        {
            return new CacheItemPolicy
            {
                RemovedCallback = TriggerChange,
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMilliseconds(CacheTimeMilliseconds)
            };
        }
        private void TriggerChange(CacheEntryRemovedArguments args)
        {
            var fileEvent = (FileEvent)args.CacheItem.Value;
            using (var ctx = new FilesContext())
            {
                Console.WriteLine("Triggered cached file.");
                ctx.UpdateFile(fileEvent.FullPath, watcher);
            }
        }

        public void Add(ErrorEventArgs e)
        {
            queue.Add(new FileEvent(e));
        }
        public void Add(FileSystemEventArgs e)
        {
            queue.Add(new FileEvent(e));
        }

        public void Add(RenamedEventArgs e)
        {
            queue.Add(new FileEvent(e));
        }

        private void Loop(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    var fileEvent = queue.Take(token);
                    SyncMutex.WaitOne();
                    try
                    {
                        HandleEvent(fileEvent);
                        SyncTimer.Reset();
                    } finally
                    {
                        SyncMutex.ReleaseMutex();
                    }
                }
            }
            catch (Exception e)
            {
                if (!(e is System.OperationCanceledException))
                {
                    Console.WriteLine(e);
                    fsWatcher.ForceStop(e);
                }
                // Do nothing if cancelled.
            }
            finally
            {
                _closed = true;
            }
        }

        private void HandleEvent(FileEvent fileEvent)
        {
            Console.WriteLine($"Handling {fileEvent}.");
            using (var ctx = new FilesContext())
            {
                if (fileEvent.Type == EventType.Deleted)
                {
                    Console.WriteLine($"Performing delete on {fileEvent.FullPath}");
                    _changedCache.Remove(fileEvent.FullPath, CacheEntryRemovedReason.Removed);
                    ctx.DeleteFile(fileEvent.FullPath, watcher);
                    fsWatcher.SendChangeOccurred();
                }
                else if (fileEvent.Type == EventType.Renamed)
                {
                    HandleRenamed(fileEvent, ctx);
                    fsWatcher.SendChangeOccurred();
                }
                else if (fileEvent.Type == EventType.Created)
                {
                    HandleCreated(fileEvent, ctx);
                    fsWatcher.SendChangeOccurred();
                }
                else if (fileEvent.Type == EventType.Changed)
                {
                    HandleChanged(fileEvent, ctx);
                }
                else if (fileEvent.Type == EventType.Error)
                {
                    throw fileEvent.Error.GetException();
                }
            }
        }

        private void HandleRenamed(FileEvent fileEvent, FilesContext ctx)
        {
            Console.WriteLine($"File: {fileEvent.OldFullPath} renamed to {fileEvent.FullPath}");
            if (!Directory.Exists(fileEvent.FullPath) && watcher.WhitelistedFile(fileEvent.FullPath))
            {
                _changedCache.Remove(fileEvent.FullPath, CacheEntryRemovedReason.Removed);
                ctx.RenameFile(fileEvent.OldFullPath, fileEvent.FullPath, watcher);
            }
            else if (Directory.Exists(fileEvent.FullPath))
            {
                ctx.RenameFilesInDirectory(fileEvent.OldFullPath, fileEvent.FullPath, watcher);
            }
        }
        private void HandleCreated(FileEvent fileEvent, FilesContext ctx)
        {
            Console.WriteLine($"File: {fileEvent.FullPath} created event receieved.");
            if (!Directory.Exists(fileEvent.FullPath) && watcher.WhitelistedFile(fileEvent.FullPath))
            {
                ctx.AddFile(fileEvent.FullPath, watcher);
            }
            else if (Directory.Exists(fileEvent.FullPath))
            {
                ctx.SyncCreatedDirectory(watcher, fileEvent.FullPath);
            }
        }

        private void HandleChanged(FileEvent fileEvent, FilesContext ctx)
        {
            Console.WriteLine($"File: {fileEvent.FullPath} changed.");
            if (!Directory.Exists(fileEvent.FullPath) && watcher.WhitelistedFile(fileEvent.FullPath))
            {
                _changedCache.AddOrGetExisting(fileEvent.FullPath, fileEvent, ChangeCachePolicy());
                ctx.UpdateFile(fileEvent.FullPath, watcher);
            } 
        }


        public void Close()
        {
            CloseAsync();
            Wait();
        }

        public void CloseAsync()
        {
            cts.Cancel();
        }

        public void Wait()
        {
            loopTask.Wait();
        }
    }
    class FSWatcher
    {
        private const int InitIntervalMillis = 2000;
        private const int SyncIntervalMillis = 120000;
        FileSystemWatcher FsWatcher;
        Watcher Watcher { get; }
        FileEventQueue queue;
        private Mutex syncMutex = new Mutex();
        private System.Timers.Timer syncTimer;
        readonly EventWaitHandle stoppedEwh = new EventWaitHandle(false, EventResetMode.ManualReset);
        WaitResult waitResult = new WaitResult();
        bool firstSync = true;

        public static event EventHandler<SyncOccurredEventArgs> SyncOccurred;

        public FSWatcher(Watcher dirWatcher)
        {
            Watcher = dirWatcher;
            FsWatcher = new FileSystemWatcher();
            FsWatcher.NotifyFilter = NotifyFilters.LastWrite
                                   | NotifyFilters.FileName
                                   | NotifyFilters.DirectoryName;
            FsWatcher.Changed += OnChanged;
            FsWatcher.Created += OnChanged;
            FsWatcher.Deleted += OnChanged;
            FsWatcher.Renamed += OnRenamed;
            FsWatcher.Error += OnError;
            FsWatcher.IncludeSubdirectories = true;

            syncTimer = new System.Timers.Timer(InitIntervalMillis);
            syncTimer.Elapsed += HandleSyncTimer;
            syncTimer.AutoReset = false;
            queue = new FileEventQueue(Watcher, syncMutex, syncTimer, waitResult, this);
        }

        private static string GetFilter(HashSet<string> whitelist)
        {
            return String.Join("|", (from ext in whitelist select '*' + ext));
        }
        public void Start()
        {
            FsWatcher.Path = Watcher.Directory;
            FsWatcher.EnableRaisingEvents = true;
            syncTimer.Enabled = true;
        }

        public void Stop()
        {
            Console.WriteLine($"Stopped {Watcher.Directory}");
            if (queue != null)
            {
                queue.Close();
            }
            FsWatcher.EnableRaisingEvents = false;
            syncTimer.Enabled = false;
            stoppedEwh.Set();
        }
        public void ForceStop(Exception e)
        {
            if (queue != null)
            {
                queue.CloseAsync();
            }
            FsWatcher.EnableRaisingEvents = false;
            syncTimer.Enabled = false;
            waitResult.exception = e;
            stoppedEwh.Set();
        }
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            queue.Add(e);
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            queue.Add(e);
        }

        private void OnError(object source, ErrorEventArgs e)
        {
            queue.Add(e);
        }

        private void HandleSyncTimer(object source, ElapsedEventArgs e)
        {
            //Console.WriteLine("The Sync event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
            Sync();
            if (firstSync)
            {
                firstSync = false;
                syncTimer.Interval = SyncIntervalMillis;
            }
        }
        private void Sync()
        {
            // Add timeout?
            syncMutex.WaitOne();
            try
            {
                using (var ctx = new FilesContext())
                {
                    var changed = ctx.Sync(Watcher);
                    OnSyncOccurredEvent(new SyncOccurredEventArgs { Changed = changed, WatcherId = Watcher.Id });
                    syncTimer.Reset();
                }
            } catch (Exception e)
            {
                ForceStop(e);
            }
            finally
            {
                syncMutex.ReleaseMutex();
            }
        }

        public class SyncOccurredEventArgs : EventArgs
        {
            public bool Changed { get; set; }
            public int WatcherId { get; set; }
        }

        public void SendChangeOccurred()
        {
            OnSyncOccurredEvent(new SyncOccurredEventArgs { Changed = true, WatcherId = Watcher.Id });
        }
        protected virtual void OnSyncOccurredEvent(SyncOccurredEventArgs e)
        {
            SyncOccurred?.Invoke(this, e);
        }

        public Task StartAndWait()
        {
            return Task.Run(() =>
            {
                Start();
                stoppedEwh.WaitOne();
                if (waitResult.exception != null)
                {
                    throw new WatcherFailure("Watcher stopped abnormally.",  waitResult.exception);
                }
                return;
            });
        }


        public class WatcherFailure : Exception
        {
            public WatcherFailure(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }
}
