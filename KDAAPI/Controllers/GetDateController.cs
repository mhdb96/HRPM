using KDAAPI.Model;
using KDABackendLibrary;
using KDABackendLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KDAAPI.Controllers
{
    public class GetDateController : ApiController
    {
        [Route("api/getdate/getdates")]
        [HttpPost]
        public List<FileModel> GetDates(Date selectedDate)
        {
            string date = selectedDate.SelectedDate;
            List<FileModel> dates = new List<FileModel>();
            dates = GlobalConfig.Connection.GetFiles_BySearchDate(date);
            return dates;
        }
    }
}
