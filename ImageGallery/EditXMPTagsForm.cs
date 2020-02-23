using ImageGallery.Database;
using ImageGallery.Database.Models;
using ImageGallery.XMPLib;
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
    public partial class EditXMPTagsForm : Form
    {
        private SearchTagEditor _searchTagEditor;

        File EditedFile { get; set; }
        public EditXMPTagsForm(File file, SearchTagEditor searchTagEditor)
        {
            InitializeComponent();
            EditedFile = file;
            var currentTags = EditedFile.XMPTags_fts;
            if (currentTags != null)
            {
                tagsBox.Text = Helpers.HashToTagString(EditedFile.XMPTags_fts);
            }
            tagsBox.SelectionStart = tagsBox.Text.Length;
            tagsBox.SelectionLength = 0;
            FilenameLabel.Text = $"Filename: {file.FullName}";
            _searchTagEditor = searchTagEditor;
        }

        private void EditXMPTagsForm_Load(object sender, EventArgs e)
        {

        }

        private void FilenameLabel_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var tags = tagsBox.Text;
            if (tags.Contains("|"))
            {
                MessageBox.Show("Cannot use character '|' in tags.");
                return;
            }
            EditedFile.XMPTags_fts = Helpers.TagStringToHash(tags);
            var editResult = _searchTagEditor.setSearchTag(EditedFile.FullName, EditedFile.XMPTags_fts.ToHashSet());
            if (editResult == false)
            {
                MessageBox.Show("Could not set XMP tags for image.", "ImageGallery", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                using (var ctx = new FilesContext())
                {
                    ctx.UpdateXMPFileTags(EditedFile);
                }
            }
        }

        private void tagsBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
