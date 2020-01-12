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
                ZoomIfTooLarge();
                poppedPreviewBox.EndUpdate();
            }
        }

        private void ZoomIfTooLarge()
        {
            if (previewImage == null || poppedPreviewBox.Image == null)
            {
                Console.WriteLine("image null");
                return;
            }
            Console.WriteLine($"{previewImage.Width}, {poppedPreviewBox.Width}, {previewImage.Height}, {poppedPreviewBox.Height}");

            if (previewImage.Width > poppedPreviewBox.Width || previewImage.Height > poppedPreviewBox.Height )
            {
                poppedPreviewBox.ZoomToFit();
                return;
            }
            poppedPreviewBox.Zoom = 100;
            Console.WriteLine("Not zooming");
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
            ZoomIfTooLarge();
        }

        private void poppedPreviewBox_Resize(object sender, EventArgs e)
        {

        }

        private void PopoutPreview_Resize(object sender, EventArgs e)
        {
            Console.WriteLine(sender.GetType().ToString());
            ZoomIfTooLarge();
        }
    }
}
