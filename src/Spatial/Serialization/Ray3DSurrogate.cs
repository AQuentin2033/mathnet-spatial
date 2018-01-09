using MathNet.Spatial.Euclidean;
using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace MathNet.Spatial.Serialization
{
    [DataContract(Name = "Ray3D")]
    public class Ray3DSurrogate
    {
        [DataMember(Order = 1)]
        public Point3D ThroughPoint;
        [DataMember(Order = 2)]
        public UnitVector3D Direction;

        public static implicit operator Ray3DSurrogate(Ray3D ray) => new Ray3DSurrogate { ThroughPoint = ray.ThroughPoint, Direction = ray.Direction };

        public static implicit operator Ray3D(Ray3DSurrogate ray) => new Ray3D(ray.ThroughPoint, ray.Direction);
    }
}
