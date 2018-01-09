namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean;

    internal class UnitVector3DSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            UnitVector3D point = (UnitVector3D)obj;
            info.AddValue("x", point.X);
            info.AddValue("y", point.Y);
            info.AddValue("z", point.Z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            double x = info.GetDouble("x");
            double y = info.GetDouble("y");
            double z = info.GetDouble("z");
            return new UnitVector3D(x, y, z);
        }
    }
}
