using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ImageGallery.Database.Models
{
    public class File
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }
        // Full directory
        [Required]
        public string Directory { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Extension { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? FileCreatedTime { get; set; }

        public DateTime? FileModifiedTime { get; set; }
        public DateTime? LastUseTime { get; set; }
        public DateTime? LastChangeTime { get; set; }
        public byte[] Thumbnail { get; set; }
        [Required]
        public int TimesAccessed { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string Directory_fts { get; set; }
        [Required]
        public string Name_fts { get; set; }
        [Required]
        public string Name_tokenized_fts { get; set; }
        [Required]
        public string Extension_fts { get; set; }
        [Required]
        public ObservableHashSet<string> Custom_fts { get; set; }
        public ObservableHashSet<string> XMPTags_fts { get; set; }





        // Foreign Keys
        public int WatcherId { get; set; }
        public Watcher Watcher { get; set; }

        public void RecordUse()
        {
            LastUseTime = DateTime.UtcNow;
            TimesAccessed += 1;
        }

        public static ObservableHashSet<string> TagStringToHash(string tagString)
        {
            return tagString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToObservableHashSet();
        }

        public static string EnumToTagString(IEnumerable<string> tags)
        {
            return String.Join("|", tags);
        }


    }
}
