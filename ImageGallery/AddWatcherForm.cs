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
    using Database;
    using Database.Models;
    public partial class AddWatcherForm : Form
    {
        public Watcher AddedWatcher { get; set; }
        public AddWatcherForm()
        {
            InitializeComponent();


        }

        private void AddWatcherForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void NameLabel_Click(object sender, EventArgs e)
        {

        }

        private void CancelAddButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!ValidCheck())
            {
                return;
            }
            var name = NameTextBox.Text;
            var directory = DirectoryTextBox.Text;
            var whitelist = ExtensionTextBox.Text;
            Watcher watcher;
            using (var ctx = new FilesContext())
            {
                watcher = ctx.AddWatcherForm(name, directory, whitelist);
            }
            AddedWatcher = watcher;
            this.Close();
        }

        private bool ValidCheck()
        {
            if (NameTextBox.TextLength == 0)
            {
                return false;
            }
            if (DirectoryTextBox.TextLength == 0)
            {
                return false;
            }
            return true; ;
        }

        private string GetTextToAppend(string[] extensions)
        {
            var strings = from ext in extensions select '.' + ext;
            string textToAppend = String.Join(", ", strings);
            if (ExtensionTextBox.TextLength != 0)
            {
                textToAppend = ", " + textToAppend;
            }
            return textToAppend;
        }
        private void ImagesButton_Click(object sender, EventArgs e)
        {
            ExtensionTextBox.AppendText(GetTextToAppend(Helpers.ImageFileExtensions));
        }

        private void VideoButton_Click(object sender, EventArgs e)
        {
            ExtensionTextBox.AppendText(GetTextToAppend(Helpers.VideoFileExtensions));

        }

        private void FileSelectButton_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    DirectoryTextBox.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
