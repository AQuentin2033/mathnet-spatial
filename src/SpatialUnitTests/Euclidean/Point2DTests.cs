﻿// ReSharper disable InconsistentNaming
namespace MathNet.Spatial.UnitTests.Euclidean
{
    using System;
    using MathNet.Numerics.LinearAlgebra.Double;
    using MathNet.Spatial;
    using MathNet.Spatial.Euclidean;
    using MathNet.Spatial.Euclidean2D;
    using NUnit.Framework;

    public class Point2DTests
    {
        [Test]
        public void Ctor()
        {
            var p = new Point2D(1, 2);
            Assert.AreEqual(1, p.X);
            Assert.AreEqual(2, p.Y);
        }

        [TestCase(5, "90 °", "0, 5")]
        [TestCase(3, "-90 °", "0, -3")]
        [TestCase(1, "45 °", "0.71, 0.71")]
        [TestCase(1, "-45 °", "0.71, -0.71")]
        [TestCase(1, "0 °", "1, 0")]
        [TestCase(1, "180 °", "-1, 0")]
        public void FromPolar(int radius, string avs, string eps)
        {
            var angle = Angle.Parse(avs);
            var p = Point2D.FromPolar(radius, angle);
            var ep = Point2D.Parse(eps);
            AssertGeometry.AreEqual(ep, p, 1e-2);
        }

