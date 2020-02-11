using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using System.IO;
using System.Threading;

namespace ImageGallery
{
    using ImageGallery.Database.Models;
    public partial class VideoControl : UserControl
    {
        Media _media;
        public bool playTriggered;
        long currentMediaDuration = -1;
        const int parseTimeout = 3000;
        CancellationTokenSource parseTokenSource = new CancellationTokenSource();
        public enum ButtonState
        {
            Pause,
            Unpause,
            Repeat,
            Play
        }
        private delegate void setMPDelegate(MediaPlayer mp);
        private delegate void setButtonDelegate(ButtonState state);
        private delegate void updateCurrentMediaInfoDelegate(long time);
        private delegate void setPlayLabelDelegate(bool visible);
        private delegate void updateVolumeDelegate(float volume);
        private delegate void setVideoViewVisibleDelegate(bool visible);
        private delegate void stopVideoViewDelegate();
        private const int trackBarGapWidth = 14;
        private const int trackBarHitWidth = 8;
        private bool _closingPlayer;
        public VLCTaskQueue Queue { get; set; }
        public LibVLC LibVLC { get; set; }
        private MediaPlayer _mp;
        private bool _settingSeek;

        public bool Started { get; set; }

        static readonly byte[] DefaultImage;

        static VideoControl()
        {
            using (var ms = new MemoryStream())
            {
                var img = Properties.Resources.nothumb;
                img.Save(ms, img.RawFormat);
                DefaultImage = ms.ToArray();
            }
        }
        private void SetMPSafe(MediaPlayer mp)
        {
            if (videoView.InvokeRequired)
            {
                var d = new setMPDelegate(SetMPSafe);
                videoView.Invoke(d, new object[] { mp });
            }
            else
            {
                videoView.MediaPlayer = mp;
            }
        }

        private static byte[] getPreviewImage(File file)
        {
            if (file.Thumbnail != null)
            {
                return file.Thumbnail;
            }
            return DefaultImage;
        }
        private void SetButtonStateSafe(ButtonState state)
        {

            if (playButton.InvokeRequired)
            {
                if (_closingPlayer)
                {
                    return;
                }
                var d = new setButtonDelegate(SetButtonStateSafe);
                playButton.Invoke(d, new object[] { state });
            }
            else
            {
                Bitmap backgroundIcon = null;
                switch (state)
                {
                    case ButtonState.Pause:
                        backgroundIcon = Properties.Resources.pause;
                        break;
                    case ButtonState.Unpause:
                        backgroundIcon = Properties.Resources.play;
                        break;
                    case ButtonState.Play:
                        backgroundIcon = Properties.Resources.play;
                        break;
                    case ButtonState.Repeat:
                        backgroundIcon = Properties.Resources.repeat;
                        break;
                }
                playButton.BackgroundImage = backgroundIcon;
            }
        }

        private void SetPlayLabelVisibleSafe(bool visible)
        {
            if (playButton.InvokeRequired)
            {
                if (_closingPlayer)
                {
                    return;
                }
                var d = new setPlayLabelDelegate(SetPlayLabelVisibleSafe);
                playLabel.Invoke(d, new object[] { visible });
            }
            else
            {
                playLabel.Visible = visible;
            }
        }

        public void setMediaFromFile(File file)
        {
            if (_mp == null || Queue == null)
            {
                return;
            }
            playTriggered = false;
            seekBar.Enabled = false;
            SetButtonStateSafe(ButtonState.Play);
            SetPlayLabelVisibleSafe(true);

            _media = new Media(LibVLC, file.FullName, FromType.FromPath);


            // parse media time:
            parseTokenSource.Cancel();
            parseTokenSource = new CancellationTokenSource();

            var task = Task.Run(async () =>
            {
                var f = await _media.Parse(MediaParseOptions.ParseNetwork, parseTimeout, parseTokenSource.Token);
                if (f == MediaParsedStatus.Done)
                {
                    currentMediaDuration = _media.Duration;
                }
                UpdateCurrentMediaInfoSafe(-1);
            }, parseTokenSource.Token);

            var preview = new Media(LibVLC, new MemoryStream(getPreviewImage(file)));
            Queue.PlayerSetMedia(preview);
            UpdateCurrentMediaInfo(-1);
            Queue.PlayerPlay();
        }

