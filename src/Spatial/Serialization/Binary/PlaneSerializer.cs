namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean;

    internal class PlaneSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Plane p = (Plane)obj;
            info.AddValue("RootPoint", p.RootPoint);
            info.AddValue("Normal", p.Normal);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Point3D throughPoint = (Point3D)info.GetValue("RootPoint", typeof(Point3D));
            UnitVector3D direction = (UnitVector3D)info.GetValue("Normal", typeof(UnitVector3D));
            return new Plane(throughPoint, direction);
        }
    }
}
