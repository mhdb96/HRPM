using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMBackendLibrary.Models
{
    public class ProofModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int TaskId { get; set; }
    }
}
