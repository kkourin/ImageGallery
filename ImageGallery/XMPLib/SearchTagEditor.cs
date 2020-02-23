using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.XMPLib
{
    public class SearchTagEditor
    {
        private ExifToolWrapper _etw;
        private const string searchFullTagString = "xmp-duckknife:SearchTags";
        private const string searchTagString = "Search Tags";
        private const string ConfigFileName = "exiftoolconfig.cfg";
        public static readonly string[] supportedExts = new string[] { "jpeg", "jpg", "png", "gif", "tif", "tiff" };

        public static ExifToolWrapper MakeSearchEtw()
        {
            string assemblyDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var configPath = Path.Combine(assemblyDir, ConfigFileName);
            return new ExifToolWrapper(configPath: configPath);
        }

        public void StartExifTool()
        {
            _etw.Start();
        }
        public SearchTagEditor(ExifToolWrapper etw)
        {
            _etw = etw;
        }

        public static bool usableExtension(string filename)
        {
            var extension = Path.GetExtension(filename);
            if (extension == null)
            {
                return false;
            }
            return supportedExts.Contains(extension.Trim(new char[] { '.' }));
        }

        public bool setSearchTag(string filename, IEnumerable<string> tags)
        {
            if (!usableExtension(filename))
            {
                return false;
            }
            var tagString = Database.Models.File.EnumToTagString(tags);
            var result = _etw.SetExifInto(
                filename,
                new Dictionary<string, string> {
                    { searchFullTagString, tagString } 
                }, true);
            return result.IsSuccess;
        }

        public string getSearchTag(string filename)
        {
            if (!usableExtension(filename))
            {
                return null;
            }
            string tag;

            var result = _etw.FetchExifFrom(filename, searchFullTagString);
            Console.WriteLine(result.Keys.FirstOrDefault());
            result.TryGetValue(searchTagString, out tag);
            return tag;
        }
    }
}
