using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HRPMSharedLibrary.Helpers
{
    public static class FileHelper
    {
        public static string GetDescription(this Enum e)
        {
            var attribute =
                e.GetType()
                    .GetTypeInfo()
                    .GetMember(e.ToString())
                    .FirstOrDefault(member => member.MemberType == MemberTypes.Field)
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .SingleOrDefault()
                    as DescriptionAttribute;
            return attribute?.Description ?? e.ToString();
        }
        public static int GetEnumCount<T>()
        {
            return Enum.GetNames(typeof(T)).Length - 1;
        }

        public static long ConvertSeconToNanosecond(int second)
        {
            return second * 10000000;
        }

        public static decimal ConvertNanosecondToSecond(long nanosecond)
        {
            return nanosecond / 10000000.0M;
        }

        public static string MakePath(string root, string fileName, string extention)
        {
            return $@"{root}\{fileName}.{extention}";
        }

        //public static string MatrixToString<T>(T[] matrix)
        //{
        //    StringBuilder sb = new StringBuilder();
           

        //    return sb.ToString();
        //}
    }
}
