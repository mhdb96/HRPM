using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMBackendLibrary.Models
{
    public class AppModel
    {
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
