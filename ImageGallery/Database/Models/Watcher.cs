using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
        public string Whitelist { get; set; }
        [Required]
        public bool Enabled { get; set; }

        public List<File> Files { get; set; }
    }
}
