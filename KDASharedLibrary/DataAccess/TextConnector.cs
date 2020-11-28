using KDASharedLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDASharedLibrary.DataAccess
{
    public class TextConnector
    {
        public static T Load<T>(string fileName, string extention = "kdf", string rootFolder = "")
        {
            throw new NotImplementedException();
        }

        public static void Save<T>(T stringBuilder, string fileName = "", string extention = "kdf", string rootFolder = "")
        {
            if (rootFolder == "")
            {
                rootFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            if (fileName == "")
            {
                fileName = Guid.NewGuid().ToString();
            }
            string filePath = FileHelper.MakePath(rootFolder, fileName, extention);
            File.WriteAllText(filePath, stringBuilder.ToString());
        }
    }
}
