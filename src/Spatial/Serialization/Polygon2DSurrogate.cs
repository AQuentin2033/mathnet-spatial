namespace MathNet.Spatial.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Linq;
    using MathNet.Spatial.Euclidean2D;

    [DataContract(Name = "Polygon2D")]
    public class Polygon2DSurrogate
    {
        [DataMember(Order = 1)]
        public List<Point2D> Points;

        public static implicit operator Polygon2DSurrogate(Polygon2D polygon) => new Polygon2DSurrogate { Points = new List<Point2D>(polygon.Vertices) };

        public static implicit operator Polygon2D(Polygon2DSurrogate polygon) => new Polygon2D(polygon.Points);
    }
}
