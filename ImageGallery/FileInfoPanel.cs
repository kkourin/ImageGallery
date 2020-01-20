using System;
using System.Windows.Forms;

namespace ImageGallery
{
    using Database.Models;
    public partial class FileInfoPanel : UserControl
    {
        private File _file;
        private bool _loading;
        public File File
        {
            get
            {
                return _file;
            }
            set
            {
                _file = value;
                if (value == null)
                {
                    NameLabel.Text = "No file selected.";
                    PathLabel.Text = "";
                    return;
                }
                NameLabel.Text = value.Name;
                PathLabel.Text = value.FullName;
            }
        }

        public bool Loading
        {
            get
            {
                return _loading;
            }
            set
            {
                _loading = value;
                LoadingLabel.Visible = value;
                LoadingLabel.Update();
            }
        }
        public FileInfoPanel()
        {
            InitializeComponent();
        }

        private void NameLabel_Click(object sender, EventArgs e)
        {

        }

    }
}
