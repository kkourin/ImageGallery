using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using log4net;
using log4net.Config;


namespace ImageGallery
{
    using Database;
    using LibVLCSharp.Shared;
    using System.Threading;
    using XMPLib;

    static class Program
    {

#if DEBUG
        static Mutex mutex = new Mutex(true, "{79e2b92a-1b4d-4c63-afd1-086dd4ca20d7}");
#else
        static Mutex mutex = new Mutex(true, "{cfc6b5e4-6337-4744-8844-b554f9cc62ea}");
#endif
        public static void InitDatabase(SearchTagEditor editor)
        {

            FilesContext.videoThumbnailExtractor = new VideoThumbnailExtractor();
            FilesContext.searchTagEditor = editor;
            using (var ctx = new FilesContext())
            {
                ctx.Database.Migrate();
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    using (var etw = SearchTagEditor.MakeSearchEtw())
                    {
                        var tagEditor = new SearchTagEditor(etw);
                        tagEditor.StartExifTool();

                        XmlConfigurator.Configure();
                        Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
                        Core.Initialize();

                        InitDatabase(tagEditor);
                        var monitor = WatcherMonitor.InitMonitorFromDB();
                        var libVLC = new LibVLC();
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new MainForm(monitor, libVLC, tagEditor));
                    }

                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else
            {
                Helpers.DoShowMe();
            }

            
        }
    }
}
