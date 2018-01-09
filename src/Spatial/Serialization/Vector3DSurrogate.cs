﻿namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean;

    [DataContract(Name = "Vector3D")]
    public class Vector3DSurrogate
    {
        [DataMember(Order = 1)]
        public double X;
        [DataMember(Order = 2)]
        public double Y;
        [DataMember(Order = 3)]
        public double Z;

        public static implicit operator Vector3DSurrogate(Vector3D vector) => new Vector3DSurrogate { X = vector.X, Y = vector.Y, Z = vector.Z };

        public static implicit operator Vector3D(Vector3DSurrogate vector) => new Vector3D(vector.X, vector.Y, vector.Z);
    }
}
