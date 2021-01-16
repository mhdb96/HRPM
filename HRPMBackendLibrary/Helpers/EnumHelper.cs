using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRPMBackendLibrary.Helpers
{
    public static class EnumHelper
    {
        public static List<string> GetValues<T>()
        {
            List<string> values = new List<string>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                //For each value of this enumeration, add a new EnumValue instance
                values.Add(Enum.GetName(typeof(T), itemType));
            }
            return values;
        }
    }
}