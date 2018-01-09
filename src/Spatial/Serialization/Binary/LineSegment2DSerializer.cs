namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean2D;

    internal class LineSegment2DSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            LineSegment2D line = (LineSegment2D)obj;
            info.AddValue("StartPoint", line.StartPoint);
            info.AddValue("EndPoint", line.EndPoint);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Point2D start = (Point2D)info.GetValue("StartPoint", typeof(Point2D));
            Point2D end = (Point2D)info.GetValue("EndPoint", typeof(Point2D));
            return new LineSegment2D(start, end);
        }
    }
}
