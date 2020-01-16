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
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.infoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.searchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.RefreshButton = new System.Windows.Forms.ToolStripButton();
            this.sortButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.nameSort = new System.Windows.Forms.ToolStripMenuItem();
            this.usedOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.createdSort = new System.Windows.Forms.ToolStripMenuItem();
            this.timesUsedSort = new System.Windows.Forms.ToolStripMenuItem();
            this.WatcherDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.ShowAllButton = new System.Windows.Forms.ToolStripButton();
            this.PopoutPreviewButton = new System.Windows.Forms.ToolStripButton();
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.LoadingLabel = new System.Windows.Forms.Label();
            this.NameListLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MinimizedIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TrayExitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.PathLabel = new System.Windows.Forms.Label();
            this.PathListLabel = new System.Windows.Forms.Label();
            this.GalleryRightClick.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.InfoPanel.SuspendLayout();
            this.TrayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilvThumbs
            // 
            this.ilvThumbs.AllowDrop = true;
            this.ilvThumbs.AllowItemReorder = false;
            this.ilvThumbs.CacheLimit = "20000000";
            this.ilvThumbs.ContextMenuStrip = this.GalleryRightClick;
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
            // 
            // GalleryRightClick
            // 
            this.GalleryRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenButton});
            this.GalleryRightClick.Name = "GalleryRightClick";
            this.GalleryRightClick.Size = new System.Drawing.Size(147, 26);
            // 
            // OpenButton
            // 
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenButton.Size = new System.Drawing.Size(146, 22);
            this.OpenButton.Text = "Open";
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // previewBox
            // 
            this.previewBox.ContextMenuStrip = this.GalleryRightClick;
            this.previewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewBox.Location = new System.Drawing.Point(0, 0);
            this.previewBox.MinimumSize = new System.Drawing.Size(150, 0);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(293, 482);
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
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.previewBox);
            this.splitContainer1.Panel2.Controls.Add(this.InfoPanel);
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
            this.infoLabel.Size = new System.Drawing.Size(42, 17);
            this.infoLabel.Text = "Ready.";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchTextBox,
            this.RefreshButton,
            this.sortButton,
            this.WatcherDropDown,
            this.ShowAllButton,
            this.PopoutPreviewButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(783, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "MainToolStrip";
            this.toolStrip1.Enter += new System.EventHandler(this.toolStrip1_Enter);
            // 
            // searchTextBox
            // 
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(250, 25);
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyDown);
            this.searchTextBox.Click += new System.EventHandler(this.searchTextBox_Click);
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // RefreshButton
            // 
            this.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RefreshButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshButton.Image")));
            this.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(23, 22);
            this.RefreshButton.Text = "RefreshButton";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // sortButton
            // 
            this.sortButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameSort,
            this.usedOrder,
            this.createdSort,
            this.timesUsedSort});
            this.sortButton.Image = ((System.Drawing.Image)(resources.GetObject("sortButton.Image")));
            this.sortButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(88, 22);
            this.sortButton.Text = "Sort order";
            // 
            // nameSort
            // 
            this.nameSort.Name = "nameSort";
            this.nameSort.Size = new System.Drawing.Size(151, 22);
            this.nameSort.Text = "Name";
            // 
            // usedOrder
            // 
            this.usedOrder.Name = "usedOrder";
            this.usedOrder.Size = new System.Drawing.Size(151, 22);
            this.usedOrder.Text = "Date Last Used";
            this.usedOrder.Click += new System.EventHandler(this.usedOrder_Click);
            // 
            // createdSort
            // 
            this.createdSort.Name = "createdSort";
            this.createdSort.Size = new System.Drawing.Size(151, 22);
            this.createdSort.Text = "Date Created";
            // 
            // timesUsedSort
            // 
            this.timesUsedSort.Name = "timesUsedSort";
            this.timesUsedSort.Size = new System.Drawing.Size(151, 22);
            this.timesUsedSort.Text = "Times Used";
            // 
            // WatcherDropDown
            // 
            this.WatcherDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.WatcherDropDown.Image = ((System.Drawing.Image)(resources.GetObject("WatcherDropDown.Image")));
            this.WatcherDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.WatcherDropDown.Name = "WatcherDropDown";
            this.WatcherDropDown.Size = new System.Drawing.Size(29, 22);
            this.WatcherDropDown.Text = "toolStripDropDownButton1";
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
            // InfoPanel
            // 
            this.InfoPanel.Controls.Add(this.PathListLabel);
            this.InfoPanel.Controls.Add(this.PathLabel);
            this.InfoPanel.Controls.Add(this.LoadingLabel);
            this.InfoPanel.Controls.Add(this.NameListLabel);
            this.InfoPanel.Controls.Add(this.NameLabel);
            this.InfoPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.InfoPanel.Location = new System.Drawing.Point(0, 482);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(293, 77);
            this.InfoPanel.TabIndex = 3;
            // 
            // LoadingLabel
            // 
            this.LoadingLabel.AutoSize = true;
            this.LoadingLabel.Location = new System.Drawing.Point(3, 56);
            this.LoadingLabel.Name = "LoadingLabel";
            this.LoadingLabel.Size = new System.Drawing.Size(94, 13);
            this.LoadingLabel.TabIndex = 2;
            this.LoadingLabel.Text = "Loading preview...";
            this.LoadingLabel.Visible = false;
            // 
            // NameListLabel
            // 
            this.NameListLabel.AutoSize = true;
            this.NameListLabel.Location = new System.Drawing.Point(4, 10);
            this.NameListLabel.Name = "NameListLabel";
            this.NameListLabel.Size = new System.Drawing.Size(38, 13);
            this.NameListLabel.TabIndex = 0;
            this.NameListLabel.Text = "Name:";
            // 
            // NameLabel
            // 
            this.NameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameLabel.AutoEllipsis = true;
            this.NameLabel.Location = new System.Drawing.Point(42, 10);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(240, 20);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "No file selected.";
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
            // PathLabel
            // 
            this.PathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathLabel.AutoEllipsis = true;
            this.PathLabel.Location = new System.Drawing.Point(42, 25);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(240, 29);
            this.PathLabel.TabIndex = 3;
            // 
            // PathListLabel
            // 
            this.PathListLabel.AutoSize = true;
            this.PathListLabel.Location = new System.Drawing.Point(10, 25);
            this.PathListLabel.Margin = new System.Windows.Forms.Padding(0);
            this.PathListLabel.Name = "PathListLabel";
            this.PathListLabel.Size = new System.Drawing.Size(32, 13);
            this.PathListLabel.TabIndex = 4;
            this.PathListLabel.Text = "Path:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
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
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.InfoPanel.ResumeLayout(false);
            this.InfoPanel.PerformLayout();
            this.TrayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Manina.Windows.Forms.ImageListView ilvThumbs;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel infoLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton RefreshButton;
        private System.Windows.Forms.ToolStripTextBox searchTextBox;
        private System.Windows.Forms.ToolStripDropDownButton sortButton;
        private System.Windows.Forms.ToolStripMenuItem nameSort;
        private System.Windows.Forms.ToolStripMenuItem usedOrder;
        private System.Windows.Forms.ToolStripMenuItem createdSort;
        private System.Windows.Forms.ToolStripMenuItem timesUsedSort;
        private System.Windows.Forms.Panel InfoPanel;
        private System.Windows.Forms.Label NameListLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.ToolTip NameToolTip;
        private System.Windows.Forms.Label LoadingLabel;
        private System.Windows.Forms.ToolStripDropDownButton WatcherDropDown;
        private System.Windows.Forms.ToolStripButton ShowAllButton;
        private System.Windows.Forms.ToolStripButton PopoutPreviewButton;
        private System.Windows.Forms.ContextMenuStrip GalleryRightClick;
        private System.Windows.Forms.ToolStripMenuItem OpenButton;
        private System.Windows.Forms.NotifyIcon MinimizedIcon;
        private System.Windows.Forms.ContextMenuStrip TrayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem TrayExitButton;
        private System.Windows.Forms.Label PathListLabel;
        private System.Windows.Forms.Label PathLabel;
    }
}

