namespace MathNet.Spatial.Serialization
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Linq;
    using MathNet.Spatial.Euclidean2D;

    [DataContract(Name = "Point2D")]
    [Serializable]
    public class Point2DSurrogate
    {
        [DataMember(Order=1)]
        public double X;
        [DataMember(Order=2)]
        public double Y;

        public static implicit operator Point2DSurrogate(Point2D point) => new Point2DSurrogate { X = point.X, Y = point.Y };

        public static implicit operator Point2D(Point2DSurrogate point) => new Point2D(point.X, point.Y);
    }
}
