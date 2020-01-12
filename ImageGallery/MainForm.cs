using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Manina.Windows.Forms;
using Manina.Windows.Forms.ImageListViewRenderers;
using Microsoft.EntityFrameworkCore;
using Cyotek.Windows.Forms;

namespace ImageGallery
{
    using Database;
    using Database.Models;
    public partial class MainForm : Form
    {
        static FileModelAdapter Adaptor;

        PopoutPreview popoutPreview;
        Image previewImage;
        Database.WatcherMonitor Monitor { get; set; }
        private const int InitSearchResultSize = 50;
        private int SearchResultSize = InitSearchResultSize;
        private const int InitDefaultSearchSize = 10;
        private int DefaultSearchSize = InitDefaultSearchSize;
        CancellationTokenSource previewCts = new CancellationTokenSource();
        private bool ShowAllReset;
        private bool ShowAllDefaultView;
        private ToolStripMenuItem ManageWatchersButton;

        static MainForm()
        {
            Adaptor = new FileModelAdapter();
        }
        public MainForm(Database.WatcherMonitor monitor)
        {
            // Init Form
            InitializeComponent();
            WatcherDropDown.DropDown.Closing += WatcherDropDown_Closing;

            // Fill fields
            Monitor = monitor;
            ManageWatchersButton = new ToolStripMenuItem("Manage Watchers", null, ManageWatchersButton_onClick);

            // Initialize preview
            popoutPreview = new PopoutPreview();
            popoutPreview.FormClosing += new FormClosingEventHandler(popoutPreview_Closing);

            // Initialize Watchers
            RefreshWatchers();



            // Load configuration
            LoadConfiguration();


            ilvThumbs.SetRenderer(new XPRenderer());
            ilvThumbs.Columns.Add(ColumnType.Name);

            // Set up grouper.
            var ResultTypeCol = new ImageListView.ImageListViewColumnHeader(ColumnType.Custom, "Type", "Result Type");
            ResultTypeCol.Comparer = new ResultTypeComparer();
            ilvThumbs.Columns.Add(ResultTypeCol);
            var grouper = new ImageGrouper();
            ilvThumbs.Columns[1].Grouper = grouper;
            ilvThumbs.GroupColumn = 1;
            ilvThumbs.GroupOrder = Manina.Windows.Forms.SortOrder.Ascending;
            ilvThumbs.SortColumn = 1;
            ilvThumbs.SortOrder = Manina.Windows.Forms.SortOrder.Descending;

            RefreshView();

        }

        private void WatcherDropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {        
                e.Cancel = true;
            }
        }

        private static bool IsWatcherToolStripButton(Object o)
        {
            return o is ToolStripMenuItem && ((ToolStripMenuItem)o).Tag is Watcher;
        }

        private HashSet<int> GetActiveWatcherIds()
        {
            return (from Object item in WatcherDropDown.DropDownItems
                   where IsWatcherToolStripButton(item) && ((ToolStripMenuItem)item).Checked
                   select GetWatcherIdFromToolStripObject(item)).ToHashSet();
        }

        private void RefreshWatchers()
        {
            HashSet<int> oldIds = new HashSet<int>();
            foreach (var o in WatcherDropDown.DropDownItems)
            {
                if (!IsWatcherToolStripButton(o))
                {
                    continue;
                }
                var item = (ToolStripMenuItem)o;
                if (item.Checked)
                {
                    var watcher = (Watcher)item.Tag;
                    oldIds.Add(watcher.Id);
                }
            }
            WatcherDropDown.DropDownItems.Clear();
            var watchers = new List<Watcher>();
            using (var ctx = new FilesContext())
            {
                watchers.AddRange(ctx.Watchers);
            }
            var newItems = new List<ToolStripMenuItem>();
            foreach (var watcher in watchers)
            {
                var newItem = new ToolStripMenuItem(watcher.Name);
                newItem.Tag = watcher;
                newItem.CheckOnClick = true;
                if (oldIds.Contains(watcher.Id))
                {
                    newItem.Checked = true;
                }
                newItem.Click += WatcherItem_Click;
                newItems.Add(newItem);
            }
            WatcherDropDown.DropDownItems.AddRange(newItems.ToArray());

            WatcherDropDown.DropDownItems.Add(new ToolStripSeparator());
            WatcherDropDown.DropDownItems.Add(ManageWatchersButton);
        }

