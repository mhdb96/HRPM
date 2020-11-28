using KDASharedLibrary.DataAccess;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KDABackendLibrary.Models
{
    public class SessionKeyModel
    {
        public int Id { get; set; }
        public int HoldTimesCount { get; set; }
        public int HoldTimesAvg { get; set; }
        public int SessionId { get; set; }
        public int KeyId { get; set; }
        public List<HoldTimeNumberModel> HoldTimeNumbers { get; set; } = new List<HoldTimeNumberModel>();
        //public string Key 
        //{ 
        //    get 
        //    {
        //        return ((KeysList)KeyId).GetDescription();
        //    }
        //}
        public SessionModel Session { get; set; }
        public KeyModel Key { get; set; }
        //public List<SessionCombinationModel> SeekTimes { get; set; } = new List<SessionCombinationModel>();

    }
}
