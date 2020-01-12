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
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.infoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.searchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.sortButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.nameSort = new System.Windows.Forms.ToolStripMenuItem();
            this.usedOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.createdSort = new System.Windows.Forms.ToolStripMenuItem();
            this.timesUsedSort = new System.Windows.Forms.ToolStripMenuItem();
            this.popoutButton = new System.Windows.Forms.ToolStripButton();
            this.WatchersButton = new System.Windows.Forms.ToolStripButton();
            this.WatcherDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.ShowAllButton = new System.Windows.Forms.ToolStripButton();
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.LoadingLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameListLabel = new System.Windows.Forms.Label();
            this.NameToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PopoutPreviewButton = new System.Windows.Forms.ToolStripButton();
            this.GalleryRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.InfoPanel.SuspendLayout();
            this.GalleryRightClick.SuspendLayout();
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
            this.ilvThumbs.TabIndex = 1;
            this.ilvThumbs.UseEmbeddedThumbnails = Manina.Windows.Forms.UseEmbeddedThumbnails.Never;
            this.ilvThumbs.UseWIC = true;
            this.ilvThumbs.ItemDoubleClick += new Manina.Windows.Forms.ItemDoubleClickEventHandler(this.ilvThumbs_ItemDoubleClick);
            this.ilvThumbs.SelectionChanged += new System.EventHandler(this.ilvThumbs_SelectionChanged);
            // 
            // previewBox
            // 
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
            this.splitContainer1.TabIndex = 4;
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
            this.toolStripButton2,
            this.searchTextBox,
            this.sortButton,
            this.popoutButton,
            this.WatchersButton,
            this.WatcherDropDown,
            this.ShowAllButton,
            this.PopoutPreviewButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(783, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // searchTextBox
            // 
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(200, 25);
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyDown);
            this.searchTextBox.Click += new System.EventHandler(this.searchTextBox_Click);
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
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
            this.nameSort.Size = new System.Drawing.Size(180, 22);
            this.nameSort.Text = "Name";
            // 
            // usedOrder
            // 
            this.usedOrder.Name = "usedOrder";
            this.usedOrder.Size = new System.Drawing.Size(180, 22);
            this.usedOrder.Text = "Date Last Used";
            this.usedOrder.Click += new System.EventHandler(this.usedOrder_Click);
            // 
            // createdSort
            // 
            this.createdSort.Name = "createdSort";
            this.createdSort.Size = new System.Drawing.Size(180, 22);
            this.createdSort.Text = "Date Created";
            // 
            // timesUsedSort
            // 
            this.timesUsedSort.Name = "timesUsedSort";
            this.timesUsedSort.Size = new System.Drawing.Size(180, 22);
            this.timesUsedSort.Text = "Times Used";
            // 
            // popoutButton
            // 
            this.popoutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.popoutButton.Image = ((System.Drawing.Image)(resources.GetObject("popoutButton.Image")));
            this.popoutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.popoutButton.Name = "popoutButton";
            this.popoutButton.Size = new System.Drawing.Size(94, 22);
            this.popoutButton.Text = "Popout Preview";
            this.popoutButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // WatchersButton
            // 
            this.WatchersButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.WatchersButton.Image = ((System.Drawing.Image)(resources.GetObject("WatchersButton.Image")));
            this.WatchersButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.WatchersButton.Name = "WatchersButton";
            this.WatchersButton.Size = new System.Drawing.Size(60, 22);
            this.WatchersButton.Text = "Watchers";
            this.WatchersButton.Click += new System.EventHandler(this.watchersButton_Click_1);
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
            // InfoPanel
            // 
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
            // NameLabel
            // 
            this.NameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameLabel.Location = new System.Drawing.Point(0, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Padding = new System.Windows.Forms.Padding(40, 3, 3, 3);
            this.NameLabel.Size = new System.Drawing.Size(293, 77);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "No file selected.";
            this.NameToolTip.SetToolTip(this.NameLabel, "None");
            // 
            // NameListLabel
            // 
            this.NameListLabel.AutoSize = true;
            this.NameListLabel.Location = new System.Drawing.Point(3, 3);
            this.NameListLabel.Name = "NameListLabel";
            this.NameListLabel.Size = new System.Drawing.Size(38, 13);
            this.NameListLabel.TabIndex = 0;
            this.NameListLabel.Text = "Name:";
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
            // 
            // GalleryRightClick
            // 
            this.GalleryRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testItemToolStripMenuItem});
            this.GalleryRightClick.Name = "GalleryRightClick";
            this.GalleryRightClick.Size = new System.Drawing.Size(181, 48);
            // 
            // testItemToolStripMenuItem
            // 
            this.testItemToolStripMenuItem.Name = "testItemToolStripMenuItem";
            this.testItemToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.testItemToolStripMenuItem.Text = "test item";
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
            this.GalleryRightClick.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Manina.Windows.Forms.ImageListView ilvThumbs;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel infoLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton popoutButton;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
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
        private System.Windows.Forms.ToolStripButton WatchersButton;
        private System.Windows.Forms.ToolStripDropDownButton WatcherDropDown;
        private System.Windows.Forms.ToolStripButton ShowAllButton;
        private System.Windows.Forms.ToolStripButton PopoutPreviewButton;
        private System.Windows.Forms.ContextMenuStrip GalleryRightClick;
        private System.Windows.Forms.ToolStripMenuItem testItemToolStripMenuItem;
    }
}

