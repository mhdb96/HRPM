using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDABackendLibrary.Models
{
    public class SessionCombinationModel
    {
        public int Id { get; set; }
        public int SeekTimesCount { get; set; }
        public int SeekTimesAvg { get; set; }
        public int SessionId { get; set; }
        public int KeyCombinationId { get; set; }
        public List<SessionCombinationNumberModel> SessionCombinationNumbers { get; set; } = new List<SessionCombinationNumberModel>();
        public KeyCombinationModel KeyCombination { get; set; } = new KeyCombinationModel();
        public SessionModel Session { get; set; }
        //public int FromKeyId { get; set; }
        //public string FromKey
        //{
        //    get
        //    {
        //        return ((KeysList)FromKeyId).GetDescription();
        //    }
        //}
        //public SessionKeyModel KeystrokeData { get; set; }
        //public int KeystrokeDataId { get; set; }
    }
}
