using HRPMSharedLibrary.Enums;
using HRPMSharedLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HRPMSharedLibrary.Models
{
    [Serializable]
    public class KeystrokeData: ISerializable
    {
        public Key Key { get; set; } = new Key();
        public List<ushort>[] SeekTimes { get; set; } = new List<ushort>[FileHelper.GetEnumCount<KeysList>()];
        public List<ushort> HoldTimes { get; set; } = new List<ushort>();
        public KeystrokeData()
        {

        }
        public KeystrokeData(SerializationInfo info, StreamingContext context)
        {
            Key = (Key)info.GetValue("Key", typeof(Key));
            HoldTimes = (List<ushort>)info.GetValue("HoldTimes", typeof(List<ushort>));
            SeekTimes = (List<ushort>[])info.GetValue("SeekTimes", typeof(List<ushort>[]));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Key", Key, typeof(Key));
            info.AddValue("HoldTimes", HoldTimes, typeof(List<ushort>));
            info.AddValue("SeekTimes", SeekTimes, typeof(List<ushort>[]));
        }
    }
}
