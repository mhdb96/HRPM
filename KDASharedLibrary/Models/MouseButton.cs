using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KDASharedLibrary.Models
{
    [Serializable]
    public class MouseButton : ISerializable
    {
        public MouseButtonList Data { get; set; }
        public string KeyName
        {
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
        public MouseButton()
        {

        }
        public MouseButton(SerializationInfo info, StreamingContext context)
        {
            Data = (MouseButtonList)info.GetValue("Data", typeof(MouseButtonList));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Data", Data, typeof(MouseButtonList));
        }
    }
}
