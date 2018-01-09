using MathNet.Spatial.Euclidean;


namespace MathNet.Spatial.Serialization
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Linq;

    [DataContract(Name = "LineSegment3D")]
    public class LineSegment3DSurrogate
    {
        [DataMember(Order = 1)]
        public Point3D StartPoint;
        [DataMember(Order = 2)]
        public Point3D EndPoint;

        public static implicit operator LineSegment3DSurrogate(LineSegment3D line) => new LineSegment3DSurrogate { StartPoint = line.StartPoint, EndPoint = line.EndPoint };

        public static implicit operator LineSegment3D(LineSegment3DSurrogate line) => new LineSegment3D(line.StartPoint, line.EndPoint);
    }
}
