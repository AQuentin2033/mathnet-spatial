﻿namespace MathNet.Spatial.Serialization
{
    using System;
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean;

    [DataContract(Name = "Point3D")]
    [Serializable]
    public class Point3DSurrogate
    {
        [DataMember(Order = 1)]
        public double X;
        [DataMember(Order = 2)]
        public double Y;
        [DataMember(Order = 3)]
        public double Z;

        public static implicit operator Point3DSurrogate(Point3D point) => new Point3DSurrogate { X = point.X, Y = point.Y, Z = point.Z };

        public static implicit operator Point3D(Point3DSurrogate point) => new Point3D(point.X, point.Y, point.Z);
    }
}
