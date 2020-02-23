namespace ImageGallery
{
    partial class MultiXMPEditForm
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
            this.selectedListBox = new System.Windows.Forms.ListBox();
            this.selectedItemsLabel = new System.Windows.Forms.Label();
            this.editLabel = new System.Windows.Forms.Label();
            this.addTagsTextBox = new System.Windows.Forms.TextBox();
            this.editTabControl = new System.Windows.Forms.TabControl();
            this.addTagsTabPage = new System.Windows.Forms.TabPage();
            this.addLabel = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.removeTabPage = new System.Windows.Forms.TabPage();
            this.removeTagsLabel = new System.Windows.Forms.Label();
            this.removeTagsTextBox = new System.Windows.Forms.TextBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.previewPictureBox = new System.Windows.Forms.PictureBox();
            this.currentTagsLabel = new System.Windows.Forms.Label();
            this.tagsTabControl = new System.Windows.Forms.TabControl();
            this.inSelectedTabPage = new System.Windows.Forms.TabPage();
            this.addInSelectedTagButton = new System.Windows.Forms.Button();
            this.inSelectedListBox = new System.Windows.Forms.ListBox();
            this.inAllSelectedTabPage = new System.Windows.Forms.TabPage();
            this.addAllSelectedTagButton = new System.Windows.Forms.Button();
            this.inAllSelectedListBox = new System.Windows.Forms.ListBox();
            this.inWatchers = new System.Windows.Forms.TabPage();
            this.watcherTagsLabel = new System.Windows.Forms.Label();
            this.addWatcherTagButton = new System.Windows.Forms.Button();
            this.tagsWatcherListBox = new System.Windows.Forms.ListBox();
            this.watcherLabel = new System.Windows.Forms.Label();
            this.watcherListBox = new System.Windows.Forms.ListBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.selectedItemTagsLabel = new System.Windows.Forms.Label();
            this.addStatusLabel = new System.Windows.Forms.Label();
            this.removeStatusLabel = new System.Windows.Forms.Label();
            this.editTabControl.SuspendLayout();
            this.addTagsTabPage.SuspendLayout();
            this.removeTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).BeginInit();
            this.tagsTabControl.SuspendLayout();
            this.inSelectedTabPage.SuspendLayout();
            this.inAllSelectedTabPage.SuspendLayout();
            this.inWatchers.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectedListBox
            // 
            this.selectedListBox.FormattingEnabled = true;
            this.selectedListBox.Location = new System.Drawing.Point(13, 29);
            this.selectedListBox.Name = "selectedListBox";
            this.selectedListBox.Size = new System.Drawing.Size(325, 95);
            this.selectedListBox.TabIndex = 0;
            this.selectedListBox.SelectedIndexChanged += new System.EventHandler(this.selectedListBox_SelectedIndexChanged);
            // 
            // selectedItemsLabel
            // 
            this.selectedItemsLabel.AutoSize = true;
            this.selectedItemsLabel.Location = new System.Drawing.Point(9, 9);
            this.selectedItemsLabel.Name = "selectedItemsLabel";
            this.selectedItemsLabel.Size = new System.Drawing.Size(130, 13);
            this.selectedItemsLabel.TabIndex = 1;
            this.selectedItemsLabel.Text = "Preview of selected items:";
            this.selectedItemsLabel.Click += new System.EventHandler(this.selectedItemsLabel_Click);
            // 
            // editLabel
            // 
            this.editLabel.AutoSize = true;
            this.editLabel.Location = new System.Drawing.Point(16, 176);
            this.editLabel.Name = "editLabel";
            this.editLabel.Size = new System.Drawing.Size(28, 13);
            this.editLabel.TabIndex = 2;
            this.editLabel.Text = "Edit:";
            // 
            // addTagsTextBox
            // 
            this.addTagsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addTagsTextBox.Location = new System.Drawing.Point(6, 19);
            this.addTagsTextBox.Multiline = true;
            this.addTagsTextBox.Name = "addTagsTextBox";
            this.addTagsTextBox.Size = new System.Drawing.Size(462, 111);
            this.addTagsTextBox.TabIndex = 3;
            // 
            // editTabControl
            // 
            this.editTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editTabControl.Controls.Add(this.addTagsTabPage);
            this.editTabControl.Controls.Add(this.removeTabPage);
            this.editTabControl.Location = new System.Drawing.Point(15, 192);
            this.editTabControl.Name = "editTabControl";
            this.editTabControl.SelectedIndex = 0;
            this.editTabControl.Size = new System.Drawing.Size(480, 185);
            this.editTabControl.TabIndex = 4;
            // 
            // addTagsTabPage
            // 
            this.addTagsTabPage.Controls.Add(this.addStatusLabel);
            this.addTagsTabPage.Controls.Add(this.addLabel);
            this.addTagsTabPage.Controls.Add(this.addTagsTextBox);
            this.addTagsTabPage.Controls.Add(this.addButton);
            this.addTagsTabPage.Location = new System.Drawing.Point(4, 22);
            this.addTagsTabPage.Name = "addTagsTabPage";
            this.addTagsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.addTagsTabPage.Size = new System.Drawing.Size(472, 159);
            this.addTagsTabPage.TabIndex = 0;
            this.addTagsTabPage.Text = "Add";
            this.addTagsTabPage.UseVisualStyleBackColor = true;
            // 
            // addLabel
            // 
            this.addLabel.AutoSize = true;
            this.addLabel.Location = new System.Drawing.Point(9, 3);
            this.addLabel.Name = "addLabel";
            this.addLabel.Size = new System.Drawing.Size(67, 13);
            this.addLabel.TabIndex = 5;
            this.addLabel.Text = "Tags to add:";
            // 
            // addButton
            // 
            this.addButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.addButton.Location = new System.Drawing.Point(394, 136);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 6;
            this.addButton.Text = "Write to all";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeTabPage
            // 
            this.removeTabPage.Controls.Add(this.removeStatusLabel);
            this.removeTabPage.Controls.Add(this.removeTagsLabel);
            this.removeTabPage.Controls.Add(this.removeTagsTextBox);
            this.removeTabPage.Controls.Add(this.removeButton);
            this.removeTabPage.Location = new System.Drawing.Point(4, 22);
            this.removeTabPage.Name = "removeTabPage";
            this.removeTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.removeTabPage.Size = new System.Drawing.Size(472, 159);
            this.removeTabPage.TabIndex = 1;
            this.removeTabPage.Text = "Remove";
            this.removeTabPage.UseVisualStyleBackColor = true;
            // 
            // removeTagsLabel
            // 
            this.removeTagsLabel.AutoSize = true;
            this.removeTagsLabel.Location = new System.Drawing.Point(7, 3);
            this.removeTagsLabel.Name = "removeTagsLabel";
            this.removeTagsLabel.Size = new System.Drawing.Size(84, 13);
            this.removeTagsLabel.TabIndex = 7;
            this.removeTagsLabel.Text = "Tags to remove:";
            // 
            // removeTagsTextBox
            // 
            this.removeTagsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.removeTagsTextBox.Location = new System.Drawing.Point(6, 19);
            this.removeTagsTextBox.Multiline = true;
            this.removeTagsTextBox.Name = "removeTagsTextBox";
            this.removeTagsTextBox.Size = new System.Drawing.Size(462, 111);
            this.removeTagsTextBox.TabIndex = 6;
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Location = new System.Drawing.Point(378, 136);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(91, 23);
            this.removeButton.TabIndex = 7;
            this.removeButton.Text = "Remove from all";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // previewPictureBox
            // 
            this.previewPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewPictureBox.Location = new System.Drawing.Point(344, 29);
            this.previewPictureBox.Name = "previewPictureBox";
            this.previewPictureBox.Size = new System.Drawing.Size(150, 95);
            this.previewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewPictureBox.TabIndex = 5;
            this.previewPictureBox.TabStop = false;
            // 
            // currentTagsLabel
            // 
            this.currentTagsLabel.AutoSize = true;
            this.currentTagsLabel.Location = new System.Drawing.Point(20, 380);
            this.currentTagsLabel.Name = "currentTagsLabel";
            this.currentTagsLabel.Size = new System.Drawing.Size(75, 13);
            this.currentTagsLabel.TabIndex = 6;
            this.currentTagsLabel.Text = "Tag shortcuts:";
            // 
            // tagsTabControl
            // 
            this.tagsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagsTabControl.Controls.Add(this.inSelectedTabPage);
            this.tagsTabControl.Controls.Add(this.inAllSelectedTabPage);
            this.tagsTabControl.Controls.Add(this.inWatchers);
            this.tagsTabControl.Location = new System.Drawing.Point(19, 396);
            this.tagsTabControl.Name = "tagsTabControl";
            this.tagsTabControl.SelectedIndex = 0;
            this.tagsTabControl.Size = new System.Drawing.Size(472, 221);
            this.tagsTabControl.TabIndex = 7;
            // 
            // inSelectedTabPage
            // 
            this.inSelectedTabPage.Controls.Add(this.addInSelectedTagButton);
            this.inSelectedTabPage.Controls.Add(this.inSelectedListBox);
            this.inSelectedTabPage.Location = new System.Drawing.Point(4, 22);
            this.inSelectedTabPage.Name = "inSelectedTabPage";
            this.inSelectedTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inSelectedTabPage.Size = new System.Drawing.Size(464, 195);
            this.inSelectedTabPage.TabIndex = 0;
            this.inSelectedTabPage.Text = "In at least one of selected";
            this.inSelectedTabPage.UseVisualStyleBackColor = true;
            this.inSelectedTabPage.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // addInSelectedTagButton
            // 
            this.addInSelectedTagButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addInSelectedTagButton.Location = new System.Drawing.Point(309, 169);
            this.addInSelectedTagButton.Name = "addInSelectedTagButton";
            this.addInSelectedTagButton.Size = new System.Drawing.Size(152, 23);
            this.addInSelectedTagButton.TabIndex = 5;
            this.addInSelectedTagButton.Text = "Add selected tags to edit box";
            this.addInSelectedTagButton.UseVisualStyleBackColor = true;
            this.addInSelectedTagButton.Click += new System.EventHandler(this.addInSelectedTagButton_Click);
            // 
            // inSelectedListBox
            // 
            this.inSelectedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inSelectedListBox.FormattingEnabled = true;
            this.inSelectedListBox.Location = new System.Drawing.Point(6, 5);
            this.inSelectedListBox.Name = "inSelectedListBox";
            this.inSelectedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.inSelectedListBox.Size = new System.Drawing.Size(455, 160);
            this.inSelectedListBox.TabIndex = 0;
            // 
            // inAllSelectedTabPage
            // 
            this.inAllSelectedTabPage.Controls.Add(this.addAllSelectedTagButton);
            this.inAllSelectedTabPage.Controls.Add(this.inAllSelectedListBox);
            this.inAllSelectedTabPage.Location = new System.Drawing.Point(4, 22);
            this.inAllSelectedTabPage.Name = "inAllSelectedTabPage";
            this.inAllSelectedTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inAllSelectedTabPage.Size = new System.Drawing.Size(464, 195);
            this.inAllSelectedTabPage.TabIndex = 1;
            this.inAllSelectedTabPage.Text = "In every selected";
            this.inAllSelectedTabPage.UseVisualStyleBackColor = true;
            // 
            // addAllSelectedTagButton
            // 
            this.addAllSelectedTagButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addAllSelectedTagButton.Location = new System.Drawing.Point(309, 169);
            this.addAllSelectedTagButton.Name = "addAllSelectedTagButton";
            this.addAllSelectedTagButton.Size = new System.Drawing.Size(153, 23);
            this.addAllSelectedTagButton.TabIndex = 4;
            this.addAllSelectedTagButton.Text = "Add selected tags to edit box";
            this.addAllSelectedTagButton.UseVisualStyleBackColor = true;
            this.addAllSelectedTagButton.Click += new System.EventHandler(this.addAllSelectedTagButton_Click);
            // 
            // inAllSelectedListBox
            // 
            this.inAllSelectedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inAllSelectedListBox.BackColor = System.Drawing.Color.White;
            this.inAllSelectedListBox.FormattingEnabled = true;
            this.inAllSelectedListBox.Location = new System.Drawing.Point(6, 5);
            this.inAllSelectedListBox.Name = "inAllSelectedListBox";
            this.inAllSelectedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.inAllSelectedListBox.Size = new System.Drawing.Size(456, 160);
            this.inAllSelectedListBox.TabIndex = 0;
            this.inAllSelectedListBox.SelectedIndexChanged += new System.EventHandler(this.inAllSelectedListBox_SelectedIndexChanged);
            // 
            // inWatchers
            // 
            this.inWatchers.Controls.Add(this.watcherTagsLabel);
            this.inWatchers.Controls.Add(this.addWatcherTagButton);
            this.inWatchers.Controls.Add(this.tagsWatcherListBox);
            this.inWatchers.Controls.Add(this.watcherLabel);
            this.inWatchers.Controls.Add(this.watcherListBox);
            this.inWatchers.Location = new System.Drawing.Point(4, 22);
            this.inWatchers.Name = "inWatchers";
            this.inWatchers.Padding = new System.Windows.Forms.Padding(3);
            this.inWatchers.Size = new System.Drawing.Size(464, 195);
            this.inWatchers.TabIndex = 2;
            this.inWatchers.Text = "Browse all";
            this.inWatchers.UseVisualStyleBackColor = true;
            // 
            // watcherTagsLabel
            // 
            this.watcherTagsLabel.AutoSize = true;
            this.watcherTagsLabel.Location = new System.Drawing.Point(111, 4);
            this.watcherTagsLabel.Name = "watcherTagsLabel";
            this.watcherTagsLabel.Size = new System.Drawing.Size(34, 13);
            this.watcherTagsLabel.TabIndex = 4;
            this.watcherTagsLabel.Text = "Tags:";
            // 
            // addWatcherTagButton
            // 
            this.addWatcherTagButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addWatcherTagButton.Location = new System.Drawing.Point(309, 169);
            this.addWatcherTagButton.Name = "addWatcherTagButton";
            this.addWatcherTagButton.Size = new System.Drawing.Size(152, 23);
            this.addWatcherTagButton.TabIndex = 3;
            this.addWatcherTagButton.Text = "Add selected tags to edit box";
            this.addWatcherTagButton.UseVisualStyleBackColor = true;
            this.addWatcherTagButton.Click += new System.EventHandler(this.addWatcherTagButton_Click);
            // 
            // tagsWatcherListBox
            // 
            this.tagsWatcherListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagsWatcherListBox.FormattingEnabled = true;
            this.tagsWatcherListBox.Location = new System.Drawing.Point(111, 18);
            this.tagsWatcherListBox.Name = "tagsWatcherListBox";
            this.tagsWatcherListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.tagsWatcherListBox.Size = new System.Drawing.Size(347, 147);
            this.tagsWatcherListBox.TabIndex = 2;
            // 
            // watcherLabel
            // 
            this.watcherLabel.AutoSize = true;
            this.watcherLabel.Location = new System.Drawing.Point(7, 4);
            this.watcherLabel.Name = "watcherLabel";
            this.watcherLabel.Size = new System.Drawing.Size(51, 13);
            this.watcherLabel.TabIndex = 1;
            this.watcherLabel.Text = "Watcher:";
            // 
            // watcherListBox
            // 
            this.watcherListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.watcherListBox.FormattingEnabled = true;
            this.watcherListBox.Location = new System.Drawing.Point(7, 17);
            this.watcherListBox.Name = "watcherListBox";
            this.watcherListBox.Size = new System.Drawing.Size(98, 160);
            this.watcherListBox.TabIndex = 0;
            this.watcherListBox.SelectedIndexChanged += new System.EventHandler(this.watcherListBox_SelectedIndexChanged);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(420, 628);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 8;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // selectedItemTagsLabel
            // 
            this.selectedItemTagsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedItemTagsLabel.AutoEllipsis = true;
            this.selectedItemTagsLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selectedItemTagsLabel.Location = new System.Drawing.Point(12, 127);
            this.selectedItemTagsLabel.Name = "selectedItemTagsLabel";
            this.selectedItemTagsLabel.Size = new System.Drawing.Size(479, 46);
            this.selectedItemTagsLabel.TabIndex = 9;
            this.selectedItemTagsLabel.Text = "XMP tags for previewed item:";
            // 
            // addStatusLabel
            // 
            this.addStatusLabel.AutoEllipsis = true;
            this.addStatusLabel.Location = new System.Drawing.Point(3, 141);
            this.addStatusLabel.Name = "addStatusLabel";
            this.addStatusLabel.Size = new System.Drawing.Size(385, 20);
            this.addStatusLabel.TabIndex = 7;
            this.addStatusLabel.Text = "Status:";
            // 
            // removeStatusLabel
            // 
            this.removeStatusLabel.AutoEllipsis = true;
            this.removeStatusLabel.Location = new System.Drawing.Point(3, 141);
            this.removeStatusLabel.Name = "removeStatusLabel";
            this.removeStatusLabel.Size = new System.Drawing.Size(369, 20);
            this.removeStatusLabel.TabIndex = 8;
            this.removeStatusLabel.Text = "Status:";
            // 
            // MultiXMPEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 663);
            this.Controls.Add(this.selectedItemTagsLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.tagsTabControl);
            this.Controls.Add(this.currentTagsLabel);
            this.Controls.Add(this.previewPictureBox);
            this.Controls.Add(this.editTabControl);
            this.Controls.Add(this.editLabel);
            this.Controls.Add(this.selectedItemsLabel);
            this.Controls.Add(this.selectedListBox);
            this.Name = "MultiXMPEditForm";
            this.Text = "Edit XMP";
            this.Load += new System.EventHandler(this.MultiXMPEditForm_Load);
            this.editTabControl.ResumeLayout(false);
            this.addTagsTabPage.ResumeLayout(false);
            this.addTagsTabPage.PerformLayout();
            this.removeTabPage.ResumeLayout(false);
            this.removeTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).EndInit();
            this.tagsTabControl.ResumeLayout(false);
            this.inSelectedTabPage.ResumeLayout(false);
            this.inAllSelectedTabPage.ResumeLayout(false);
            this.inWatchers.ResumeLayout(false);
            this.inWatchers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox selectedListBox;
        private System.Windows.Forms.Label selectedItemsLabel;
        private System.Windows.Forms.Label editLabel;
        private System.Windows.Forms.TextBox addTagsTextBox;
        private System.Windows.Forms.TabControl editTabControl;
        private System.Windows.Forms.TabPage addTagsTabPage;
        private System.Windows.Forms.TabPage removeTabPage;
        private System.Windows.Forms.Label addLabel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label removeTagsLabel;
        private System.Windows.Forms.TextBox removeTagsTextBox;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.PictureBox previewPictureBox;
        private System.Windows.Forms.Label currentTagsLabel;
        private System.Windows.Forms.TabControl tagsTabControl;
        private System.Windows.Forms.TabPage inSelectedTabPage;
        private System.Windows.Forms.TabPage inAllSelectedTabPage;
        private System.Windows.Forms.ListBox inSelectedListBox;
        private System.Windows.Forms.ListBox inAllSelectedListBox;
        private System.Windows.Forms.TabPage inWatchers;
        private System.Windows.Forms.Button addInSelectedTagButton;
        private System.Windows.Forms.Button addAllSelectedTagButton;
        private System.Windows.Forms.Button addWatcherTagButton;
        private System.Windows.Forms.ListBox tagsWatcherListBox;
        private System.Windows.Forms.Label watcherLabel;
        private System.Windows.Forms.ListBox watcherListBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label watcherTagsLabel;
        private System.Windows.Forms.Label selectedItemTagsLabel;
        private System.Windows.Forms.Label addStatusLabel;
        private System.Windows.Forms.Label removeStatusLabel;
    }
}