using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMBackendLibrary.Models
{
    public class DomainModel
    {
        public int Id { get; set; }
        public string Domain { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
