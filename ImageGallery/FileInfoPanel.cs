using System;
using System.Windows.Forms;

namespace ImageGallery
{
    using Database.Models;
    using Manina.Windows.Forms;
    public partial class FileInfoPanel : UserControl
    {
        private bool _loading;
        private ImageListView.ImageListViewSelectedItemCollection _items;

        public ImageListView.ImageListViewSelectedItemCollection Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                if (value == null)
                {
                    return;
                }
                var numItems = value.Count;
                if (numItems == 0)
                {
                    NameLabel.Text = "No file selected.";
                    PathLabel.Text = "";
                    return;
                }
                else if (numItems > 1)
                {
                    NameLabel.Text = "Multiple items selected.";
                    PathLabel.Text = "";
                    return;
                }
                var item = value[0].VirtualItemKey as File;
                if (item == null)
                {
                    NameLabel.Text = "Error - Could not get item information.";
                    PathLabel.Text = "";
                    return;
                }
                NameLabel.Text = item.Name;
                PathLabel.Text = item.FullName;
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
