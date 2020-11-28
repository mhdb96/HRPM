using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDABackendLibrary.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<SessionModel> Sessions { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
