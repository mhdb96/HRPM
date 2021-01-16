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
    public class DepartmentsController : ApiController
    {
        [Route("api/departments/getall")]
        [HttpGet]
        public List<Department> GetDepartments()
        {
            
            var dbDeps = GlobalConfig.Connection.Department_SelectAll();
            List<Department> localDeps = new List<Department>();
            foreach (var dep in dbDeps)
            {
                localDeps.Add(new Department { Id = dep.Id, Name = dep.Name });
            }
            return localDeps;
        }
    }
}