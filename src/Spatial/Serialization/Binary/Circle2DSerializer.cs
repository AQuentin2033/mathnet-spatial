namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean2D;

    internal class Circle2DSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Circle2D circle = (Circle2D)obj;
            info.AddValue("Center", circle.Center);
            info.AddValue("Radius", circle.Radius);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Point2D center = (Point2D)info.GetValue("Center", typeof(Point2D));
            double radius = info.GetDouble("Radius");
            return new Circle2D(center, radius);
        }
    }
}
