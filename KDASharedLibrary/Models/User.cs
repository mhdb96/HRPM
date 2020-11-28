using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KDASharedLibrary.Models
{
    [Serializable]
    public class User : ISerializable
    {
        public string UserName { get; set; } = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public User()
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("UserName", UserName, typeof(string));
        }
        public User(SerializationInfo info, StreamingContext context)
        {
            UserName = (string)info.GetValue("UserName", typeof(string));

        }
    }
}
