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
        private static readonly KeysConverter _keysConverter;
        private Keys _originalKey;
        private MainForm _mainForm;

        static SettingsForm()
        {
            _keysConverter = new KeysConverter();
        }

        public SettingsForm(MainForm mainForm)
        {
            InitializeComponent();
            var settings = Properties.Settings.Default;
            recentlyCreatedBox.Checked = settings.ShowRecentlyCreated;
            recentlyUsedBox.Checked = settings.ShowRecentlyUsed;
            frequentlyClickedBox.Checked = settings.ShowFrequentlyClicked;
            recentlyCreatedUpDown.Value = settings.RecentlyCreatedCount;
            recentlyUsedUpDown.Value = settings.RecentlyUsedCount;
            frequentlyClickedUpDown.Value = settings.FrequentlyClickedCount;
            _mainForm = mainForm;

            _originalKey = Properties.Settings.Default.OpenShortcut;
            hotkeyTextBox.Text = _keysConverter.ConvertToInvariantString(_originalKey);
            enabledKeyBox.Checked = _originalKey != Keys.None;
            hotkeyTextBox.Enabled = enabledKeyBox.Checked;


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
            if (!MaybeUpdateKey())
            {
                MessageBox.Show("Invalid key", "Could not set to this key.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

        private bool MaybeUpdateKey()
        {
            
            if (hotkeyTextBox.Text.Length == 0 || !enabledKeyBox.Checked)
            {
                Properties.Settings.Default.OpenShortcut = Keys.None;
                return true;
            }
            else
            {
                try
                {
                    object keyObject = _keysConverter.ConvertFromInvariantString(hotkeyTextBox.Text);
                    if (keyObject == null)
                    {
                        throw new ArgumentException();
                    }
                    Properties.Settings.Default.OpenShortcut = (Keys)keyObject;
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }
            return true;
        }

        private void hotkeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Keys modifierKeys = e.Modifiers;
            Keys pressedKey = e.KeyData ^ modifierKeys; //remove modifier keys

            if (pressedKey != Keys.None)
            {
                hotkeyTextBox.Text = _keysConverter.ConvertToInvariantString(e.KeyData);
                e.Handled = false;
                e.SuppressKeyPress = true;
            }
            else
            {
                hotkeyTextBox.Text = "";
                e.Handled = false;
                e.SuppressKeyPress = true;
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            _mainForm.MaybeUnsetGlobalKey(Properties.Settings.Default.OpenShortcut);
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mainForm.MaybeSetGlobalKey(Properties.Settings.Default.OpenShortcut);

        }

        private void enabledKeyBox_CheckedChanged(object sender, EventArgs e)
        {
            hotkeyTextBox.Enabled = enabledKeyBox.Checked;
        }

        private void ClearCacheButton_Click(object sender, EventArgs e)
        {
            FileModelAdapter.RefreshIconCache();
        }
    }
}
