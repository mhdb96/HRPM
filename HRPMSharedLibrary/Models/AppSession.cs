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
    public class AppSession : ISerializable
    {        
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration 
        {
            get 
            {
                return new TimeSpan(EndTime.Ticks - StartTime.Ticks).TotalSeconds;
            } 
        }        
        public KeyboardData KeyboardData { get; set; }
        public MouseData MouseData { get; set; }
        public App App { get; set; } = new App();

        public AppSession()
        {

        }

        public AppSession(SerializationInfo info, StreamingContext context)
        {
            StartTime = (DateTime)info.GetValue("StartTime", typeof(DateTime));
            EndTime = (DateTime)info.GetValue("EndTime", typeof(DateTime));
            KeyboardData = (KeyboardData)info.GetValue("KeyboardData", typeof(KeyboardData));
            MouseData = (MouseData)info.GetValue("MouseData", typeof(MouseData));
            App = (App)info.GetValue("App", typeof(App));

        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("StartTime", StartTime, typeof(DateTime));
            info.AddValue("EndTime", EndTime, typeof(DateTime));
            info.AddValue("KeyboardData", KeyboardData, typeof(KeyboardData));
            info.AddValue("MouseData", MouseData, typeof(MouseData));
            info.AddValue("App", App, typeof(App));



        }

    }
}
