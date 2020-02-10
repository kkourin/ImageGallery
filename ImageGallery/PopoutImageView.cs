using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cyotek.Windows.Forms;

namespace ImageGallery
{
    using Database.Models;
    using LibVLCSharp.Shared;

    public partial class PopoutPreview : Form
    {
        private Image previewImage;
        private LibVLC _libVLC;

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }
        public FileInfoPanel FileInfoPanel
        {
            get
            {
                return fileInfoPanel;
            }
        }
        public Image PreviewImage
        {
            get
            {
                return previewImage;
            }
            set
            {
                poppedPreviewBox.BeginUpdate();
                previewImage = value;
                poppedPreviewBox.Image = value;
                ZoomIfTooLarge();
                poppedPreviewBox.EndUpdate();
            }
        }

        private void ZoomIfTooLarge()
        {
            if (previewImage == null || poppedPreviewBox.Image == null)
            {
                return;
            }

            if (previewImage.Width > poppedPreviewBox.Width || previewImage.Height > poppedPreviewBox.Height )
            {
                poppedPreviewBox.ZoomToFit();
                return;
            }
            poppedPreviewBox.Zoom = 100;
        }
        public PopoutPreview(LibVLC libVLC)
        {
            InitializeComponent();
            Helpers.DisableFormTransition(Handle);
            _libVLC = libVLC;
            videoView.LibVLC = _libVLC;
        }

        public void setMediaPlayer(MediaPlayer mediaPlayer)
        {
            videoView.SetPlayer(mediaPlayer);
        }

        public void stopMediaPlayer()
        {
            videoView.stopVideoViewSafe();
        }

        public void SetVideoPlayerVisible(bool visible)
        {
            videoView.setVideoViewVisibleSafe(visible);
        }

        public void SetMediaPlayerMedia(File file)
        {
            videoView.setMediaFromFile(file);
        }

        private void PopoutPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void PopoutPreview_Load(object sender, EventArgs e)
        {
            ZoomIfTooLarge();
        }

        private void poppedPreviewBox_Resize(object sender, EventArgs e)
        {

        }

        private void PopoutPreview_Resize(object sender, EventArgs e)
        {
            ZoomIfTooLarge();
        }
    }
}
