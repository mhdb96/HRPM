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
    public class UsageTimesController : ApiController
    {
        [Route("api/usagetimes/postusage")]
        [HttpPost]
        public void PostUsageTime(UsageTime usage)
        {
            UsageTimeModel model = new UsageTimeModel();
            model.UserId = usage.User.Id;
            model.ActiveMinutes = usage.ActiveMinutes;
            model.IdleMinutes = usage.IdleMinutes;
            model.StartTime = usage.StartTime;
            model.EndTime = usage.EndTime;
            GlobalConfig.Connection.UsageTime_Insert(model);
        }
    }
}