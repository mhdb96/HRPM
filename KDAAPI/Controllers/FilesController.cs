using KDABackendLibrary;
using KDABackendLibrary.DataAccess;
using KDABackendLibrary.Helpers;
using KDABackendLibrary.Models;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace KDAAPI.Controllers
{
    public class FilesController : ApiController
    {
        [Route("api/files/UploadFile")]
        [HttpPost]
        public void UploadFile(BinaryFile file)
        {
            FileModel model = new FileModel();
            model.Date = file.Date;
            model.User.UserName = file.User.UserName;
            model.Path = FileManager.GetFileManager().SaveKeystrokesFile(file);
            DbHelper.CheckForUser(model.User);
            GlobalConfig.Connection.File_Insert(model);
        }

        [Route("api/files/GetAllFiles")]
        [HttpPost]
        public List<FileModel> GetAllFiles()
        {
            List<FileModel> files = new List<FileModel>();
            files = GlobalConfig.Connection.Get_AllFiles();
            return files;
        }

        [Route("api/files/GetUserUploadFiles/{id}")]
        [HttpPost]
        public List<FileModel> GetUserUploadFiles(int id)
        {
            List<FileModel> files = new List<FileModel>();
            files = GlobalConfig.Connection.GetFiles_BySearchValue(id);
            return files;
        }
    }
}
