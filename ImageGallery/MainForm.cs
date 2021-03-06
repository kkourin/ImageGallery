﻿using Manina.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using NHotkey.WindowsForms;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace ImageGallery
{
    using Database;
    using Database.Models;
    using ImageGallery.XMPLib;
    using LibVLCSharp.Shared;
    using NHotkey;
    using System.Configuration;
    using static ImageGallery.Database.FilesContext;

    public partial class MainForm : Form
    {
        static FileModelAdapter Adaptor;

        PopoutPreview popoutPreview;
        Image previewImage;
        private HashSet<int> VisibleIds = new HashSet<int>();
        Database.WatcherMonitor Monitor { get; set; }
        public int refreshRequested = 0;
        public System.Timers.Timer RefreshTimer { get; private set; }
        public ImageListView.ImageListViewColumnHeader _relPathCol { get; private set; }
#if DEBUG
        private const string OpenHotKeyName = "a5168172-901e-4951-871a-1ea72e8728e7_ImageGalleryOpenDEBUG";
#else
         private const string OpenHotKeyName = "a5168172-901e-4951-871a-1ea72e8728e7_ImageGalleryOpen";
#endif

        delegate void setInfoLabelSafeDelegate(string text);
        private const int RefreshGracePeriod = 3000;

        private const int InitSearchResultSize = 50;
        private int SearchResultSizeLimit = InitSearchResultSize;
        CancellationTokenSource previewCts = new CancellationTokenSource();
        CancellationTokenSource searchCts = new CancellationTokenSource();
        private ToolStripMenuItem ManageWatchersButton;
        private const long maxPreviewSize = 15*1024*1024;
        private const long maxClipboardSize = 30*1024*1024;
        private bool _displayingVideo;

        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        private SearchTagEditor _searchTagEditor;

        private WatcherKeyManager _watcherKeyManager;

        static MainForm()
        {
            Adaptor = new FileModelAdapter();
        }
        public MainForm(Database.WatcherMonitor monitor, LibVLC libVLC, SearchTagEditor searchTagEditor)
        {
            // Init Form
            InitializeComponent();

            _libVLC = libVLC;
            _mediaPlayer = new MediaPlayer(libVLC);
            _mediaPlayer.EnableHardwareDecoding = true;
            mainVideoView.LibVLC = _libVLC;

            _searchTagEditor = searchTagEditor;

            WatcherDropDown.DropDown.Closing += WatcherDropDown_Closing;
            Helpers.DisableFormTransition(Handle);

            // Start refresh timer
            RefreshTimer = new System.Timers.Timer() { AutoReset = false, Interval =  RefreshGracePeriod};
            RefreshTimer.Elapsed += RefreshTimer_Elapsed;

            _watcherKeyManager = new WatcherKeyManager(this);

            // Fill fields
            Monitor = monitor;
            ManageWatchersButton = new ToolStripMenuItem("Manage Watchers", null, ManageWatchersButton_onClick);

            // Initialize preview
            popoutPreview = new PopoutPreview(_libVLC);
            popoutPreview.FormClosing += new FormClosingEventHandler(popoutPreview_Closing);

            // Initialize Watchers
            RefreshWatchers();

            // Load configuration
            LoadConfiguration();
            

            ilvThumbs.SetRenderer(new GalleryRenderer());
            ilvThumbs.Columns.Add(ColumnType.Name);

            // Set up groupers.
            var ResultTypeCol = new ImageListView.ImageListViewColumnHeader(ColumnType.Custom, "Type", "Result Type");
            ResultTypeCol.Comparer = new ResultTypeComparer();
            ilvThumbs.Columns.Add(ResultTypeCol);
            var grouper = new ImageGrouper();
            ilvThumbs.Columns[1].Grouper = grouper;

            _relPathCol = new ImageListView.ImageListViewColumnHeader(ColumnType.Custom, "Relative Directory", "Relative Directory");
            ilvThumbs.Columns.Add(_relPathCol);

            ilvThumbs.GroupHeaderFont = new Font("Arial", 11);

            if (searchTextBox.Enabled && searchTextBox.Visible)
            {
                ActiveControl = searchTextBox.Control;
            }

            RefreshView();

            // Add sync event handler
            FSWatcher.SyncOccurred += FSWatcher_SyncOccurred;
        }


        private void FSWatcher_SyncOccurred(object sender, FSWatcher.SyncOccurredEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                OnSync(e);
            });
        }

        private void OpenGalleryHotkey_Press(object sender, HotkeyEventArgs e)
        {
            Restore();
            e.Handled = true;
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
            var watcherListForm = new WatcherListForm(Monitor, _watcherKeyManager);
            watcherListForm.ShowDialog();
            RefreshWatchers();
            RefreshView();
        }

        private void WatcherItem_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private HashSet<(string fullName, int watcherId)> GetSelectedFiles()
        {
            var selectedFiles = new HashSet<(string fullName, int watcherId)>();
            foreach (var item in ilvThumbs.Items)
            {
                if (item.Selected)
                {
                    var file = item.VirtualItemKey as File;
                    selectedFiles.Add((file.FullName, file.WatcherId));
                }
            }
            return selectedFiles;
        }

        private void SelectFiles(HashSet<(string fullName, int watcherId)> files)
        {
            foreach (var item in ilvThumbs.Items)
            {
                var file = item.VirtualItemKey as File;
                if (files.Contains((file.FullName, file.WatcherId)))
                {
                    item.Selected = true;
                }
            }
        }

        private int GetFirstSelectedIndex()
        {
            for (int i = 0; i < ilvThumbs.Items.Count; i++) { 
                if (ilvThumbs.Items[i].Selected)
                {
                    return i;
                }
            }
            return -1;
        }

        private void RefreshViewAndReselect()
        {
            var prevSelectedFiles = GetSelectedFiles();
            RefreshView();
            SelectFiles(prevSelectedFiles);
            int firstSelectedIndex = GetFirstSelectedIndex();
            if (firstSelectedIndex != -1)
            {
                ilvThumbs.EnsureVisible(firstSelectedIndex);
            }
        }
        private void RefreshView()
        {
            ilvThumbs.Items.Clear();
            if (!searchTextBox.Text.Any())
            {
                if (ShowAllButton.Checked)
                {
                    ShowAllDefault();
                } else {
                    ShowDefault();
                }
                setInfoLabelSafe("Ready.");
            } else
            {
                DoSearch();
            }

        }

        private void SelectFirstIfExists()
        {
            var firstItem = ilvThumbs.Items.FirstOrDefault();
            if (firstItem != null)
            {
                firstItem.Selected = true;
            }
        }
        private void DoSearch()
        {


            searchCts.Cancel();
            searchCts = new CancellationTokenSource();
            DisplaySearchTask(searchCts.Token);
        }

        private void UncheckOtherSortOrders(ToolStripMenuItem selectedMenuItem)
        {
            foreach (var ltoolStripMenuItem in (from object
                                                    item in sortButton.DropDownItems
                                                let ltoolStripMenuItem = item as ToolStripMenuItem
                                                where ltoolStripMenuItem != null
                                                where !item.Equals(selectedMenuItem)
                                                select ltoolStripMenuItem))
                (ltoolStripMenuItem).Checked = false;
        }

        private FilesContext.SortColumn GetSortColumn()
        {
            foreach (var t in sortButton.DropDownItems)
            {
                var item = t as ToolStripMenuItem;
                if (item.Checked)
                {
                    var orderString = (String)item.Tag;
                    return (FilesContext.SortColumn) Enum.Parse(typeof(FilesContext.SortColumn), orderString);
                }
            }
            return FilesContext.SortColumn.DateChanged;
        }

        private void SetSortColumn(FilesContext.SortColumn sort)
        {
            foreach (var t in sortButton.DropDownItems)
            {
                var item = t as ToolStripMenuItem;
                var itemSort = (FilesContext.SortColumn)Enum.Parse(typeof(FilesContext.SortColumn), (String)item.Tag);
                if (itemSort == sort)
                {
                    item.Checked = true;
                    break;
                }
            }
        }

        private Task DisplaySearchTask(CancellationToken token)
        {
            return Task.Run(() =>
            {
                var watcherIds = GetActiveWatcherIds();
                var searchString = FilesContext.MakeQuery(searchTextBox.Text);
                var result = new List<File>();
                List<ImageListViewItem> searchResult;
                if (token.IsCancellationRequested)
                {
                    return;
                }
                using (var ctx = new FilesContext())
                {
                    //result.AddRange(ctx.Search(searchString, watcherIds, GetSortColumn()).Take(SearchResultSize));
                    try
                    {
                        searchResult = ctx.Search(searchString, watcherIds, GetSortColumn())
                            .Take(SearchResultSizeLimit + 1)
                            .Select(file => MakeListViewItem(file, file.Watcher, "Search Result"))
                            .ToList();
                    } catch (Microsoft.Data.Sqlite.SqliteException)
                    {
                        setInfoLabelSafe("Error: Invalid query syntax (try a different search).");
                        searchResult = new List<ImageListViewItem>();
                    }

                }
                if (searchResult.Count > SearchResultSizeLimit)
                {
                    searchResult.RemoveAt(searchResult.Count - 1);
                    setInfoLabelSafe($"{searchResult.Count} results displayed. Search limit exceeded. Press Show All to show all results.");
                } else
                {
                    setInfoLabelSafe($"{searchResult.Count} results displayed. ");
                }
                if (token.IsCancellationRequested)
                {
                    return;
                }
                this.Invoke((MethodInvoker)delegate
                {
                    ilvThumbs.Items.Clear();
                    if (groupFoldersButton.Checked)
                    {
                        AddFolderGroupedItemsAndSort(searchResult);
                    } else
                    {
                        AddTypeGroupedItems(searchResult);
                    }
                    SelectFirstIfExists();
                });
            }, token);
        }

        private void AddFolderGroupedItemsAndSort(List<ImageListViewItem> items)
        {
            var grouper = new FolderImageGrouper(items);
            var sorter = new RelativePathComparer(GetSortColumn(), grouper.IdToPath);
            _relPathCol.Comparer = sorter;
            ilvThumbs.Columns[2].Grouper = grouper;

            ilvThumbs.GroupColumn = 2;
            ilvThumbs.GroupOrder = Manina.Windows.Forms.SortOrder.Ascending;

            ilvThumbs.SortColumn = 2;
            ilvThumbs.SortOrder = Manina.Windows.Forms.SortOrder.Ascending;
            ilvThumbs.Items.AddRange(items.ToArray(), Adaptor);
            ilvThumbs.Sort();
        }

        // Unforunately, ImageListView does not have a stable sort, so sometimes
        // we need to just not sort to preserve the orignal order.
        private void AddTypeGroupedItemsAndSort(List<ImageListViewItem> items)
        {
            AddTypeGroupedItems(items);
            ilvThumbs.Sort();
        }

        private void AddTypeGroupedItems(List<ImageListViewItem> items)
        {
            ilvThumbs.GroupColumn = 1;
            ilvThumbs.GroupOrder = Manina.Windows.Forms.SortOrder.Ascending;
            ilvThumbs.SortColumn = 1;
            ilvThumbs.SortOrder = Manina.Windows.Forms.SortOrder.Ascending;
            ilvThumbs.Items.AddRange(items.ToArray(), Adaptor);
        }

        private static ImageListViewItem MakeListViewItem(File model, Watcher watcher, string heading)
        {
            ImageListViewItem item = new ImageListViewItem(model, model.Name);
            item.Tag = watcher.Directory;
            string relativeDirectory;
            if (DBHelpers.IsInDir(new DirectoryInfo(model.Directory), watcher.Directory))
            {
                relativeDirectory = DBHelpers.GetRelativePath(model.Directory, watcher.Directory);
            } else
            {
                relativeDirectory = "Unknown";
            }
            item.SubItems.Add("Type", heading);
            item.SubItems.Add("Relative Directory", $"{watcher.Name} \\\\ {relativeDirectory}");
            return item;
        }

        private void ShowAllDefault()
        {
            var activeWatchers = GetActiveWatcherIds();
            var items = new List<ImageListViewItem>();

            using (var ctx = new FilesContext())
            {
                var models = FilesContext.OrderBySort(from file in ctx.Files.AsNoTracking()
                                                      where activeWatchers.Contains(file.WatcherId)
                                                      select file, GetSortColumn());

                items.AddRange(models.Select(file => MakeListViewItem(file, file.Watcher, "All Items")));
            }
            VisibleIds.Clear();
            VisibleIds = items.Select((t) => (t.VirtualItemKey as File).Id).ToHashSet();

            ilvThumbs.Items.Clear();
            if (groupFoldersButton.Checked)
            {
                AddFolderGroupedItemsAndSort(items);
            }
            else
            {
                AddTypeGroupedItems(items);
            }
        }
        private void ShowDefault()
        {
            var items = new List<ImageListViewItem>();
            var activeWatchers = GetActiveWatcherIds();
            using (var ctx = new FilesContext())
            {
                var settings = Properties.Settings.Default;
                // Recently Created
                if (settings.ShowRecentlyCreated)
                {
                    var modifiedModels = (from file in ctx.Files.AsNoTracking()
                                          where activeWatchers.Contains(file.WatcherId)
                                          orderby file.LastChangeTime descending
                                          select MakeListViewItem(file, file.Watcher, "Recently Created"));
                    items.AddRange(modifiedModels.Take(settings.RecentlyCreatedCount));
                }
                if (settings.ShowRecentlyUsed)
                {
                    var LastUsemodels = from file in ctx.Files.AsNoTracking()
                                        where activeWatchers.Contains(file.WatcherId)
                                        orderby file.LastUseTime descending
                                        select MakeListViewItem(file, file.Watcher, "Recently Used");
                    items.AddRange(LastUsemodels.Take(settings.RecentlyUsedCount));
                }
                if (settings.ShowFrequentlyClicked)
                {
                    var frequentModels = from file in ctx.Files
                                         where activeWatchers.Contains(file.WatcherId)
                                         orderby file.TimesAccessed descending
                                         select MakeListViewItem(file, file.Watcher, "Frequently Used");
                    items.AddRange(frequentModels.Take(settings.FrequentlyClickedCount));
                }

            }
            AddTypeGroupedItemsAndSort(items);
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
                var xType = x.SubItems["Type"].Text;
                var yType = y.SubItems["Type"].Text;
                if (xType != yType)
                {
                    return ImageGrouper.TypeDict[xType].CompareTo(ImageGrouper.TypeDict[yType]);
                }
                File xFile = (File)x.VirtualItemKey;
                File yFile = (File)y.VirtualItemKey;

                if (xType == "Recently Created")
                {
                    return ComparerFromSort(SortColumn.DateChanged).Compare(xFile, yFile);
                }
                else if (xType == "Recently Used")
                {

                    return ComparerFromSort(SortColumn.DateAccessed).Compare(xFile, yFile);
                }
                else if (xType == "Frequently Used")
                {

                    return xFile.TimesAccessed.CompareTo(yFile.TimesAccessed);
                }
                else if (xType == "All Items" || xType == "Search Result")
                {
                    return 0;
                }
                else
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
            MaybeSetMediaWindow();
            RefreshPreview();
        }


        void SetInfoPanel(ImageListView.ImageListViewSelectedItemCollection items)
        {
            fileInfoPanel.Items = items;
            popoutPreview.FileInfoPanel.Items = items;
        }

        void RefreshInfoPanel()
        {
            SetInfoPanel(ilvThumbs.SelectedItems);
        }

        void SetInfoPanelLoading(bool loading)
        {
            fileInfoPanel.Loading = loading;
            popoutPreview.FileInfoPanel.Loading = loading;
        }

        private void StopVideos()
        {
            mainVideoView.stopVideoViewSafe();
            popoutPreview.stopMediaPlayer();
        }

        private void ilvThumbs_SelectionChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }


        private void RefreshPreview()
        {
            RefreshInfoPanel();
            var numSelected = ilvThumbs.SelectedItems.Count;

            if (numSelected != 1)
            {
                previewImage = null;
                if (_displayingVideo)
                {
                    StopVideos();
                }
                _displayingVideo = false;
                MaybeShowVideoPlayers();
                RefreshPreviewImage();
                return;
            }
            Image thumbnail = ilvThumbs.SelectedItems[0].ThumbnailImage;
            File file = (File)ilvThumbs.SelectedItems[0].VirtualItemKey;
            previewCts.Cancel();
            previewCts = new CancellationTokenSource();
            SetPreviewImage(thumbnail, file, previewCts.Token);
        }

       

        private Task SetPreviewImage(Image thumbnail, File file, CancellationToken token)
        {
            
            return Task.Run(() =>
            {
                if (Helpers.IsVideoFile(file.FullName) || Helpers.IsAudioFile(file.FullName))
                {
                    setLoadedVideo(thumbnail, file, token);
                    return;
                }
                setLoadedImage(thumbnail, file, token);
            }, token);
            
        }

        public void MaybeShowVideoPlayers()
        {
            if (!_displayingVideo)
            {
                popoutPreview.SetVideoPlayerVisible(false);
                mainVideoView.setVideoViewVisibleSafe(false);

                return;
            }
            if (!splitContainer1.Panel2Collapsed)
            {
                mainVideoView.setVideoViewVisibleSafe(true);
            }
            if (popoutPreview.Visible)
            {
                popoutPreview.SetVideoPlayerVisible(true);
            }

        }

        private void setLoadedVideo(Image thumbnail, File file, CancellationToken token)
        {
            
            if (thumbnail != null)
            {
                previewImage = thumbnail;
            }
            else
            {
                previewImage = (Image) FileModelAdapter.getThumbnailFromFile(file).Clone();
            }
            

            if (token.IsCancellationRequested)
            {
                return;
            }
            _displayingVideo = true;
            this.Invoke((MethodInvoker)delegate
            {
                RefreshPreviewImage();
                SetInfoPanelLoading(true);
                mainVideoView.setMediaFromFile(file);
                popoutPreview.SetMediaPlayerMedia(file);
            });
            MaybeShowVideoPlayers();
            if (token.IsCancellationRequested)
            {
                return;
            }
            this.Invoke((MethodInvoker)delegate
            {
                SetInfoPanelLoading(false);
            });
        }
        private void setLoadedImage(Image thumbnail, File file, CancellationToken token)
        {
            if (_displayingVideo)
            {
                StopVideos();
            }
            _displayingVideo = false;
            MaybeShowVideoPlayers();
            if (thumbnail != null)
            {
                previewImage = thumbnail;
            }
            else
            {
                previewImage = FileModelAdapter.getThumbnailFromFile(file);
            }
            if (token.IsCancellationRequested)
            {
                return;
            }
            this.Invoke((MethodInvoker)delegate
            {
                RefreshPreviewImage();
            });
            if (!Helpers.IsImageFile(file.Name))
            {
                return;
            }
            // Is image file. 
            this.Invoke((MethodInvoker)delegate
            {
                SetInfoPanelLoading(true);
            });
            if (token.IsCancellationRequested)
            {
                return;
            }

            Image image;
            try
            {
                image = Helpers.LoadImage(new FileInfo(file.FullName), maxPreviewSize);
            }
            catch (ArgumentException e)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.infoLabel.Text = $"Could not show preview. {e.Message}";
                    SetInfoPanelLoading(false);
                });
                return;
            }
            if (image == null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    SetInfoPanelLoading(false);
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
                RefreshPreviewImage();
                SetInfoPanelLoading(false);
            });
        }

        private void ilvThumbs_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            File file = (File)e.Item.VirtualItemKey;
            CopyToClipboard(new List<File> { file }, true);

        }

        private void CopyToClipboard(List<File> files, bool moveToTray)
        {

            if (files.Count == 0)
            {
                return;
            }
            else if (files.Count == 1)
            {
                File file = files.First();
                CopySingleToClipboard(file, moveToTray);
            }
            else if (files.Count > 1)
            {
                CopyMultipleToClipboard(files, moveToTray);
            }
            // TODO: multiple files selected
        }
        private void CopySingleToClipboard(File file, bool moveToTray)
        {
            FileInfo fileInfo = new FileInfo(file.FullName);
            if (Helpers.IsImageFile(fileInfo.Name))
            {
                Image image;
                try
                {
                    image = Helpers.LoadImage(fileInfo, maxClipboardSize);
                } catch (ArgumentException e)
                {
                    infoLabel.Text = $"Could not copy image. {e.Message}";
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
                StringCollection sc = new StringCollection();
                sc.Add(file.FullName);
                Clipboard.SetFileDropList(sc);
                infoLabel.Text = String.Format(
                    "Copied file {0} to clipboard.",
                    Helpers.Truncate(fileInfo.Name, 30));
            }
            using (var ctx = new FilesContext())
            {
                ctx.RecordUse(file);
            }
            if (moveToTray)
            {
                MoveToTray();
            }
        }

        private void CopyMultipleToClipboard(List<File> files, bool moveToTray)
        {
            if (!files.Any())
            {
                return;
            }
            StringCollection sc = new StringCollection();
            sc.AddRange(files.Select(t => t.FullName).ToHashSet().ToArray());
            infoLabel.Text = String.Format(
                $"Copied {files.Count} files to clipboard.");
            Clipboard.SetFileDropList(sc);
            using (var ctx = new FilesContext())
            {
                ctx.RecordUse(files);
            }
            if (moveToTray)
            {
                MoveToTray();
            }
        }

        private void RefreshPreviewImage()
        {
            if (previewImage == null)
            {
                previewBox.Image = null;
                popoutPreview.PreviewImage = null;
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
            popoutPreview.Show(this);
            //MaybeShowVideoPlayers();
            MaybeSetMediaWindow();
            RefreshPreview();
        }

        private void MaybeSetMediaWindow()
        {
            mainVideoView.SetPlayer(null);
            popoutPreview.setMediaPlayer(null);
            if (!splitContainer1.Panel2Collapsed)
            {
                mainVideoView.SetPlayer(_mediaPlayer);
                //previewBox.Image = previewImage;
            } else if (popoutPreview.Visible)
            {
                popoutPreview.setMediaPlayer(_mediaPlayer);
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void searchTextBox_Click(object sender, EventArgs e)
        {
        }

        private void Restore()
        {
            Show();
            WindowState = FormWindowState.Normal;
            if (PopoutPreviewButton.Checked)
            {
                popoutPreview.Show();
                popoutPreview.Activate();
                popoutPreview.WindowState = FormWindowState.Normal;
            }
            searchTextBox.Focus();
            Activate();
            RefreshView();
        }
        private void MoveToTray()
        {
            popoutPreview.WindowState = FormWindowState.Minimized;
            Hide();
            popoutPreview.Hide();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (_watcherKeyManager.HandleKey(e))
            {
                return;
            }
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) && e.Modifiers == Keys.None)
            {
                CopyToClipboard(ilvThumbs.SelectedItems.Select(t => (File)t.VirtualItemKey).ToList(), true);
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
            if (e.KeyCode == Keys.O && e.Modifiers == Keys.Control)
            {
                OpenButton.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
            if (e.KeyCode == Keys.L && e.Modifiers == Keys.Control)
            {
                openFolderToolStripMenuItem.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
            if (e.KeyCode == Keys.R && e.Modifiers == Keys.Control)
            {
                renameToolStripMenuItem.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
            if (e.KeyCode == Keys.G && e.Modifiers == Keys.Control)
            {
                editXMPTagsMenuItem.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
            if (e.KeyCode == Keys.F5)
            {
                RefreshView();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.E )
            {
                ShowAllButton.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)
            {
                PopoutPreviewButton.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
        }


        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ShowAllButton.Checked)
            {
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
                SearchResultSizeLimit = Int32.MaxValue;
            }
            else
            {
                SearchResultSizeLimit = InitSearchResultSize;
            }

            RefreshView();
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
            /*
            if (e.KeyCode == Keys.O && e.Modifiers == Keys.Control)
            {
                OpenButton.PerformClick();
                e.SuppressKeyPress = true;
            }
            */

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                MoveToTray();
                e.Cancel = true;
                return;
            }
            SaveConfiguration();
        }

        private void LoadConfiguration()
        {
            var settings = Properties.Settings.Default;
            
            Width = settings.MainWidth;
            Height = settings.MainHeight;
            if (Properties.Settings.Default.MainPosSet)
            {
                StartPosition = FormStartPosition.Manual;
                Location = new Point(Math.Max(settings.MainX, 0), Math.Max(settings.MainY, 0));
            }
            splitContainer1.SplitterDistance = settings.SplitterDistance;
            if (settings.PopoutShow)
            {
                // Set the location before pressing button.
                popoutPreview.StartPosition = FormStartPosition.Manual;
                popoutPreview.Location = new Point(Math.Max(settings.PopoutX, 0), Math.Max(settings.PopoutY, 0));
                popoutPreview.Width = settings.PopoutWidth;
                popoutPreview.Height = settings.PopoutHeight;
                // This will also set the mediaplayer.
                PopoutPreviewButton.Checked = true;
            } else
            {
                popoutPreview.Left = Left + Width;
                popoutPreview.Top = Top;

                popoutPreview.Width = previewBox.Width;
                popoutPreview.Height = Height;
                mainVideoView.SetPlayer(_mediaPlayer);
            }
            groupFoldersButton.Checked = settings.GroupByFolder;
            MaybeSetGlobalKey(Properties.Settings.Default.OpenShortcut);
            SetActiveIdsFromConfigString(settings.ActiveWatchers);
            SetSortColumn((FilesContext.SortColumn)settings.SortColumn);

        }

        public void MaybeSetGlobalKey(Keys openShortcut)
        {
            try
            {
                HotkeyManager.Current.AddOrReplace(OpenHotKeyName, openShortcut, OpenGalleryHotkey_Press);
            }
            catch (HotkeyAlreadyRegisteredException)
            {
                MessageBox.Show("Failed to set the global open hotkey. It likely conflicts with one of your watcher hotkeys.  This may be fixed by changing the open key in the settings. Proceeding without the key shortcut.", "ImageGallery: Failed to add hotkey", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void MaybeUnsetGlobalKey(Keys openShortcut)
        {
            try
            {
                HotkeyManager.Current.Remove(OpenHotKeyName);
            }
            catch (HotkeyAlreadyRegisteredException)
            {
            }
        }

        private void SaveConfiguration()
        {
            var settings = Properties.Settings.Default;

            if (WindowState == FormWindowState.Minimized)
            {
                settings.MainWidth = RestoreBounds.Width;
                settings.MainHeight = RestoreBounds.Height;
                settings.MainX = RestoreBounds.Left;
                settings.MainY = RestoreBounds.Top;
            }
            else
            {
                settings.MainWidth = Width;
                settings.MainHeight = Height;
                settings.MainX = Left;
                settings.MainY = Top;
            }
            settings.MainPosSet = true;

            settings.SplitterDistance = splitContainer1.SplitterDistance;

            settings.PopoutShow = PopoutPreviewButton.Checked;
            if (PopoutPreviewButton.Checked)
            {
                if (popoutPreview.WindowState == FormWindowState.Minimized || popoutPreview.WindowState == FormWindowState.Maximized)
                {
                    settings.MainWidth = RestoreBounds.Width;
                    settings.MainHeight = RestoreBounds.Height;
                    settings.MainX = RestoreBounds.Left;
                    settings.MainY = RestoreBounds.Top;
                }
                else
                {
                    settings.PopoutWidth = popoutPreview.Width;
                    settings.PopoutHeight = popoutPreview.Height;
                    settings.PopoutX = popoutPreview.Left;
                    settings.PopoutY = popoutPreview.Top;
                }
            }
            settings.ActiveWatchers = GetActiveIdsConfigString();
            settings.SortColumn = (int)GetSortColumn();
            settings.GroupByFolder = groupFoldersButton.Checked;

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
            HashSet<int> ids = idsConfigString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToHashSet();
            SetActiveIdsFromSet(ids);
        }

        public void SetActiveIdsFromSet(HashSet<int> ids, bool restore = false)
        {
            foreach (var item in WatcherDropDown.DropDownItems)
            {
                if (IsWatcherToolStripButton(item))
                {
                    Console.WriteLine(GetWatcherIdFromToolStripObject(item));
                    ((ToolStripMenuItem)item).Checked = ids.Contains(GetWatcherIdFromToolStripObject(item));
                }
            }
            if (restore)
            {
                RefreshView();
                Restore();
            }
        }

        private static int GetWatcherIdFromToolStripObject(Object o)
        {
            return ((Watcher)(((ToolStripMenuItem)o).Tag)).Id;
        }

        private void PopoutPreviewButton_Click(object sender, EventArgs e)
        {

        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshView();

        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            var numSelected = ilvThumbs.SelectedItems.Count;
            if (numSelected != 1)
            {
                return;
            }
            File file = (File)ilvThumbs.SelectedItems[0].VirtualItemKey;
            System.Diagnostics.Process.Start("explorer.exe", file.FullName);
        }

        private void ilvThumbs_Enter(object sender, EventArgs e)
        {
            //SelectFirstIfExists();
        }

        private void toolStrip1_Enter(object sender, EventArgs e)
        {
            searchTextBox.Focus();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                MoveToTray();
            }
        }

        private void MinimizedIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Restore();
        }

        private void TrayExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Helpers.WM_SHOWME)
            {
                Restore();
            }
            base.WndProc(ref m);
        }

        void OnSync(FSWatcher.SyncOccurredEventArgs e)
        {
            if (!e.Changed || !GetActiveWatcherIds().Contains(e.WatcherId))
            {
                return;
            }

            if (!RefreshTimer.Enabled)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    RefreshViewAndReselect();
                });
                RefreshTimer.Enabled = true;
            } else
            {
                refreshRequested = 1;
            }
        }

        private void RefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {

            if (Interlocked.Exchange(ref refreshRequested, 0) != 0)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    RefreshViewAndReselect();
                });
                RefreshTimer.Reset();
            }
            else
            {

                RefreshTimer.Enabled = false;
            }
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var numSelected = ilvThumbs.SelectedItems.Count;
            if (numSelected != 1)
            {
                return;
            }
            File file = (File)ilvThumbs.SelectedItems[0].VirtualItemKey;
            string argument = "/select, \"" + file.FullName + "\"";

            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        private void SortOrderClicked(ToolStripMenuItem clicked)
        {
            if (clicked.Checked)
            {
                return;
            }
            UncheckOtherSortOrders(clicked);
            clicked.Checked = true;
            RefreshView();
        }
        private void usedOrder_Click(object sender, EventArgs e)
        {
            SortOrderClicked((ToolStripMenuItem)sender);
        }

        private void NameOrder_Click(object sender, EventArgs e)
        {
            SortOrderClicked((ToolStripMenuItem)sender);
        }

        private void CreatedOrder_Click(object sender, EventArgs e)
        {
            SortOrderClicked((ToolStripMenuItem)sender);
        }

        private void TimesUsedOrder_Click(object sender, EventArgs e)
        {
            SortOrderClicked((ToolStripMenuItem)sender);
        }

        private void fileInfoPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void searchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void CopyFileButton_Click(object sender, EventArgs e)
        {
            CopyMultipleToClipboard(ilvThumbs.SelectedItems.Select(t => (File)t.VirtualItemKey).ToList(), false);
        }

        private void GalleryRightClick_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var numSelected = ilvThumbs.SelectedItems.Count;
            if (numSelected == 0)
            {
                e.Cancel = true;
            }
        }

        private void ilvThumbs_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void GalleryRightClick_Opened(object sender, EventArgs e)
        {

        }

        private bool CheckSelectedForXMP(IEnumerable<ImageListViewItem> items)
        {
            return items.Select(item => item.VirtualItemKey as File).All(file => SearchTagEditor.usableExtension(file.FullName));
        }

        private void ilvThumbs_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var numSelected = ilvThumbs.SelectedItems.Count;
                if (numSelected == 0)
                {
                    return;
                }
                if (numSelected != 1)
                {
                    CopyImageButton.Enabled = false;
                    editTagsToolStripMenuItem.Enabled = false;
                    openFolderToolStripMenuItem.Enabled = false;
                    renameToolStripMenuItem.Enabled = false;
                    editXMPTagsMenuItem.Enabled = CheckSelectedForXMP(ilvThumbs.SelectedItems);
                    GalleryRightClick.Show(this, new Point(e.X + ilvThumbs.Left, e.Y + ilvThumbs.Top));
                    return;
                }
                editTagsToolStripMenuItem.Enabled = true;
                openFolderToolStripMenuItem.Enabled = true;

                File file = (File)ilvThumbs.SelectedItems[0].VirtualItemKey;


                CopyImageButton.Enabled = Helpers.IsImageFile(file.FullName);
                editXMPTagsMenuItem.Enabled = CheckSelectedForXMP(ilvThumbs.SelectedItems);


                GalleryRightClick.Show(this, new Point(e.X + ilvThumbs.Left, e.Y + ilvThumbs.Top));

            }
        }

        private void CopyImageButton_Click(object sender, EventArgs e)
        {
            var numSelected = ilvThumbs.SelectedItems.Count;
            if (numSelected != 1)
            {
                return;
            }
            File file = (File)ilvThumbs.SelectedItems[0].VirtualItemKey;
            CopySingleToClipboard(file, false);
        }

        private void editTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var numSelected = ilvThumbs.SelectedItems.Count;
            if (numSelected != 1)
            {
                return;
            }
            File file = ilvThumbs.SelectedItems[0].VirtualItemKey as File;
            var editTagsForm = new EditTagsForm(file);
            editTagsForm.ShowDialog();
            RefreshInfoPanel();
        }

        private void addTagsButton_Click(object sender, EventArgs e)
        {
            var numSelected = ilvThumbs.SelectedItems.Count;
            if (numSelected == 0)
            {
                return;
            }
            List<File> selectedFiles = ilvThumbs.SelectedItems.Select(item => item.VirtualItemKey as File).ToList();
            var addTagsForm = new AddTagsForm(selectedFiles);
            addTagsForm.ShowDialog();
            RefreshInfoPanel();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm(this);

            settingsForm.ShowDialog();
            if (searchTextBox.Text.Length == 0)
            {
                RefreshView();
            }
        }

        private void shutdownButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "This shuts down the application so that you will need to manually restart it. Are you sure?",
                "Shutdown Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation
            );
            if (dialogResult == DialogResult.No)
            {
                return;
            } else
            {
                Application.Exit();
            }
        }

        private void groupFoldersButton_CheckedChanged(object sender, EventArgs e)
        {
            RefreshView();
        }


        public void setInfoLabelSafe(string text)
        {
            if (InvokeRequired)
            {
                var d = new setInfoLabelSafeDelegate(setInfoLabelSafe);
                Invoke(d, new object[] { text });
            }
            else
            {
                infoLabel.Text = text;
            }
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            LogForm.GetInstance().BringToFront();
        }

        private void ilvThumbs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ilvThumbs.SelectAll();
            }
        }

        private void editXMPTags_Click(object sender, EventArgs e)
        {
            var numSelected = ilvThumbs.SelectedItems.Count;
            var selected = ilvThumbs.SelectedItems.Select(f => f).ToList();
            if (numSelected == 0 || !CheckSelectedForXMP(selected))
            {
                return;
            }
            var editTagsForm = new MultiXMPEditForm(selected.Select(item => item.VirtualItemKey as File).ToList(), _searchTagEditor, Monitor);
            editTagsForm.ShowDialog();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var numSelected = ilvThumbs.SelectedItems.Count;
            if (numSelected != 1)
            {
                return;
            }
            var form = new RenameForm(ilvThumbs.SelectedItems[0].VirtualItemKey as File);
            form.ShowDialog();
        }
    }

}
