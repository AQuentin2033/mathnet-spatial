namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean2D;

    [DataContract(Name = "Vector2D")]
    public class Vector2DSurrogate
    {
        [DataMember(Order = 1)]
        public double X;
        [DataMember(Order = 2)]
        public double Y;

        public static implicit operator Vector2DSurrogate(Vector2D vector) => new Vector2DSurrogate { X = vector.X, Y = vector.Y };

        public static implicit operator Vector2D(Vector2DSurrogate vector) => new Vector2D(vector.X, vector.Y);
    }
}
