using LibVLCSharp.Shared;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageGallery
{
    public class VLCTaskQueue
    {
        // Implementation of a priority queue that has bounding and blocking functionality.
        public class SimplePriorityQueue<TPriority, TValue> : IProducerConsumerCollection<KeyValuePair<int, TValue>>
        {
            // Each internal queue in the array represents a priority level. 
            // All elements in a given array share the same priority.
            private ConcurrentQueue<KeyValuePair<int, TValue>>[] _queues = null;

            // The number of queues we store internally.
            private int priorityCount = 0;
            private int m_count = 0;

            public SimplePriorityQueue(int priCount)
            {
                this.priorityCount = priCount;
                _queues = new ConcurrentQueue<KeyValuePair<int, TValue>>[priorityCount];
                for (int i = 0; i < priorityCount; i++)
                    _queues[i] = new ConcurrentQueue<KeyValuePair<int, TValue>>();
            }

            // IProducerConsumerCollection members
            public bool TryAdd(KeyValuePair<int, TValue> item)
            {
                _queues[item.Key].Enqueue(item);
                Interlocked.Increment(ref m_count);
                return true;
            }

            public bool TryTake(out KeyValuePair<int, TValue> item)
            {
                bool success = false;

                // Loop through the queues in priority order
                // looking for an item to dequeue.
                for (int i = 0; i < priorityCount; i++)
                {
                    // Lock the internal data so that the Dequeue
                    // operation and the updating of m_count are atomic.
                    lock (_queues)
                    {
                        success = _queues[i].TryDequeue(out item);
                        if (success)
                        {
                            Interlocked.Decrement(ref m_count);
                            return true;
                        }
                    }
                }

                // If we get here, we found nothing. 
                // Assign the out parameter to its default value and return false.
                item = new KeyValuePair<int, TValue>(0, default(TValue));
                return false;
            }

            public int Count
            {
                get { return m_count; }
            }

            // Required for ICollection
            void ICollection.CopyTo(Array array, int index)
            {
                CopyTo(array as KeyValuePair<int, TValue>[], index);
            }

            // CopyTo is problematic in a producer-consumer.
            // The destination array might be shorter or longer than what 
            // we get from ToArray due to adds or takes after the destination array was allocated.
            // Therefore, all we try to do here is fill up destination with as much
            // data as we have without running off the end.                
            public void CopyTo(KeyValuePair<int, TValue>[] destination, int destStartingIndex)
            {
                if (destination == null) throw new ArgumentNullException();
                if (destStartingIndex < 0) throw new ArgumentOutOfRangeException();

                int remaining = destination.Length;
                KeyValuePair<int, TValue>[] temp = this.ToArray();
                for (int i = 0; i < destination.Length && i < temp.Length; i++)
                    destination[i] = temp[i];
            }

            public KeyValuePair<int, TValue>[] ToArray()
            {
                KeyValuePair<int, TValue>[] result;

                lock (_queues)
                {
                    result = new KeyValuePair<int, TValue>[this.Count];
                    int index = 0;
                    foreach (var q in _queues)
                    {
                        if (q.Count > 0)
                        {
                            q.CopyTo(result, index);
                            index += q.Count;
                        }
                    }
                    return result;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IEnumerator<KeyValuePair<int, TValue>> GetEnumerator()
            {
                for (int i = 0; i < priorityCount; i++)
                {
                    foreach (var item in _queues[i])
                        yield return item;
                }
            }

            public bool IsSynchronized
            {
                get
                {
                    throw new NotSupportedException();
                }
            }

            public object SyncRoot
            {
                get { throw new NotSupportedException(); }
            }
        }

        private const int DrainMillisecondsTimeout = 125;
        public MediaPlayer MediaPlayer;

        BlockingCollection<KeyValuePair<int, Action<MediaPlayer>>> _bc;
        readonly CancellationTokenSource cts = new CancellationTokenSource();
        Task _loopTask;
        EventWaitHandle _closeWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
        public VLCTaskQueue(MediaPlayer mediaPlayer)
        {
            MediaPlayer = mediaPlayer;
            SimplePriorityQueue<int, Action<MediaPlayer>> queue = new SimplePriorityQueue<int, Action<MediaPlayer>>(2);
            _bc = new BlockingCollection<KeyValuePair<int, Action<MediaPlayer>>>();
            _loopTask = Task.Factory.StartNew(() => executeLoop(cts.Token), CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void executeLoop(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    var kv = _bc.Take(token);
                    var command = kv.Value;
                    command(MediaPlayer);
                }
            }
            catch (System.OperationCanceledException)
            {
                Console.WriteLine("Cancelled");
            }
            finally
            {
                MediaPlayer.Stop();
                Console.WriteLine("setting");
                _closeWaitHandle.Set();
            }
        }


        public void CloseAndWait() {
            if (!cts.IsCancellationRequested)
            {
                cts.Cancel();
            }

            while (!_closeWaitHandle.WaitOne(DrainMillisecondsTimeout))
            {
                Console.WriteLine("doin");
                Application.DoEvents();
            }
            Console.WriteLine("finished");
        }

        private void AddLowPriority(Action<MediaPlayer> command)
        {
            _bc.Add(new KeyValuePair<int, Action<MediaPlayer>>(1, command));
        }

        private void AddHighPriority(Action<MediaPlayer> command)
        {
            _bc.Add(new KeyValuePair<int, Action<MediaPlayer>>(0, command));
        }

        public void PlayerStop()
        {
            AddHighPriority(mp => mp.Stop());
        }

        public void PlayerSetVolume(int volume)
        {
            AddLowPriority(mp => mp.Volume = volume);
        }


        public void PlayerSetMedia(Media media)
        {
            AddHighPriority(mp => mp.Media = media);
        }


        public void PlayerPlay()
        {
            AddLowPriority(mp => mp.Play());
        }

        public void PlayerPause()
        {

            AddLowPriority(mp => mp.Pause());
        }

        public void PlayerPlayPause()
        {
            AddLowPriority(mp =>
            {
                if (mp.IsPlaying)
                {
                    mp.Pause();
                }
                else
                {
                    mp.Play();
                }
            });
        }


        public void PlayerSeekPosition(float position)
        {
            AddLowPriority(mp => mp.Position = position);
        }

        public void PlayerSetScale(float scale)
        {
            AddLowPriority(mp => mp.Scale = scale);
        }

    }
}
