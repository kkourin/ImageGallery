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
    public partial class LogForm : Form
    {
        private static LogForm openForm = null;

        public static LogForm GetInstance()
        {
            if (openForm == null)
            {
                openForm = new LogForm();
                openForm.FormClosed += delegate { openForm = null; };
                openForm.Show();
            }
            return openForm;
        }
        public LogForm()
        {
            InitializeComponent();
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            TextBoxAppender.ConfigureTextBoxAppender(logTextBox);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
