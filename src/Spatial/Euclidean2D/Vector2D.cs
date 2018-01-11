namespace MathNet.Spatial.Euclidean2D
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using MathNet.Spatial;
    using MathNet.Spatial.Internals;

    /// <summary>
    /// A struct representing a vector in 2D space
    /// </summary>
    public class Vector2D : IEquatable<Vector2D>, IFormattable
    {
        private double x;

        private double y;

        /// <summary>
        /// A reference to the coordinate system in use, if any
        /// </summary>
        private CoordinateSystem cs = null;

        /// <summary>
        /// A value to indicate if the struct is unmodifiable
        /// </summary>
        private bool frozen = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="x">The x component.</param>
        /// <param name="y">The y component.</param>
        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Gets or sets the x component.
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

        /// <summary>
        /// Gets the length of the vector
        /// </summary>
        public double Length => Math.Sqrt((this.x * this.x) + (this.y * this.y));

        /// <summary>
        /// Returns a value that indicates whether each pair of elements in two specified vectors is equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>True if the vectors are the same; otherwise false.</returns>
        public static bool operator ==(Vector2D left, Vector2D right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Returns a value that indicates whether any pair of elements in two specified vectors is not equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>True if the vectors are different; otherwise false.</returns>
        public static bool operator !=(Vector2D left, Vector2D right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Adds two vectors
        /// </summary>
        /// <param name="left">The first vector</param>
        /// <param name="right">The second vector</param>
        /// <returns>A new summed vector</returns>
        public static Vector2D operator +(Vector2D left, Vector2D right)
        {
            left.Add(right);
            return left;
        }

        /// <summary>
        /// Subtracts two vectors
        /// </summary>
        /// <param name="left">The first vector</param>
        /// <param name="right">The second vector</param>
        /// <returns>A new difference vector</returns>
        public static Vector2D operator -(Vector2D left, Vector2D right)
        {
            left.Subtract(right);
            return left;
        }

        /// <summary>
        /// Negates the vector
        /// </summary>
        /// <param name="v">A vector to negate</param>
        /// <returns>A new negated vector</returns>
        public static Vector2D operator -(Vector2D v)
        {
            return v.Negate();
        }

        /// <summary>
        /// Multiplies a vector by a scalar
        /// </summary>
        /// <param name="d">A scalar</param>
        /// <param name="v">A vector</param>
        /// <returns>A scaled vector</returns>
        public static Vector2D operator *(double d, Vector2D v)
        {
            v.x *= d;
            v.y *= d;
            return v;
        }

        /// <summary>
        /// Multiplies a vector by a scalar
        /// </summary>
        /// <param name="v">A vector</param>
        /// <param name="d">A scalar</param>
        /// <returns>A scaled vector</returns>
        public static Vector2D operator *(Vector2D v, double d)
        {
            v.x *= d;
            v.y *= d;
            return v;
        }

        /// <summary>
        /// Divides a vector by a scalar
        /// </summary>
        /// <param name="v">A vector</param>
        /// <param name="d">A scalar</param>
        /// <returns>A scaled vector</returns>
        public static Vector2D operator /(Vector2D v, double d)
        {
            v.x /= d;
            v.y /= d;
            return v;
        }

        /// <summary>
        /// Creates a Vector from Polar coordinates
        /// </summary>
        /// <param name="radius">The distance of the point from the origin</param>
        /// <param name="angle">The angle of the point as measured from the X Axis</param>
        /// <returns>A vector.</returns>
        public static Vector2D FromPolar(double radius, Angle angle)
        {
            if (radius < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(radius), radius, "Expected a radius greater than or equal to zero.");
            }

            return new Vector2D(
                radius * Math.Cos(angle.Radians),
                radius * Math.Sin(angle.Radians));
        }

        /// <summary>
        /// Attempts to convert a string of the form x,y into a point
        /// </summary>
        /// <param name="text">The string to be converted</param>
        /// <param name="result">A point at the coordinates specified</param>
        /// <returns>True if <paramref name="text"/> could be parsed.</returns>
        public static bool TryParse(string text, out Vector2D result)
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
        public static bool TryParse(string text, IFormatProvider formatProvider, out Vector2D result)
        {
            if (Text.TryParse2D(text, formatProvider, out var x, out var y))
            {
                result = new Vector2D(x, y);
                return true;
            }

            result = default(Vector2D);
            return false;
        }

        /// <summary>
        /// Attempts to convert a string of the form x,y into a point
        /// </summary>
        /// <param name="value">The string to be converted</param>
        /// <param name="formatProvider">The <see cref="IFormatProvider"/></param>
        /// <returns>A point at the coordinates specified</returns>
        public static Vector2D Parse(string value, IFormatProvider formatProvider = null)
        {
            if (TryParse(value, formatProvider, out var p))
            {
                return p;
            }

            throw new FormatException($"Could not parse a Vector2D from the string {value}");
        }

        /// <summary>
        /// Computes whether or not this vector is perpendicular to <paramref name="other"/> vector by:
        /// 1. Normalizing both
        /// 2. Computing the dot product.
        /// 3. Comparing 1- Math.Abs(dot product) to <paramref name="tolerance"/>
        /// </summary>
        /// <param name="other">The other <see cref="Vector2D"/></param>
        /// <param name="tolerance">The tolerance for when vectors are said to be parallel</param>
        /// <returns>True if the vector dot product is within the given double tolerance of unity, false if not</returns>
        [Pure]
        public bool IsParallelTo(Vector2D other, double tolerance = 1e-10)
        {
            double l = this.Length * other.Length;
            var dp = this.DotProduct(other) / l;
            return dp > 1 - tolerance;
        }

        /// <summary>
        /// Computes whether or not this vector is parallel to another vector within a given angle tolerance.
        /// </summary>
        /// <param name="other">The other <see cref="Vector2D"/></param>
        /// <param name="tolerance">The tolerance for when vectors are said to be parallel</param>
        /// <returns>True if the vectors are parallel within the angle tolerance, false if they are not</returns>
        [Pure]
        public bool IsParallelTo(Vector2D other, Angle tolerance)
        {
            // Compute the angle between these vectors
            var angle = this.AngleTo(other);
            if (angle < tolerance)
            {
                return true;
            }

            // Compute the 180° opposite of the angle
            return Angle.FromRadians(Math.PI) - angle < tolerance;
        }

        /// <summary>
        /// Computes whether or not this vector is perpendicular to <paramref name="other"/> vector by:
        /// 1. Normalizing both
        /// 2. Computing the dot product.
        /// 3. Comparing Math.Abs(dot product) to <paramref name="tolerance"/>
        /// </summary>
        /// <param name="other">The other <see cref="Vector2D"/></param>
        /// <param name="tolerance">The tolerance for when vectors are said to be parallel</param>
        /// <returns>True if the vector dot product is within the given double tolerance of unity, false if not</returns>
        [Pure]
        public bool IsPerpendicularTo(Vector2D other, double tolerance = 1e-10)
        {
            double l = this.Length * other.Length;
            return Math.Abs(this.DotProduct(other) / l) < tolerance;
        }

        /// <summary>
        /// Computes whether or not this vector is parallel to another vector within a given angle tolerance.
        /// </summary>
        /// <param name="other">The other <see cref="Vector2D"/></param>
        /// <param name="tolerance">The tolerance for when vectors are said to be parallel</param>
        /// <returns>True if the vectors are parallel within the angle tolerance, false if they are not</returns>
        [Pure]
        public bool IsPerpendicularTo(Vector2D other, Angle tolerance)
        {
            var angle = this.AngleTo(other);
            const double Perpendicular = Math.PI / 2;
            return Math.Abs(angle.Radians - Perpendicular) < tolerance.Radians;
        }

        /// <summary>
        /// Compute the signed angle to another vector.
        /// </summary>
        /// <param name="other">The other <see cref="Vector2D"/></param>
        /// <param name="clockWise">Positive in clockwise direction</param>
        /// <param name="returnNegative">When true and the result is > 180° a negative value is returned</param>
        /// <returns>The angle between the vectors.</returns>
        [Pure]
        public Angle SignedAngleTo(Vector2D other, bool clockWise = false, bool returnNegative = false)
        {
            const double twopi = 2 * Math.PI;
            var sign = clockWise ? -1 : 1;
            var a1 = Math.Atan2(this.y, this.x);
            if (a1 < 0)
            {
                a1 += twopi;
            }

            var a2 = Math.Atan2(other.y, other.x);
            if (a2 < 0)
            {
                a2 += twopi;
            }

            var a = sign * (a2 - a1);
            if (a < 0 && !returnNegative)
            {
                a += twopi;
            }

            if (a > Math.PI && returnNegative)
            {
                a -= twopi;
            }

            return Angle.FromRadians(a);
        }

        /// <summary>
        /// Compute the angle between this vector and another using the arccosine of the dot product.
        /// </summary>
        /// <param name="other">The other <see cref="Vector2D"/></param>
        /// <returns>The angle between vectors, with a range between 0° and 180°</returns>
        [Pure]
        public Angle AngleTo(Vector2D other)
        {
            return Angle.FromRadians(
                Math.Abs(
                    Math.Atan2(
                        (this.x * other.y) - (other.x * this.y),
                        (this.x * other.x) + (this.y * other.y))));
        }

        /// <summary>
        /// Rotates a Vector by an angle
        /// </summary>
        /// <param name="angle">The angle.</param>
        public void Rotate(Angle angle)
        {
            var cs = angle.Cos();
            var sn = angle.Sin();
            var x = (this.x * cs) - (this.y * sn);
            var y = (this.x * sn) + (this.y * cs);
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Perform the dot product on a pair of vectors
        /// </summary>
        /// <param name="other">The second vector</param>
        /// <returns>The result of the dot product.</returns>
        [Pure]
        public double DotProduct(Vector2D other)
        {
            return (this.x * other.x) + (this.y * other.y);
        }

        /// <summary>
        /// Performs the 2D 'cross product' as if the 2D vectors were really 3D vectors in the z=0 plane, returning
        /// the scalar magnitude and direction of the resulting z value.
        /// Formula: (this.X * other.Y) - (this.Y * other.X)
        /// </summary>
        /// <param name="other">The other <see cref="Vector2D"/></param>
        /// <returns>(this.X * other.Y) - (this.Y * other.X)</returns>
        [Pure]
        public double CrossProduct(Vector2D other)
        {
            // Though the cross product is undefined in 2D space, this is a useful mathematical operation to
            // determine angular direction and to compute the area of 2D shapes
            return (this.x * other.y) - (this.y * other.x);
        }

        /// <summary>
        /// Projects this vector onto another vector
        /// </summary>
        /// <param name="other">The other <see cref="Vector2D"/></param>
        /// <returns>A <see cref="Vector2D"/> representing this vector projected on <paramref name="other"/></returns>
        public Vector2D ProjectOn(Vector2D other)
        {
            var dpthis = (this.x * other.x) + (this.y * other.y);
            var dpthat = (other.x * other.x) + (other.y * other.y);
            var d = dpthis / dpthat;
            other.x *= d;
            other.y *= d;
            return other;
        }

        /// <summary>
        /// Creates a new unit vector from the existing vector.
        /// </summary>
        public void Normalize()
        {
            this.x /= this.Length;
            this.y /= this.Length;
        }

        /// <summary>
        /// Scales the vector by the provided value
        /// </summary>
        /// <param name="d">a scaling factor</param>
        [Pure]
        public void ScaleBy(double d)
        {
            this.x *= d;
            this.y *= d;
        }

        /// <summary>
        /// Returns the negative of the vector
        /// </summary>
        /// <returns>A new negated vector.</returns>
        [Pure]
        public Vector2D Negate()
        {
            this.x *= -1.0;
            this.y *= -1.0;
            return this;
        }

        /// <summary>
        /// Subtracts a vector from this vector.
        /// </summary>
        /// <param name="v">A vector to subtract</param>
        public void Subtract(Vector2D v)
        {
            this.x -= v.x;
            this.y -= v.y;
        }

        /// <summary>
        /// Adds a vector to this vector
        /// </summary>
        /// <param name="v">A vector to add</param>
        public void Add(Vector2D v)
        {
            this.x += v.x;
            this.y += v.y;
        }

        /// <inheritdoc />
        [Pure]
        public bool Equals(Vector2D other)
        {
            //// ReSharper disable CompareOfFloatsByEqualityOperator
            return this.X.Equals(other.X) && this.Y.Equals(other.Y);
            //// ReSharper restore CompareOfFloatsByEqualityOperator
        }

        /// <summary>
        /// Compare this instance with <paramref name="other"/>
        /// </summary>
        /// <param name="other">The other <see cref="Vector2D"/></param>
        /// <param name="tolerance">The tolerance when comparing the x and y components</param>
        /// <returns>True if found to be equal.</returns>
        [Pure]
        public bool Equals(Vector2D other, double tolerance)
        {
            if (tolerance < 0)
            {
                throw new ArgumentException("epsilon < 0");
            }

            return Math.Abs(other.X - this.X) < tolerance &&
                   Math.Abs(other.Y - this.Y) < tolerance;
        }

        /// <inheritdoc />
        [Pure]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Vector2D v && this.Equals(v);
        }

        /// <inheritdoc />
        [Pure]
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.X.GetHashCode() * 397) ^ this.Y.GetHashCode();
            }
        }

        /// <inheritdoc />
        [Pure]
        public override string ToString()
        {
            return this.ToString(null, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns a string representation of this instance using the provided <see cref="IFormatProvider"/>
        /// </summary>
        /// <param name="provider">A <see cref="IFormatProvider"/></param>
        /// <returns>The string representation of this instance.</returns>
        [Pure]
        public string ToString(IFormatProvider provider)
        {
            return this.ToString(null, provider);
        }

        /// <inheritdoc />
        [Pure]
        public string ToString(string format, IFormatProvider provider = null)
        {
            var numberFormatInfo = provider != null ? NumberFormatInfo.GetInstance(provider) : CultureInfo.InvariantCulture.NumberFormat;
            var separator = numberFormatInfo.NumberDecimalSeparator == "," ? ";" : ",";
            return $"({this.X.ToString(format, numberFormatInfo)}{separator}\u00A0{this.Y.ToString(format, numberFormatInfo)})";
        }
    }
}
