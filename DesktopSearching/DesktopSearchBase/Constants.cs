using System;
using System.Collections.Generic;

namespace DesktopSearchBase
{
    public static class Constants
    {
        public static string DirectoryIndexPath = @"D:\directoryIndex.data";

        public static string FileIndexPath = @"D:\fileIndex.data";

        //Ana Directory ve File Depoları
        public static List<string> mainDirectoryRepo = new List<string>();
        public static List<string> mainFileRepo = new List<string>();
        //**************************************************************

        //THREAD DEĞİŞKENLERİ
        //Root sonunu kontrol için
        public static bool isLast = false;
        //Dosyaya kayda uygun mu kontrolü
        public static bool isSuitibleForSaving = false;
        //Programdan çıkış için
        public static bool isTraversingFinished = false;
        //Traversing bitiş kontrolü
        public static bool isSavingFinished = false;

        public static bool IsFinishedIndexing = false;

        public static string parserSymbols = "**";
    }
}

