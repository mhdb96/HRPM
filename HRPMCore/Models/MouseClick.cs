using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HRPMCore.Models
{
    [Serializable]
    public class MouseClick : ISerializable
    {
        public MouseButton MouseButton { get; set; } = new MouseButton();
        public uint ButtonDown { get; set; }
        public uint ButtonUp { get; set; }
        public uint HoldTime
        {
            get
            {
                return (uint)(ButtonUp - ButtonDown);
            }
        }
        public MouseClick()
        {

        }
        public MouseClick(SerializationInfo info, StreamingContext context)
        {
            MouseButton = (MouseButton)info.GetValue("MouseButton", typeof(MouseButton));
            ButtonDown = (uint)info.GetValue("ButtonDown", typeof(uint));
            ButtonUp = (uint)info.GetValue("ButtonUp", typeof(uint));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("MouseButton", MouseButton, typeof(MouseButton));
            info.AddValue("ButtonDown", ButtonDown, typeof(uint));
            info.AddValue("ButtonUp", ButtonUp, typeof(uint));
        }
    }
}
