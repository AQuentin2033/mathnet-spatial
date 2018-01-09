// ReSharper disable InconsistentNaming
namespace MathNet.Spatial.UnitTests.Euclidean
{
    using System.IO;
    using MathNet.Spatial.Euclidean;
    using NUnit.Framework;

    [TestFixture]
    public class Ray3DTests
    {
        [TestCase("1, 2, 3", "0, 0, 1", "1, 2, 3", "0, 0, 1")]
        public void Parse(string rootPoint, string unitVector, string eps, string evs)
        {
            var ray = new Ray3D(Point3D.Parse(rootPoint), UnitVector3D.Parse(unitVector));
            AssertGeometry.AreEqual(Point3D.Parse(eps), ray.ThroughPoint);
            AssertGeometry.AreEqual(Vector3D.Parse(evs), ray.Direction);
        }

        [TestCase("0, 0, 0", "0, 0, 1", "0, 0, 0", "0, 1, 0", "0, 0, 0", "-1, 0, 0")]
        [TestCase("0, 0, 2", "0, 0, 1", "0, 0, 0", "0, 1, 0", "0, 0, 2", "-1, 0, 0")]
        public void IntersectionOf(string rootPoint1, string unitVector1, string rootPoint2, string unitVector2, string eps, string evs)
        {
            var plane1 = new Plane(Point3D.Parse(rootPoint1), UnitVector3D.Parse(unitVector1));
            var plane2 = new Plane(Point3D.Parse(rootPoint2), UnitVector3D.Parse(unitVector2));
            var actual = Ray3D.IntersectionOf(plane1, plane2);
            var expected = Ray3D.Parse(eps, evs);
            AssertGeometry.AreEqual(expected, actual);
        }

        [Test]
        public void LineToTest()
        {
            var ray = new Ray3D(new Point3D(0, 0, 0), UnitVector3D.ZAxis);
            var point3D = new Point3D(1, 0, 0);
            var line3DTo = ray.ShortestLineTo(point3D);
            AssertGeometry.AreEqual(new Point3D(0, 0, 0), line3DTo.StartPoint);
            AssertGeometry.AreEqual(point3D, line3DTo.EndPoint, float.Epsilon);
        }

        [TestCase("0, 0, 0", "1, -1, 1", "0, 0, 0", "1, -1, 1", true)]
        [TestCase("0, 0, 2", "1, -1, 1", "0, 0, 0", "1, -1, 1", false)]
        [TestCase("0, 0, 0", "1, -1, 1", "0, 0, 0", "2, -1, 1", false)]
        public void Equals(string p1s, string v1s, string p2s, string v2s, bool expected)
        {
            var ray1 = new Ray3D(Point3D.Parse(p1s), UnitVector3D.Parse(v1s, tolerance: 2));
            var ray2 = new Ray3D(Point3D.Parse(p2s), UnitVector3D.Parse(v2s, tolerance: 2));
            Assert.AreEqual(expected, ray1.Equals(ray2));
            Assert.AreEqual(expected, ray1 == ray2);
            Assert.AreEqual(!expected, ray1 != ray2);
        }
    }
}
