using HRPMAPI.Model;
using HRPMBackendLibrary;
using HRPMBackendLibrary.Helpers;
using HRPMBackendLibrary.Models;
using HRPMSharedLibrary.Enums;
using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http;

namespace HRPMAPI.Controllers
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
            model.Date = log.Date;
            DbHelper.CheckForUser(model.User);
            GlobalConfig.Connection.Log_Insert(model);
        }

        [Route("api/logs/GetUserLogsByLogType")]
        [HttpPost]
        public List<LogModel> GetUserLogsByLogType(StringType type)
        {
            List<LogModel> logs = new List<LogModel>();
            string types = type.SelectedType;
            logs = GlobalConfig.Connection.GetLogs_ByLogType(types);
            return logs;
        }

        [Route("api/logs/getdates")]
        [HttpPost]
        public List<LogModel> GetDates(Date selectedDate)
        {
            string date = selectedDate.SelectedDate;
            List<LogModel> dates = new List<LogModel>();
            dates = GlobalConfig.Connection.GetLogs_BySearchDate(date);
            return dates;
        }

        [HttpPost]
        [Route("api/logs/GetLogType")]
        public IHttpActionResult GetLogType()
        {
            return Ok(EnumHelper.GetValues<LogType>());
        }

        [Route("api/logs/GetUsersByLogType")]
        [HttpPost]
        public List<UserModel> GetUsersByLogType(StringType type)
        {
            List<UserModel> users;
            string types = type.SelectedType;
            users = GlobalConfig.Connection.GetUsers_BySearchLogType(types);
            return users;
        }

        [Route("api/logs/GetLogsByUserId/{id}")]
        [HttpPost]
        public List<LogModel> GetLogsByUserId(int id)
        {
            List<LogModel> logs = new List<LogModel>();
            logs = GlobalConfig.Connection.GetLogs_BySearchUser(id);
            return logs;
        }

        [Route("api/logs/GetAllLogs")]
        [HttpPost]
        public List<LogModel> GetAllLogs()
        {
            List<LogModel> logs = new List<LogModel>();
            logs = GlobalConfig.Connection.GetLogs_AllLogs();
            return logs;
        }

        [Route("api/logs/GetLogsByTypeAndUserId/{id}")]
        [HttpPost]
        public List<LogModel> GetLogsByTypeAndUserId(int id, StringType type)
        {
            List<LogModel> logs = new List<LogModel>();
            string types = type.SelectedType;
            logs = GlobalConfig.Connection.GetLogs_ByLogTypeAndUserId(id, types);
            return logs;
        }
    }
}