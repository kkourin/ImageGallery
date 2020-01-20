namespace ImageGallery
{
    partial class PopoutPreview
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
            this.poppedPreviewBox = new Cyotek.Windows.Forms.ImageBox();
            this.fileInfoPanel = new ImageGallery.FileInfoPanel();
            this.SuspendLayout();
            // 
            // poppedPreviewBox
            // 
            this.poppedPreviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.poppedPreviewBox.DropShadowSize = 0;
            this.poppedPreviewBox.GridColor = System.Drawing.Color.WhiteSmoke;
            this.poppedPreviewBox.GridColorAlternate = System.Drawing.Color.WhiteSmoke;
            this.poppedPreviewBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
            this.poppedPreviewBox.Location = new System.Drawing.Point(0, 0);
            this.poppedPreviewBox.Name = "poppedPreviewBox";
            this.poppedPreviewBox.Size = new System.Drawing.Size(800, 385);
            this.poppedPreviewBox.TabIndex = 0;
            this.poppedPreviewBox.Resize += new System.EventHandler(this.poppedPreviewBox_Resize);
            // 
            // fileInfoPanel
            // 
            this.fileInfoPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fileInfoPanel.File = null;
            this.fileInfoPanel.Loading = false;
            this.fileInfoPanel.Location = new System.Drawing.Point(0, 385);
            this.fileInfoPanel.Name = "fileInfoPanel";
            this.fileInfoPanel.Padding = new System.Windows.Forms.Padding(3);
            this.fileInfoPanel.Size = new System.Drawing.Size(800, 65);
            this.fileInfoPanel.TabIndex = 1;
            // 
            // PopoutPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.poppedPreviewBox);
            this.Controls.Add(this.fileInfoPanel);
            this.Name = "PopoutPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Preview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PopoutPreview_FormClosing);
            this.Load += new System.EventHandler(this.PopoutPreview_Load);
            this.Resize += new System.EventHandler(this.PopoutPreview_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private Cyotek.Windows.Forms.ImageBox poppedPreviewBox;
        private FileInfoPanel fileInfoPanel;
    }
}