using HRPMAPI.Model;
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
    public class GetDateController : ApiController
    {
        [Route("api/getdate/getdates")]
        [HttpPost]
        public List<DataFileModel> GetDates(Date selectedDate)
        {
            string date = selectedDate.SelectedDate;
            List<DataFileModel> dates = new List<DataFileModel>();
            dates = GlobalConfig.Connection.GetFiles_BySearchDate(date);
            return dates;
        }
    }
}
