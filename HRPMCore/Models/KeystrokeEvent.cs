using HRPMCore.Enums;
using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRPMCore.Models
{
    public class KeystrokeEvent
    {
        public Key Key { get; set; } = new Key();
        public uint EventTime { get; set; }
        public KeystrokeType Type { get; set; }
    }
}
