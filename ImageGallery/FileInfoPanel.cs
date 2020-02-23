using System;
using System.Windows.Forms;

namespace ImageGallery
{
    using Database.Models;
    using Manina.Windows.Forms;
    using System.Collections.Generic;

    public partial class FileInfoPanel : UserControl
    {
        private bool _loading;
        private ImageListView.ImageListViewSelectedItemCollection _items;

        private void UpdateLabels()
        {
            NameLabel.Update();
            PathLabel.Update();
            TagsLabel.Update();
        }
        public ImageListView.ImageListViewSelectedItemCollection Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;

                // Off by default.
                TagsHeadingLabel.Visible = false;
                TagsLabel.Text = "";
                XMPTagsHeadingLabel.Visible = false;
                XMPTagsLabel.Text = "";


                if (value == null)
                {
                    NameLabel.Text = "";
                    PathLabel.Text = "";
                    UpdateLabels();
                    return;
                }


                var numItems = value.Count;
                if (numItems == 0)
                {
                    NameLabel.Text = "No file selected.";
                    PathLabel.Text = "";
                    UpdateLabels();
                    return;
                }
                else if (numItems > 1)
                {
                    NameLabel.Text = $"Multiple ({numItems}) items selected.";
                    PathLabel.Text = "";
                    TagsLabel.Text = "";
                    XMPTagsLabel.Text = "";
                    UpdateLabels();
                    return;
                }
                var item = value[0].VirtualItemKey as File;
                if (item == null)
                {
                    NameLabel.Text = "Error - Could not get item information.";
                    PathLabel.Text = "";
                    UpdateLabels();
                    return;
                }
                NameLabel.Text = item.Name;
                PathLabel.Text = item.FullName;
                if (item.Custom_fts.Count != 0)
                {
                    TagsHeadingLabel.Visible = true;
                    var tags = new List<string>(item.Custom_fts);
                    tags.Sort();
                    TagsLabel.Text = String.Join(", ", tags);
                }
                if (item.XMPTags_fts != null && item.XMPTags_fts.Count != 0)
                {
                    XMPTagsHeadingLabel.Visible = true;
                    var tags = new List<string>(item.XMPTags_fts);
                    tags.Sort();
                    XMPTagsLabel.Text = String.Join(", ", tags);
                }
                UpdateLabels();

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
