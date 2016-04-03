using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DesktopSearchBase
{
    public class DesktopSearchBaseClass
    {

        public DesktopSearchBaseClass()
        {
            Continue();
        }

        public void Start()
        {
            //Console.WriteLine("Başladı:" + DateTime.Now);

            Thread traversingThread = new Thread(() => new Traversing(new List<string>() { @"D:\Videolar" }));
            Thread savingThread = new Thread(() => new Saving());

            traversingThread.Start();
            savingThread.Start();
        }

        public void Continue()
        {
            if (!checkIsValidIndexFiles())
            {
                Start();
            }
            else
            {
                Constants.IsFinishedIndexing = true;
            }
        }

        public bool checkIsValidIndexFiles()
        {
            if (File.Exists(Constants.DirectoryIndexPath) && File.Exists(Constants.FileIndexPath))
            {
                DateTime directoryLCT = File.GetCreationTimeUtc(Constants.DirectoryIndexPath);

                if ((DateTime.UtcNow - directoryLCT).Days < 2)
                {

                    return true;
                }
                else
                {
                    try
                    {
                        File.Delete(Constants.DirectoryIndexPath);
                        File.Delete(Constants.FileIndexPath);
                    }
                    catch (Exception ex)
                    { }
                }
            }

            return false;
        }
    }
}
