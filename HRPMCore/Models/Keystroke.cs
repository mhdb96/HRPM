using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HRPMCore.Models
{
    [Serializable]
    public class Keystroke: ISerializable
    {
        public Key Key { get; set; } = new Key();
        public uint KeyDown { get; set; }
        public uint KeyUp { get; set; }
        public ushort HoldTime { 
            get 
            {
                return (ushort)(KeyUp - KeyDown); 
            }
        }
        public Keystroke()
        {

        }
        public Keystroke(SerializationInfo info, StreamingContext context)
        {
            Key = (Key)info.GetValue("Key", typeof(Key));
            KeyDown = (uint)info.GetValue("KeyDown", typeof(uint));
            KeyUp = (uint)info.GetValue("KeyUp", typeof(uint));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Key", Key, typeof(Key));
            info.AddValue("KeyDown", KeyDown, typeof(uint));
            info.AddValue("KeyUp", KeyUp, typeof(uint));
        }
    } 
}
