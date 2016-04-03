using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DesktopSearchBase
{
    public static class ControllingExt
    {
        private static FileAttributes fileAttrb;

        public static bool IsDirectory(this string path)
        {
            fileAttrb = File.GetAttributes(path);

            if (fileAttrb == FileAttributes.Directory)
                return true;
            else
                return false;
        }

        public static bool IsFileOrDirectoryExist(this string path)
        {
            return (Directory.Exists(path) || File.Exists(path));
        }

        public static List<string> GetDirectory(this string root)
        {

            try
            {
                string[] input = Directory.GetDirectories(root);
                return input.ToList<string>();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }


        public static List<string> GetFile(this string root)
        {

            try
            {
                string[] input = Directory.GetFiles(root);
                return input.ToList<string>();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

    }
}
