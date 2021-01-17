using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMBackendLibrary.Models
{
    public class UsageTimeModel
    {
        public int Id { get; set; }
        public double IdleMinutes { get; set; }
        public double ActiveMinutes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int UserId { get; set; }
    }
}
