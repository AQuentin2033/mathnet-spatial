namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean2D;

    [DataContract(Name = "Circle2D")]
    public class Circle2DSurrogate
    {
        [DataMember(Order = 1)]
        public Point2D Center;
        [DataMember(Order = 2)]
        public double Radius;

        public static implicit operator Circle2DSurrogate(Circle2D circle) => new Circle2DSurrogate { Center = circle.Center, Radius = circle.Radius };

        public static implicit operator Circle2D(Circle2DSurrogate circle) => new Circle2D(circle.Center, circle.Radius);
    }
}
