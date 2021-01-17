using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMSharedLibrary.Models
{
    public class UsageTime
    {
        public double IdleMinutes { get; set; }
        public double ActiveMinutes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public User User { get; set; } = new User();

    }
}
