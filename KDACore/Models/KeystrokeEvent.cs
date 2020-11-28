using KDACore.Enums;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDACore.Models
{
    public class KeystrokeEvent
    {
        public Key Key { get; set; } = new Key();
        public DateTime EventTime { get; set; }
        public KeystrokeType Type { get; set; }
    }
}
