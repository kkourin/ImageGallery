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
    public partial class AddWatcherForm : Form
    {
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
            using (var ctx = new FilesContext())
            {
                ctx.AddWatcherForm(name, directory, whitelist);
            }
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
    }
}
