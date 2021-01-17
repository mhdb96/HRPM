using HRPMSharedLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace HRPMSharedLibrary.DataAccess
{
    public class BinaryConnector
    {
        public static void StaticSave<T>(T obj, string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, obj);
            }
        }

        public static T StaticLoad<T>(string path)
        {
            T obj;
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                IFormatter formatter = new BinaryFormatter();
                obj = (T)formatter.Deserialize(fs);
            }
            return obj;
        }
    }
}
