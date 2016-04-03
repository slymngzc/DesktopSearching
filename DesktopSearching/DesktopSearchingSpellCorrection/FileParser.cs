using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopSearchingSpellCorrection
{
    public class FileParser
    {
        public FileParser()
        {

        }

        public void Start()
        {
            DesktopSearchBase.Constants.IsFinishedIndexing = false;

            var directoryInfos = File.ReadAllText(DesktopSearchBase.Constants.DirectoryIndexPath)
                .Split(DesktopSearchBase.Constants.parserSymbols.ToCharArray())
                .Where(s => s.Length > 0).ToList<string>();

            var fileInfos = File.ReadAllText(DesktopSearchBase.Constants.FileIndexPath).
                Split(DesktopSearchBase.Constants.parserSymbols.ToCharArray())
                .Where(s => s.Length > 0).ToList<string>();

            foreach (var info in directoryInfos)
            {
                try
                {
                    FileSystemModel temp = new FileSystemModel();
                    temp.Path = info;
                    temp.Name = info.Split('\\').LastOrDefault();
                    temp.ASCIIValue = string.Empty;

                    Constants.directoryList.Add(temp);
                }
                catch (Exception ex)
                {

                }
            }

            foreach (var info in fileInfos)
            {
                try
                {
                    FileSystemModel temp = new FileSystemModel();
                    temp.Path = info;
                    temp.Name = Path.GetFileNameWithoutExtension(info);
                    temp.Extension = Path.GetExtension(info);
                    temp.ASCIIValue = string.Empty;

                    Constants.fileList.Add(temp);
                }
                catch (Exception ex)
                {

                }
            }

            DesktopSearchBase.Constants.IsFinishedIndexing = true;
        }
    }
}
