using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Windows.Forms;

namespace ImageGallery.Database.Models
{
    public class Watcher
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Directory { get; set; }
        [Required]
        public HashSet<string> Whitelist { get; set; }
        public bool? Enabled { get; set; }
        public bool? GenerateVideoThumbnails { get; set; }
        public bool? ScanSubdirectories { get; set; }

        public Keys ShortcutKeys { get; set; }
        public bool GlobalShortcut { get; set; }

        public List<File> Files { get; set; }


        public static HashSet<string> ExtensionStringToHash(string whitelist)
        {
            return whitelist.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToHashSet<string>();
        }

        public static string HashToExtensionString(HashSet<string> extensions)
        {
            return String.Join(", ", extensions);
        }

        public bool WhitelistedFile(string filename)
        {
            var extension = Path.GetExtension(filename);
            if (!Whitelist.Any())
            {
                return true;
            }
            return Whitelist.Contains(extension);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
