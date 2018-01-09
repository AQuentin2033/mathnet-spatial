namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;

    internal class AngleSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Angle point = (Angle)obj;
            info.AddValue("Value", point.Radians);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            double rad = info.GetDouble("Value");
            return Angle.FromRadians(rad);
        }
    }
}
