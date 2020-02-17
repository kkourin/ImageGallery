namespace ImageGallery
{
    partial class WatcherListForm
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
            this.AddButton = new System.Windows.Forms.Button();
            this.watcherListView = new System.Windows.Forms.ListView();
            this.WatcherName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WatcherEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WatcherDirectory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WatcherExtensions = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Subdirs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VidThumbs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Hotkey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Global = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RemoveButton = new System.Windows.Forms.Button();
            this.LastErrorButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.Icon = Properties.Resources.icon;

            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(12, 383);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // watcherListView
            // 
            this.watcherListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.WatcherName,
            this.WatcherEnabled,
            this.WatcherDirectory,
            this.WatcherExtensions,
            this.Subdirs,
            this.VidThumbs,
            this.Hotkey,
            this.Global});
            this.watcherListView.FullRowSelect = true;
            this.watcherListView.HideSelection = false;
            this.watcherListView.Location = new System.Drawing.Point(12, 12);
            this.watcherListView.MultiSelect = false;
            this.watcherListView.Name = "watcherListView";
            this.watcherListView.ShowItemToolTips = true;
            this.watcherListView.Size = new System.Drawing.Size(624, 353);
            this.watcherListView.TabIndex = 0;
            this.watcherListView.UseCompatibleStateImageBehavior = false;
            this.watcherListView.View = System.Windows.Forms.View.Details;
            this.watcherListView.SelectedIndexChanged += new System.EventHandler(this.watcherListView_SelectedIndexChanged);
            // 
            // WatcherName
            // 
            this.WatcherName.Text = "Name";
            this.WatcherName.Width = 106;
            // 
            // WatcherEnabled
            // 
            this.WatcherEnabled.Text = "Enabled";
            this.WatcherEnabled.Width = 51;
            // 
            // WatcherDirectory
            // 
            this.WatcherDirectory.Text = "Directory";
            this.WatcherDirectory.Width = 300;
            // 
            // WatcherExtensions
            // 
            this.WatcherExtensions.Text = "Allowed Extensions";
            this.WatcherExtensions.Width = 163;
            // 
            // Subdirs
            // 
            this.Subdirs.Text = "Scan Subdirs.";
            // 
            // VidThumbs
            // 
            this.VidThumbs.Text = "Vid Thumbs.";
            // 
            // Hotkey
            // 
            this.Hotkey.Text = "Hotkey";
            this.Hotkey.Width = 100;
            // 
            // Global
            // 
            this.Global.Text = "GlobalHotkey";
            this.Global.Width = 100;
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(174, 383);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveButton.TabIndex = 2;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // LastErrorButton
            // 
            this.LastErrorButton.Location = new System.Drawing.Point(561, 383);
            this.LastErrorButton.Name = "LastErrorButton";
            this.LastErrorButton.Size = new System.Drawing.Size(75, 23);
            this.LastErrorButton.TabIndex = 3;
            this.LastErrorButton.Text = "Last Error";
            this.LastErrorButton.UseVisualStyleBackColor = true;
            this.LastErrorButton.Click += new System.EventHandler(this.LastErrorButton_Click);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(93, 383);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 4;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // WatcherListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 428);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.LastErrorButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.watcherListView);
            this.Controls.Add(this.AddButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "WatcherListForm";
            this.Text = "Watchers";
            this.Load += new System.EventHandler(this.WatcherListForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.ListView watcherListView;
        private System.Windows.Forms.ColumnHeader WatcherName;
        private System.Windows.Forms.ColumnHeader WatcherDirectory;
        private System.Windows.Forms.ColumnHeader WatcherExtensions;
        private System.Windows.Forms.ColumnHeader WatcherEnabled;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button LastErrorButton;
        private System.Windows.Forms.ColumnHeader Subdirs;
        private System.Windows.Forms.ColumnHeader VidThumbs;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.ColumnHeader Hotkey;
        private System.Windows.Forms.ColumnHeader Global;
    }
}