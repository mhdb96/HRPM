using HRPMSharedLibrary.DataAccess;
using HRPMSharedLibrary.Enums;
using HRPMSharedLibrary.Models;
using HRPMUILibrary.Helpers;
using HRPMUILibrary.Logic.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMUILibrary.Logic.Files
{
    public class FilesManager
    {
        private static readonly FilesManager _instance = new FilesManager();
        private Queue<BinaryFile> files = new Queue<BinaryFile>();
        public bool IsSync
        {
            get
            {
                return files.Count <= 0;
            }
        }
        private FilesManager()
        {
            try
            {
                files = BinaryConnector.StaticLoad<Queue<BinaryFile>>(GlobalConfig.DataCacheFilePath);
            }
            catch (Exception e)
            {
                files = new Queue<BinaryFile>();
                LogsManager.LogToTextFile(new ErrorLog
                {
                    Severity = LogSeverity.Low,
                    Type = ErrorType.IOError,
                    Text = "No cahced data file was found. - " + e.Message
                });
            }
        }
        public static FilesManager GetFilesManager()
        {
            return _instance;
        }
        public void AddFile(BinaryFile file)
        {
            files.Enqueue(file);
            LogsManager.LogToTextFile(new Log
            {
                Severity = LogSeverity.Low,
                Type = LogType.FileCreated,
                Text = "New data file created."
            });
        }
        public async Task SyncFiles(User user)
        {
            var api = ApiHelper.GetApiHelper();
            while (files.Count > 0)
            {
                bool isPosted;
                try
                {
                    // !!!!!!!!!!!!!! NOT SENDING DATA TO SERVER !!!!!!!!!!!!!!!
                    isPosted = await api.PostFile(files.Peek(), user);
                    if (isPosted)
                    {
                        files.Dequeue();
                    }
                }
                catch (Exception)
                {
                    BinaryConnector.StaticSave(files, GlobalConfig.DataCacheFilePath);
                    LogsManager.LogToTextFile(new ErrorLog
                    {
                        Severity = LogSeverity.Medium,
                        Type = ErrorType.ConnectionError,
                        Text = "Failed to sync data files, no connection to the server."
                    }); 
                    break;
                }
            }
            if (files.Count == 0)
            {
                if (File.Exists(GlobalConfig.DataCacheFilePath))
                {
                    File.Delete(GlobalConfig.DataCacheFilePath);
                    LogsManager.LogToTextFile(new Log
                    {
                        Severity = LogSeverity.Low,
                        Type = LogType.FileDeleted,
                        Text = "Data cache file deleted after syncing properly."
                    });
                }
            }
        }

        public void CreateLocalFile(List<AppSession> data)
        {
            BinaryConnector.StaticSave(data, GlobalConfig.DataFilePath);
            BinaryFile file =  EncodeIntoBinaryFile(GlobalConfig.DataFilePath);
            AddFile(file);
        }

        private BinaryFile EncodeIntoBinaryFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            BinaryFile file = new BinaryFile();
            file.Bytes = bytes;
            file.Name = Path.GetFileName(path);
            return file;
        }

        public void DeleteLiveDataFile()
        {
            if (File.Exists(GlobalConfig.LiveDataFilePath))
            {
                File.Delete(GlobalConfig.LiveDataFilePath);
                LogsManager.LogToTextFile(new Log
                {
                    Severity = LogSeverity.Low,
                    Type = LogType.FileDeleted,
                    Text = "Live data file deleted at the end of the work day."
                });
            }
        }
    }
}
