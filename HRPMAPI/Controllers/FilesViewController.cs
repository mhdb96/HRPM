using HRPMBackendLibrary;
using HRPMBackendLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace HRPMAPI.Controllers
{
    public class FilesViewController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult FileIndexGrid()
        {
            List<DataFileModel> files = new List<DataFileModel>();
            files = GlobalConfig.Connection.File_SelectAll();
            return PartialView("FileIndexGrid", files);
        }

        [HttpGet]
        public ActionResult DownloadFile(int id)
        {
            DataFileModel file = new DataFileModel();
            file = GlobalConfig.Connection.GetFilesById(id);
            byte[] bfile = System.IO.File.ReadAllBytes(file.Path);
            return File(
                bfile, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(file.Path));
        }
    }
}