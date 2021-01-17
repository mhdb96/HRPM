using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HRPMCore.Managers
{
    public class TimeManager
    {
        private static readonly TimeManager _instance = new TimeManager();
        DateTime lastSeen = DateTime.Now;
        double activeMinutes;
        double idleMinutes;
        Timer timer;
        DateTime startTime = DateTime.Now;
        public UsageTime Usage;

        private TimeManager()
        {
            timer = new Timer(60*60*1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CreateNewAction();
            Usage = new UsageTime();
            Usage.ActiveMinutes = activeMinutes;
            Usage.IdleMinutes = idleMinutes;
            Usage.StartTime = startTime;
            Usage.EndTime = DateTime.Now;
            activeMinutes = 0;
            idleMinutes = 0;
            startTime = DateTime.Now;
        }

        public static TimeManager GetTimeManager()
        {
            return _instance;
        }

        public void CreateNewAction()
        {
            if((DateTime.Now - lastSeen).TotalMinutes > 2)
            {
                idleMinutes += (DateTime.Now - lastSeen).TotalMinutes;
                lastSeen = DateTime.Now;
            }
            else
            {
                activeMinutes += (DateTime.Now - lastSeen).TotalMinutes;
                lastSeen = DateTime.Now;
            }
        }
    }
}
