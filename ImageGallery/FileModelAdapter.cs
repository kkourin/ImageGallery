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
    using Database;
    using Database.Models;
    class FileModelAdapter : ImageListView.ImageListViewItemAdaptor
    {
        private static readonly ImageConverter _imageConverter;
        string ColumnGroup { get; set; }
        static FileModelAdapter()
        {
            _imageConverter = new ImageConverter();
        }


        private bool disposed;
        public override void Dispose()
        {
            disposed = true;
        }

        public override Utility.Tuple<ColumnType, string, object>[] GetDetails(object key)
        {
            File file = (File)key;
            List<Utility.Tuple<ColumnType, string, object>> details = new List<Utility.Tuple<ColumnType, string, object>>();
            //details.Add(new Utility.Tuple<ColumnType, string, object>(ColumnType.Custom, "columnGroup", ColumnGroup));
            return details.ToArray();
        }

        public override string GetSourceImage(object key)
        {
            if (disposed)
            {
                return null;
            }
            File file = (File)key;
            return file.FullName;
        }

        public override Image GetThumbnail(object key, Size size, UseEmbeddedThumbnails useEmbeddedThumbnails, bool useExifOrientation)
        {
            if (disposed)
            {
                return null;
            }
            File file = (File)key;
            //return file.thumbnailImage;

            if (file.Thumbnail == null)
            {
                try
                {
                    Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                    return icon.ToBitmap();
                } catch (ArgumentException)
                {
                    return null;
                }
            }
            try
            {
                return MakeImageFromJpegBlob(file.Thumbnail);
            } catch (NotSupportedException)
            {
                return null;
            }
        }

        private Image MakeImageFromJpegBlob(byte[] blob)
        {
            return (Bitmap)_imageConverter.ConvertFrom(blob);
        }

        public override string GetUniqueIdentifier(object key, Size size, UseEmbeddedThumbnails useEmbeddedThumbnails, bool useExifOrientation)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
