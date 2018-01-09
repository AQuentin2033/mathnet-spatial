namespace MathNet.Numerics.UnitTests.Serialization
{
    using MathNet.Spatial.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SpatialJsonConverter : JsonConverter
    {
        public SpatialJsonConverter()
        {
        }

        public override bool CanRead => true;

        public override bool CanWrite => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Type surrogateType = SerializerFactory.GetSurrogateType(objectType);
            JToken token = JToken.Load(reader);
            object surrogateData = token.ToObject(surrogateType, serializer);
            return SerializerFactory.GetDeserializedObject(surrogateType, surrogateData);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            object surrogate = SerializerFactory.GetObjectToSerialize(value.GetType(), value);
            JToken t = JToken.FromObject(surrogate, serializer);
            t.WriteTo(writer);
        }

        public override bool CanConvert(Type objectType)
        {
            return SerializerFactory.CanConvert(objectType);
        }
    }
}
