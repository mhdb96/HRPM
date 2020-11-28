using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDABackendLibrary.Models
{
    public class SessionCombinationNumberModel
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int SessionCombinationId { get; set; }
        public SessionCombinationModel SessionCombination { get; set; }
    }
}
