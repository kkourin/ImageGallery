using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;


namespace ImageGallery
{
    using Database;
    static class Program
    {
        public static void InitDatabase()
        {
            var config = new DatabaseConfig();
            FilesContext.Config = config;
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
            InitDatabase();
            //Properties.Settings.Default
            var monitor = WatcherMonitor.InitMonitorFromDB();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(monitor));
        }
    }
}