        public void UpdateCurrentMediaInfo(long currentTime)
        {
            if (_media == null || _mp == null)
            {
                return;
            }

            var fullSpan = TimeSpan.FromMilliseconds(currentMediaDuration == -1 ? 0 : currentMediaDuration);
            var currentSpan = TimeSpan.FromMilliseconds(currentTime == -1 ? 0 : currentTime);
            if (fullSpan.TotalHours < 1 && currentSpan.TotalHours < 1)
            {
                timeLabel.Text = String.Format("{0:mm\\:ss}/{1:mm\\:ss}", currentSpan, fullSpan);
            }
            else
            {
                timeLabel.Text = String.Format("{0:hh\\:mm\\:ss}/{1:hh\\:mm\\:ss}", currentSpan, fullSpan);
            }

            if (fullSpan.TotalMilliseconds != 0 && !_settingSeek)
            {
                var frac = (double)(seekBar.Maximum - seekBar.Minimum) * ((double)currentSpan.TotalMilliseconds / fullSpan.TotalMilliseconds);
                setTrackBarValue(Convert.ToInt32(frac));
            }

        }

        private void setTrackBarValue(int value)
        {
            seekBar.Value = Math.Max(seekBar.Minimum, Math.Min(seekBar.Maximum, value));
        }

        private void UpdateCurrentMediaInfoSafe(long time)
        {

            if (timeLabel.InvokeRequired)
            {
                if (_closingPlayer)
                {
                    return;
                }
                var d = new updateCurrentMediaInfoDelegate(UpdateCurrentMediaInfoSafe);
                this.Invoke(d, new object[] { time });
            }
            else
            {
                UpdateCurrentMediaInfo(time);

            }
        }

        private void UpdateVolumeSafe(float volume)
        {
            if (volumeBar.InvokeRequired)
            {
                if (_closingPlayer)
                {
                    return;
                }
                var d = new updateVolumeDelegate(UpdateVolumeSafe);

                volumeBar.Invoke(d, new object[] { volume });

            }
            else
            {

                // This prevents the player's volume change event from triggering this event and causeing
                // a feedback loop.
                if (volumeBar.Focused)
                {
                    return;
                }

                volumeBar.Value = volume == (float)-1 ? 0 : Convert.ToInt32(volume * 100);

            }
        }
        public void SetPlayer(MediaPlayer mp)
        {
            // Clean up old MP state.
            SetPlayLabelVisibleSafe(false);
            if (_mp != null)
            {
                _closingPlayer = true;
                Queue.CloseAndWait();
                _mp.TimeChanged -= _mp_TimeChanged;
                _mp.VolumeChanged -= _mp_VolumeChanged;
                _mp.Playing -= _mp_Playing;
                _mp.Paused -= _mp_Paused;
                _mp.EndReached -= _mp_EndReached;

                // We must have unsubscribed from all events or else we may deadlock
                // if mp.Stop() calls any of the event handlers.
                //Queue.CloseAndWait();


            }

            _media = null;
            playTriggered = false;
            seekBar.Enabled = false;
            _settingSeek = false;

            Queue = null;

            // Add the new one.
            _mp = mp;
            SetMPSafe(mp);
            if (mp != null)
            {
                _closingPlayer = false;
                Queue = new VLCTaskQueue(mp);
                _mp.EndReached += _mp_EndReached;
                _mp.Paused += _mp_Paused;
                _mp.Playing += _mp_Playing;
                _mp.VolumeChanged += _mp_VolumeChanged;
                _mp.TimeChanged += _mp_TimeChanged;

            }

        }

        private void _mp_TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            if (!playTriggered)
            {
                return;
            }
            UpdateCurrentMediaInfoSafe(e.Time);

        }

        private void _mp_VolumeChanged(object sender, MediaPlayerVolumeChangedEventArgs e) {

            UpdateVolumeSafe(e.Volume);

        }

        private void _mp_Playing(object sender, EventArgs e)
        {

            if (playTriggered)
            {
                SetButtonStateSafe(ButtonState.Pause);
                SetPlayLabelVisibleSafe(false);
            }

        }

        private void _mp_Paused(object sender, EventArgs e)
        {
            if (playTriggered)
            {
                SetButtonStateSafe(ButtonState.Unpause);

            }

        }

