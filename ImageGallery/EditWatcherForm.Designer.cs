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
            this.hotkeyLabel = new System.Windows.Forms.Label();
            this.hotkeyTextBox = new System.Windows.Forms.TextBox();
            this.globalBox = new System.Windows.Forms.CheckBox();
            this.enabledBox = new System.Windows.Forms.CheckBox();
            this.shortcutsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(35, 124);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(168, 124);
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
            this.shortcutsBox.Location = new System.Drawing.Point(12, 12);
            this.shortcutsBox.Name = "shortcutsBox";
            this.shortcutsBox.Size = new System.Drawing.Size(257, 106);
            this.shortcutsBox.TabIndex = 2;
            this.shortcutsBox.TabStop = false;
            this.shortcutsBox.Text = "Keyboard Shortcuts";
            // 
            // hotkeyLabel
            // 
            this.hotkeyLabel.AutoSize = true;
            this.hotkeyLabel.Location = new System.Drawing.Point(23, 25);
            this.hotkeyLabel.Name = "hotkeyLabel";
            this.hotkeyLabel.Size = new System.Drawing.Size(110, 13);
            this.hotkeyLabel.TabIndex = 2;
            this.hotkeyLabel.Text = "Type the new hotkey:";
            // 
            // hotkeyTextBox
            // 
            this.hotkeyTextBox.Location = new System.Drawing.Point(23, 44);
            this.hotkeyTextBox.Name = "hotkeyTextBox";
            this.hotkeyTextBox.Size = new System.Drawing.Size(208, 20);
            this.hotkeyTextBox.TabIndex = 1;
            this.hotkeyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hotkeyTextBox_KeyDown);
            this.hotkeyTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.hotkeyTextBox_MouseDown);
            // 
            // globalBox
            // 
            this.globalBox.AutoSize = true;
            this.globalBox.Location = new System.Drawing.Point(138, 80);
            this.globalBox.Name = "globalBox";
            this.globalBox.Size = new System.Drawing.Size(93, 17);
            this.globalBox.TabIndex = 0;
            this.globalBox.Text = "Global Hotkey";
            this.globalBox.UseVisualStyleBackColor = true;
            // 
            // enabledBox
            // 
            this.enabledBox.AutoSize = true;
            this.enabledBox.Location = new System.Drawing.Point(26, 80);
            this.enabledBox.Name = "enabledBox";
            this.enabledBox.Size = new System.Drawing.Size(101, 17);
            this.enabledBox.TabIndex = 3;
            this.enabledBox.Text = "Hotkey enabled";
            this.enabledBox.UseVisualStyleBackColor = true;
            this.enabledBox.CheckedChanged += new System.EventHandler(this.enabledBox_CheckedChanged);
            // 
            // EditWatcherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 158);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.shortcutsBox);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditWatcherForm";
            this.Text = "Edit Watcher";
            this.Load += new System.EventHandler(this.EditWatcherForm_Load);
            this.shortcutsBox.ResumeLayout(false);
            this.shortcutsBox.PerformLayout();
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
    }
}