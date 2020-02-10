namespace ImageGallery
{
    partial class FileInfoPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NameHeadingLabel = new System.Windows.Forms.Label();
            this.PathHeadingLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.PathLabel = new System.Windows.Forms.Label();
            this.LoadingLabel = new System.Windows.Forms.Label();
            this.TagsHeadingLabel = new System.Windows.Forms.Label();
            this.TagsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NameHeadingLabel
            // 
            this.NameHeadingLabel.AutoSize = true;
            this.NameHeadingLabel.Location = new System.Drawing.Point(3, 0);
            this.NameHeadingLabel.Name = "NameHeadingLabel";
            this.NameHeadingLabel.Size = new System.Drawing.Size(38, 13);
            this.NameHeadingLabel.TabIndex = 0;
            this.NameHeadingLabel.Text = "Name:";
            // 
            // PathHeadingLabel
            // 
            this.PathHeadingLabel.AutoSize = true;
            this.PathHeadingLabel.Location = new System.Drawing.Point(9, 13);
            this.PathHeadingLabel.Name = "PathHeadingLabel";
            this.PathHeadingLabel.Size = new System.Drawing.Size(32, 13);
            this.PathHeadingLabel.TabIndex = 1;
            this.PathHeadingLabel.Text = "Path:";
            // 
            // NameLabel
            // 
            this.NameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameLabel.AutoEllipsis = true;
            this.NameLabel.Location = new System.Drawing.Point(47, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(278, 13);
            this.NameLabel.TabIndex = 2;
            this.NameLabel.Text = "No file selected.";
            this.NameLabel.Click += new System.EventHandler(this.NameLabel_Click);
            // 
            // PathLabel
            // 
            this.PathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathLabel.AutoEllipsis = true;
            this.PathLabel.Location = new System.Drawing.Point(47, 13);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(278, 26);
            this.PathLabel.TabIndex = 4;
            // 
            // LoadingLabel
            // 
            this.LoadingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LoadingLabel.AutoSize = true;
            this.LoadingLabel.Location = new System.Drawing.Point(-3, 67);
            this.LoadingLabel.Name = "LoadingLabel";
            this.LoadingLabel.Size = new System.Drawing.Size(85, 13);
            this.LoadingLabel.TabIndex = 5;
            this.LoadingLabel.Text = "Loading image...";
            this.LoadingLabel.Visible = false;
            // 
            // TagsHeadingLabel
            // 
            this.TagsHeadingLabel.AutoSize = true;
            this.TagsHeadingLabel.Location = new System.Drawing.Point(9, 38);
            this.TagsHeadingLabel.Name = "TagsHeadingLabel";
            this.TagsHeadingLabel.Size = new System.Drawing.Size(34, 13);
            this.TagsHeadingLabel.TabIndex = 7;
            this.TagsHeadingLabel.Text = "Tags:";
            // 
            // TagsLabel
            // 
            this.TagsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TagsLabel.AutoEllipsis = true;
            this.TagsLabel.Location = new System.Drawing.Point(47, 38);
            this.TagsLabel.Name = "TagsLabel";
            this.TagsLabel.Size = new System.Drawing.Size(278, 13);
            this.TagsLabel.TabIndex = 8;
            this.TagsLabel.Text = "No tags.";
            // 
            // FileInfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LoadingLabel);
            this.Controls.Add(this.TagsLabel);
            this.Controls.Add(this.TagsHeadingLabel);
            this.Controls.Add(this.PathLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.PathHeadingLabel);
            this.Controls.Add(this.NameHeadingLabel);
            this.Name = "FileInfoPanel";
            this.Size = new System.Drawing.Size(338, 80);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameHeadingLabel;
        private System.Windows.Forms.Label PathHeadingLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.Label LoadingLabel;
        private System.Windows.Forms.Label TagsHeadingLabel;
        private System.Windows.Forms.Label TagsLabel;
    }
}
