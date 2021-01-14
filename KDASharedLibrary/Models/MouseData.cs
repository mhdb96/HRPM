using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KDASharedLibrary.Models
{
    [Serializable]
    public class MouseData : ISerializable
    {
        public uint MouseClickCount { get; set; }
        public uint MouseClickTotalTime { get; set; }
        public uint StrokeHoldTimesAvg
        {
            get
            {
                if (MouseClickCount > 0)
                {
                    return MouseClickTotalTime / MouseClickCount;
                }
                else
                {
                    return 0;
                }
                
            }
        }

        public MouseData()
        {

        }

        public MouseData(SerializationInfo info, StreamingContext context)
        {
            MouseClickCount = (uint)info.GetValue("MouseClickCount", typeof(uint));
            MouseClickTotalTime = (uint)info.GetValue("MouseClickTotalTime", typeof(uint));            

        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("MouseClickCount", MouseClickCount, typeof(uint));
            info.AddValue("MouseClickTotalTime", MouseClickTotalTime, typeof(uint));            

        }
    }
}
