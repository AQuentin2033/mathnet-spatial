namespace MathNet.Spatial.Serialization
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Linq;
    using MathNet.Spatial.Euclidean2D;

    internal class Point2DSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Point2D point = (Point2D)obj;
            info.AddValue("x", point.X);
            info.AddValue("y", point.Y);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            double x = info.GetDouble("x");
            double y = info.GetDouble("y");
            return new Point2D(x, y);
        }
    }
}
