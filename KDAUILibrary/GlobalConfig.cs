using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDAUILibrary
{
    public static class GlobalConfig
    {
        private readonly static string _mainFolderName = "HRPM_Files";
        private readonly static string _dataFilesFolderName = "Data";
        private readonly static string _logFilesFolderName = "Logs";
        private readonly static string _cacheFilesFolderName = ".cache";

        private static string _dataFileName= "data-file_{0}.hdf";
        private static string _logFileName= "log-file_{0}.hlf";
        private readonly static string _logCacheFileName= "log-cache-file.hcf";
        private readonly static string _dataCacheFileName = "data-cache-file.hcf";
        private readonly static string _liveDataFileName = "live-data-file.hcf";
        private readonly static string ProjectRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static string CurruntUser
        {
            get
            {
                return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
        }
        public static string MainFolderPath
        {
            get { return Path.Combine(ProjectRootFolder, _mainFolderName); }
        }
        public static string DataFilesFolderPath
        {
            get { return Path.Combine(ProjectRootFolder, _mainFolderName, _dataFilesFolderName); }
        }
        public static string LogFilesFolderPath
        {
            get { return Path.Combine(ProjectRootFolder, _mainFolderName, _logFilesFolderName); }
        }
        public static string CacheFilesFolderPath
        {
            get { return Path.Combine(ProjectRootFolder, _mainFolderName, _cacheFilesFolderName); }
        }
        public static string DataFilePath
        {
            get
            {
                return Path.Combine(ProjectRootFolder, _mainFolderName, _dataFilesFolderName, DateTime.Now.ToString("dd-MM-yyyy"), string.Format(_dataFileName, DateTime.Now.ToString("HH"))); 
            }
        }
        public static string logFilePath
        {
            get 
            {
                return Path.Combine(ProjectRootFolder, _mainFolderName, _logFilesFolderName, string.Format( _logFileName, DateTime.Now.ToString("dd-MM-yyyy")));
            }
        }
        public static string LogCacheFilePath
        {
            get { return Path.Combine(ProjectRootFolder, _mainFolderName, _cacheFilesFolderName, _logCacheFileName); }
        }
        public static string DataCacheFilePath
        {
            get { return Path.Combine(ProjectRootFolder, _mainFolderName, _cacheFilesFolderName, _dataCacheFileName); }
        }
        public static string LiveDataFilePath
        {
            get { return Path.Combine(ProjectRootFolder, _mainFolderName, _cacheFilesFolderName, _liveDataFileName); }
        }

    }
}
