using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Manina.Windows.Forms;
using Manina.Windows.Forms.ImageListViewRenderers;

namespace ImageGallery
{
    public partial class MainForm : Form
    {
        static FileModelAdapter adapter;

        PopoutPreview popoutPreview;
        Image previewImage;
        Database.WatcherMonitor _monitor;

        static MainForm()
        {
            adapter = new FileModelAdapter();
        }
        private List<FileModel> GetFileModels(List<string> files)
        {
            List<Task<FileModel>> tasks = new List<Task<FileModel>>();
            var timer = new Stopwatch();
            timer.Start();
            foreach (string path in files.Take(50))
            {
                tasks.Add(Task<FileModel>.Factory.StartNew(() => FileModel.makeFileModel(path)));
            }
            var result = Task.WhenAll(tasks).Result;

            TimeSpan timeTaken = timer.Elapsed;
            Console.WriteLine("Time taken: " + timeTaken.ToString(@"m\:ss\.fff"));
            GC.Collect();
            return result.ToList<FileModel>();
        }
        public MainForm(Database.WatcherMonitor monitor)
        {
            InitializeComponent();
            _monitor = monitor;
            //List<string> files = Directory.GetFiles("E:\\danbooru2018\\original\\0005").ToList();
            //List<string> files = Directory.GetFiles("E:\\danbooru2018\\original\\0099").ToList();
            List<string> files = new List<string>();

            ilvThumbs.SetRenderer(new XPRenderer());
            var models = GetFileModels(files);
            foreach (var model in models)
            {
                //Console.WriteLine(model.guid);

            }

            foreach (FileModel model in models)
            {
                ilvThumbs.Items.Add(model, model.path, adapter);
                //ilvThumbs.Items.Add(model.path);
            }

            var col = new ImageListView.ImageListViewColumnHeader(ColumnType.Custom, "meta", "Meta Column");
            ilvThumbs.Columns.Add(ColumnType.Name);
            ilvThumbs.Columns.Add(col);

            popoutPreview = new PopoutPreview();
            popoutPreview.FormClosing += new FormClosingEventHandler(popoutPreview_Closing);
        }

        void popoutPreview_Closing(object sender, FormClosingEventArgs e)
        {
            splitContainer1.Panel2Collapsed = false;
            RefreshPreviews();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //GC.Collect();
            //ilvThumbs.View = Manina.Windows.Forms.View.Details;
            foreach(var f in ilvThumbs.SelectedItems)
            {
                //Console.WriteLine(f.Text);
            }
            ShowPopoutPreview();
        }

        private void ilvThumbs_SelectionChanged(object sender, EventArgs e)
        {
            if (ilvThumbs.SelectedItems.Count != 1)
            {
                return;
            }
            FileModel file = (FileModel)ilvThumbs.SelectedItems[0].VirtualItemKey;
            if (!splitContainer1.Panel2Collapsed || popoutPreview.Visible)
            {
                Image image = Helpers.LoadImage(file.fileInfo);
                if (image == null)
                {
                    return;
                }
                previewImage = image;
                RefreshPreviews();
            }

        }

        private void ilvThumbs_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            FileModel file = (FileModel)e.Item.VirtualItemKey;
            CopyToClipboard(new List<FileModel> { file });
        }

        private void CopyToClipboard(List<FileModel> files)
        {
            if (files.Count == 0)
            {
                return;
            }
            if (files.Count == 1)
            {
                FileModel file = files[0];
                CopyToClipboard(file);
            }
        }
        private void CopyToClipboard(FileModel file)
        {
            FileInfo fileInfo = file.fileInfo;
            if (Helpers.IsImageFile(file.fileInfo.Name))
            {
                Image image = Helpers.LoadImage(fileInfo);
                if (image == null)
                {
                    return;
                }
                Clipboard.SetImage(image);
                infoLabel.Text = String.Format(
                    "Copied image {0} to clipboard.",
                    Helpers.Truncate(fileInfo.Name, 30));
            }
            else
            {
                if (file.fileInfo.Exists)
                {
                    StringCollection sc = new StringCollection();
                    sc.Add(file.path);
                    Clipboard.SetFileDropList(sc);
                    infoLabel.Text = String.Format(
                        "Copied file {0} to clipboard.",
                        Helpers.Truncate(fileInfo.Name, 30));
                }
            }
        }

        private void RefreshPreviews()
        {
            if (!splitContainer1.Panel2Collapsed)
            {
                previewBox.Image = previewImage;
            }
            if (popoutPreview.Visible)
            {
                Console.WriteLine("Preview Visible");
                popoutPreview.PreviewImage = previewImage;
            }

        }
        private void ShowPopoutPreview()
        {
            if (popoutPreview == null)
            {
                return;
            }
            splitContainer1.Panel2Collapsed = true;
            popoutPreview.PreviewImage = previewImage;
            popoutPreview.Left = Left + Width;
            popoutPreview.Top = Top;

            popoutPreview.Width = previewBox.Width;
            popoutPreview.Height = Height;
            popoutPreview.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //GC.Collect();
            //ilvThumbs.View = Manina.Windows.Forms.View.Details;
            foreach (var f in ilvThumbs.SelectedItems)
            {
                //Console.WriteLine(f.Text);
            }
            ShowPopoutPreview();
        }

        private void usedOrder_Click(object sender, EventArgs e)
        {

        }

        private void watchersButton_Click_1(object sender, EventArgs e)
        {
            var watcherListForm = new WatcherListForm(_monitor);
            watcherListForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void searchTextBox_Click(object sender, EventArgs e)
        {

        }
    }
    


}
