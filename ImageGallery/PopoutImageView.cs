using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageGallery
{
    public partial class PopoutPreview : Form
    {
        private Image previewImage;
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
                poppedPreviewBox.ZoomToFit();
                poppedPreviewBox.EndUpdate();
            }
        }
        public PopoutPreview()
        {
            InitializeComponent();
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
            poppedPreviewBox.ZoomToFit();
        }
    }
}
