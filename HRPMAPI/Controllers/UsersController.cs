using HRPMBackendLibrary;
using HRPMBackendLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRPMAPI.Controllers
{
    public class UsersController : ApiController
    {
        [Route("api/users/getusers")]
        [HttpGet]
        public List<UserModel> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();
            users = GlobalConfig.Connection.User_SelectAll();
            return users;
        }

        [Route("api/users/getlogusers")]
        [HttpGet]
        public List<UserModel> GetLogUsers()
        {
            List<UserModel> users = new List<UserModel>();
            users = GlobalConfig.Connection.User_SelectAll();
            return users;
        }
    }
}
