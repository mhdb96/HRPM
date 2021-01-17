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
    public class TasksController : ApiController
    {
        [Route("api/tasks/PostTask")]
        [HttpPost]
        public void PostTask(WorkTask task)
        {
            TaskModel model = new TaskModel();
            model.UserId = task.User.Id;
            model.Title = task.Title;
            model.Description = task.Description;
            model.StartTime = task.StartTime;
            model.EndTime = task.EndTime;            
            GlobalConfig.Connection.Task_Insert(model);
        }
    }
}