namespace MathNet.Spatial.Euclidean2D
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using MathNet.Numerics.LinearAlgebra;
    using MathNet.Spatial;
    using MathNet.Spatial.Internals;

    /// <summary>
    /// Represents a point in 2 dimensional space
    /// </summary>
    public struct Point2D : IEquatable<Point2D>, IDataTag, IGeometricContainable<Point2D>
    {
        /// <summary>
        /// The x coordinate
        /// </summary>
        private double x;

        /// <summary>
        /// the y coordinate
        /// </summary>
        private double y;

        /// <summary>
        /// A reference to the coordinate system in use, if any
        /// </summary>
        private CoordinateSystem cs;

        /// <summary>
        /// A value to indicate if the struct is unmodifiable
        /// </summary>
        private bool frozen;

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2D"/> struct.
        /// Creates a point for given coordinates (x, y)
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.cs = null;
            this.Tag = null;
            this.Parent = null;
            this.frozen = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2D"/> struct.
        /// Creates a point for given coordinates (x, y)
        ///
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="parent">The parent container</param>
        internal Point2D(double x, double y, IGeometricContainer parent)
        {
            this.x = x;
            this.y = y;
            this.Tag = null;
            this.Parent = parent;
            this.cs = parent?.CoordinateSystem;
            this.frozen = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2D"/> struct.
        /// Creates a point for given coordinates (x, y)
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="cs">The coordinate system</param>
        internal Point2D(double x, double y, CoordinateSystem cs)
        {
            this.x = x;
            this.y = y;
            this.cs = cs;
            this.Tag = null;
            this.Parent = null;
            this.frozen = false;
        }

        /// <summary>
        /// Gets a point at the origin (0,0)
        /// </summary>
        public static Point2D Origin => new Point2D(0, 0);

        /// <summary>
        /// Gets the exact internal value of X.
        /// When used in a coordinate system, it returns the internal double representation of the x coordinate.
        /// When used outside a coordinate system, it returns the same as X
        /// </summary>
        public double ExactX => this.x;

        /// <summary>
        /// Gets the exact internal value of Y.
        /// When used in a coordinate system, it returns the internal double representation of the y coordinate.
        /// When used outside a coordinate system, it returns the same as Y
        /// </summary>
        public double ExactY => this.y;

        /// <summary>
        /// Gets or sets the x coordinate
        /// </summary>
        public double X
        {
            get
            {
                if (this.cs is null)
                {
                    return this.x;
                }
                else
                {
                    return Math.Round(this.x, this.cs.Precision, MidpointRounding.AwayFromZero);
                }
            }

            set
            {
                if (this.frozen)
                {
                    throw new InvalidOperationException("Point is frozen");
                }

                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the y coordinate
        /// </summary>
        public double Y
        {
            get
            {
                if (this.cs is null)
                {
                    return this.y;
                }
                else
                {
                    return Math.Round(this.y, this.cs.Precision, MidpointRounding.AwayFromZero);
                }
            }

            set
            {
                if (this.frozen)
                {
                    throw new InvalidOperationException("Point is frozen");
                }

                this.y = value;
            }
        }

        /// <inheritdoc />
        public object Tag { get; set; }

        /// <inheritdoc />
        public IGeometricContainer Parent { get; private set; }

        /// <inheritdoc />
        public bool CanFreeze => !this.frozen;

        /// <inheritdoc />
        public bool IsFrozen => this.frozen;

        /// <summary>
        /// Gets a reference to the coordinate system used
        /// </summary>
        internal CoordinateSystem CSReference => this.cs;

        /// <summary>
        /// Adds a point and a vector together
        /// </summary>
        /// <param name="point">A point</param>
        /// <param name="vector">A vector</param>
        /// <returns>A new point at the summed location</returns>
        public static Point2D operator +(Point2D point, Vector2D vector)
        {
            return new Point2D(point.X + vector.X, point.Y + vector.Y);
        }

        /// <summary>
        /// Subtracts a vector from a point
        /// </summary>
        /// <param name="left">A point</param>
        /// <param name="right">A vector</param>
        /// <returns>A new point at the difference</returns>
        public static Point2D operator -(Point2D left, Vector2D right)
        {
            return new Point2D(left.X - right.X, left.Y - right.Y);
        }

        /// <summary>
        /// Subtracts the first point from the second point
        /// </summary>
        /// <param name="left">The first point</param>
        /// <param name="right">The second point</param>
        /// <returns>A vector pointing to the difference</returns>
        public static Vector2D operator -(Point2D left, Point2D right)
        {
            return new Vector2D(left.X - right.X, left.Y - right.Y);
        }

        /// <summary>
        /// Returns a value that indicates whether each pair of elements in two specified points is equal.
        /// </summary>
        /// <param name="left">The first point to compare</param>
        /// <param name="right">The second point to compare</param>
        /// <returns>True if the points are the same; otherwise false.</returns>
        public static bool operator ==(Point2D left, Point2D right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Returns a value that indicates whether any pair of elements in two specified points is not equal.
        /// </summary>
        /// <param name="left">The first point to compare</param>
        /// <param name="right">The second point to compare</param>
        /// <returns>True if the points are different; otherwise false.</returns>
        public static bool operator !=(Point2D left, Point2D right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2D"/> struct.
        /// Creates a point r from origin rotated a counterclockwise from X-Axis
        /// </summary>
        /// <param name="radius">distance from origin</param>
        /// <param name="angle">the angle</param>
        /// <returns>The <see cref="Point2D"/></returns>
        public static Point2D FromPolar(double radius, Angle angle)
        {
            if (radius < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(radius), radius, "Expected a radius greater than or equal to zero.");
            }

            return new Point2D(
                radius * Math.Cos(angle.Radians),
                radius * Math.Sin(angle.Radians));
        }

        /// <summary>
        /// Attempts to convert a string of the form x,y into a point
        /// </summary>
        /// <param name="text">The string to be converted</param>
        /// <param name="result">A point at the coordinates specified</param>
        /// <returns>True if <paramref name="text"/> could be parsed.</returns>
        public static bool TryParse(string text, out Point2D result)
        {
            return TryParse(text, null, out result);
        }

        /// <summary>
        /// Attempts to convert a string of the form x,y into a point
        /// </summary>
        /// <param name="text">The string to be converted</param>
        /// <param name="formatProvider">The <see cref="IFormatProvider"/></param>
        /// <param name="result">A point at the coordinates specified</param>
        /// <returns>True if <paramref name="text"/> could be parsed.</returns>
        public static bool TryParse(string text, IFormatProvider formatProvider, out Point2D result)
        {
            if (Text.TryParse2D(text, formatProvider, out var x, out var y))
            {
                result = new Point2D(x, y);
                return true;
            }

            result = default(Point2D);
            return false;
        }

        /// <summary>
        /// Attempts to convert a string of the form x,y into a point
        /// </summary>
        /// <param name="value">The string to be converted</param>
        /// <param name="formatProvider">The <see cref="IFormatProvider"/></param>
        /// <returns>A point at the coordinates specified</returns>
        public static Point2D Parse(string value, IFormatProvider formatProvider = null)
        {
            if (TryParse(value, formatProvider, out var p))
            {
                return p;
            }

            throw new FormatException($"Could not parse a Point2D from the string {value}");
        }

        /// <summary>
        /// Returns the centeroid or center of mass of any set of points
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>the centeroid point</returns>
        public static Point2D Centroid(IEnumerable<Point2D> points)
        {
            return Centroid(points.ToArray());
        }

        /// <summary>
        /// Returns the centeroid or center of mass of any set of points
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>the centeroid point</returns>
        public static Point2D Centroid(params Point2D[] points)
        {
            return new Point2D(
                points.Average(point => point.X),
                points.Average(point => point.Y));
        }

        /// <summary>
        /// Returns a point midway between the provided points <paramref name="point1"/> and <paramref name="point2"/>
        /// </summary>
        /// <param name="point1">point A</param>
        /// <param name="point2">point B</param>
        /// <returns>a new point midway between the provided points</returns>
        public static Point2D MidPoint(Point2D point1, Point2D point2)
        {
            return Centroid(point1, point2);
        }

        /// <summary>
        /// Create a new Point2D from a Math.NET Numerics vector of length 2.
        /// </summary>
        /// <param name="vector"> A vector with length 2 to populate the created instance with.</param>
        /// <returns> A <see cref="Point2D"/></returns>
        public static Point2D OfVector(Vector<double> vector)
        {
            if (vector.Count != 2)
            {
                throw new ArgumentException("The vector length must be 2 in order to convert it to a Point2D");
            }

            return new Point2D(vector.At(0), vector.At(1));
        }

        /// <summary>
        /// Applies a transform matrix to the point
        /// </summary>
        /// <param name="m">A transform matrix</param>
        /// <returns>A new point</returns>
        public Point2D TransformBy(Matrix<double> m)
        {
            return OfVector(m.Multiply(this.ToVector()));
        }

        /// <summary>
        /// Gets a vector from this point to another point
        /// </summary>
        /// <param name="otherPoint">The point to which the vector should go</param>
        /// <returns>A vector pointing to the other point.</returns>
        public Vector2D VectorTo(Point2D otherPoint)
        {
            return otherPoint - this;
        }

        /// <summary>
        /// Finds the straight line distance to another point
        /// </summary>
        /// <param name="otherPoint">The other point</param>
        /// <returns>a distance measure</returns>
        public double DistanceTo(Point2D otherPoint)
        {
            var vector = this.VectorTo(otherPoint);
            if (this.cs != null)
            {
                return Math.Round(vector.Length, this.cs.Precision, MidpointRounding.AwayFromZero);
            }
            else
            {
                return vector.Length;
            }
        }

        /// <summary>
        /// Converts this point into a vector from the origin
        /// </summary>
        /// <returns>A vector equivalent to this point</returns>
        public Vector2D ToVector2D()
        {
            return new Vector2D(this.X, this.Y);
        }

        /// <summary>
        /// Convert to a Math.NET Numerics dense vector of length 2.
        /// </summary>
        /// <returns> A <see cref="Vector{Double}"/> with the x and y values from this instance.</returns>
        public Vector<double> ToVector()
        {
            return Vector<double>.Build.Dense(new[] { this.X, this.Y });
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var numberFormatInfo = CultureInfo.InvariantCulture.NumberFormat;
            var separator = numberFormatInfo.NumberDecimalSeparator == "," ? ";" : ",";
            return $"({this.X}{separator}\u00A0{this.Y})";
        }

        /// <inheritdoc />
        public bool Equals(Point2D other)
        {
            //// ReSharper disable CompareOfFloatsByEqualityOperator
            return this.X == other.X && this.Y == other.Y;
            //// ReSharper restore CompareOfFloatsByEqualityOperator
        }

        /// <summary>
        /// Returns a value to indicate if a pair of points are equal
        /// </summary>
        /// <param name="other">The point to compare against.</param>
        /// <param name="tolerance">A tolerance (epsilon) to adjust for floating point error</param>
        /// <returns>true if the points are equal; otherwise false</returns>
        public bool Equals(Point2D other, double tolerance)
        {
            if (tolerance < 0)
            {
                throw new ArgumentException("epsilon < 0");
            }

            return Math.Abs(other.X - this.X) < tolerance &&
                   Math.Abs(other.Y - this.Y) < tolerance;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Point2D p && this.Equals(p);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.X.GetHashCode() * 397) ^ this.Y.GetHashCode();
            }
        }

        /// <inheritdoc />
        public Point2D Clone()
        {
            return new Point2D(this.x, this.y, this.cs);
        }

        /// <inheritdoc />
        public void Freeze()
        {
            if (this.frozen)
            {
                throw new InvalidOperationException("Point is already frozen");
            }

            this.frozen = true;
        }
    }
}
