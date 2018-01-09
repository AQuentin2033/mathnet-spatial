namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean;

    internal class CoordinateSystemSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            CoordinateSystem c = (CoordinateSystem)obj;
            info.AddValue("Origin", c.Origin);
            info.AddValue("XAxis", c.XAxis);
            info.AddValue("YAxis", c.YAxis);
            info.AddValue("ZAxis", c.ZAxis);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Point3D origin = (Point3D)info.GetValue("Origin", typeof(Point3D));
            Vector3D x = (Vector3D)info.GetValue("XAxis", typeof(Vector3D));
            Vector3D y = (Vector3D)info.GetValue("YAxis", typeof(Vector3D));
            Vector3D z = (Vector3D)info.GetValue("ZAxis", typeof(Vector3D));
            return new CoordinateSystem(origin, x, y, z);
        }
    }
}
