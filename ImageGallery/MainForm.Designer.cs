namespace ImageGallery
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ilvThumbs = new Manina.Windows.Forms.ImageListView();
            this.GalleryRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenButton = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyFileButton = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyImageButton = new System.Windows.Forms.ToolStripMenuItem();
            this.editTagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTagsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.infoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.searchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.RefreshButton = new System.Windows.Forms.ToolStripButton();
            this.sortButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.CreatedOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.NameOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.UsedOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.TimesUsedOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.WatcherDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.ShowAllButton = new System.Windows.Forms.ToolStripButton();
            this.PopoutPreviewButton = new System.Windows.Forms.ToolStripButton();
            this.mainVideoView = new ImageGallery.VideoControl();
            this.fileInfoPanel = new ImageGallery.FileInfoPanel();
            this.NameToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MinimizedIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TrayExitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsButton = new System.Windows.Forms.ToolStripButton();
            this.GalleryRightClick.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.MainToolStrip.SuspendLayout();
            this.TrayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilvThumbs
            // 
            this.ilvThumbs.AllowDrop = true;
            this.ilvThumbs.AllowItemReorder = false;
            this.ilvThumbs.CacheLimit = "20000000";
            this.ilvThumbs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ilvThumbs.Location = new System.Drawing.Point(0, 25);
            this.ilvThumbs.Name = "ilvThumbs";
            this.ilvThumbs.PersistentCacheDirectory = "";
            this.ilvThumbs.PersistentCacheSize = ((long)(0));
            this.ilvThumbs.Size = new System.Drawing.Size(783, 512);
            this.ilvThumbs.TabIndex = 2;
            this.ilvThumbs.UseEmbeddedThumbnails = Manina.Windows.Forms.UseEmbeddedThumbnails.Never;
            this.ilvThumbs.UseWIC = true;
            this.ilvThumbs.ItemDoubleClick += new Manina.Windows.Forms.ItemDoubleClickEventHandler(this.ilvThumbs_ItemDoubleClick);
            this.ilvThumbs.SelectionChanged += new System.EventHandler(this.ilvThumbs_SelectionChanged);
            this.ilvThumbs.Enter += new System.EventHandler(this.ilvThumbs_Enter);
            this.ilvThumbs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ilvThumbs_MouseClick);
            this.ilvThumbs.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ilvThumbs_MouseUp);
            // 
            // GalleryRightClick
            // 
            this.GalleryRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenButton,
            this.openFolderToolStripMenuItem,
            this.CopyFileButton,
            this.CopyImageButton,
            this.editTagsToolStripMenuItem,
            this.addTagsButton});
            this.GalleryRightClick.Name = "GalleryRightClick";
            this.GalleryRightClick.Size = new System.Drawing.Size(178, 136);
            this.GalleryRightClick.Opening += new System.ComponentModel.CancelEventHandler(this.GalleryRightClick_Opening);
            this.GalleryRightClick.Opened += new System.EventHandler(this.GalleryRightClick_Opened);
            // 
            // OpenButton
            // 
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenButton.Size = new System.Drawing.Size(177, 22);
            this.OpenButton.Text = "Open";
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.openFolderToolStripMenuItem.Text = "Open folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // CopyFileButton
            // 
            this.CopyFileButton.Name = "CopyFileButton";
            this.CopyFileButton.Size = new System.Drawing.Size(177, 22);
            this.CopyFileButton.Text = "Copy file";
            this.CopyFileButton.Click += new System.EventHandler(this.CopyFileButton_Click);
            // 
            // CopyImageButton
            // 
            this.CopyImageButton.Name = "CopyImageButton";
            this.CopyImageButton.Size = new System.Drawing.Size(177, 22);
            this.CopyImageButton.Text = "Copy as image";
            this.CopyImageButton.Click += new System.EventHandler(this.CopyImageButton_Click);
            // 
            // editTagsToolStripMenuItem
            // 
            this.editTagsToolStripMenuItem.Name = "editTagsToolStripMenuItem";
            this.editTagsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.editTagsToolStripMenuItem.Text = "Edit tags";
            this.editTagsToolStripMenuItem.Click += new System.EventHandler(this.editTagsToolStripMenuItem_Click);
            // 
            // addTagsButton
            // 
            this.addTagsButton.Name = "addTagsButton";
            this.addTagsButton.Size = new System.Drawing.Size(177, 22);
            this.addTagsButton.Text = "Add tags";
            this.addTagsButton.Click += new System.EventHandler(this.addTagsButton_Click);
            // 
            // previewBox
            // 
            this.previewBox.ContextMenuStrip = this.GalleryRightClick;
            this.previewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewBox.Location = new System.Drawing.Point(0, 0);
            this.previewBox.MinimumSize = new System.Drawing.Size(150, 0);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(293, 485);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewBox.TabIndex = 2;
            this.previewBox.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ilvThumbs);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip);
            this.splitContainer1.Panel1.Controls.Add(this.MainToolStrip);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mainVideoView);
            this.splitContainer1.Panel2.Controls.Add(this.previewBox);
            this.splitContainer1.Panel2.Controls.Add(this.fileInfoPanel);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(1084, 561);
            this.splitContainer1.SplitterDistance = 785;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 537);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(783, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // infoLabel
            // 
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(768, 17);
            this.infoLabel.Spring = true;
            this.infoLabel.Text = "Ready.";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchTextBox,
            this.RefreshButton,
            this.sortButton,
            this.WatcherDropDown,
            this.ShowAllButton,
            this.PopoutPreviewButton,
            this.settingsButton});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MainToolStrip.Size = new System.Drawing.Size(783, 25);
            this.MainToolStrip.TabIndex = 1;
            this.MainToolStrip.TabStop = true;
            this.MainToolStrip.Text = "MainToolStrip";
            this.MainToolStrip.Enter += new System.EventHandler(this.toolStrip1_Enter);
            // 
            // searchTextBox
            // 
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(250, 25);
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyDown);
            this.searchTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchTextBox_KeyPress);
            this.searchTextBox.Click += new System.EventHandler(this.searchTextBox_Click);
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // RefreshButton
            // 
            this.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RefreshButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshButton.Image")));
            this.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(50, 22);
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // sortButton
            // 
            this.sortButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sortButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreatedOrder,
            this.NameOrder,
            this.UsedOrder,
            this.TimesUsedOrder});
            this.sortButton.Image = ((System.Drawing.Image)(resources.GetObject("sortButton.Image")));
            this.sortButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(72, 22);
            this.sortButton.Text = "Sort order";
            // 
            // CreatedOrder
            // 
            this.CreatedOrder.Name = "CreatedOrder";
            this.CreatedOrder.Size = new System.Drawing.Size(151, 22);
            this.CreatedOrder.Tag = "DateChanged";
            this.CreatedOrder.Text = "Date Modified";
            this.CreatedOrder.Click += new System.EventHandler(this.CreatedOrder_Click);
            // 
            // NameOrder
            // 
            this.NameOrder.Name = "NameOrder";
            this.NameOrder.Size = new System.Drawing.Size(151, 22);
            this.NameOrder.Tag = "Name";
            this.NameOrder.Text = "Name";
            this.NameOrder.Click += new System.EventHandler(this.NameOrder_Click);
            // 
            // UsedOrder
            // 
            this.UsedOrder.Name = "UsedOrder";
            this.UsedOrder.Size = new System.Drawing.Size(151, 22);
            this.UsedOrder.Tag = "DateAccessed";
            this.UsedOrder.Text = "Date Last Used";
            this.UsedOrder.Click += new System.EventHandler(this.usedOrder_Click);
            // 
            // TimesUsedOrder
            // 
            this.TimesUsedOrder.Name = "TimesUsedOrder";
            this.TimesUsedOrder.Size = new System.Drawing.Size(151, 22);
            this.TimesUsedOrder.Tag = "TimesAccessed";
            this.TimesUsedOrder.Text = "Times Used";
            this.TimesUsedOrder.Click += new System.EventHandler(this.TimesUsedOrder_Click);
            // 
            // WatcherDropDown
            // 
            this.WatcherDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.WatcherDropDown.Image = ((System.Drawing.Image)(resources.GetObject("WatcherDropDown.Image")));
            this.WatcherDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.WatcherDropDown.Name = "WatcherDropDown";
            this.WatcherDropDown.Size = new System.Drawing.Size(69, 22);
            this.WatcherDropDown.Text = "Watchers";
            // 
            // ShowAllButton
            // 
            this.ShowAllButton.CheckOnClick = true;
            this.ShowAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ShowAllButton.Image = ((System.Drawing.Image)(resources.GetObject("ShowAllButton.Image")));
            this.ShowAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShowAllButton.Name = "ShowAllButton";
            this.ShowAllButton.Size = new System.Drawing.Size(57, 22);
            this.ShowAllButton.Text = "Show All";
            this.ShowAllButton.CheckedChanged += new System.EventHandler(this.ShowAllButton_CheckedChanged);
            // 
            // PopoutPreviewButton
            // 
            this.PopoutPreviewButton.CheckOnClick = true;
            this.PopoutPreviewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PopoutPreviewButton.Image = ((System.Drawing.Image)(resources.GetObject("PopoutPreviewButton.Image")));
            this.PopoutPreviewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PopoutPreviewButton.Name = "PopoutPreviewButton";
            this.PopoutPreviewButton.Size = new System.Drawing.Size(50, 22);
            this.PopoutPreviewButton.Text = "Popout";
            this.PopoutPreviewButton.CheckedChanged += new System.EventHandler(this.PopoutPreviewButton_CheckedChanged);
            this.PopoutPreviewButton.Click += new System.EventHandler(this.PopoutPreviewButton_Click);
            // 
            // mainVideoView
            // 
            this.mainVideoView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainVideoView.LibVLC = null;
            this.mainVideoView.Location = new System.Drawing.Point(0, 0);
            this.mainVideoView.Name = "mainVideoView";
            this.mainVideoView.Queue = null;
            this.mainVideoView.Size = new System.Drawing.Size(293, 485);
            this.mainVideoView.Started = false;
            this.mainVideoView.TabIndex = 4;
            this.mainVideoView.Visible = false;
            // 
            // fileInfoPanel
            // 
            this.fileInfoPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fileInfoPanel.Items = null;
            this.fileInfoPanel.Loading = false;
            this.fileInfoPanel.Location = new System.Drawing.Point(0, 485);
            this.fileInfoPanel.Name = "fileInfoPanel";
            this.fileInfoPanel.Padding = new System.Windows.Forms.Padding(3);
            this.fileInfoPanel.Size = new System.Drawing.Size(293, 74);
            this.fileInfoPanel.TabIndex = 3;
            this.fileInfoPanel.TabStop = false;
            // 
            // MinimizedIcon
            // 
            this.MinimizedIcon.ContextMenuStrip = this.TrayContextMenu;
            this.MinimizedIcon.Text = "Image Gallery";
            this.MinimizedIcon.Visible = true;
            this.MinimizedIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MinimizedIcon_MouseDoubleClick);
            // 
            // TrayContextMenu
            // 
            this.TrayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TrayExitButton});
            this.TrayContextMenu.Name = "TrayContextMenu";
            this.TrayContextMenu.Size = new System.Drawing.Size(94, 26);
            // 
            // TrayExitButton
            // 
            this.TrayExitButton.Name = "TrayExitButton";
            this.TrayExitButton.Size = new System.Drawing.Size(93, 22);
            this.TrayExitButton.Text = "Exit";
            this.TrayExitButton.Click += new System.EventHandler(this.TrayExitButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsButton.Image")));
            this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(53, 22);
            this.settingsButton.Text = "Settings";
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Image Gallery";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.GalleryRightClick.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.TrayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Manina.Windows.Forms.ImageListView ilvThumbs;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel infoLabel;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton RefreshButton;
        private System.Windows.Forms.ToolStripTextBox searchTextBox;
        private System.Windows.Forms.ToolStripDropDownButton sortButton;
        private System.Windows.Forms.ToolStripMenuItem NameOrder;
        private System.Windows.Forms.ToolStripMenuItem UsedOrder;
        private System.Windows.Forms.ToolStripMenuItem CreatedOrder;
        private System.Windows.Forms.ToolStripMenuItem TimesUsedOrder;
        private System.Windows.Forms.ToolTip NameToolTip;
        private System.Windows.Forms.ToolStripDropDownButton WatcherDropDown;
        private System.Windows.Forms.ToolStripButton ShowAllButton;
        private System.Windows.Forms.ToolStripButton PopoutPreviewButton;
        private System.Windows.Forms.ContextMenuStrip GalleryRightClick;
        private System.Windows.Forms.ToolStripMenuItem OpenButton;
        private System.Windows.Forms.NotifyIcon MinimizedIcon;
        private System.Windows.Forms.ContextMenuStrip TrayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem TrayExitButton;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private FileInfoPanel fileInfoPanel;
        private System.Windows.Forms.ToolStripMenuItem CopyFileButton;
        private System.Windows.Forms.ToolStripMenuItem CopyImageButton;
        private VideoControl mainVideoView;
        private System.Windows.Forms.ToolStripMenuItem editTagsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTagsButton;
        private System.Windows.Forms.ToolStripButton settingsButton;
    }
}

