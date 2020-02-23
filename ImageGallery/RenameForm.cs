using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageGallery
{
    public partial class RenameForm : Form
    {
        Database.Models.File _file;
        public RenameForm(Database.Models.File file)
        {
            InitializeComponent();
            _file = file;
            filenameTextbox.Text = file.Name;
            HighlightToLastDot(filenameTextbox);
        }

        private void HighlightToLastDot(TextBox textbox)
        {
            var text = filenameTextbox.Text;
            int dotIndex = text.LastIndexOf('.');
            int lastIndex;
            if (dotIndex == -1)
            {
                lastIndex = filenameTextbox.Text.Length;
            } else
            {
                lastIndex = dotIndex;
            }
            textbox.SelectionStart = 0;
            textbox.SelectionLength = lastIndex;


        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var fileInfo = new FileInfo(_file.FullName);
            var newPath = Path.Combine(fileInfo.DirectoryName, filenameTextbox.Text);
            var newFileInfo = new FileInfo(newPath);
            try
            {
                if (fileInfo.Extension != newFileInfo.Extension)
                {
                    var messageBoxResult = MessageBox.Show(
                        $"You are changing the file extension from {fileInfo.Extension} to {newFileInfo.Extension}. Are you sure?", "ImageGallery", MessageBoxButtons.YesNo, MessageBoxIcon.Warning
                    );
                    if (messageBoxResult == DialogResult.No)
                    {
                        return;
                    }
                }
                File.Move(_file.FullName, newPath);
            } catch (Exception ex)
            {
                MessageBox.Show($"Could not show message: {ex.ToString()}", "ImageGallery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Close();
        }

        private void nameLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
