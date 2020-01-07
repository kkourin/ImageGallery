using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manina.Windows.Forms;
using System.Threading;

namespace ImageGallery
{
    class FileModelAdapter : ImageListView.ImageListViewItemAdaptor
    {
        private static readonly ImageConverter imageConverter;

        static FileModelAdapter()
        {
            imageConverter = new ImageConverter();
        }

        private bool disposed;
        public override void Dispose()
        {
            disposed = true;
        }

        public override Utility.Tuple<ColumnType, string, object>[] GetDetails(object key)
        {
            FileModel file = (FileModel)key;
            List<Utility.Tuple<ColumnType, string, object>> details = new List<Utility.Tuple<ColumnType, string, object>>();
            details.Add(new Utility.Tuple<ColumnType, string, object>(ColumnType.Custom, "meta", file.meta));
            return details.ToArray();
        }

        public override string GetSourceImage(object key)
        {
            if (disposed)
            {
                return null;
            }
            Thread.Sleep(1000);
            FileModel file = (FileModel)key;
            return file.path;
        }

        public override Image GetThumbnail(object key, Size size, UseEmbeddedThumbnails useEmbeddedThumbnails, bool useExifOrientation)
        {
            if (disposed)
            {
                return null;
            }
            FileModel file = (FileModel)key;
            //return file.thumbnailImage;
            if (!file.fileInfo.Exists)
            {
                return null;
            }
            try
            {
                if (file.thumbnail == null)
                {
                    Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(file.fileInfo.FullName);
                    return icon.ToBitmap();
                }
                return MakeImageFromJpegBlob(file.thumbnail);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Image MakeImageFromJpegBlob(byte[] blob)
        {
            return (Bitmap)imageConverter.ConvertFrom(blob);
        }

        public override string GetUniqueIdentifier(object key, Size size, UseEmbeddedThumbnails useEmbeddedThumbnails, bool useExifOrientation)
        {

            FileModel file = (FileModel)key;
            return file.guid.ToString();
        }
    }
}
