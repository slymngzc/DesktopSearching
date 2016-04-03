using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DesktopSearchBase
{
    public class Saving
    {
        public int temp = 10;
        public Saving()
        {
            SavingInit();

            while (true)
            {
                Thread.Sleep(10);
                if (Constants.isSuitibleForSaving)
                {
                    Constants.isSuitibleForSaving = false;

                    lock (Constants.mainDirectoryRepo)
                    {
                        lock (Constants.mainFileRepo)
                        {
                            this.SaveDirectoryInformation(Constants.mainDirectoryRepo);
                            this.SaveFileInformation(Constants.mainFileRepo);

                            Constants.mainDirectoryRepo.Clear();
                            Constants.mainFileRepo.Clear();
                        }
                    }
                }

                if (Constants.isTraversingFinished && Constants.mainDirectoryRepo.Count == 0 && Constants.mainFileRepo.Count == 0)
                {
                    //Console.WriteLine("Saving thread sonlandı");
                    //Console.WriteLine("Sonlandı:" + DateTime.Now);
                    Constants.isSavingFinished = true;
                    Constants.IsFinishedIndexing = true;
                    Thread.CurrentThread.Abort();
                }
            }

        }

        public void SaveFileInformation(List<string> filePathList)
        {
            string parserTemp = Constants.parserSymbols;

            using (FileStream fs = new FileStream(Constants.FileIndexPath, FileMode.Append))
            {
                BinaryWriter writer = new BinaryWriter(fs);
                filePathList.ForEach(f => writer.Write(f + parserTemp));
                //Console.WriteLine(filePathList.Count + " Adet dosya yazıldı");
                writer.Close();
            }
        }

        public void SaveDirectoryInformation(List<string> directoryPathList)
        {
            string parserTemp = Constants.parserSymbols;

            using (FileStream fs = new FileStream(Constants.DirectoryIndexPath, FileMode.Append))
            {
                BinaryWriter writer = new BinaryWriter(fs);
                directoryPathList.ForEach(f => writer.Write(f + parserTemp));
                //Console.WriteLine(directoryPathList.Count + " Adet dosya yazıldı");
                writer.Close();
            }
        }

        public void SavingInit()
        {
            if (File.Exists(Constants.DirectoryIndexPath))
            {
                File.Delete(Constants.DirectoryIndexPath);
            }

            if (File.Exists(Constants.FileIndexPath))
            {
                File.Delete(Constants.FileIndexPath);
            }
        }
    }
}
