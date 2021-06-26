using System;
using System.IO;

namespace LotManager.Utilities
{
    public static class FileSystemConstants
    {
        private static string AppName = "ParkingLotManager";
        public static string AppPath
        {
            get
            {
                var DocsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                return Path.Combine(DocsPath, AppName);
            }
        }
    }
    public static class FileSystemUtilities
    {
        /// <summary>
        /// Utility function for checking if a file exists
        /// </summary>
        /// <param name="fullpath">
        /// Full, well-formed path to the file 
        /// </param>
        /// <returns>
        /// <b>TRUE</b> if a file exists at the given path. <br/>
        /// <b>FALSE</b> if a file does not exist at the given path.
        /// </returns>
        public static bool FileExists(string fullpath)
        {
            FileInfo fInfo = new FileInfo(fullpath);
            return fInfo.Exists;
        }

        /// <summary>
        /// Utility function for checking if a directory exists
        /// </summary>
        /// <param name="dirPath">
        /// Full, well-formed path to Directory
        /// </param>
        /// <returns>
        /// <b>TRUE</b> if a directory exists at the given path. <br/>
        /// <b>FALSE</b> if a directory does not exist at the given path.
        /// </returns>
        public static bool DirectoryExists(string dirPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(dirPath);
            return dInfo.Exists;
        }
    }
}