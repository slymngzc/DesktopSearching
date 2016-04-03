using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DesktopSearchBase
{
    /// <summary>
    /// Dosyalar arasında dolaşmak için
    /// </summary>
    public class Traversing
    {
        //Bütün Mantıksal Sürücüler
        public List<string> allLocalRoots = new List<string>();
        //*******

        public Traversing(List<string> searchingDrivers = null)
        {
            if (searchingDrivers == null)
            {
                InitTraversing();
            }
            else
            {
                allLocalRoots = searchingDrivers;
            }



            if (allLocalRoots.Count > 0)
            {
                foreach (var root in allLocalRoots)
                {
                    this.StartTraversing(root);
                }
            }

            this.EndTraversing();
        }

        public void InitTraversing()
        {
            allLocalRoots = Directory.GetLogicalDrives().ToList<string>();
        }

        public void StartTraversing(string root)
        {
            Constants.isLast = false;

            //Takas değişkenleri
            List<string> temp1 = new List<string>();
            List<string> temp2 = new List<string>();
            //*****************************************

            //İkincil Directory ve File Depoları
            List<string> secondaryDirectoryRepo = new List<string>();
            List<string> secondaryFileRepo = new List<string>();
            //**************************************************************

            temp1 = root.GetDirectory();

            secondaryDirectoryRepo.AddRange(temp1);
            secondaryFileRepo.AddRange(root.GetFile());

            while (!Constants.isLast)
            {

                //Önemli
                temp2.Clear();
                //*****

                foreach (var t1 in temp1)
                {
                    temp2.AddRange(t1.GetDirectory());
                    secondaryFileRepo.AddRange(t1.GetFile());
                }

                if (temp2.Count == 0)
                {
                    Constants.isLast = true;
                }
                else
                {
                    secondaryDirectoryRepo.AddRange(temp2);

                    temp1.Clear();

                    foreach (var t2 in temp2)
                    {
                        temp1.AddRange(t2.GetDirectory());
                        secondaryFileRepo.AddRange(t2.GetFile());
                    }
                }

                if (temp1.Count == 0)
                {
                    Constants.isLast = true;
                }
                else
                {
                    secondaryDirectoryRepo.AddRange(temp1);
                }

                lock (Constants.mainDirectoryRepo)
                {
                    lock (Constants.mainFileRepo)
                    {
                        //Ana dosya ve file listesine ekleme
                        Constants.mainDirectoryRepo.AddRange(secondaryDirectoryRepo);
                        Constants.mainFileRepo.AddRange(secondaryFileRepo);
                        //***********

                        //Boşaltma
                        secondaryDirectoryRepo.Clear();
                        secondaryFileRepo.Clear();

                        //Dosyaya yazmak için uygun
                    }
                }

                Constants.isSuitibleForSaving = true;
            }
        }

        public void EndTraversing()
        {
            Constants.isTraversingFinished = true;
            //Console.WriteLine("Traversing thread sonlandı");
        }

        public static List<string> ConvertArrayToList(string[] input)
        {

            if (input.Length == 0)
            {
                return new List<string>();
            }
            else
            {
                return input.ToList<string>();
            }
        }
    }
}
