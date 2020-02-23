using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ImageGallery.Database
{
    using Models;
    public class WatcherMonitor
    {
        class FSWatcherTaskPack
        {
            public Watcher Watcher { get; set; }
            public FSWatcher FSWatcher { get; set; }
            public Task Task { get; set; }
            public int Failures { get; set; }
            public string LastError { get; set; } = "";

        }
        Dictionary<int, FSWatcherTaskPack> watchers;
        private int WaitInterval = 10000;
        public WatcherMonitor()
        {
            watchers = new Dictionary<int, FSWatcherTaskPack>();
        }

        public static WatcherMonitor InitMonitorFromDB()
        {
            var monitor = new WatcherMonitor();
            using (var ctx = new FilesContext())
            {
                foreach (var watcher in ctx.Watchers.ToList()) {
                    monitor.AddWatcher(watcher);
                }
            }
            return monitor;
        }

        private void HandleFailedWatch(Task t, object o)
        {
            
            FSWatcherTaskPack pack = (FSWatcherTaskPack)o;
            lock(pack)
            {
                pack.Failures += 1;
                pack.LastError = t.Exception.ToString();
                Console.WriteLine($"Failed. Watcher has had {pack.Failures} failure(s). Got exception: {t.Exception}");
                if (!watchers.ContainsKey(pack.Watcher.Id))
                {
                    return;
                }
                Console.WriteLine("Restarting.");
                Thread.Sleep(WaitInterval);
                Console.WriteLine("Wait finished. Restarting watcher.");
                FSWatcher fsWatcher = new FSWatcher(pack.Watcher);
                pack.FSWatcher = fsWatcher;
                if (!watchers.ContainsKey(pack.Watcher.Id))
                {
                    return;
                }
                // TODO: there is a still a race condition here... eventually use a disable flag on the watcher model.
                var waitStop = fsWatcher.StartAndWait();
                pack.Task = waitStop;
                waitStop.ContinueWith(HandleFailedWatch, pack, TaskContinuationOptions.OnlyOnFaulted);
            }

        }
        public void AddWatcher(Watcher watcher)
        {
            if (watcher == null)
            {
                return;
            }
            FSWatcher fsWatcher;
            try {
                fsWatcher = new FSWatcher(watcher);
            } catch (Exception e)
            {
                throw new AddFailure($"Failed to add watcher with Id {watcher.Id}.", e);
            }
            if (fsWatcher == null)
            {
                return;
            }

            FSWatcherTaskPack pack = new FSWatcherTaskPack
            {
                Watcher = watcher,
                FSWatcher = fsWatcher,
            };
            var waitStop = fsWatcher.StartAndWait();
            pack.Task = waitStop;
            waitStop.ContinueWith(HandleFailedWatch, pack, TaskContinuationOptions.OnlyOnFaulted);
            watchers.Add(watcher.Id, pack);
        }


        public class AddFailure : Exception
        {
            public AddFailure(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public void StopAll()
        {
            foreach (var pack in watchers.Values)
            {
                lock(pack)
                {
                    pack.FSWatcher.Stop();
                }
            }
            foreach (var pack in watchers.Values)
            {
                lock (pack)
                {
                    try
                    {
                        pack.Task.Wait();
                    }
                    catch (Exception)
                    {
                        // Pass... TODO: filter only useful expcetions
                    }
                    watchers.Remove(pack.Watcher.Id);
                }

            }
        }
        public void Stop(Watcher watcher)
        {
            FSWatcherTaskPack pack;
            if (!watchers.TryGetValue(watcher.Id, out pack))
            {
                return;
            }
            pack.FSWatcher.Stop();
            try
            {
                pack.Task.Wait();
            } catch (Exception)
            {
                // Pass... TODO: filter only useful expcetions
            }
            watchers.Remove(watcher.Id);
        }

        public void RestartById(int id)
        {
            FSWatcherTaskPack pack;
            if (watchers.TryGetValue(id, out pack))
            {
                lock(pack)
                {
                    pack.FSWatcher.Stop();
                    try
                    {
                        pack.Task.Wait();
                    }
                    catch (Exception)
                    {
                        // Pass... TODO: filter only useful expcetions
                    }
                }

            }
            watchers.Remove(id);

            Watcher found = null;
            using (var ctx = new FilesContext())
            {
                found = ctx.Watchers.Find(id);
            }
            if (found == null)
            {
                return;
            }
            AddWatcher(found);
        }

        public string GetLastError(int id)
        {
            if (!watchers.ContainsKey(id))
            {
                throw new KeyNotFoundException($"Could not find watcher with id {id}.");
            }
            return watchers[id].LastError;
        }

        public void DisableFSOperations(int id)
        {
            FSWatcherTaskPack pack;

            if (watchers.TryGetValue(id, out pack))
            {
                lock (pack)
                {

                    pack.FSWatcher.DisableRaisingEventsAndSync();
                }

            }
        }

        public void ReenableFSOperations(int id)
        {
            FSWatcherTaskPack pack;

            if (watchers.TryGetValue(id, out pack))
            {
                lock (pack)
                {
                    pack.FSWatcher.EnableRaisingEventsAndSync();
                }

            }
        }

        public void SyncById(int id, bool updateThumbnails)
        {
            FSWatcherTaskPack pack;

            if (watchers.TryGetValue(id, out pack))
            {
                lock (pack)
                {
                    pack.FSWatcher.Sync(updateThumbnails);
                }

            }
        }


    }
}
