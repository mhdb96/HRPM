using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HRPMSharedLibrary.Models
{
    [Serializable]
    public class BinaryFile : ISerializable
    {
        public User User { get; set; } = new User();
        public DateTime Date { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public byte[] Bytes { get; set; }

        public BinaryFile()
        {

        }

        public BinaryFile(SerializationInfo info, StreamingContext context)
        {
            User = (User)info.GetValue("User", typeof(User));
            Date = (DateTime)info.GetValue("Date", typeof(DateTime));
            Name = (string)info.GetValue("Name", typeof(string));
            Bytes = (byte[])info.GetValue("Bytes", typeof(byte[]));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("User", User, typeof(User));
            info.AddValue("Date", Date, typeof(DateTime));
            info.AddValue("Name", Name, typeof(string));
            info.AddValue("Bytes", Bytes, typeof(byte[]));
        }
    }
}
