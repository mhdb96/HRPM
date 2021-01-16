using HRPMBackendLibrary;
using HRPMBackendLibrary.DataAccess;
using HRPMBackendLibrary.Helpers;
using HRPMBackendLibrary.Models;
using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace HRPMAPI.Controllers
{
    public class FilesController : ApiController
    {
        [Route("api/files/UploadFile")]
        [HttpPost]
        public void UploadFile(BinaryFile file)
        {
            DataFileModel model = new DataFileModel();
            model.Date = file.Date;
            model.User.Username = file.User.UserName;
            model.Path = FileManager.GetFileManager().SaveKeystrokesFile(file);
            DbHelper.CheckForUser(model.User);
            GlobalConfig.Connection.File_Insert(model);
        }

        [Route("api/files/GetAllFiles")]
        [HttpPost]
        public List<DataFileModel> GetAllFiles()
        {
            List<DataFileModel> files = new List<DataFileModel>();
            files = GlobalConfig.Connection.Get_AllFiles();
            return files;
        }

        [Route("api/files/GetUserUploadFiles/{id}")]
        [HttpPost]
        public List<DataFileModel> GetUserUploadFiles(int id)
        {
            List<DataFileModel> files = new List<DataFileModel>();
            files = GlobalConfig.Connection.GetFiles_BySearchValue(id);
            return files;
        }
    }
}
