namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean;

    internal class Circle3DSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Circle3D circle = (Circle3D)obj;
            info.AddValue("CenterPoint", circle.CenterPoint);
            info.AddValue("Axis", circle.Axis);
            info.AddValue("Radius", circle.Radius);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Point3D center = (Point3D)info.GetValue("CenterPoint", typeof(Point3D));
            UnitVector3D axis = (UnitVector3D)info.GetValue("Axis", typeof(UnitVector3D));
            double radius = info.GetDouble("Radius");
            return new Circle3D(center, axis, radius);
        }
    }
}
