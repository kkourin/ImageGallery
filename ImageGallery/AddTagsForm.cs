using ImageGallery.Database;
using ImageGallery.Database.Models;
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
    public partial class AddTagsForm : Form
    {
        List<File> EditedFiles { get; set; }

        public AddTagsForm(List<File> files)
        {
            InitializeComponent();
            EditedFiles = files;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var tags = tagsBox.Text;
            if (tags.Contains("|"))
            {
                MessageBox.Show(
                    "Cannot use character '|' in tags.",
                    "Invalid tags",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
                return;
            }
            var newTags = Helpers.TagStringToHash(tags);
            int count = 0;
            using (var ctx = new FilesContext())
            {
                count = ctx.UpdateFilesTags(EditedFiles, newTags);
            }
            MessageBox.Show(
                $"Updated {count} file(s).",
                "Updated"
            );
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddTagsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
