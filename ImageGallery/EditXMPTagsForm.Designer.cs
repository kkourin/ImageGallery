namespace ImageGallery
{
    partial class EditXMPTagsForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.tagsBox = new System.Windows.Forms.TextBox();
            this.editTagsLabel = new System.Windows.Forms.Label();
            this.FilenameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(218, 245);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.Location = new System.Drawing.Point(75, 245);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "Apply";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // tagsBox
            // 
            this.tagsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagsBox.Location = new System.Drawing.Point(32, 73);
            this.tagsBox.Multiline = true;
            this.tagsBox.Name = "tagsBox";
            this.tagsBox.Size = new System.Drawing.Size(318, 166);
            this.tagsBox.TabIndex = 5;
            this.tagsBox.TextChanged += new System.EventHandler(this.tagsBox_TextChanged);
            // 
            // editTagsLabel
            // 
            this.editTagsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editTagsLabel.Location = new System.Drawing.Point(29, 12);
            this.editTagsLabel.Name = "editTagsLabel";
            this.editTagsLabel.Size = new System.Drawing.Size(321, 35);
            this.editTagsLabel.TabIndex = 4;
            this.editTagsLabel.Text = "Tags separated with line breaks or spaces.";
            // 
            // FilenameLabel
            // 
            this.FilenameLabel.Location = new System.Drawing.Point(32, 33);
            this.FilenameLabel.Name = "FilenameLabel";
            this.FilenameLabel.Size = new System.Drawing.Size(318, 37);
            this.FilenameLabel.TabIndex = 8;
            this.FilenameLabel.Text = "Filename";
            this.FilenameLabel.Click += new System.EventHandler(this.FilenameLabel_Click);
            // 
            // EditXMPTagsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 280);
            this.Controls.Add(this.FilenameLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tagsBox);
            this.Controls.Add(this.editTagsLabel);
            this.Name = "EditXMPTagsForm";
            this.Text = "Edit XMP Tags";
            this.Load += new System.EventHandler(this.EditXMPTagsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox tagsBox;
        private System.Windows.Forms.Label editTagsLabel;
        private System.Windows.Forms.Label FilenameLabel;
    }
}