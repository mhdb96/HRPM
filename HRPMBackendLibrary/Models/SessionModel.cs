using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMBackendLibrary.Models
{
    public class SessionModel
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int StrokesCount { get; set; }
        public int StrokeHoldTimes { get; set; }
        public int UniqueKeysCount { get; set; }
        public int BackspaceStrokesCount { get; set; }
        public int MouseClickCount { get; set; }
        public int MouseClickTotalTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int AppId { get; set; }
        public DomainModel Domain { get; set; } = new DomainModel();

    }
}
