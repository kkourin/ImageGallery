namespace ImageGallery
{
    partial class VideoControl
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
            this.controlPanel = new System.Windows.Forms.Panel();
            this.timeLabel = new System.Windows.Forms.Label();
            this.volumeLabel = new System.Windows.Forms.Label();
            this.zoomBox = new System.Windows.Forms.CheckBox();
            this.loopBox = new System.Windows.Forms.CheckBox();
            this.playButton = new System.Windows.Forms.Button();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.seekBar = new System.Windows.Forms.TrackBar();
            this.videoPanel = new System.Windows.Forms.Panel();
            this.playLabel = new System.Windows.Forms.Label();
            this.videoView = new LibVLCSharp.WinForms.VideoView();
            this.controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seekBar)).BeginInit();
            this.videoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.videoView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlPanel
            // 
            this.controlPanel.Controls.Add(this.timeLabel);
            this.controlPanel.Controls.Add(this.volumeLabel);
            this.controlPanel.Controls.Add(this.zoomBox);
            this.controlPanel.Controls.Add(this.loopBox);
            this.controlPanel.Controls.Add(this.playButton);
            this.controlPanel.Controls.Add(this.volumeBar);
            this.controlPanel.Controls.Add(this.seekBar);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.controlPanel.Location = new System.Drawing.Point(0, 203);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(298, 65);
            this.controlPanel.TabIndex = 0;
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(100, 40);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(66, 13);
            this.timeLabel.TabIndex = 5;
            this.timeLabel.Text = "00:00/00:00";
            // 
            // volumeLabel
            // 
            this.volumeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeLabel.Location = new System.Drawing.Point(177, 40);
            this.volumeLabel.Name = "volumeLabel";
            this.volumeLabel.Size = new System.Drawing.Size(36, 16);
            this.volumeLabel.TabIndex = 6;
            this.volumeLabel.Text = "100%";
            this.volumeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // zoomBox
            // 
            this.zoomBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.zoomBox.BackgroundImage = global::ImageGallery.Properties.Resources.full;
            this.zoomBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zoomBox.Location = new System.Drawing.Point(66, 34);
            this.zoomBox.Name = "zoomBox";
            this.zoomBox.Size = new System.Drawing.Size(28, 28);
            this.zoomBox.TabIndex = 3;
            this.zoomBox.UseVisualStyleBackColor = true;
            this.zoomBox.CheckedChanged += new System.EventHandler(this.zoomBox_CheckedChanged);
            // 
            // loopBox
            // 
            this.loopBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.loopBox.BackgroundImage = global::ImageGallery.Properties.Resources.repeat;
            this.loopBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.loopBox.Location = new System.Drawing.Point(35, 34);
            this.loopBox.Name = "loopBox";
            this.loopBox.Size = new System.Drawing.Size(28, 28);
            this.loopBox.TabIndex = 2;
            this.loopBox.UseVisualStyleBackColor = true;
            // 
            // playButton
            // 
            this.playButton.BackgroundImage = global::ImageGallery.Properties.Resources.play;
            this.playButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.playButton.Location = new System.Drawing.Point(4, 34);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(28, 28);
            this.playButton.TabIndex = 1;
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // volumeBar
            // 
            this.volumeBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeBar.AutoSize = false;
            this.volumeBar.BackColor = System.Drawing.SystemColors.Control;
            this.volumeBar.LargeChange = 1;
            this.volumeBar.Location = new System.Drawing.Point(208, 36);
            this.volumeBar.Maximum = 100;
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.Size = new System.Drawing.Size(87, 27);
            this.volumeBar.TabIndex = 4;
            this.volumeBar.TickFrequency = 10;
            this.volumeBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.volumeBar.Value = 100;
            this.volumeBar.ValueChanged += new System.EventHandler(this.volumeBar_ValueChanged);
            this.volumeBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.volumeBar_MouseDown);
            this.volumeBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.volumeBar_MouseMove);
            // 
            // seekBar
            // 
            this.seekBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seekBar.AutoSize = false;
            this.seekBar.BackColor = System.Drawing.SystemColors.Control;
            this.seekBar.Enabled = false;
            this.seekBar.LargeChange = 1;
            this.seekBar.Location = new System.Drawing.Point(4, 4);
            this.seekBar.Maximum = 5000;
            this.seekBar.Name = "seekBar";
            this.seekBar.Size = new System.Drawing.Size(291, 27);
            this.seekBar.TabIndex = 0;
            this.seekBar.TickFrequency = 500;
            this.seekBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.seekBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.seekBar_MouseDown);
            this.seekBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.seekBar_MouseMove);
            this.seekBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.seekBar_MouseUp);
            // 
            // videoPanel
            // 
            this.videoPanel.Controls.Add(this.playLabel);
            this.videoPanel.Controls.Add(this.videoView);
            this.videoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoPanel.Location = new System.Drawing.Point(0, 0);
            this.videoPanel.Name = "videoPanel";
            this.videoPanel.Size = new System.Drawing.Size(298, 203);
            this.videoPanel.TabIndex = 1;
            this.videoPanel.Click += new System.EventHandler(this.videoPanel_Click);
            // 
            // playLabel
            // 
            this.playLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.playLabel.AutoSize = true;
            this.playLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playLabel.Location = new System.Drawing.Point(3, 180);
            this.playLabel.Name = "playLabel";
            this.playLabel.Size = new System.Drawing.Size(92, 20);
            this.playLabel.TabIndex = 1;
            this.playLabel.Text = "Click to play";
            this.playLabel.Visible = false;
            this.playLabel.Click += new System.EventHandler(this.playLabel_Click);
            // 
            // videoView
            // 
            this.videoView.BackColor = System.Drawing.Color.Black;
            this.videoView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoView.Location = new System.Drawing.Point(0, 0);
            this.videoView.MediaPlayer = null;
            this.videoView.Name = "videoView";
            this.videoView.Size = new System.Drawing.Size(298, 203);
            this.videoView.TabIndex = 0;
            this.videoView.Text = "videoView1";
            // 
            // VideoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.videoPanel);
            this.Controls.Add(this.controlPanel);
            this.Name = "VideoControl";
            this.Size = new System.Drawing.Size(298, 268);
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seekBar)).EndInit();
            this.videoPanel.ResumeLayout(false);
            this.videoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.videoView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.TrackBar seekBar;
        private System.Windows.Forms.TrackBar volumeBar;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label volumeLabel;
        private System.Windows.Forms.CheckBox zoomBox;
        private System.Windows.Forms.CheckBox loopBox;
        private System.Windows.Forms.Panel videoPanel;
        private System.Windows.Forms.Label playLabel;
        private LibVLCSharp.WinForms.VideoView videoView;
    }
}
