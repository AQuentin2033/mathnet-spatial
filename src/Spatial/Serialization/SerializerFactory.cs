namespace MathNet.Spatial.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using MathNet.Spatial;
    using MathNet.Spatial.Euclidean;
    using MathNet.Spatial.Euclidean2D;

    public static class SerializerFactory
    {
        internal class ContractConvertor
        {
            public Type Source;
            public Type Surrogate;
            public ISerializationSurrogate Serializer;

            public ContractConvertor(Type source, Type surrogate, ISerializationSurrogate serializer)
            {
                Source = source;
                Surrogate = surrogate;
                Serializer = serializer;
            }
        }

        internal static List<ContractConvertor> SurrogateMap = new List<ContractConvertor>()
        {
            new ContractConvertor(typeof(Point2D), typeof(Point2DSurrogate), new Point2DSerializer()),
            new ContractConvertor(typeof(Point3D), typeof(Point3DSurrogate), new Point3DSerializer()),
            new ContractConvertor(typeof(Vector2D), typeof(Vector2DSurrogate), new Vector2DSerializer()),
            new ContractConvertor(typeof(Vector3D), typeof(Vector3DSurrogate), new Vector3DSerializer()),
            new ContractConvertor(typeof(UnitVector3D), typeof(UnitVector3DSurrogate), new UnitVector3DSerializer()),
            new ContractConvertor(typeof(Angle), typeof(AngleSurrogate), new AngleSerializer()),
            new ContractConvertor(typeof(EulerAngles), typeof(EulerAnglesSurrogate), new EulerAnglesSerializer()),
            new ContractConvertor(typeof(LineSegment2D), typeof(LineSegment2DSurrogate), new LineSegment2DSerializer()),
            new ContractConvertor(typeof(LineSegment3D), typeof(LineSegment3DSurrogate), new LineSegment3DSerializer()),
            new ContractConvertor(typeof(Quaternion), typeof(QuaternionSurrogate), new QuaternionSerializer()),
            new ContractConvertor(typeof(Circle2D), typeof(Circle2DSurrogate), new Circle2DSerializer()),
            new ContractConvertor(typeof(Circle3D), typeof(Circle3DSurrogate), new Circle3DSerializer()),
            new ContractConvertor(typeof(Polygon2D), typeof(Polygon2DSurrogate), new Polygon2DSerializer()),
            new ContractConvertor(typeof(PolyLine2D), typeof(PolyLine2DSurrogate), new PolyLine2DSerializer()),
            new ContractConvertor(typeof(PolyLine3D), typeof(PolyLine3DSurrogate), new PolyLine3DSerializer()),
            new ContractConvertor(typeof(Euclidean.CoordinateSystem), typeof(CoordinateSystemSurrogate), new CoordinateSystemSerializer()),
            new ContractConvertor(typeof(Ray3D), typeof(Ray3DSurrogate), new Ray3DSerializer()),
            new ContractConvertor(typeof(Plane), typeof(PlaneSurrogate), new PlaneSerializer())
        };

        public static SurrogateSelector CreateSurrogateSelector()
        {
            SurrogateSelector s = new SurrogateSelector();
            for (int i = 0; i < SerializerFactory.SurrogateMap.Count; i++)
            {
                s.AddSurrogate(SerializerFactory.SurrogateMap[i].Source, new StreamingContext(StreamingContextStates.All), SerializerFactory.SurrogateMap[i].Serializer);
            }

            return s;
        }

        public static List<Tuple<Type, Type>> KnownSurrogates()
        {
            return SurrogateMap.Select(t => new Tuple<Type, Type>(t.Source, t.Surrogate)).ToList();
        }

        public static bool CanConvert(Type type)
        {
            for (int i = 0; i < SerializerFactory.SurrogateMap.Count; i++)
            {
                if (SurrogateMap[i].Source == type)
                {
                    return true;
                }
            }

            return false;
        }

        public static Type GetSurrogateType(Type type)
        {
            if (SerializerFactory.SurrogateMap.Exists(t => t.Source == type))
            {
                return SerializerFactory.SurrogateMap.Where(t => t.Source == type).First().Surrogate;
            }
            else
            {
                return type;
            }
        }

        public static object GetObjectToSerialize(Type type, object obj)
        {
            if (SerializerFactory.SurrogateMap.Exists(t => t.Source == type))
            {
                var y = SerializerFactory.SurrogateMap.Where(t => t.Source == type).First();
                var conversionmethod = y.Surrogate.GetMethod("op_Implicit", new[] { y.Source });
                return conversionmethod.Invoke(null, new[] { obj });
            }

            return obj;
        }

        public static object GetDeserializedObject(Type surrogateType, object obj)
        {
            if (SerializerFactory.SurrogateMap.Exists(t => t.Surrogate == surrogateType))
            {
                var y = SerializerFactory.SurrogateMap.Where(t => t.Surrogate == surrogateType).First();
                var conversionmethod = y.Surrogate.GetMethod("op_Implicit", new[] { y.Surrogate });
                return conversionmethod.Invoke(null, new[] { obj });
            }

            return obj;
        }
    }
}
