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
        public WatcherListForm()
        {
            InitializeComponent();
            RefreshListView();
        }
        private void RefreshListView()
        {
            watcherListView.Items.Clear();
            List<ListViewItem> watcherList = new List<ListViewItem>();
            using (var ctx = new FilesContext())
            {
                watcherList
                    .AddRange(from watcher in ctx.Watchers
                              select new ListViewItem(new[] { watcher.Name, watcher.Enabled.ToString(), watcher.Directory, watcher.Whitelist }));
            }
            watcherListView.Items.AddRange(watcherList.ToArray());

        }

        private void watcherListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var addWatcherForm = new AddWatcherForm();
            addWatcherForm.ShowDialog();
            RefreshListView();
        }

        private void WatcherListForm_Load(object sender, EventArgs e)
        {

        }
    }
}
