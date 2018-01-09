namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean2D;

    [DataContract(Name = "LineSegment2D")]
    public class LineSegment2DSurrogate
    {
        [DataMember(Order = 1)]
        public Point2D StartPoint;
        [DataMember(Order = 2)]
        public Point2D EndPoint;

        public static implicit operator LineSegment2DSurrogate(LineSegment2D line) => new LineSegment2DSurrogate { StartPoint = line.StartPoint, EndPoint = line.EndPoint };
        public static implicit operator LineSegment2D(LineSegment2DSurrogate line) => new LineSegment2D(line.StartPoint, line.EndPoint);
    }
}
