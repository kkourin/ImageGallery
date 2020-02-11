namespace ImageGallery
{
    partial class SettingsForm
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
            this.recentlyCreatedBox = new System.Windows.Forms.CheckBox();
            this.recentlyUsedBox = new System.Windows.Forms.CheckBox();
            this.frequentlyClickedBox = new System.Windows.Forms.CheckBox();
            this.recentlyCreatedUpDown = new System.Windows.Forms.NumericUpDown();
            this.recentlyUsedUpDown = new System.Windows.Forms.NumericUpDown();
            this.frequentlyClickedUpDown = new System.Windows.Forms.NumericUpDown();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.homeGroup = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.recentlyCreatedUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recentlyUsedUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequentlyClickedUpDown)).BeginInit();
            this.homeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // recentlyCreatedBox
            // 
            this.recentlyCreatedBox.AutoSize = true;
            this.recentlyCreatedBox.Location = new System.Drawing.Point(28, 28);
            this.recentlyCreatedBox.Name = "recentlyCreatedBox";
            this.recentlyCreatedBox.Size = new System.Drawing.Size(132, 17);
            this.recentlyCreatedBox.TabIndex = 0;
            this.recentlyCreatedBox.Text = "Show recently created";
            this.recentlyCreatedBox.UseVisualStyleBackColor = true;
            this.recentlyCreatedBox.CheckedChanged += new System.EventHandler(this.RecentlyUsedBox_CheckedChanged);
            // 
            // recentlyUsedBox
            // 
            this.recentlyUsedBox.AutoSize = true;
            this.recentlyUsedBox.Location = new System.Drawing.Point(28, 54);
            this.recentlyUsedBox.Name = "recentlyUsedBox";
            this.recentlyUsedBox.Size = new System.Drawing.Size(119, 17);
            this.recentlyUsedBox.TabIndex = 1;
            this.recentlyUsedBox.Text = "Show recently used";
            this.recentlyUsedBox.UseVisualStyleBackColor = true;
            this.recentlyUsedBox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // frequentlyClickedBox
            // 
            this.frequentlyClickedBox.AutoSize = true;
            this.frequentlyClickedBox.Location = new System.Drawing.Point(28, 80);
            this.frequentlyClickedBox.Name = "frequentlyClickedBox";
            this.frequentlyClickedBox.Size = new System.Drawing.Size(139, 17);
            this.frequentlyClickedBox.TabIndex = 2;
            this.frequentlyClickedBox.Text = "Show frequently clicked";
            this.frequentlyClickedBox.UseVisualStyleBackColor = true;
            this.frequentlyClickedBox.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // recentlyCreatedUpDown
            // 
            this.recentlyCreatedUpDown.Location = new System.Drawing.Point(232, 27);
            this.recentlyCreatedUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.recentlyCreatedUpDown.Name = "recentlyCreatedUpDown";
            this.recentlyCreatedUpDown.Size = new System.Drawing.Size(68, 20);
            this.recentlyCreatedUpDown.TabIndex = 3;
            // 
            // recentlyUsedUpDown
            // 
            this.recentlyUsedUpDown.Location = new System.Drawing.Point(232, 53);
            this.recentlyUsedUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.recentlyUsedUpDown.Name = "recentlyUsedUpDown";
            this.recentlyUsedUpDown.Size = new System.Drawing.Size(68, 20);
            this.recentlyUsedUpDown.TabIndex = 4;
            // 
            // frequentlyClickedUpDown
            // 
            this.frequentlyClickedUpDown.Location = new System.Drawing.Point(232, 79);
            this.frequentlyClickedUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.frequentlyClickedUpDown.Name = "frequentlyClickedUpDown";
            this.frequentlyClickedUpDown.Size = new System.Drawing.Size(68, 20);
            this.frequentlyClickedUpDown.TabIndex = 5;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(70, 129);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 6;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(213, 129);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Amount:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Amount:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(180, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Amount:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // homeGroup
            // 
            this.homeGroup.Controls.Add(this.label3);
            this.homeGroup.Controls.Add(this.label2);
            this.homeGroup.Controls.Add(this.label1);
            this.homeGroup.Controls.Add(this.frequentlyClickedUpDown);
            this.homeGroup.Controls.Add(this.recentlyUsedUpDown);
            this.homeGroup.Controls.Add(this.recentlyCreatedUpDown);
            this.homeGroup.Controls.Add(this.frequentlyClickedBox);
            this.homeGroup.Controls.Add(this.recentlyUsedBox);
            this.homeGroup.Controls.Add(this.recentlyCreatedBox);
            this.homeGroup.Location = new System.Drawing.Point(12, 12);
            this.homeGroup.Name = "homeGroup";
            this.homeGroup.Size = new System.Drawing.Size(338, 111);
            this.homeGroup.TabIndex = 11;
            this.homeGroup.TabStop = false;
            this.homeGroup.Text = "Home settings";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 161);
            this.Controls.Add(this.homeGroup);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.CancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.recentlyCreatedUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recentlyUsedUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequentlyClickedUpDown)).EndInit();
            this.homeGroup.ResumeLayout(false);
            this.homeGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox recentlyCreatedBox;
        private System.Windows.Forms.CheckBox recentlyUsedBox;
        private System.Windows.Forms.CheckBox frequentlyClickedBox;
        private System.Windows.Forms.NumericUpDown recentlyCreatedUpDown;
        private System.Windows.Forms.NumericUpDown recentlyUsedUpDown;
        private System.Windows.Forms.NumericUpDown frequentlyClickedUpDown;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox homeGroup;
    }
}