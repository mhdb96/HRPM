using HRPMSharedLibrary.Enums;
using HRPMSharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMSharedLibrary.Models
{
    public class ErrorLog : ILog
    {
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public ErrorType Type { get; set; }
        public LogSeverity Severity { get; set; }
        public string GetLogText()
        {
            return $"{Date:dd/MM/yyyy HH:mm:ss} - {Type} ({Severity}) - {Text}";
        }
    }
}
