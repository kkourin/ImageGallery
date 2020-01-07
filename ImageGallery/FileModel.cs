using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace ImageGallery
{
    public class FileModel
    {
        public Guid guid { get; set; }
        public string path { get; set; }
        public string meta { get; set; }
        public byte[] thumbnail { get; set; }

        public FileInfo fileInfo { get
            {
                return new FileInfo(path);
            }
        }
        public FileModel(string path, string meta, byte[] thumbnail) : this(path, meta)
        {
            this.thumbnail = thumbnail;
        }

        public FileModel(string path, string meta)
        {
            this.guid = Helpers.NewSequentialId();
            this.path = path;
            this.meta = meta;
        }

        public static FileModel makeFileModel(string path)
        {
            var file = new FileInfo(path);
            if (!file.Exists)
            {
                throw new FileNotFoundException($"Could not find file with path {path}.");
            }
            Image img = Helpers.LoadImage(file);
            if (img == null)
            {
                return new FileModel(path, "");
            } else
            {
                Image thumbnailImage = Helpers.CreateThumbnail(img, 150, 150);
                byte[] thumbnail = Helpers.ImageToJpegByteArray(thumbnailImage);
                Console.WriteLine("Thumbnail size: {0}", thumbnail.Length);
                return new FileModel(path, "IMAGE", thumbnail);
                
            }
            
        }

    }
}
