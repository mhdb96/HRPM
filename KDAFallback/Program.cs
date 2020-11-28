using KDAUILibrary.Helpers;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KDAUILibrary.Logic.Logs;
using System.Runtime.CompilerServices;

namespace KDAFallback
{
    class Program
    {
        static async Task Main()
        {
            Log log = new Log();
            log.Text = "Program forceblly closed";
            log.Type = LogType.ProgramClosed;
            LogsManager.GetLogsManager().AddLog(log);
            await LogsManager.GetLogsManager().SyncLogs();
            Console.WriteLine("Logged");
            Console.WriteLine("ProgramClosed");
        }
    }
}