        private void ManageWatchersButton_onClick(object sender, EventArgs e)
        {
            WatcherDropDown.DropDown.Close();
            var watcherListForm = new WatcherListForm(Monitor);
            watcherListForm.ShowDialog();
            RefreshWatchers();
            RefreshView();
        }

        private void WatcherItem_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            ilvThumbs.Items.Clear();
            if (!searchTextBox.Text.Any())
            {
                if (ShowAllButton.Checked)
                {
                    ShowAllDefault();
                    return;
                }
                ShowDefault();
                return;
            }
            DoSearch();

        }

        private void DoSearch()
        {
            var watcherIds = GetActiveWatcherIds();
            var searchString = FilesContext.MakeQuery(searchTextBox.Text);
            var result = new List<File>();
            using (var ctx = new FilesContext())
            {
                result.AddRange(ctx.Search(searchString, watcherIds).Take(SearchResultSize));
            }
            var items = from file in result
                        select MakeListViewItem(file, "Search Result");
            ilvThumbs.Items.AddRange(items.ToArray(), Adaptor);
            var firstItem = ilvThumbs.Items.FirstOrDefault();
            if (firstItem != null)
            {
                firstItem.Selected = true;
            }
        }


        private static ImageListViewItem MakeListViewItem(File model, string heading)
        {
            ImageListViewItem item = new ImageListViewItem(model, model.Name);
            item.SubItems.Add("Type", heading);
            return item;
        }

        private void ShowAllDefault()
        {
            var activeWatchers = GetActiveWatcherIds();
            var items = new List<ImageListViewItem>();

            using (var ctx = new FilesContext())
            {
                var models = (from file in ctx.Files.AsNoTracking()
                              where activeWatchers.Contains(file.WatcherId)
                              orderby file.FileModifiedTime descending
                              select MakeListViewItem(file, "All Items"));
                items.AddRange(models);
            }
            ilvThumbs.Items.AddRange(items.ToArray(), Adaptor);
        }
        private void ShowDefault()
        {
            var items = new List<ImageListViewItem>();
            var activeWatchers = GetActiveWatcherIds();
            Console.WriteLine(activeWatchers.Count);
            using (var ctx = new FilesContext())
            {

                // Recently Created
                var modifiedModels = (from file in ctx.Files.AsNoTracking()
                                      where activeWatchers.Contains(file.WatcherId)
                                      orderby file.FileModifiedTime descending
                                      select MakeListViewItem(file, "Recently Created"));
                var LastUsemodels = from file in ctx.Files.AsNoTracking()
                                    where activeWatchers.Contains(file.WatcherId)
                                    orderby file.LastUseTime descending
                                    select MakeListViewItem(file, "Recently Used");
                var frequentModels = from file in ctx.Files.AsNoTracking()
                                     where activeWatchers.Contains(file.WatcherId)
                                     orderby file.TimesAccessed descending
                                     select MakeListViewItem(file, "Frequently Used");
                items.AddRange(modifiedModels.Take(DefaultSearchSize));
                items.AddRange(LastUsemodels.Take(DefaultSearchSize));
                items.AddRange(frequentModels.Take(DefaultSearchSize));
            }
            ilvThumbs.Items.AddRange(items.ToArray(), Adaptor);
            ilvThumbs.Sort();
        }


        public class ResultTypeComparer : IComparer<ImageListViewItem>
        {
            private static int CompareTimes(DateTime? t1, DateTime? t2)
            {
                if (!t1.HasValue)
                {
                    return -1;
                }
                if (!t2.HasValue)
                {
                    return 1;
                }
                return DateTime.Compare(t1.Value, t2.Value);
            }
            public int Compare(ImageListViewItem x, ImageListViewItem y)
            {
                //Console.WriteLine("Mehot used");
                var xType = x.SubItems["Type"].Text;
                var yType = y.SubItems["Type"].Text;
                if (xType != yType)
                {
                    return ImageGrouper.TypeDict[xType].CompareTo(ImageGrouper.TypeDict[yType]);
                    //return 0;
                }
                File xFile = (File)x.VirtualItemKey;
                File yFile = (File)y.VirtualItemKey;

                if (xType == "Recently Created")
                {
                    return CompareTimes(xFile.FileModifiedTime, yFile.FileModifiedTime);
                }
                else if (xType == "Recently Used")
                {

                    return CompareTimes(xFile.LastUseTime, yFile.LastUseTime);
                }
                else if (xType == "Frequently Used")
                {

                    return xFile.TimesAccessed.CompareTo(yFile.TimesAccessed);
                } else
                {
                    throw new NotImplementedException("Sort order.");
                }
                //return 0;
            }
        }

        void popoutPreview_Closing(object sender, FormClosingEventArgs e)
        {
            PopoutPreviewButton.Checked = false;

            splitContainer1.Panel2Collapsed = false;
            RefreshPreviews();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //GC.Collect();
            //ilvThumbs.View = Manina.Windows.Forms.View.Details;
            foreach(var f in ilvThumbs.SelectedItems)
            {
                //Console.WriteLine(f.Text);
            }
            ShowPopoutPreview();
        }

        private void ilvThumbs_SelectionChanged(object sender, EventArgs e)
        {
            var numSelected = ilvThumbs.SelectedItems.Count;
            if (numSelected != 1)
            {
                this.NameLabel.Text = "No file selected.";
                this.NameToolTip.Active = false;
                return;
            }
            Image thumbnail = ilvThumbs.SelectedItems[0].ThumbnailImage;
            File file = (File)ilvThumbs.SelectedItems[0].VirtualItemKey;
            this.NameLabel.Text = file.Name;
            this.NameToolTip.Active = true;
            this.NameToolTip.SetToolTip(this.NameLabel, file.Name);
            previewCts.Cancel();
            previewCts = new CancellationTokenSource();
            SetPreviewImage(thumbnail, file, previewCts.Token);

        }

        private Task SetPreviewImage(Image thumbnail, File file, CancellationToken token)
        {
            return Task.Run(() =>
            {
                previewImage = thumbnail;
                this.Invoke((MethodInvoker)delegate
                {
                    RefreshPreviews();
                    LoadingLabel.Visible = true;
                });
                if (token.IsCancellationRequested)
                {
                    return;
                }
                Image image;
                try
                {
                    image = Helpers.LoadImage(new FileInfo(file.FullName));
                } catch (ArgumentException)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        LoadingLabel.Visible = false;
                    });
                    return;
                }
                if (image == null)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        LoadingLabel.Visible = false;
                    });
                    return;
                }
                if (token.IsCancellationRequested)
                {
                    return;
                }
                previewImage = image;
                this.Invoke((MethodInvoker)delegate
                {
                    RefreshPreviews();
                    LoadingLabel.Visible = false;
                });
            }, token);
        }

        private void ilvThumbs_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            File file = (File)e.Item.VirtualItemKey;
            CopyToClipboard(new List<File> { file });
            using (var ctx = new FilesContext())
            {
                ctx.RecordUse(file);
            }
            RefreshView();
        }

        private void CopyToClipboard(List<File> files)
        {
            if (files.Count == 0)
            {
                return;
            }
            if (files.Count == 1)
            {
                File file = files[0];
                CopyToClipboard(file);
            }
            // TODO: multiple files selected
        }
        private void CopyToClipboard(File file)
        {
            FileInfo fileInfo = new FileInfo(file.FullName);
            if (Helpers.IsImageFile(fileInfo.Name))
            {
                Image image;
                try
                {
                    image = Helpers.LoadImage(fileInfo);
                } catch (ArgumentException)
                {
                    return;
                }
                if (image == null)
                {
                    return;
                }
                Clipboard.SetImage(image);
                infoLabel.Text = String.Format(
                    "Copied image {0} to clipboard.",
                    Helpers.Truncate(fileInfo.Name, 30));
            }
            else
            {
                if (fileInfo.Exists)
                {
                    StringCollection sc = new StringCollection();
                    sc.Add(file.FullName);
                    Clipboard.SetFileDropList(sc);
                    infoLabel.Text = String.Format(
                        "Copied file {0} to clipboard.",
                        Helpers.Truncate(fileInfo.Name, 30));
                }
            }
        }

        private void RefreshPreviews()
        {
            if (previewImage == null)
            {
                return;
            }
            if (previewImage.Width > previewBox.Width || previewImage.Height > previewBox.Height)
            {
                previewBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                previewBox.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            if (!splitContainer1.Panel2Collapsed)
            {

                previewBox.Image = previewImage;
            }
            if (popoutPreview.Visible)
            {
                popoutPreview.PreviewImage = previewImage;
            }

        }

        private void ShowPopoutPreview()
        {
            splitContainer1.Panel2Collapsed = true;
            if (previewImage != null)
            {
                popoutPreview.PreviewImage = previewImage;
            }
            popoutPreview.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //GC.Collect();
            //ilvThumbs.View = Manina.Windows.Forms.View.Details;
            foreach (var f in ilvThumbs.SelectedItems)
            {
                //Console.WriteLine(f.Text);
            }
            ShowPopoutPreview();
        }

        private void usedOrder_Click(object sender, EventArgs e)
        {

        }

        private void watchersButton_Click_1(object sender, EventArgs e)
        {
            var watcherListForm = new WatcherListForm(Monitor);
            watcherListForm.ShowDialog();
            RefreshWatchers();
            RefreshView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void searchTextBox_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //Console.WriteLine(e.KeyCode.ToString());
            if (e.KeyCode == Keys.F5)
            {
                RefreshView();
                e.Handled = true;
            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.E )
            {
                ShowAllButton.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var searchString = FilesContext.MakeQuery(searchTextBox.Text);
            using(var ctx = new FilesContext())
            {
                var result = ctx.Search(searchString, GetActiveWatcherIds());
                foreach (var f in result)
                {
                    Console.WriteLine(f.Name);
                }
            }
        }


        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ShowAllButton.Checked)
            {
                ShowAllReset = true;
                ShowAllButton.Checked = false;
                // This will already reset the view.
                return;
            }
            RefreshView();
        }

        private void ShowAllButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowAllButton.Checked)
            {
                SearchResultSize = Int32.MaxValue;
                ShowAllDefaultView = true;
            }
            else
            {
                SearchResultSize = InitSearchResultSize;
                ShowAllDefaultView = false;
            }

            RefreshView();
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode.ToString());
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.ControlKey)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfiguration();
        }

        private void LoadConfiguration()
        {
            var settings = Properties.Settings.Default;
            Width = settings.MainWidth;
            Height = settings.MainHeight;
            if (Properties.Settings.Default.MainPosSet)
            {

                Console.WriteLine($"{Left}, {Top}");

                StartPosition = FormStartPosition.Manual;
                Location = new Point(settings.MainX, settings.MainY);
                Console.WriteLine($"{new Point(settings.MainX, settings.MainY)}");
                Console.WriteLine($"{Location}");
                Refresh();


            }
            splitContainer1.SplitterDistance = settings.SplitterDistance;
            if (settings.PopoutShow)
            {
                Console.WriteLine("Set starting location");

                // Set the location before pressing button.
                popoutPreview.StartPosition = FormStartPosition.Manual;
                popoutPreview.Location = new Point(settings.PopoutX, settings.PopoutY);
                PopoutPreviewButton.Checked = true;
            } else
            {
                popoutPreview.Left = Left + Width;
                popoutPreview.Top = Top;

                popoutPreview.Width = previewBox.Width;
                popoutPreview.Height = Height;
            }
            SetActiveIdsFromConfigString(settings.ActiveWatchers);
        }
        private void SaveConfiguration()
        {
            var settings = Properties.Settings.Default;
            settings.MainWidth = Width;
            settings.MainHeight = Height;
            settings.MainX = Left;
            settings.MainY = Top;
            settings.MainPosSet = true;
            settings.SplitterDistance = splitContainer1.SplitterDistance;
            settings.PopoutShow = PopoutPreviewButton.Checked;
            if (PopoutPreviewButton.Checked)
            {
                settings.PopoutX = popoutPreview.Left;
                settings.PopoutY = popoutPreview.Top;
            }
            settings.ActiveWatchers = GetActiveIdsConfigString();
            settings.Save();
        }

        private void PopoutPreviewButton_CheckedChanged(object sender, EventArgs e)
        {
            if (PopoutPreviewButton.Checked)
            {
                ShowPopoutPreview();
            } else
            {
                popoutPreview.Close();
            }
        }

        private string GetActiveIdsConfigString()
        {
            new StringCollection();
            return String.Join(",", GetActiveWatcherIds());
        }

        private void SetActiveIdsFromConfigString(string idsConfigString)
        {
            Console.WriteLine($"{idsConfigString.Split(',').First()}");
            HashSet<int> ids = idsConfigString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToHashSet();
            foreach (var item in WatcherDropDown.DropDownItems)
            {
                if (IsWatcherToolStripButton(item) && ids.Contains(GetWatcherIdFromToolStripObject(item)))
                {
                    ((ToolStripMenuItem)item).Checked = true;
                }
            }
        }

        private static int GetWatcherIdFromToolStripObject(Object o)
        {
            return ((Watcher)(((ToolStripMenuItem)o).Tag)).Id;
        }
    }
    


}
