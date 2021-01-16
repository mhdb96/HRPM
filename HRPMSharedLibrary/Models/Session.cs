using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HRPMSharedLibrary.Models
{
    [Serializable]
    public class Session: ISerializable
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Program { get; set; }
        public string User { get; set; }
        public Session()
        {

        }
        public Session(SerializationInfo info, StreamingContext context)
        {
            StartTime = (DateTime)info.GetValue("StartTime", typeof(DateTime));
            EndTime = (DateTime)info.GetValue("EndTime", typeof(DateTime));
            Program = (string)info.GetValue("Program", typeof(string));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("StartTime", StartTime, typeof(DateTime));
            info.AddValue("EndTime", EndTime, typeof(DateTime));
            info.AddValue("Program", Program, typeof(string));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"This session of {Program} program started at {StartTime} and finished at {EndTime}");
            return builder.ToString();
        }
    }
}   