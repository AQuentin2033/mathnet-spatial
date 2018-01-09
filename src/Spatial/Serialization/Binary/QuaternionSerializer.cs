namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean;

    internal class QuaternionSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Quaternion point = (Quaternion)obj;
            info.AddValue("w", point.Real);
            info.AddValue("x", point.ImagX);
            info.AddValue("y", point.ImagY);
            info.AddValue("z", point.ImagZ);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            double w = info.GetDouble("w");
            double x = info.GetDouble("x");
            double y = info.GetDouble("y");
            double z = info.GetDouble("z");
            return new Quaternion(w, x, y, z);
        }

    }
}
