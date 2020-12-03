using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDACore.Models
{
    public class AppSession
    {
        public string HeaderText { get; set; }
        public DateTime StartTime { get; set; }
        public string ExcutablePath { get; set; }
        public string ProcessName { get; set; }
    }
}
