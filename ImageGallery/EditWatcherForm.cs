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
    public partial class EditWatcherForm : Form
    {
        private Watcher _watcher;
        WatcherKeyManager _watcherKeyManager;
        private static readonly KeysConverter _keysConverter;

        static EditWatcherForm() {
            _keysConverter = new KeysConverter();
        }
        public EditWatcherForm(Watcher watcher, WatcherKeyManager watcherKeyManager)
        {
            InitializeComponent();
            _watcher = watcher;
            _watcherKeyManager = watcherKeyManager;
            hotkeyTextBox.Text = _keysConverter.ConvertToInvariantString(watcher.ShortcutKeys);
            globalBox.Checked = watcher.GlobalShortcut;
            Keys key = watcher.ShortcutKeys & Keys.KeyCode;
            Keys modifier = watcher.ShortcutKeys & Keys.Modifiers;
            enabledBox.Checked = !(key == Keys.None && modifier == Keys.None);
            hotkeyTextBox.Enabled = enabledBox.Checked;
        }

        private void hotkeyTextBox_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void hotkeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Keys modifierKeys = e.Modifiers;
            Keys pressedKey = e.KeyData ^ modifierKeys; //remove modifier keys

            if (modifierKeys != Keys.None && pressedKey != Keys.None)
            {
                hotkeyTextBox.Text = _keysConverter.ConvertToInvariantString(e.KeyData);
            }
            else
            {
                hotkeyTextBox.Text = "";
                e.Handled = false;
                e.SuppressKeyPress = true;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (enabledBox.Checked == false || hotkeyTextBox.Text.Length == 0 )
            {
                using (var ctx = new FilesContext())
                {
                    ctx.UpdateShortcuts(_watcher, Keys.None, globalBox.Checked);
                }
                _watcherKeyManager.UpdateData();
                Close();
                return;
            }

            try
            {
                object keyObject = _keysConverter.ConvertFromInvariantString(hotkeyTextBox.Text);
                if (keyObject == null)
                {
                    throw new ArgumentException();
                }

                using (var ctx = new FilesContext())
                {
                    ctx.UpdateShortcuts(_watcher, (Keys)keyObject, globalBox.Checked);
                }

            }
            catch (ArgumentException)
            {
                MessageBox.Show("Invalid key", "Could not set this key.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _watcherKeyManager.UpdateData();
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void enabledBox_CheckedChanged(object sender, EventArgs e)
        {
            hotkeyTextBox.Enabled = enabledBox.Checked;
        }

        private void EditWatcherForm_Load(object sender, EventArgs e)
        {
            _watcherKeyManager.DisableKeys();
        }
    }
}
