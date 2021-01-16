using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRPMAPI.Controllers
{
    public class CheckersController : ApiController
    {
        [Route("api/checkers/isalive")]
        [HttpGet]
        public string IsAlive()
        {
            return "I'm Alive";
        }
    }
}
