using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDABackendLibrary.Models
{
    public class HoldTimeNumberModel
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int SessionKeyId { get; set; }
        public int KeystrokeDataId { get; set; }
        public SessionKeyModel SessionKey { get; set; }
    }
}
