using HRPMBackendLibrary;
using HRPMBackendLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRPMAPI.Controllers
{
    public class UserViewController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult UserIndexGrid()
        {
            List<UserModel> users = new List<UserModel>();
            users = GlobalConfig.Connection.User_SelectAll();
            return PartialView("UserIndexGrid", users);
        }
    }
}