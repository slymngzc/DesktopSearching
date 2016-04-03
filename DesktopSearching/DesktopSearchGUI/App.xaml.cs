using DesktopSearchBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace DesktopSearchGUI
{
    /// <summary>
    /// App.xaml etkileşim mantığı
    /// </summary>
    public partial class App : Application
    {
        MainWindow mainWindow = new MainWindow();
        WaitWindow waitWindow = new WaitWindow();
        DesktopSearchingSpellCorrection.FileParser parser = new DesktopSearchingSpellCorrection.FileParser();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            DesktopSearchBaseClass search = new DesktopSearchBaseClass();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0,0,500);

            if (DesktopSearchBase.Constants.IsFinishedIndexing)
            {
                parser.Start();
                mainWindow.Show();
            }
            else
            {
                waitWindow.Show();
                dispatcherTimer.Start();
            }

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (DesktopSearchBase.Constants.IsFinishedIndexing)
            {
                dispatcherTimer.Stop();
                parser.Start();
                waitWindow.Hide();
                mainWindow.Show();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            File.Delete(DesktopSearchBase.Constants.DirectoryIndexPath);
            File.Delete(DesktopSearchBase.Constants.FileIndexPath);
        }
    }
}
