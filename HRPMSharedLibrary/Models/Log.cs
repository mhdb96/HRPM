using HRPMSharedLibrary.Enums;
using HRPMSharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HRPMSharedLibrary.Models
{
    [Serializable]
    public class Log : ISerializable, ILog
    {
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public User User { get; set; } = new User();
        public LogType Type { get; set; }
        public LogSeverity Severity { get; set; }

        public Log()
        {

        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Text", Text, typeof(string));
            info.AddValue("Date", Date, typeof(DateTime));
            info.AddValue("User", User, typeof(User));
            info.AddValue("Type", Type, typeof(LogType));
            info.AddValue("Severity", Severity, typeof(LogSeverity));
        }

        public string GetLogText()
        {
            return $"{Date:dd/MM/yyyy HH:mm:ss} - {Type} ({Severity}) - {Text}";
        }

        public Log(SerializationInfo info, StreamingContext context)
        {
            Text = (string)info.GetValue("Text", typeof(string));
            Date = (DateTime)info.GetValue("Date", typeof(DateTime));
            User = (User)info.GetValue("User", typeof(User));
            Type = (LogType)info.GetValue("Type", typeof(LogType));
            Severity = (LogSeverity)info.GetValue("Severity", typeof(LogSeverity));
        }

    }
}
