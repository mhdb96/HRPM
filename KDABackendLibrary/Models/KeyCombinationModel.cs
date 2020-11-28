using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KDABackendLibrary.Models
{
    public class KeyCombinationModel
    {
        public int Id { get; set; }
        public int FromKeyId { get; set; }
        public int ToKeyId { get; set; }
        public string FromKey
        {
            get
            {
                return ((KeysList)FromKeyId).GetDescription();
            }
        }
        public string ToKey
        {
            get
            {
                return ((KeysList)ToKeyId).GetDescription();
            }
        }
    }
}
