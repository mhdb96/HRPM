using System;
using System.Runtime.Serialization;

namespace HRPMSharedLibrary.Models
{
    [Serializable]
    public class KeyboardData : ISerializable
    {
        public int StrokesCount { get; set; }
        public int StrokeHoldTimes { get; set; }
        public int UniqueKeysCount { get; set; }
        public int BackspaceStrokesCount { get; set; }
        public int StrokeHoldTimesAvg { 
            get 
            {
                if (StrokesCount > 0)
                {
                    return StrokeHoldTimes / StrokesCount;
                }
                else
                {
                    return 0;
                }
            } 
        }

        public KeyboardData()
        {

        }

        public KeyboardData(SerializationInfo info, StreamingContext context)
        {
            StrokesCount = (int)info.GetValue("StrokesCount", typeof(int));
            StrokeHoldTimes = (int)info.GetValue("StrokeHoldTimes", typeof(int));
            UniqueKeysCount = (int)info.GetValue("UniqueKeysCount", typeof(int));
            BackspaceStrokesCount = (int)info.GetValue("BackspaceStrokesCount", typeof(int));

        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("StrokesCount", StrokesCount, typeof(int));
            info.AddValue("StrokeHoldTimes", StrokeHoldTimes, typeof(int));
            info.AddValue("UniqueKeysCount", UniqueKeysCount, typeof(int));
            info.AddValue("BackspaceStrokesCount", BackspaceStrokesCount, typeof(int));

        }
    }
}
