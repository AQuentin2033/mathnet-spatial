namespace MathNet.Spatial.Serialization
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean2D;

    [DataContract(Name = "PolyLine2D")]
    public class PolyLine2DSurrogate
    {
        [DataMember(Order = 1)]
        public List<Point2D> Points;

        public static implicit operator PolyLine2DSurrogate(PolyLine2D polyline) => new PolyLine2DSurrogate { Points = new List<Point2D>(polyline) };

        public static implicit operator PolyLine2D(PolyLine2DSurrogate polyline) => new PolyLine2D(polyline.Points);
    }
}
