using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDABackendLibrary.Models
{
    public class SessionModel
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int UserId { get; set; }
        public List<SessionKeyModel> SessionKeys { get; set; }
        public List<SessionCombinationModel> SessionCombinations { get; set; }
        public UserModel User { get; set; }

    }
}
