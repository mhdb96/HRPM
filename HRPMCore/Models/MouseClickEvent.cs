using HRPMCore.Enums;
using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMCore.Models
{
    public class MouseClickEvent
    {
        public MouseButton MouseButton { get; set; } = new MouseButton();
        public uint EventTime { get; set; }
        public KeystrokeType Type { get; set; }
    }
}
