using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HRPMSharedLibrary.Models
{
    [Serializable]
    public class User : ISerializable
    {
        public string UserName { get; set; } = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public int Id { get; set; }
        public string Password { get; set; }
        public User()
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("UserName", UserName, typeof(string));
            info.AddValue("Id", Id, typeof(int));
            info.AddValue("Password", Password, typeof(string));
        }
        public User(SerializationInfo info, StreamingContext context)
        {
            UserName = (string)info.GetValue("UserName", typeof(string));
            Id = (int)info.GetValue("Id", typeof(int));
            Password = (string)info.GetValue("Password", typeof(string));

        }
    }
}
