using KDASharedLibrary.Enums;
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
        public string Content { get; set; }
        public AppType AppType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ExcutableName { get; set; }
        public string ProcessName { get; set; }
        public uint KeystrokeCount { get; set; }
        public uint KeystrokeTotalTime { get; set; }
        public uint MouseClickCount { get; set; }
        public uint MouseClickTotalCount { get; set; }

    }
}
