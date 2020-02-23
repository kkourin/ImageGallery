using ImageGallery.Database;
using ImageGallery.Database.Models;
using Manina.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Manina.Windows.Forms.ImageListView;

namespace ImageGallery
{
    public class FolderImageGrouper : ImageListView.IGrouper
    {
        public Dictionary<int, (string relPath, int _filePathOrder)> IdToPath { get; set; }
        private static string relPath(ImageListViewItem item)
        {
            return item.SubItems["Relative Directory"].Text;
        }
        public FolderImageGrouper(List<ImageListViewItem> items)
        {
            IdToPath = new Dictionary<int, (string relPath, int _filePathOrder)>();

            var paths = new SortedList<string, int>();
            foreach (var item in items)
            {
                var itemRelPath = relPath(item);
                if (!paths.ContainsKey(itemRelPath))
                {
                    paths.Add(itemRelPath, 0);
                }
            }
            int index = 0;
            foreach (var item in paths.Keys.ToList())
            {
                paths[item] = index;
                index++;
            }

            foreach (var item in items)
            {
                var file = item.VirtualItemKey as File;
                var itemRelPath = relPath(item);
                IdToPath.Add(file.Id, (itemRelPath, paths[itemRelPath]));
            }
        }
        public ImageListView.GroupInfo GetGroupInfo(ImageListViewItem item)
        {
            var file = item.VirtualItemKey as File;
            if (IdToPath.ContainsKey(file.Id))
            {
                var value = IdToPath[file.Id];
                return new ImageListView.GroupInfo(value.relPath, value._filePathOrder);
            }
            else
            {
                return new ImageListView.GroupInfo("Unknown", IdToPath.Count);
            }
        }
    }

    public class RelativePathComparer : IComparer<ImageListViewItem>
    {
        private FilesContext.SortColumn _sortColumn;
        private Dictionary<int, (string relPath, int _filePathOrder)> _idToPath;

        private static string relPath(ImageListViewItem item)
        {
            return item.SubItems["Relative Directory"].Text;
        }
        
        public RelativePathComparer(FilesContext.SortColumn sortColumn, Dictionary<int, (string relPath, int _filePathOrder)> idToPath)
        {
            _sortColumn = sortColumn;
            _idToPath = idToPath;
        }
        public int Compare(ImageListViewItem x, ImageListViewItem y)
        {
            var xFile = x.VirtualItemKey as File;
            var yFile = y.VirtualItemKey as File;
            if (xFile == null || !_idToPath.ContainsKey(xFile.Id))
            {
                return 1;
            }
            if (yFile == null || !_idToPath.ContainsKey(yFile.Id))
            {
                return -1;
            }
            var xRelPathOrder = _idToPath[xFile.Id]._filePathOrder;
            var yRelPathOrder = _idToPath[yFile.Id]._filePathOrder;
            
            var compareResult = xRelPathOrder.CompareTo(yRelPathOrder);
            if (compareResult != 0)
            {
                return compareResult;
            }

            return FilesContext.ComparerFromSort(_sortColumn).Compare(xFile, yFile);

        }
    }
}