        [Test]
        public void FromPolarFailsWhenNegativeRadius()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Point2D.FromPolar(-1.0, Angle.FromRadians(0)));
        }

        [TestCase("-1, -2", "1, 2", "0, 0")]
        public void OperatorAddVector2D(string ps, string vs, string eps)
        {
            var p = Point2D.Parse(ps);
            var v = Vector2D.Parse(vs);
            var actual = p + v;
            var expected = Point2D.Parse(eps);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("-1, -2", "1, 2", "-2, -4")]
        public void OperatorSubtractVector2D(string ps, string vs, string eps)
        {
            var p = Point2D.Parse(ps);
            var v = Vector2D.Parse(vs);
            var actual = p - v;
            var expected = Point2D.Parse(eps);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("-1, -2", "1, 2", "-2, -4")]
        public void OperatorSubtractPoint2D(string p1s, string p2s, string eps)
        {
            var p1 = Point2D.Parse(p1s);
            var p2 = Point2D.Parse(p2s);
            var actual = p1 - p2;
            var expected = Vector2D.Parse(eps);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("-1,1", -1, 1)]
        [TestCase("-1,-1", -1, -1)]
        [TestCase("1, 2", 1, 2)]
        [TestCase("1.2; 3.4", 1.2, 3.4)]
        [TestCase("1.2;3.4", 1.2, 3.4)]
        [TestCase("1,2; 3,4", 1.2, 3.4)]
        [TestCase("1.2, 3.4", 1.2, 3.4)]
        [TestCase("1.2 3.4", 1.2, 3.4)]
        [TestCase("1.2,\u00A03.4", 1.2, 3.4)]
        [TestCase("1.2\u00A03.4", 1.2, 3.4)]
        [TestCase("(1.2, 3.4)", 1.2, 3.4)]
        [TestCase("(.1, 2.3e-4)", 0.1, 0.00023000000000000001)]
        public void Parse(string text, double expectedX, double expectedY)
        {
            Assert.AreEqual(true, Point2D.TryParse(text, out var p));
            Assert.AreEqual(expectedX, p.X);
            Assert.AreEqual(expectedY, p.Y);

            p = Point2D.Parse(text);
            Assert.AreEqual(expectedX, p.X);
            Assert.AreEqual(expectedY, p.Y);

            p = Point2D.Parse(p.ToString());
            Assert.AreEqual(expectedX, p.X);
            Assert.AreEqual(expectedY, p.Y);
        }

        [TestCase("1.2")]
        [TestCase("1; 2; 3")]
        public void ParseFails(string text)
        {
            Assert.AreEqual(false, Point2D.TryParse(text, out _));
            Assert.Throws<FormatException>(() => Point2D.Parse(text));
        }

        [Test]
        public void OfVector()
        {
            var p = Point2D.OfVector(DenseVector.OfArray(new[] { 1, 2.0 }));
            Assert.AreEqual(1, p.X);
            Assert.AreEqual(2, p.Y);

            Assert.Throws<ArgumentException>(() => Point2D.OfVector(DenseVector.OfArray(new[] { 1, 2, 3.0 })));
        }

        [Test]
        public void ToDenseVector()
        {
            var p = new Point2D(1, 2);
            var v = p.ToVector();
            Assert.AreEqual(2, v.Count);
            Assert.AreEqual(1, v[0]);
            Assert.AreEqual(2, v[1]);
        }

        [TestCase("0, 0", "1, 0", 1.0)]
        [TestCase("2, 0", "1, 0", 1.0)]
        [TestCase("2, 0", "-1, 0", 3.0)]
        [TestCase("0, 2", "0, -1", 3.0)]
        public void DistanceTo(string p1s, string p2s, double expected)
        {
            var p1 = Point2D.Parse(p1s);
            var p2 = Point2D.Parse(p2s);
            Assert.AreEqual(expected, p1.DistanceTo(p2));
            Assert.AreEqual(expected, p2.DistanceTo(p1));
        }

        [TestCase("0, 0", "1, 2", "0.5, 1")]
        [TestCase("-1, -2", "1, 2", "0, 0")]
        public void MidPoint(string p1s, string p2s, string eps)
        {
            var p1 = Point2D.Parse(p1s);
            var p2 = Point2D.Parse(p2s);
            var centroids = new[]
                           {
                               Point2D.Centroid(p1, p2),
                               Point2D.MidPoint(p1, p2),
                           };
            var expected = Point2D.Parse(eps);
            foreach (var centroid in centroids)
            {
                AssertGeometry.AreEqual(expected, centroid);
            }
        }

        [TestCase("1, 0", "90 °", "0, 1")]
        [TestCase("1, 0", "-90 °", "0, -1")]
        [TestCase("1, 0", "45 °", "0.71, 0.71")]
        [TestCase("1, 0", "-45 °", "0.71, -0.71")]
        [TestCase("1, 0", "30 °", "0.87, 0.5")]
        [TestCase("-5, 0", "30 °", "-4.33, -2.5")]
        public void RotateTest(string ps, string avs, string eps)
        {
            var p = Point2D.Parse(ps);
            var av = Angle.Parse(avs);
            var expected = Point2D.Parse(eps);
            var rm = Matrix2D.Rotation(av);
            var actual = p.TransformBy(rm);
            AssertGeometry.AreEqual(expected, actual, 1e-2);
        }

        [TestCase("0, 0", "1, 2", "1, 2")]
        public void VectorTo(string p1s, string p2s, string evs)
        {
            var p1 = Point2D.Parse(p1s);
            var p2 = Point2D.Parse(p2s);
            var actual = p1.VectorTo(p2);
            var expected = Vector2D.Parse(evs);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("1, 2", "1, 2")]
        public void ToVector(string ps, string evs)
        {
            var p1 = Point2D.Parse(ps);
            var actual = p1.ToVector2D();
            var expected = Vector2D.Parse(evs);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("1, 2", "1, 2", 1e-4, true)]
        [TestCase("-1, 2", "-1, 2", 1e-4, true)]
        [TestCase("1, 2", "3, 4", 1e-4, false)]
        public void Equals(string p1s, string p2s, double tol, bool expected)
        {
            var p1 = Point2D.Parse(p1s);
            var p2 = Point2D.Parse(p2s);
            Assert.AreEqual(expected, p1 == p2);
            Assert.AreEqual(expected, p2 == p1);
            Assert.AreEqual(!expected, p1 != p2);
            Assert.AreEqual(!expected, p2 != p1);

            Assert.AreEqual(expected, p1.Equals(p2));
            Assert.AreEqual(expected, p1.Equals((object)p2));
            Assert.AreEqual(expected, Equals(p1, p2));
            Assert.AreEqual(expected, p1.Equals(p2, tol));
            Assert.AreNotEqual(expected, p1 != p2);
            Assert.AreEqual(false, p1.Equals(null));
        }

        [TestCase("-2, 0", null, "(-2,\u00A00)")]
        [TestCase("-2, 0", "N2", "(-2.00,\u00A00.00)")]
        public void ToString(string vs, string format, string expected)
        {
            var v = Point2D.Parse(vs);
            var actual = v.ToString();
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(v, Point2D.Parse(actual));
        }
    }
}
