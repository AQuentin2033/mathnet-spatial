namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean;

    internal class Ray3DSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Ray3D line = (Ray3D)obj;
            info.AddValue("ThroughPoint", line.ThroughPoint);
            info.AddValue("Direction", line.Direction);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Point3D throughPoint = (Point3D)info.GetValue("ThroughPoint", typeof(Point3D));
            UnitVector3D direction = (UnitVector3D)info.GetValue("Direction", typeof(UnitVector3D));
            return new Ray3D(throughPoint, direction);
        }
    }
}
