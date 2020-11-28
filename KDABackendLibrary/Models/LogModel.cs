using KDASharedLibrary.Enums;
using KDABackendLibrary.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDABackendLibrary.Models
{
    [JsonConverter(typeof(EnumToStringConverter))]
    public class LogModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        //public string Severity { get; set; }
        public LogType Type { get; set; }
        public LogSeverity Severity { get; set; }
        public UserModel User { get; set; } = new UserModel();
        public DateTime CreatedAt { get; set; }
        public DateTime Date { get; set; }
    }
}
