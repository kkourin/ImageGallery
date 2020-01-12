using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Manina.Windows.Forms;
namespace ImageGallery
{
    public class ImageGrouper : ImageListView.IGrouper
    {
        public static Dictionary<string, int> TypeDict = new Dictionary<string, int>
        {
            {"Recently Used", 0 },
            {"Recently Created", 1 },
            {"Frequently Used", 2 },
            {"Search Result", 3 },
            {"All Items", 4 }
        };
        public ImageListView.GroupInfo GetGroupInfo(ImageListViewItem item)
        {
            var text = item.SubItems["Type"].Text;
            //Console.WriteLine(text + TypeDict[text]);

            return new ImageListView.GroupInfo(text, TypeDict[text]);
        }
    }
}
