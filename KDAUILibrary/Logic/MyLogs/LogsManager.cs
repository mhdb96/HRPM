using KDAUILibrary.Helpers;
using KDASharedLibrary.DataAccess;
using KDASharedLibrary.Models;
using KDASharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using KDASharedLibrary.Enums;

namespace KDAUILibrary.Logic.Logs
{
    public class LogsManager
    {
        private static readonly LogsManager _instance = new LogsManager();
        private Queue<Log> logs;
        public bool IsSync 
        { 
            get 
            {
                return logs.Count <= 0;
            } 
        }

        private LogsManager()
        {
            try
            {
                logs = BinaryConnector.StaticLoad<Queue<Log>>(GlobalConfig.LogCacheFilePath);
            }
            catch (Exception)
            {
                LogToTextFile(new ErrorLog
                {
                    Severity = LogSeverity.Low,
                    Type = ErrorType.IOError,
                    Text = "No cahced log file was found."
                });
                logs = new Queue<Log>();
            }
        }
        public static LogsManager GetLogsManager()
        {
            return _instance;
        }
        public void AddLog(Log log)
        {
            LogToTextFile(log);
            logs.Enqueue(log);
        }

        public async Task SyncLogs()
        {
            var api = ApiHelper.GetApiHelper();
            while(logs.Count > 0)
            {
                bool isPosted;
                try
                {
                    // !!!!!!!!!!!!!! NOT SENDING DATA TO SERVER !!!!!!!!!!!!!!!
                    isPosted = true;/*await api.PostLog(logs.Peek());*/
                    if (isPosted)
                    {
                        logs.Dequeue();
                    }
                }
                catch (Exception)
                {
                    LogToTextFile(new ErrorLog
                    {
                        Severity = LogSeverity.Medium,
                        Type = ErrorType.ConnectionError,
                        Text = "Failed to sync logs, no connection to the server."
                    });
                    BinaryConnector.StaticSave(logs, GlobalConfig.LogCacheFilePath);
                    break;
                } 
            }
            if(logs.Count == 0)
            {
                if (File.Exists(GlobalConfig.LogCacheFilePath))
                {
                    File.Delete(GlobalConfig.LogCacheFilePath);
                    LogToTextFile(new Log
                    {
                        Severity = LogSeverity.Low,
                        Type = LogType.FileDeleted,
                        Text = "Log cache file deleted after syncing properly."
                    });
                }
            }
        }

        public static void LogToTextFile(ILog log)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(GlobalConfig.logFilePath));
            File.AppendAllText(GlobalConfig.logFilePath, log.GetLogText() + Environment.NewLine);
        }
    }
}
