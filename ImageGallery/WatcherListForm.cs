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
    public partial class WatcherListForm : Form
    {
        WatcherMonitor Monitor { get; set; }
        WatcherKeyManager _watcherKeyManager;
        readonly static KeysConverter _keysConverter;
        public WatcherListForm(WatcherMonitor monitor, WatcherKeyManager watcherKeyManager)
        {
            InitializeComponent();
            Monitor = monitor;
            _watcherKeyManager = watcherKeyManager;
            RefreshListView();
        }

        static WatcherListForm()
        {
            _keysConverter = new KeysConverter();
        }
        private void RefreshListView()
        {
            watcherListView.Items.Clear();
            List<ListViewItem> watcherList = new List<ListViewItem>();
            using (var ctx = new FilesContext())
            {
                foreach (var watcher in ctx.Watchers)
                {
                    var item = new ListViewItem(new[] {
                        watcher.Name,
                        watcher.Enabled.ToString(),
                        watcher.Directory,
                        Watcher.HashToExtensionString(watcher.Whitelist),
                        watcher.ScanSubdirectories.ToString(),
                        watcher.GenerateVideoThumbnails.ToString(),
                        watcher.ShortcutKeys == Keys.None ? "None" : _keysConverter.ConvertToString(watcher.ShortcutKeys),
                        watcher.GlobalShortcut.ToString()
                    }) ;
                    item.Tag = watcher.Id;
                    watcherList.Add(item);
                }
            }
            watcherListView.Items.AddRange(watcherList.ToArray());

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var addWatcherForm = new AddWatcherForm();
            addWatcherForm.ShowDialog();
            Watcher AddedWatcher = addWatcherForm.AddedWatcher;
            if (AddedWatcher != null)
            {
                Monitor.AddWatcher(AddedWatcher);
            }
            RefreshListView();
        }

        private void WatcherListForm_Load(object sender, EventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (watcherListView.SelectedItems.Count == 0)
            {
                return;
            }
            var item = watcherListView.SelectedItems[0];
            var id = (int)item.Tag;
            using (var ctx = new FilesContext())
            {
                Watcher found = ctx.Watchers.SingleOrDefault(watcher => watcher.Id == id);
                if (found == null)
                {
                    Console.Write($"Could not find watcher with id {id}.");
                    return;
                }
                var confirmResult = MessageBox.Show(
                    $"Are you sure you want to delete this watcher? This will delete all custom tags and command for files in:\n{found.Directory}",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (confirmResult != DialogResult.Yes)
                {
                    return;
                }
                ctx.Watchers.Remove(found);
                ctx.SaveChanges();
                Monitor.Stop(found);
            }
            RefreshListView();
        }

        private void watcherListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {

        }

        private void watcherListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LastErrorButton_Click(object sender, EventArgs e)
        {
            if (watcherListView.SelectedItems.Count == 0)
            {
                return;
            }
            var item = watcherListView.SelectedItems[0];
            var id = (int)item.Tag;
            string dialogueText;
            try {
                var lastError = Monitor.GetLastError(id);
                if (lastError == "")
                {
                    dialogueText = "No errors have occurred for this watcher.";
                } else
                {
                    dialogueText = $"Last exception details:\n{lastError}";
                }
                
            } catch (KeyNotFoundException)
            {
                dialogueText = "Error: Watcher not found.";
            }
            MessageBox.Show(dialogueText, "Last Error");
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (watcherListView.SelectedItems.Count == 0)
            {
                return;
            }
            var item = watcherListView.SelectedItems[0];
            var id = (int)item.Tag;
            Watcher found = null;
            using (var ctx = new FilesContext())
            {
                found = ctx.Watchers.SingleOrDefault(watcher => watcher.Id == id);

            }
            if (found == null)
            {
                Console.Write($"Could not find watcher with id {id}.");
                return;
            }
            var editWatcherForm = new EditWatcherForm(found, _watcherKeyManager);
            editWatcherForm.ShowDialog();
            RefreshListView();
        }
    }
}
