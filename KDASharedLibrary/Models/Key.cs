using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace KDASharedLibrary.Models
{
    [Serializable]
    public class Key : ISerializable
    {
        public KeysList Data { get; set; }
        public string KeyName { 
            get 
            {
                return Data.GetDescription();
            } 
        }
        public int KeyIndex
        {
            get
            {
                return (int)Data;
            }
        }
        public Key()
        {

        }
        public Key(SerializationInfo info, StreamingContext context)
        {
            Data = (KeysList)info.GetValue("Data", typeof(KeysList));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Data", Data, typeof(KeysList));
        }
        
    }
}
