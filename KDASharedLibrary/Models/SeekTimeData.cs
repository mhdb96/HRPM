using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KDASharedLibrary.Models
{
    [Serializable]
    public class SeekTimeData : ISerializable
    {
        public List<ushort> SeekTimes { get; set; } = new List<ushort>();
        public SeekTimeData()
        {

        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SeekTimes", SeekTimes, typeof(List<ushort>));
        }
        public SeekTimeData(SerializationInfo info, StreamingContext context)
        {
            SeekTimes = (List<ushort>)info.GetValue("SeekTimes", typeof(List<ushort>));
        }
    }
}