        public VideoControl()
        {
            InitializeComponent();

        }

        private void _mp_EndReached(object sender, EventArgs e)
        {

            if (Queue == null || !playTriggered)
            {
                return;
            }
            if (!loopBox.Checked)
            {
                SetButtonStateSafe(ButtonState.Repeat);
                SetPlayLabelVisibleSafe(true);
            }
            else
            {
                Queue.PlayerStop();
                Queue.PlayerPlay();
            }

        }

        private void playButton_Click(object sender, EventArgs e)
        {
            playClick();
        }

        private void playClick()
        {
            if (_mp == null || Queue == null)
            {
                return;
            }
            if (!playTriggered)
            {
                Queue.PlayerStop();
                Queue.PlayerSetMedia(_media);
                playTriggered = true;
                seekBar.Enabled = true;
            }
            if (_mp.State == VLCState.Ended)
            {
                Queue.PlayerStop();
            }
            // Play the media.
            Queue.PlayerPlayPause();
        }

        public void HardPause()
        {
            if (_mp == null || Queue == null)
            {
                return;
            }
            Queue.PlayerPause();
        }

        private void zoomBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Queue == null)
            {
                return;
            }
            Queue.PlayerSetScale(zoomBox.Checked ? 1 : 0);
        }

        private void refreshVolumeLabel()
        {
            volumeLabel.Text = $"{volumeBar.Value}%";
        }

        private void volumeBar_ValueChanged(object sender, EventArgs e)
        {
            refreshVolumeLabel();
            if (Queue == null)
            {
                return;
            }

            Queue.PlayerSetVolume(volumeBar.Value);

        }

        private void volumeBar_MouseDown(object sender, MouseEventArgs e)
        {
            followMouse(sender as TrackBar, e);

        }

        private void volumeBar_MouseMove(object sender, MouseEventArgs e)
        {
            followMouse(sender as TrackBar, e);
        }

        private static void followMouse(TrackBar trackBar, MouseEventArgs e)
        {
            double value;
            if (e.Button == MouseButtons.Left)
            {
                // Jump to the clicked location
                if (e.X < trackBarHitWidth || e.X > trackBar.Width - trackBarHitWidth)
                {
                    return;
                }
                value = ((double)(e.X - trackBarGapWidth) / (double)(trackBar.Width - 2 * trackBarGapWidth)) * (trackBar.Maximum - trackBar.Minimum);
                trackBar.Value = Math.Max(trackBar.Minimum, Math.Min(trackBar.Maximum, Convert.ToInt32(value)));
            }
        }

        private void seekBar_MouseDown(object sender, MouseEventArgs e)
        {
            followMouse(sender as TrackBar, e);
            _settingSeek = true;
        }

        private void seekBar_MouseMove(object sender, MouseEventArgs e)
        {
            followMouse(sender as TrackBar, e);
        }

        private void seekBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _settingSeek)
            {
                _settingSeek = false;
                float position = (float)seekBar.Value / (seekBar.Maximum - seekBar.Minimum) - (float)0.001;
                if (_mp != null && _media != null && currentMediaDuration > 0)
                {
                    var secondPercent = (float)Math.Max(0, (currentMediaDuration - 1000)) / (currentMediaDuration);
                    position = Math.Min(position, secondPercent);
                }
                else
                {
                    return;
                }
                if (Queue != null)
                {
                    Queue.PlayerSeekPosition(position);
                }
            }
        }

        private void playLabel_Click(object sender, EventArgs e)
        {
            playClick();
        }

        private void videoPanel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("test");
        }
        public void setVideoViewVisibleSafe(bool visible)
        {
            if (InvokeRequired)
            {
                var d = new setVideoViewVisibleDelegate(setVideoViewVisibleSafe);
                Invoke(d, new object[] { visible });
            }
            else
            {
                Visible = visible;
            }
        }

        public void stopVideoViewSafe()
        {
            if (InvokeRequired)
            {
                var d = new stopVideoViewDelegate(stopVideoViewSafe);
                Invoke(d);
            }
            else
            {
                if (Queue != null && videoView.MediaPlayer != null)
                {
                    Queue.PlayerStop();
                }
            }
        }

    }
}