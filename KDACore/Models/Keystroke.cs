using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace KDACore.Models
{
    [Serializable]
    public class Keystroke: ISerializable
    {
        public Key Key { get; set; } = new Key();
        public DateTime KeyDown { get; set; }
        public DateTime KeyUp { get; set; }
        public ushort HoldTime { 
            get 
            {
                return (ushort)new TimeSpan(KeyUp.Ticks - KeyDown.Ticks).TotalMilliseconds; 
            }
        }
        public Keystroke()
        {

        }
        public Keystroke(SerializationInfo info, StreamingContext context)
        {
            Key = (Key)info.GetValue("Key", typeof(Key));
            KeyDown = (DateTime)info.GetValue("KeyDown", typeof(DateTime));
            KeyUp = (DateTime)info.GetValue("KeyUp", typeof(DateTime));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Key", Key, typeof(Key));
            info.AddValue("KeyDown", KeyDown, typeof(DateTime));
            info.AddValue("KeyUp", KeyUp, typeof(DateTime));
        }
    }
}
