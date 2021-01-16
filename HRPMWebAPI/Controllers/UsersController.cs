using HRPMBackendLibrary;
using HRPMBackendLibrary.Models;
using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRPMWebAPI.Controllers
{
    public class UsersController : ApiController
    {
        [Route("api/users/getbydepid/{id}")]
        [HttpGet]
        public List<User> GetUsersByDepartmentId(int id)
        {

            var dbUsers = GlobalConfig.Connection.User_GetByDepartment(id);
            List<User> localUsers = new List<User>();
            foreach (var user in dbUsers)
            {
                localUsers.Add(new User { Id = user.Id, UserName = user.Username });
            }
            return localUsers;
        }

        [Route("api/users/checkpassword")]
        [HttpPost]
        public bool CheckPasswordForUser(User user)
        {
            string userPassword = GlobalConfig.Connection.User_GetPassword(user.Id);
            if(userPassword == user.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
            
            
        }


    }
}