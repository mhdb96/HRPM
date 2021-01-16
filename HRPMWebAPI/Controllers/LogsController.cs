using HRPMBackendLibrary;
using HRPMBackendLibrary.Helpers;
using HRPMBackendLibrary.Models;
using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http;

namespace HRPMWebAPI.Controllers
{
    public class LogsController : ApiController
    {
        [Route("api/logs/PostLog")]
        [HttpPost]
        public void PostLog(Log log)
        {
            LogModel model = new LogModel();
            model.Text = log.Text;
            model.Type = log.Type;
            model.Severity = log.Severity;
            model.User.Username = log.User.UserName;
            model.User.Id = log.User.Id;
            model.Date = log.Date;            
            GlobalConfig.Connection.Log_Insert(model);
        }
    }
}