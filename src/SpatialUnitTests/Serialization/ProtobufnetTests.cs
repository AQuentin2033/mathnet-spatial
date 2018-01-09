namespace MathNet.Spatial.Serialization.Xml.UnitTests
{
    using System.IO;
    using System.Linq;
    using MathNet.Spatial;
    using MathNet.Spatial.Euclidean;
    using MathNet.Spatial.Euclidean2D;
    using MathNet.Spatial.UnitTests;
    using NUnit.Framework;

    public class ProtobufNetTests
    {
        private const double Tolerance = 1e-6;

        public ProtobufNetTests()
        {
            var model = ProtoBuf.Meta.RuntimeTypeModel.Default;
        }

        [TestCase("15 °")]
        public void AngleProtoBuf(string vs)
        {
            var angle = Angle.Parse(vs);
            var roundTrip = this.ProtobufRoundTrip(angle);
            Assert.AreEqual(angle.Radians, roundTrip.Radians, Tolerance);
        }

        [Test]
        public void Point2DProtoBuf()
        {
            var p = new Point2D(1, 2);
            var result = this.ProtobufRoundTrip(p);
            Assert.AreEqual(p, result);
        }

        [Test]
        public void Point3DProtoBuf()
        {
            var p = new Point3D(1, -2, 3);
            var result = this.ProtobufRoundTrip(p);
            Assert.AreEqual(p, result);
        }

        [Test]
        public void QuaternionProtoBuf()
        {
            var q = new Quaternion(1, 2, 3, 4);
            var result = this.ProtobufRoundTrip(q);
            Assert.AreEqual(q, result);
        }

        [Test]
        public void EulerAnglesProtoBuf()
        {
            var q = new Quaternion(0, 0, 0, 0);
            var eulerAngles = q.ToEulerAngles();
            var result = this.ProtobufRoundTrip(eulerAngles);
            Assert.AreEqual(eulerAngles, result);
        }

        [TestCase("0, 0, 0", "0, 0, 1")]
        public void PlaneProtoBuf(string rootPoint, string unitVector)
        {
            var plane = new Plane(Point3D.Parse(rootPoint), UnitVector3D.Parse(unitVector));
            var result = this.ProtobufRoundTrip(plane);
            Assert.AreEqual(plane, result);
        }

        [TestCase("1, 2, 3", "-1, 2, 3", false)]
        public void Ray3DProtoBuf(string ps, string vs, bool asElements)
        {
            var ray = new Ray3D(Point3D.Parse(ps), UnitVector3D.Parse(vs));
            var result = this.ProtobufRoundTrip(ray);
            Assert.AreEqual(ray, result);
            AssertGeometry.AreEqual(ray, result, 1e-6);
        }

        [TestCase("1, 2, 3", "4, 5, 6")]
        public void LineSegment3DProtoBuf(string p1s, string p2s)
        {
            Point3D p1 = Point3D.Parse(p1s);
            Point3D p2 = Point3D.Parse(p2s);
            var l = new LineSegment3D(p1, p2);
            var result = this.ProtobufRoundTrip(l);
            Assert.AreEqual(l, result);
        }

        [TestCase("1, 2", "4, 5")]
        public void LineSegment2DProtoBuf(string p1s, string p2s)
        {
            Point2D p1 = Point2D.Parse(p1s);
            Point2D p2 = Point2D.Parse(p2s);
            var l = new LineSegment2D(p1, p2);
            var result = this.ProtobufRoundTrip(l);
            Assert.AreEqual(l, result);
        }

        [Test]
        public void Vector2DProtoBuf()
        {
            var v = new Vector2D(1, 2);
            var result = this.ProtobufRoundTrip(v);
            Assert.AreEqual(v, result);
        }

        [Test]
        public void Vector3DProtoBuf()
        {
            var v = new Vector3D(1, -2, 3);
            var result = this.ProtobufRoundTrip(v);
            Assert.AreEqual(v, result);
        }

        [TestCase("0, 0", 3)]
        public void Circle2DProtoBuf(string point, double radius)
        {
            var center = Point2D.Parse(point);
            var c = new Circle2D(center, radius);
            var result = this.ProtobufRoundTrip(c);
            Assert.AreEqual(c, result);
        }

        [TestCase("0, 0, 0", 2.5)]
        public void Circle3DProtoBuf(string point, double radius)
        {
            var center = Point3D.Parse(point);
            var c = new Circle3D(center, UnitVector3D.ZAxis, radius);
            var result = this.ProtobufRoundTrip(c);
            Assert.AreEqual(c, result);
        }

        [Test]
        public void Polygon2DProtoBuf()
        {
            var points = from x in new string[] { "0.25,0", "0.5,1", "1,-1" } select Point2D.Parse(x);
            var p = new Polygon2D(points);
            var result = this.ProtobufRoundTrip(p);
            Assert.AreEqual(p, result);
        }

        [Test]
        public void PolyLine2DProtoBuf()
        {
            var points = from x in new string[] { "0.25,0", "0.5,1", "1,-1" } select Point2D.Parse(x);
            var p = new PolyLine2D(points);
            var result = this.ProtobufRoundTrip(p);
            Assert.AreEqual(p, result);
        }

        [Test]
        public void PolyLine3DProtoBuf()
        {
            var points = "0, -1.5, 0; 0,1,0; 1,1,0";
            var p = new PolyLine3D(from x in points.Split(';') select Point3D.Parse(x));
            var result = this.ProtobufRoundTrip(p);
            Assert.AreEqual(p, result);
        }

        [Test]
        public void CoordinateSystemProtoBuf()
        {
            var cs = new Euclidean.CoordinateSystem(new Point3D(1, -2, 3), new Vector3D(0, 1, 0), new Vector3D(0, 0, 1), new Vector3D(1, 0, 0));
            var result = this.ProtobufRoundTrip(cs);
            AssertGeometry.AreEqual(cs, result);
        }

        private T ProtobufRoundTrip<T>(T test)
        {
            using (var ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, test);

                ms.Flush();
                ms.Position = 0;
                return ProtoBuf.Serializer.Deserialize<T>(ms);
            }
        }
    }
}
