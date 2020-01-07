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
            this.WatcherDirectory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WatcherExtensions = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WatcherEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(64, 418);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 0;
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
            this.WatcherExtensions});
            this.watcherListView.FullRowSelect = true;
            this.watcherListView.HideSelection = false;
            this.watcherListView.Location = new System.Drawing.Point(12, 12);
            this.watcherListView.Name = "watcherListView";
            this.watcherListView.Size = new System.Drawing.Size(624, 353);
            this.watcherListView.TabIndex = 2;
            this.watcherListView.UseCompatibleStateImageBehavior = false;
            this.watcherListView.View = System.Windows.Forms.View.Details;
            this.watcherListView.SelectedIndexChanged += new System.EventHandler(this.watcherListView_SelectedIndexChanged);
            // 
            // WatcherName
            // 
            this.WatcherName.Text = "Name";
            this.WatcherName.Width = 106;
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
            // WatcherEnabled
            // 
            this.WatcherEnabled.Text = "Enabled";
            this.WatcherEnabled.Width = 51;
            // 
            // WatcherListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 510);
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
    }
}