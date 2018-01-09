namespace MathNet.Spatial.Euclidean2D
{
    using System;

    /// <summary>
    /// A 2D Coordinate system
    /// </summary>
    public class CoordinateSystem
    {
        /// <summary>
        /// Gets or sets the precision in decimal digits of the coordinate system
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// Creates a new 2D point
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <returns>A new 2D point</returns>
        public Point2D CreatePoint(double x, double y)
        {
            return new Point2D(x, y, this);
        }

        /// <summary>
        /// Creates a new 2D line segment.
        /// </summary>
        /// <param name="p1">The start point</param>
        /// <param name="p2">The end point</param>
        /// <returns>A new line segment</returns>
        public LineSegment2D CreateLineSegment(Point2D p1, Point2D p2)
        {
            if (p1.CSReference != this && p2.CSReference != this)
            {
                throw new ArgumentException("Points must be created from the same coordinate system");
            }

            return new LineSegment2D(p1, p2, this);
        }
    }
}
