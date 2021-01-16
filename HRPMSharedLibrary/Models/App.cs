using HRPMSharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HRPMSharedLibrary.Models
{
    [Serializable]
    public class App : ISerializable
    {
        public string ExcutableName { get; set; }
        public string ProcessName { get; set; }
        public string HeaderText { get; set; }
        public string Content { get; set; }
        public AppType Type { get; set; }
        public App()
        {

        }

        public App(SerializationInfo info, StreamingContext context)
        {
            ExcutableName = (string)info.GetValue("ExcutableName", typeof(string));
            ProcessName = (string)info.GetValue("ProcessName", typeof(string));
            HeaderText = (string)info.GetValue("HeaderText", typeof(string));
            Content = (string)info.GetValue("Content", typeof(string));
            Type = (AppType)info.GetValue("Type", typeof(AppType));

        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ExcutableName", ExcutableName, typeof(string));
            info.AddValue("ProcessName", ProcessName, typeof(string));
            info.AddValue("HeaderText", HeaderText, typeof(string));
            info.AddValue("Content", Content, typeof(string));
            info.AddValue("Type", Type, typeof(AppType));

        }
    }

}
