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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            var settings = Properties.Settings.Default;
            recentlyCreatedBox.Checked = settings.ShowRecentlyCreated;
            recentlyUsedBox.Checked = settings.ShowRecentlyUsed;
            frequentlyClickedBox.Checked = settings.ShowFrequentlyClicked;
            recentlyCreatedUpDown.Value = settings.RecentlyCreatedCount;
            recentlyUsedUpDown.Value = settings.RecentlyUsedCount;
            frequentlyClickedUpDown.Value = settings.FrequentlyClickedCount;

        }

        private void RecentlyUsedBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var settings = Properties.Settings.Default;
            settings.ShowRecentlyCreated = recentlyCreatedBox.Checked;
            settings.ShowRecentlyUsed = recentlyUsedBox.Checked;
            settings.ShowFrequentlyClicked = frequentlyClickedBox.Checked;
            settings.RecentlyCreatedCount = Decimal.ToInt32(recentlyCreatedUpDown.Value);
            settings.RecentlyUsedCount = Decimal.ToInt32(recentlyUsedUpDown.Value);
            settings.FrequentlyClickedCount = Decimal.ToInt32(frequentlyClickedUpDown.Value);
            settings.Save();
            Close();
        }
    }
}
