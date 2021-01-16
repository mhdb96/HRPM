using HRPMBackendLibrary.Models;
using HRPMSharedLibrary.Enums;
using HRPMSharedLibrary.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRPMBackendLibrary.Converters
{
    public class EnumToStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(LogModel))
                return true;
            else 
                return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // we currently support only writing of JSON
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            // find all properties with type 'int'
            var properties = value.GetType().GetProperties();

            writer.WriteStartObject();

            foreach (var property in properties)
            {
                
                // write property name
                writer.WritePropertyName(property.Name);
                if (property.GetValue(value, null).GetType() == typeof(LogType))
                {
                    var type = (LogType)property.GetValue(value, null);
                    serializer.Serialize(writer, type.GetDescription());
                }
                else
                {
                    serializer.Serialize(writer, property.GetValue(value, null));
                }
                // let the serializer serialize the value itself
                // (so this converter will work with any other type, not just int)
            }
            writer.WriteEndObject();
        }
    }
}