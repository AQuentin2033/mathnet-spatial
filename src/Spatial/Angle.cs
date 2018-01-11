namespace MathNet.Spatial
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using MathNet.Spatial.Internals;

    /// <summary>
    /// An angle
    /// </summary>
    public readonly struct Angle : IComparable<Angle>, IFormattable
    {
        /// <summary>
        /// An Angle of Math.PI or 180 Degrees
        /// </summary>
        public static readonly Angle PI = new Angle(Math.PI);

        /// <summary>
        /// An Angle of Math.PI * 2 or 360 Degrees
        /// </summary>
        public static readonly Angle TwoPI = new Angle(Math.PI * 2);

        /// <summary>
        /// An Angle of Math.PI / 2 or 90 Degrees
        /// </summary>
        public static readonly Angle PIOverTwo = new Angle(Math.PI / 2);

        /// <summary>
        /// The value in radians
        /// </summary>
        public readonly double Radians;

        /// <summary>
        /// Conversion factor for converting Radians to Degrees
        /// </summary>
        private const double RadToDeg = 180.0 / Math.PI;

        /// <summary>
        /// Conversion factor for converting Radians to Degrees
        /// </summary>
        private const double DegToGrad = 10.0 / 9.0;

        /// <summary>
        /// Conversion factor for converting Degrees to Radians
        /// </summary>
        private const double DegToRad = Math.PI / 180.0;

        /// <summary>
        /// A lazy loaded string formatter
        /// </summary>
        private static Lazy<AngleFormatProvider> formatter = new Lazy<AngleFormatProvider>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Angle"/> struct.
        /// </summary>
        /// <param name="radians">The value in Radians</param>
        private Angle(double radians)
        {
            this.Radians = radians;
        }

        /// <summary>
        /// Gets a string formatter
        /// </summary>
        public static AngleFormatProvider Formatter => formatter.Value;

        /// <summary>
        /// Gets the value in degrees
        /// </summary>
        public double Degrees => this.Radians * RadToDeg;

        /// <summary>
        /// Gets the value in degrees
        /// </summary>
        public double Gradians => this.Degrees * DegToGrad;

        /// <summary>
        /// Returns a value that indicates whether two specified Angles are equal.
        /// </summary>
        /// <param name="left">The first angle to compare</param>
        /// <param name="right">The second angle to compare</param>
        /// <returns>True if the angles are the same; otherwise false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Angle left, Angle right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Returns a value that indicates whether two specified Angles are not equal.
        /// </summary>
        /// <param name="left">The first angle to compare</param>
        /// <param name="right">The second angle to compare</param>
        /// <returns>True if the angles are different; otherwise false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Angle left, Angle right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Returns a value that indicates if a specified Angles is less than another.
        /// </summary>
        /// <param name="left">The first angle to compare</param>
        /// <param name="right">The second angle to compare</param>
        /// <returns>True if the first angle is less than the second angle; otherwise false.</returns>
        public static bool operator <(Angle left, Angle right)
        {
            return left.Radians < right.Radians;
        }

        /// <summary>
        /// Returns a value that indicates if a specified Angles is greater than another.
        /// </summary>
        /// <param name="left">The first angle to compare</param>
        /// <param name="right">The second angle to compare</param>
        /// <returns>True if the first angle is greater than the second angle; otherwise false.</returns>
        public static bool operator >(Angle left, Angle right)
        {
            return left.Radians > right.Radians;
        }

        /// <summary>
        /// Returns a value that indicates if a specified Angles is less than or equal to another.
        /// </summary>
        /// <param name="left">The first angle to compare</param>
        /// <param name="right">The second angle to compare</param>
        /// <returns>True if the first angle is less than or equal to the second angle; otherwise false.</returns>
        public static bool operator <=(Angle left, Angle right)
        {
            return left.Radians <= right.Radians;
        }

        /// <summary>
        /// Returns a value that indicates if a specified Angles is greater than or equal to another.
        /// </summary>
        /// <param name="left">The first angle to compare</param>
        /// <param name="right">The second angle to compare</param>
        /// <returns>True if the first angle is greater than or equal to the second angle; otherwise false.</returns>
        public static bool operator >=(Angle left, Angle right)
        {
            return left.Radians >= right.Radians;
        }

        /// <summary>
        /// Multiplies an Angle by a scalar
        /// </summary>
        /// <param name="left">The scalar.</param>
        /// <param name="right">The angle.</param>
        /// <returns>A new angle equal to the product of the angle and the scalar.</returns>
        public static Angle operator *(double left, Angle right)
        {
            return new Angle(left * right.Radians);
        }

        /// <summary>
        /// Multiplies an Angle by a scalar
        /// </summary>
        /// <param name="left">The angle.</param>
        /// <param name="right">The scalar.</param>
        /// <returns>A new angle equal to the product of the angle and the scalar.</returns>
        public static Angle operator *(Angle left, double right)
        {
            return new Angle(left.Radians * right);
        }

        /// <summary>
        /// Divides an Angle by a scalar
        /// </summary>
        /// <param name="left">The angle.</param>
        /// <param name="right">The scalar.</param>
        /// <returns>A new angle equal to the division of the angle by the scalar.</returns>
        public static Angle operator /(Angle left, double right)
        {
            return new Angle(left.Radians / right);
        }

        /// <summary>
        /// Adds two angles together
        /// </summary>
        /// <param name="left">The first angle.</param>
        /// <param name="right">The second angle.</param>
        /// <returns>A new Angle equal to the sum of the provided angles.</returns>
        public static Angle operator +(Angle left, Angle right)
        {
            return new Angle(left.Radians + right.Radians);
        }

        /// <summary>
        /// Subtracts the second angle from the first
        /// </summary>
        /// <param name="left">The first angle.</param>
        /// <param name="right">The second angle.</param>
        /// <returns>A new Angle equal to the difference of the provided angles.</returns>
        public static Angle operator -(Angle left, Angle right)
        {
            return new Angle(left.Radians - right.Radians);
        }

        /// <summary>
        /// Negates the angle
        /// </summary>
        /// <param name="angle">The angle to negate.</param>
        /// <returns>The negated angle.</returns>
        public static Angle operator -(Angle angle)
        {
            return new Angle(-1 * angle.Radians);
        }

        /// <summary>
        /// Attempts to convert a string into an <see cref="Angle"/>
        /// </summary>
        /// <param name="text">The string to be converted</param>
        /// <param name="result">Am <see cref="Angle"/></param>
        /// <returns>True if <paramref name="text"/> could be parsed.</returns>
        public static bool TryParse(string text, out Angle result)
        {
            return TryParse(text, null, out result);
        }

        /// <summary>
        /// Attempts to convert a string into an <see cref="Angle"/>
        /// </summary>
        /// <param name="text">The string to be converted</param>
        /// <param name="formatProvider">The <see cref="IFormatProvider"/></param>
        /// <param name="result">An <see cref="Angle"/></param>
        /// <returns>True if <paramref name="text"/> could be parsed.</returns>
        public static bool TryParse(string text, IFormatProvider formatProvider, out Angle result)
        {
            if (Text.TryParseAngle(text, formatProvider, out result))
            {
                return true;
            }

            result = default(Angle);
            return false;
        }

        /// <summary>
        /// Attempts to convert a string into an <see cref="Angle"/>
        /// </summary>
        /// <param name="value">The string to be converted</param>
        /// <param name="formatProvider">The <see cref="IFormatProvider"/></param>
        /// <returns>An <see cref="Angle"/></returns>
        public static Angle Parse(string value, IFormatProvider formatProvider = null)
        {
            if (TryParse(value, formatProvider, out var p))
            {
                return p;
            }

            throw new FormatException($"Could not parse an Angle from the string {value}");
        }

        /// <summary>
        /// Creates a new instance of Angle.
        /// </summary>
        /// <param name="value">The value in degrees.</param>
        /// <returns> A new instance of the <see cref="Angle"/> struct.</returns>
        public static Angle FromDegrees(double value)
        {
            return new Angle(value * DegToRad);
        }

        /// <summary>
        /// Creates a new instance of Angle.
        /// </summary>
        /// <param name="value">The value in radians.</param>
        /// <returns> A new instance of the <see cref="Angle"/> struct.</returns>
        public static Angle FromRadians(double value)
        {
            return new Angle(value);
        }

        /// <summary>
        /// Creates a new instance of Angle from the sexagesimal format of the angle in degrees, minutes, seconds
        /// </summary>
        /// <param name="degrees">The degrees of the angle</param>
        /// <param name="minutes">The minutes of the angle</param>
        /// <param name="seconds">The seconds of the angle</param>
        /// <returns>A new instance of the <see cref="Angle"/> struct.</returns>
        public static Angle FromSexagesimal(int degrees, int minutes, double seconds)
        {
            return Angle.FromDegrees(degrees + (minutes / 60.0F) + (seconds / 3600.0F));
        }

        /// <summary>
        /// Returns a string representation of an Angle according to the default representation provided by <see cref="AngleFormatProvider"/>
        /// </summary>
        /// <returns>The string representation of this instance.</returns>
        public override string ToString()
        {
            return this.ToString("R");
        }

        /// <summary>
        /// Returns a string representation of the Angle using requested format.  See <see cref="AngleFormatProvider"/> for details of formatting options
        /// </summary>
        /// <param name="format">A string indicating the desired format</param>
        /// <returns>The string representation of this instance.</returns>
        public string ToString(string format)
        {
            return this.ToString(format, null);
        }

        /// <summary>
        /// Returns a string representation of this instance where the number output is specified by the <see cref="IFormatProvider"/>
        /// </summary>
        /// <param name="provider">A <see cref="IFormatProvider"/></param>
        /// <returns>The string representation of this instance.</returns>
        public string ToString(IFormatProvider provider)
        {
            return this.ToString(string.Empty, provider);
        }

        /// <inheritdoc />
        public string ToString(string format, IFormatProvider provider)
        {
            return Angle.Formatter.Format(format, this, provider);
        }

        /// <inheritdoc />
        public int CompareTo(Angle value)
        {
            return this.Radians.CompareTo(value.Radians);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="T:MathNet.Spatial.Units.Angle"/> object within the given tolerance.
        /// </summary>
        /// <returns>
        /// true if <paramref name="other"/> represents the same angle as this instance; otherwise, false.
        /// </returns>
        /// <param name="other">An <see cref="T:MathNet.Spatial.Units.Angle"/> object to compare with this instance.</param>
        /// <param name="tolerance">The maximum difference for being considered equal</param>
        public bool Equals(Angle other, double tolerance)
        {
            return Math.Abs(this.Radians - other.Radians) < tolerance;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="T:MathNet.Spatial.Units.Angle"/> object within the given tolerance.
        /// </summary>
        /// <returns>
        /// true if <paramref name="other"/> represents the same angle as this instance; otherwise, false.
        /// </returns>
        /// <param name="other">An <see cref="T:MathNet.Spatial.Units.Angle"/> object to compare with this instance.</param>
        /// <param name="tolerance">The maximum difference for being considered equal</param>
        public bool Equals(Angle other, Angle tolerance)
        {
            return Math.Abs(this.Radians - other.Radians) < tolerance.Radians;
        }

        /// <summary>
        /// Returns the sin of the angle
        /// </summary>
        /// <returns>The sin of the angle</returns>
        public double Sin()
        {
            return Math.Sin(this.Radians);
        }

        /// <summary>
        /// Returns the cos of the angle
        /// </summary>
        /// <returns>The sin of the angle</returns>
        public double Cos()
        {
            return Math.Cos(this.Radians);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Radians.GetHashCode();
        }
    }
}
