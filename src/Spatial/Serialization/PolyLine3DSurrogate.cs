namespace MathNet.Spatial.Serialization
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean;

    [DataContract(Name = "PolyLine3D")]
    public class PolyLine3DSurrogate
    {
        [DataMember(Order = 1)]
        public List<Point3D> Points;

        public static implicit operator PolyLine3DSurrogate(PolyLine3D polyline) => new PolyLine3DSurrogate { Points = new List<Point3D>(polyline) };

        public static implicit operator PolyLine3D(PolyLine3DSurrogate polyline) => new PolyLine3D(polyline.Points);
    }
}
