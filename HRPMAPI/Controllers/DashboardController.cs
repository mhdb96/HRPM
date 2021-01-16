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
    public class DashboardController : ApiController
    {
        [Route("api/Dashboard/GetChartLogsByDay")]
        [HttpPost]
        public List<DailyLogCountModel> GetChartLogsByDay(DateRange range)
        {
            string start_date;
            string end_date;
            if (string.IsNullOrWhiteSpace(range.SelectedStartDate) || string.IsNullOrWhiteSpace(range.SelectedEndDate))
            {
                end_date = DateTime.Now.ToString("yyyy-MM-dd");
                start_date = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            }
            else
            {
                start_date = range.SelectedStartDate;
                end_date = range.SelectedEndDate;
            }
            List<DailyLogCountModel> dates = new List<DailyLogCountModel>();
            DateTime start = DateTime.Parse(start_date);
            DateTime end = DateTime.Parse(end_date);
            int dayscount = (end.DayOfYear - start.DayOfYear) + 1;
            DateTime now = start;
            for (int i = 0; i < dayscount; i++)
            {
                dates.Add(new DailyLogCountModel { CountText = "0", FormatDate = now.ToString("yyyy-MM-dd") });
                now = now.AddDays(1);
            }
            List<DailyLogCountModel> logs = new List<DailyLogCountModel>();
            logs = GlobalConfig.Connection.ChartDailyLogs(start_date, end_date);
            foreach (var item in dates)
            {
                var findDate = logs.Find(x => x.FormatDate == item.FormatDate);
                if (findDate != null)
                {
                    item.CountText = findDate.CountText;
                }
            }
            return dates;
        }

        [Route("api/Dashboard/GetChartLogsByLogType")]
        [HttpPost]
        public List<ChartLogTypeCountModel> GetChartLogsByLogType(DateRange range)
        {
            string start_date;
            string end_date;
            if (string.IsNullOrWhiteSpace(range.SelectedStartDate) || string.IsNullOrWhiteSpace(range.SelectedEndDate))
            {
                end_date = DateTime.Now.ToString("yyyy-MM-dd");
                start_date = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            }
            else
            {
                start_date = range.SelectedStartDate;
                end_date = range.SelectedEndDate;
            }
            List<ChartLogTypeCountModel> logs = new List<ChartLogTypeCountModel>();
            logs = GlobalConfig.Connection.ChartLogs_ByLogType(start_date, end_date);
            return logs;
        }

        [Route("api/Dashboard/GetChartLogsMostPausedPerson_ByLogType")]
        [HttpPost]
        public List<ChartLogMostPausedPersonModel> GetChartLogsMostPausedPerson_ByLogType(DateRange range)
        {
            string start_date;
            string end_date;
            if (string.IsNullOrWhiteSpace(range.SelectedStartDate) || string.IsNullOrWhiteSpace(range.SelectedEndDate))
            {
                end_date = DateTime.Now.ToString("yyyy-MM-dd");
                start_date = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            }
            else
            {
                start_date = range.SelectedStartDate;
                end_date = range.SelectedEndDate;
            }
            List<ChartLogMostPausedPersonModel> logs = new List<ChartLogMostPausedPersonModel>();
            logs = GlobalConfig.Connection.ChartLogsMostPausedPersone(start_date, end_date);
            return logs;
        }
    }
}
