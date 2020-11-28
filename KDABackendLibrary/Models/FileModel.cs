using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDABackendLibrary.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public UserModel User { get; set; } = new UserModel();
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
