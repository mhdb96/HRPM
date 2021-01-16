using HRPMBackendLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HRPMBackendLibrary.Helpers
{
    public class DbHelper
    {
        public static void CheckForUser(UserModel model)
        {
            if (!GlobalConfig.Connection.User_Exists(model))
            {
                GlobalConfig.Connection.User_Insert(model);
            }
        }
        public static byte[] Serialize<T>(T data)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, data);
            return mStream.ToArray();
        }
        public static T Deserialize<T>(byte[] data) where T : class
        {
            var mStream = new MemoryStream();
            var binFormatter = new BinaryFormatter();
            // Where 'objectBytes' is your byte array.
            mStream.Write(data, 0, data.Length);
            mStream.Position = 0;
            return binFormatter.Deserialize(mStream) as T;
        }
    }
}
