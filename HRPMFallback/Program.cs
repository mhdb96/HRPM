using HRPMUILibrary.Helpers;
using HRPMSharedLibrary.Enums;
using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HRPMUILibrary.Logic.Logs;
using System.Runtime.CompilerServices;

namespace HRPMFallback
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
