namespace MathNet.Spatial.Serialization
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean2D;

    internal class PolyLine2DSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            PolyLine2D line = (PolyLine2D)obj;

            //unfortunately the legacy formatters do not handled nested objects in lists when the object has readonly properties
            List<Point2DSurrogate> linepoints = line.Select(x => (Point2DSurrogate)x).ToList();
            info.AddValue("Points", linepoints);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            List<Point2DSurrogate> points = (List<Point2DSurrogate>)info.GetValue("Points", typeof(List<Point2DSurrogate>));
            List<Point2D> linepoints = points.Select(x => (Point2D)x).ToList();
            return new PolyLine2D(linepoints);
        }
    }
}
