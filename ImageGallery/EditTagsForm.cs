using ImageGallery.Database;
using ImageGallery.Database.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public partial class EditTagsForm : Form
    {
        File EditedFile { get; set; }

        public EditTagsForm(File file)
        {
            InitializeComponent();
            EditedFile = file;
            tagsBox.Text = Helpers.HashToTagString(EditedFile.Custom_fts);
            tagsBox.SelectionStart = tagsBox.Text.Length;
            tagsBox.SelectionLength = 0;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var tags = tagsBox.Text;
            if (tags.Contains("|"))
            {
                MessageBox.Show("Cannot use character '|' in tags.");
                return;
            }
            EditedFile.Custom_fts = Helpers.TagStringToHash(tags);
            using (var ctx = new FilesContext())
            {
                ctx.UpdateFileTags(EditedFile);
            }
            Close();
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
