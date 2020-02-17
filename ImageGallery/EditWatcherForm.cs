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
        string _orignalName;
        HashSet<string> _orignalWhitelist;
        bool _originalScanBool;
        bool _originalVideoBool;
        Keys _orignalKey;
        bool _orignalGlobalKeyBool;
        WatcherMonitor _watcherMonitor;

        static EditWatcherForm() {
            _keysConverter = new KeysConverter();
        }
        public EditWatcherForm(Watcher watcher, WatcherKeyManager watcherKeyManager, WatcherMonitor watcherMonitor)
        {
            InitializeComponent();
            _watcher = watcher;
            _watcherKeyManager = watcherKeyManager;
            NameTextBox.Text = watcher.Name;
            ExtensionTextBox.Text = String.Join(", ", watcher.Whitelist);
            GenerateVideoThumbnailsBox.Checked = watcher.GenerateVideoThumbnails.Value;
            scanSubdirectoriesBox.Checked = watcher.ScanSubdirectories.Value;

            _watcherMonitor = watcherMonitor;

            _orignalName = watcher.Name;
            _orignalWhitelist = watcher.Whitelist;
            _originalScanBool = watcher.ScanSubdirectories.Value;
            _originalVideoBool = watcher.GenerateVideoThumbnails.Value;
            _orignalKey = watcher.ShortcutKeys;
            _orignalGlobalKeyBool = watcher.GlobalShortcut; ;

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

        private bool MaybeUpdateKey()
        {
            if ((enabledBox.Checked == false || hotkeyTextBox.Text.Length == 0) && _orignalKey != Keys.None)
            {
                using (var ctx = new FilesContext())
                {
                    ctx.UpdateShortcuts(_watcher, Keys.None, globalBox.Checked);
                }
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

                    if (_orignalGlobalKeyBool != globalBox.Checked || (Keys)keyObject != _orignalKey)
                    {
                        using (var ctx = new FilesContext())
                        {
                            ctx.UpdateShortcuts(_watcher, (Keys)keyObject, globalBox.Checked);
                        }
                    }

                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Invalid key", "Could not set this key.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            _watcherKeyManager.UpdateDataAndEnable();
            return true;
        }
        private void okButton_Click(object sender, EventArgs e)
        {
            if (!MaybeUpdateKey())
            {
                return;
            }
            MaybeUpdateWatcher();
            Close();

        }

        private void MaybeUpdateWatcher()
        {
            bool whitelistChanged = !_orignalWhitelist.SetEquals(Watcher.ExtensionStringToHash(ExtensionTextBox.Text));
            if (_orignalName != NameTextBox.Text ||
                whitelistChanged ||
                _originalScanBool != scanSubdirectoriesBox.Checked ||
                _originalVideoBool != GenerateVideoThumbnailsBox.Checked)
            {
                using(var ctx = new FilesContext())
                {
                    ctx.UpdateWatcherForm(_watcher.Id, NameTextBox.Text, ExtensionTextBox.Text, scanSubdirectoriesBox.Checked, scanSubdirectoriesBox.Checked);
                }
                if (_originalScanBool != scanSubdirectoriesBox.Checked || whitelistChanged)
                {
                    Console.WriteLine("restart");
                    _watcherMonitor.RestartById(_watcher.Id);
                }
            }
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

        private void EditWatcherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _watcherKeyManager.EnableKeys();
        }

        private void ImagesButton_Click(object sender, EventArgs e)
        {
            ExtensionTextBox.AppendText(GetTextToAppend(Helpers.ImageFileExtensions));
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

        private void VideoButton_Click(object sender, EventArgs e)
        {
            ExtensionTextBox.AppendText(GetTextToAppend(Helpers.VideoFileExtensions));
        }

        private void AudioButton_Click(object sender, EventArgs e)
        {
            ExtensionTextBox.AppendText(GetTextToAppend(Helpers.AudioFileExtensions));
        }
    }
}
