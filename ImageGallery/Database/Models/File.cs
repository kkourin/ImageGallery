using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime? LastUseTime { get; set; }
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
        public string Custom_fts { get; set; }




        // Foreign Keys
        public int WatcherId { get; set; }
        public Watcher Watcher { get; set; }


    }
}
