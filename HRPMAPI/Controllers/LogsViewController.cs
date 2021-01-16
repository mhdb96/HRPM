using HRPMBackendLibrary;
using HRPMBackendLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HRPMAPI.Controllers
{
    public class LogsViewController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult IndexGrid()
        {
            List<LogModel> logs = new List<LogModel>();
            logs = GlobalConfig.Connection.Log_SelectAll();
            return PartialView("IndexGrid", logs);
        }

        public ViewResult LogsCharts()
        {
            return View();
        }
    }
}
