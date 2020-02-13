namespace ImageGallery
{
    partial class EditWatcherForm
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.shortcutsBox = new System.Windows.Forms.GroupBox();
            this.enabledBox = new System.Windows.Forms.CheckBox();
            this.hotkeyLabel = new System.Windows.Forms.Label();
            this.hotkeyTextBox = new System.Windows.Forms.TextBox();
            this.globalBox = new System.Windows.Forms.CheckBox();
            this.dataGroupBox = new System.Windows.Forms.GroupBox();
            this.GenerateVideoThumbnailsBox = new System.Windows.Forms.CheckBox();
            this.scanSubdirectoriesBox = new System.Windows.Forms.CheckBox();
            this.VideoButton = new System.Windows.Forms.Button();
            this.ImagesButton = new System.Windows.Forms.Button();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.ExtensionTextBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.HintLabel = new System.Windows.Forms.Label();
            this.extensionsLabel = new System.Windows.Forms.Label();
            this.shortcutsBox.SuspendLayout();
            this.dataGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(97, 312);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(230, 312);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // shortcutsBox
            // 
            this.shortcutsBox.Controls.Add(this.enabledBox);
            this.shortcutsBox.Controls.Add(this.hotkeyLabel);
            this.shortcutsBox.Controls.Add(this.hotkeyTextBox);
            this.shortcutsBox.Controls.Add(this.globalBox);
            this.shortcutsBox.Location = new System.Drawing.Point(12, 239);
            this.shortcutsBox.Name = "shortcutsBox";
            this.shortcutsBox.Size = new System.Drawing.Size(369, 67);
            this.shortcutsBox.TabIndex = 2;
            this.shortcutsBox.TabStop = false;
            this.shortcutsBox.Text = "Keyboard Shortcuts";
            // 
            // enabledBox
            // 
            this.enabledBox.AutoSize = true;
            this.enabledBox.Location = new System.Drawing.Point(130, 40);
            this.enabledBox.Name = "enabledBox";
            this.enabledBox.Size = new System.Drawing.Size(101, 17);
            this.enabledBox.TabIndex = 3;
            this.enabledBox.Text = "Hotkey enabled";
            this.enabledBox.UseVisualStyleBackColor = true;
            this.enabledBox.CheckedChanged += new System.EventHandler(this.enabledBox_CheckedChanged);
            // 
            // hotkeyLabel
            // 
            this.hotkeyLabel.AutoSize = true;
            this.hotkeyLabel.Location = new System.Drawing.Point(15, 17);
            this.hotkeyLabel.Name = "hotkeyLabel";
            this.hotkeyLabel.Size = new System.Drawing.Size(110, 13);
            this.hotkeyLabel.TabIndex = 2;
            this.hotkeyLabel.Text = "Type the new hotkey:";
            // 
            // hotkeyTextBox
            // 
            this.hotkeyTextBox.Location = new System.Drawing.Point(130, 14);
            this.hotkeyTextBox.Name = "hotkeyTextBox";
            this.hotkeyTextBox.Size = new System.Drawing.Size(217, 20);
            this.hotkeyTextBox.TabIndex = 1;
            this.hotkeyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hotkeyTextBox_KeyDown);
            this.hotkeyTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.hotkeyTextBox_MouseDown);
            // 
            // globalBox
            // 
            this.globalBox.AutoSize = true;
            this.globalBox.Location = new System.Drawing.Point(237, 40);
            this.globalBox.Name = "globalBox";
            this.globalBox.Size = new System.Drawing.Size(93, 17);
            this.globalBox.TabIndex = 0;
            this.globalBox.Text = "Global Hotkey";
            this.globalBox.UseVisualStyleBackColor = true;
            // 
            // dataGroupBox
            // 
            this.dataGroupBox.Controls.Add(this.extensionsLabel);
            this.dataGroupBox.Controls.Add(this.HintLabel);
            this.dataGroupBox.Controls.Add(this.GenerateVideoThumbnailsBox);
            this.dataGroupBox.Controls.Add(this.scanSubdirectoriesBox);
            this.dataGroupBox.Controls.Add(this.VideoButton);
            this.dataGroupBox.Controls.Add(this.ImagesButton);
            this.dataGroupBox.Controls.Add(this.NameTextBox);
            this.dataGroupBox.Controls.Add(this.ExtensionTextBox);
            this.dataGroupBox.Controls.Add(this.NameLabel);
            this.dataGroupBox.Location = new System.Drawing.Point(12, 13);
            this.dataGroupBox.Name = "dataGroupBox";
            this.dataGroupBox.Size = new System.Drawing.Size(369, 220);
            this.dataGroupBox.TabIndex = 3;
            this.dataGroupBox.TabStop = false;
            this.dataGroupBox.Text = "Data";
            // 
            // GenerateVideoThumbnailsBox
            // 
            this.GenerateVideoThumbnailsBox.AutoSize = true;
            this.GenerateVideoThumbnailsBox.Checked = true;
            this.GenerateVideoThumbnailsBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GenerateVideoThumbnailsBox.Location = new System.Drawing.Point(176, 160);
            this.GenerateVideoThumbnailsBox.Name = "GenerateVideoThumbnailsBox";
            this.GenerateVideoThumbnailsBox.Size = new System.Drawing.Size(152, 17);
            this.GenerateVideoThumbnailsBox.TabIndex = 16;
            this.GenerateVideoThumbnailsBox.Text = "Generate video thumbnails";
            this.GenerateVideoThumbnailsBox.UseVisualStyleBackColor = true;
            // 
            // scanSubdirectoriesBox
            // 
            this.scanSubdirectoriesBox.AutoSize = true;
            this.scanSubdirectoriesBox.Checked = true;
            this.scanSubdirectoriesBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scanSubdirectoriesBox.Location = new System.Drawing.Point(29, 160);
            this.scanSubdirectoriesBox.Name = "scanSubdirectoriesBox";
            this.scanSubdirectoriesBox.Size = new System.Drawing.Size(119, 17);
            this.scanSubdirectoriesBox.TabIndex = 15;
            this.scanSubdirectoriesBox.Text = "Scan subdirectories";
            this.scanSubdirectoriesBox.UseVisualStyleBackColor = true;
            // 
            // VideoButton
            // 
            this.VideoButton.Location = new System.Drawing.Point(15, 119);
            this.VideoButton.Name = "VideoButton";
            this.VideoButton.Size = new System.Drawing.Size(96, 23);
            this.VideoButton.TabIndex = 12;
            this.VideoButton.Text = "Add Videos";
            this.VideoButton.UseVisualStyleBackColor = true;
            this.VideoButton.Click += new System.EventHandler(this.VideoButton_Click);
            // 
            // ImagesButton
            // 
            this.ImagesButton.Location = new System.Drawing.Point(15, 90);
            this.ImagesButton.Name = "ImagesButton";
            this.ImagesButton.Size = new System.Drawing.Size(96, 23);
            this.ImagesButton.TabIndex = 11;
            this.ImagesButton.Text = "Add Images";
            this.ImagesButton.UseVisualStyleBackColor = true;
            this.ImagesButton.Click += new System.EventHandler(this.ImagesButton_Click);
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(118, 15);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(229, 20);
            this.NameTextBox.TabIndex = 10;
            // 
            // ExtensionTextBox
            // 
            this.ExtensionTextBox.Location = new System.Drawing.Point(118, 41);
            this.ExtensionTextBox.Multiline = true;
            this.ExtensionTextBox.Name = "ExtensionTextBox";
            this.ExtensionTextBox.Size = new System.Drawing.Size(229, 113);
            this.ExtensionTextBox.TabIndex = 13;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(73, 18);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(38, 13);
            this.NameLabel.TabIndex = 14;
            this.NameLabel.Text = "Name:";
            // 
            // HintLabel
            // 
            this.HintLabel.Location = new System.Drawing.Point(50, 180);
            this.HintLabel.Name = "HintLabel";
            this.HintLabel.Size = new System.Drawing.Size(262, 33);
            this.HintLabel.TabIndex = 17;
            this.HintLabel.Text = "Separate with commas (e.g. .png, .jpg, .webm). Or leave blank to watch all files." +
    "";
            // 
            // extensionsLabel
            // 
            this.extensionsLabel.AutoSize = true;
            this.extensionsLabel.Location = new System.Drawing.Point(12, 44);
            this.extensionsLabel.Name = "extensionsLabel";
            this.extensionsLabel.Size = new System.Drawing.Size(101, 13);
            this.extensionsLabel.TabIndex = 18;
            this.extensionsLabel.Text = "Allowed Extensions:";
            this.extensionsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // EditWatcherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 348);
            this.Controls.Add(this.dataGroupBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.shortcutsBox);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditWatcherForm";
            this.Text = "Edit Watcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditWatcherForm_FormClosing);
            this.Load += new System.EventHandler(this.EditWatcherForm_Load);
            this.shortcutsBox.ResumeLayout(false);
            this.shortcutsBox.PerformLayout();
            this.dataGroupBox.ResumeLayout(false);
            this.dataGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox shortcutsBox;
        private System.Windows.Forms.Label hotkeyLabel;
        private System.Windows.Forms.TextBox hotkeyTextBox;
        private System.Windows.Forms.CheckBox globalBox;
        private System.Windows.Forms.CheckBox enabledBox;
        private System.Windows.Forms.GroupBox dataGroupBox;
        private System.Windows.Forms.CheckBox GenerateVideoThumbnailsBox;
        private System.Windows.Forms.CheckBox scanSubdirectoriesBox;
        private System.Windows.Forms.Button VideoButton;
        private System.Windows.Forms.Button ImagesButton;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox ExtensionTextBox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label extensionsLabel;
        private System.Windows.Forms.Label HintLabel;
    }
}