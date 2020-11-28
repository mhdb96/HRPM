using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDABackendLibrary.DataAccess
{
    public class FileManager
    {
        private static readonly FileManager _instance = new FileManager();
        private readonly string _rootFolder = @"C:\Users\mhdb9\Desktop\API\";
        private readonly string _rootKeystrokesFolder = @"Data\";
        private FileManager()
        {
            Directory.CreateDirectory(_rootFolder);
        }

        public static FileManager GetFileManager()
        {
            return _instance;
        }

        private void WriteBytesToDisk(byte[] bytes, string path)
        {
            try
            {
                File.WriteAllBytes(path, bytes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string SaveKeystrokesFile(BinaryFile file)
        {
            string path = $@"{_rootFolder}{_rootKeystrokesFolder}{file.User.UserName.Replace('\\', '-')}";
            Directory.CreateDirectory(path);
            try
            {
                path += $@"\{DateTime.Now:yyyy-MM-dd}-{file.Name}";
                WriteBytesToDisk(file.Bytes, path);
                return path;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
