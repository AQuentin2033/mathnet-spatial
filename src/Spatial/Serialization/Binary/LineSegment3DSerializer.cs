namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean;

    internal class LineSegment3DSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            LineSegment3D line = (LineSegment3D)obj;
            info.AddValue("StartPoint", line.StartPoint);
            info.AddValue("EndPoint", line.EndPoint);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Point3D start = (Point3D)info.GetValue("StartPoint", typeof(Point3D));
            Point3D end = (Point3D)info.GetValue("EndPoint", typeof(Point3D));
            return new LineSegment3D(start, end);
        }
    }
}
