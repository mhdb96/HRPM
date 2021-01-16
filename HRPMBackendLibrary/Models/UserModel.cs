using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMBackendLibrary.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime LastSeen { get; set; }
        public List<SessionModel> Sessions { get; set; }
        public DateTime CreatedAt { get; set; }
        public int DepartmentId { get; set; }
    }
}
