namespace ImageGallery
{
    partial class AddWatcherForm
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
            this.DirLabel = new System.Windows.Forms.Label();
            this.WhitelistedLabel = new System.Windows.Forms.Label();
            this.DirectoryTextBox = new System.Windows.Forms.TextBox();
            this.ExtensionTextBox = new System.Windows.Forms.TextBox();
            this.HintLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.CancelAddButton = new System.Windows.Forms.Button();
            this.ImagesButton = new System.Windows.Forms.Button();
            this.VideoButton = new System.Windows.Forms.Button();
            this.FileSelectButton = new System.Windows.Forms.Button();
            this.scanSubdirectoriesBox = new System.Windows.Forms.CheckBox();
            this.GenerateVideoThumbnailsBox = new System.Windows.Forms.CheckBox();
            this.AudioButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DirLabel
            // 
            this.DirLabel.AutoSize = true;
            this.DirLabel.Location = new System.Drawing.Point(62, 42);
            this.DirLabel.Name = "DirLabel";
            this.DirLabel.Size = new System.Drawing.Size(52, 13);
            this.DirLabel.TabIndex = 0;
            this.DirLabel.Text = "Directory:";
            // 
            // WhitelistedLabel
            // 
            this.WhitelistedLabel.AutoSize = true;
            this.WhitelistedLabel.Location = new System.Drawing.Point(15, 68);
            this.WhitelistedLabel.Name = "WhitelistedLabel";
            this.WhitelistedLabel.Size = new System.Drawing.Size(101, 13);
            this.WhitelistedLabel.TabIndex = 1;
            this.WhitelistedLabel.Text = "Allowed Extensions:";
            // 
            // DirectoryTextBox
            // 
            this.DirectoryTextBox.Location = new System.Drawing.Point(121, 39);
            this.DirectoryTextBox.Name = "DirectoryTextBox";
            this.DirectoryTextBox.Size = new System.Drawing.Size(190, 20);
            this.DirectoryTextBox.TabIndex = 1;
            // 
            // ExtensionTextBox
            // 
            this.ExtensionTextBox.Location = new System.Drawing.Point(121, 65);
            this.ExtensionTextBox.Multiline = true;
            this.ExtensionTextBox.Name = "ExtensionTextBox";
            this.ExtensionTextBox.Size = new System.Drawing.Size(229, 113);
            this.ExtensionTextBox.TabIndex = 5;
            this.ExtensionTextBox.TextChanged += new System.EventHandler(this.ExtensionTextBox_TextChanged);
            // 
            // HintLabel
            // 
            this.HintLabel.Location = new System.Drawing.Point(62, 221);
            this.HintLabel.Name = "HintLabel";
            this.HintLabel.Size = new System.Drawing.Size(262, 33);
            this.HintLabel.TabIndex = 4;
            this.HintLabel.Text = "Separate with commas (e.g. .png, .jpg, .webm). Or leave blank to watch all files." +
    "";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(76, 16);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(38, 13);
            this.NameLabel.TabIndex = 5;
            this.NameLabel.Text = "Name:";
            this.NameLabel.Click += new System.EventHandler(this.NameLabel_Click);
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(121, 13);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(229, 20);
            this.NameTextBox.TabIndex = 0;
            this.NameTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(90, 257);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 6;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // CancelAddButton
            // 
            this.CancelAddButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelAddButton.Location = new System.Drawing.Point(206, 257);
            this.CancelAddButton.Name = "CancelAddButton";
            this.CancelAddButton.Size = new System.Drawing.Size(75, 23);
            this.CancelAddButton.TabIndex = 7;
            this.CancelAddButton.Text = "Cancel";
            this.CancelAddButton.UseVisualStyleBackColor = true;
            this.CancelAddButton.Click += new System.EventHandler(this.CancelAddButton_Click);
            // 
            // ImagesButton
            // 
            this.ImagesButton.Location = new System.Drawing.Point(18, 96);
            this.ImagesButton.Name = "ImagesButton";
            this.ImagesButton.Size = new System.Drawing.Size(96, 23);
            this.ImagesButton.TabIndex = 3;
            this.ImagesButton.Text = "Add Images";
            this.ImagesButton.UseVisualStyleBackColor = true;
            this.ImagesButton.Click += new System.EventHandler(this.ImagesButton_Click);
            // 
            // VideoButton
            // 
            this.VideoButton.Location = new System.Drawing.Point(18, 125);
            this.VideoButton.Name = "VideoButton";
            this.VideoButton.Size = new System.Drawing.Size(96, 23);
            this.VideoButton.TabIndex = 4;
            this.VideoButton.Text = "Add Videos";
            this.VideoButton.UseVisualStyleBackColor = true;
            this.VideoButton.Click += new System.EventHandler(this.VideoButton_Click);
            // 
            // FileSelectButton
            // 
            this.FileSelectButton.Location = new System.Drawing.Point(318, 39);
            this.FileSelectButton.Name = "FileSelectButton";
            this.FileSelectButton.Size = new System.Drawing.Size(32, 23);
            this.FileSelectButton.TabIndex = 2;
            this.FileSelectButton.Text = "...";
            this.FileSelectButton.UseVisualStyleBackColor = true;
            this.FileSelectButton.Click += new System.EventHandler(this.FileSelectButton_Click);
            // 
            // scanSubdirectoriesBox
            // 
            this.scanSubdirectoriesBox.AutoSize = true;
            this.scanSubdirectoriesBox.Checked = true;
            this.scanSubdirectoriesBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scanSubdirectoriesBox.Location = new System.Drawing.Point(36, 196);
            this.scanSubdirectoriesBox.Name = "scanSubdirectoriesBox";
            this.scanSubdirectoriesBox.Size = new System.Drawing.Size(119, 17);
            this.scanSubdirectoriesBox.TabIndex = 8;
            this.scanSubdirectoriesBox.Text = "Scan subdirectories";
            this.scanSubdirectoriesBox.UseVisualStyleBackColor = true;
            // 
            // GenerateVideoThumbnailsBox
            // 
            this.GenerateVideoThumbnailsBox.AutoSize = true;
            this.GenerateVideoThumbnailsBox.Checked = true;
            this.GenerateVideoThumbnailsBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GenerateVideoThumbnailsBox.Location = new System.Drawing.Point(183, 196);
            this.GenerateVideoThumbnailsBox.Name = "GenerateVideoThumbnailsBox";
            this.GenerateVideoThumbnailsBox.Size = new System.Drawing.Size(152, 17);
            this.GenerateVideoThumbnailsBox.TabIndex = 9;
            this.GenerateVideoThumbnailsBox.Text = "Generate video thumbnails";
            this.GenerateVideoThumbnailsBox.UseVisualStyleBackColor = true;
            // 
            // AudioButton
            // 
            this.AudioButton.Location = new System.Drawing.Point(20, 154);
            this.AudioButton.Name = "AudioButton";
            this.AudioButton.Size = new System.Drawing.Size(96, 23);
            this.AudioButton.TabIndex = 10;
            this.AudioButton.Text = "Add Audio";
            this.AudioButton.UseVisualStyleBackColor = true;
            this.AudioButton.Click += new System.EventHandler(this.AudioButton_Click);
            // 
            // AddWatcherForm
            // 
            this.AcceptButton = this.AddButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelAddButton;
            this.ClientSize = new System.Drawing.Size(362, 294);
            this.Controls.Add(this.AudioButton);
            this.Controls.Add(this.GenerateVideoThumbnailsBox);
            this.Controls.Add(this.scanSubdirectoriesBox);
            this.Controls.Add(this.FileSelectButton);
            this.Controls.Add(this.VideoButton);
            this.Controls.Add(this.ImagesButton);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.DirectoryTextBox);
            this.Controls.Add(this.ExtensionTextBox);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.CancelAddButton);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.HintLabel);
            this.Controls.Add(this.WhitelistedLabel);
            this.Controls.Add(this.DirLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::ImageGallery.Properties.Resources.icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddWatcherForm";
            this.Text = "Add Watcher";
            this.Load += new System.EventHandler(this.AddWatcherForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DirLabel;
        private System.Windows.Forms.Label WhitelistedLabel;
        private System.Windows.Forms.TextBox DirectoryTextBox;
        private System.Windows.Forms.TextBox ExtensionTextBox;
        private System.Windows.Forms.Label HintLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button CancelAddButton;
        private System.Windows.Forms.Button ImagesButton;
        private System.Windows.Forms.Button VideoButton;
        private System.Windows.Forms.Button FileSelectButton;
        private System.Windows.Forms.CheckBox scanSubdirectoriesBox;
        private System.Windows.Forms.CheckBox GenerateVideoThumbnailsBox;
        private System.Windows.Forms.Button AudioButton;
    }
}