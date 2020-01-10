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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ilvThumbs = new Manina.Windows.Forms.ImageListView();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.infoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.searchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.sortButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.nameSort = new System.Windows.Forms.ToolStripMenuItem();
            this.usedOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.createdSort = new System.Windows.Forms.ToolStripMenuItem();
            this.timesUsedSort = new System.Windows.Forms.ToolStripMenuItem();
            this.popoutButton = new System.Windows.Forms.ToolStripButton();
            this.WatchersButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.ilvThumbs.Size = new System.Drawing.Size(713, 512);
            this.ilvThumbs.TabIndex = 0;
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
            this.previewBox.Size = new System.Drawing.Size(263, 559);
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
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(984, 561);
            this.splitContainer1.SplitterDistance = 715;
            this.splitContainer1.TabIndex = 4;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 537);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(713, 22);
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
            this.toolStripButton3,
            this.sortButton,
            this.popoutButton,
            this.WatchersButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(713, 25);
            this.toolStrip1.TabIndex = 4;
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
            this.searchTextBox.Click += new System.EventHandler(this.searchTextBox_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Image Gallery";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripTextBox searchTextBox;
        private System.Windows.Forms.ToolStripDropDownButton sortButton;
        private System.Windows.Forms.ToolStripMenuItem nameSort;
        private System.Windows.Forms.ToolStripMenuItem usedOrder;
        private System.Windows.Forms.ToolStripMenuItem createdSort;
        private System.Windows.Forms.ToolStripMenuItem timesUsedSort;
        private System.Windows.Forms.ToolStripButton WatchersButton;
    }
}

