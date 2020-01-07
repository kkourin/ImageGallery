using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ImageGallery.Database
{
    using Models;
    class WatcherMonitor
    {
        class FSWatcherTaskPack
        {
            public Watcher Watcher { get; set; }
            public FSWatcher FSWatcher { get; set; }
            public Task Task { get; set; }
            public int Failures { get; set; }
        }
        Dictionary<int, FSWatcherTaskPack> watchers;
        private int WaitInterval = 10000;
        public WatcherMonitor()
        {
            watchers = new Dictionary<int, FSWatcherTaskPack>();
        }

        private void HandleFailedWatch(Task t, object o)
        {
            FSWatcherTaskPack pack = (FSWatcherTaskPack)o;
            pack.Failures += 1;
            Console.WriteLine($"Failed. Watcher has had {pack.Failures} failure(s). Got exception: {t.Exception}");
            Console.WriteLine("Restarting.");
            Thread.Sleep(WaitInterval);
            Console.WriteLine("Wait finished. Restarting watcher.");

            FSWatcher fsWatcher = new FSWatcher(pack.Watcher);
            pack.FSWatcher = fsWatcher;

            var waitStop = fsWatcher.StartAndWait();
            pack.Task = waitStop;
            waitStop.ContinueWith(HandleFailedWatch, pack, TaskContinuationOptions.OnlyOnFaulted);
        }
        public void AddWatcher(Watcher watcher)
        {

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
        public void StopAll()
        {
            foreach (var pack in watchers.Values)
            {
                pack.FSWatcher.Stop();
            }
            foreach (var pack in watchers.Values)
            {
                pack.Task.Wait();
            }
        }

        public class AddFailure : Exception
        {
            public AddFailure(string message, Exception inner)
                : base(message, inner)
            {
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
            pack.Task.Wait();
            watchers.Remove(watcher.Id);
        }
    }
}
