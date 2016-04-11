using System;

namespace DesktopSearchingSpellCorrection
{
    public class FileSystemModel
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string Extension { get; set; }

        public string ASCIIValue { get; set; }

        public int Distance { get; set; }
    }
}
