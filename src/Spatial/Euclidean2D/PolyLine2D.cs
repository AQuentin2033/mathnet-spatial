﻿namespace MathNet.Spatial.Euclidean2D
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The PolyLine2D class represents a 2D curve in space made up of line segments joined end-to-end, and is
    /// stored as a sequential list of 2D points.
    /// </summary>
    public class PolyLine2D : IEnumerable<Point2D>
    {
        /// <summary>
        /// Internal storage for the points
        /// </summary>
        private readonly List<Point2D> points;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyLine2D"/> class.
        /// Creates a PolyLine2D from a pre-existing IEnumerable of Point2Ds
        /// </summary>
        /// <param name="points">A list of points.</param>
        public PolyLine2D(IEnumerable<Point2D> points)
        {
            this.points = new List<Point2D>(points);
        }

        /// <summary>
        /// Gets the number of points in the polyline
        /// </summary>
        public int Count => this.points.Count;

        /// <summary>
        /// Gets the length of the polyline as the sum of the length of the individual segments
        /// </summary>
        public double Length => this.GetPolyLineLength();

        /// <summary>
        /// Returns a point in the polyline by index number
        /// </summary>
        /// <param name="key">The index of a point</param>
        /// <returns>The indexed point</returns>
        public Point2D this[int key] => this.points[key];

        /// <summary>
        /// Reduce the complexity of a manifold of points represented as an IEnumerable of Point2D objects by
        /// iteratively removing all nonadjacent points which would each result in an error of less than the
        /// single step tolerance if removed.  Iterate until no further changes are made.
        /// </summary>
        /// <param name="points">A list of points.</param>
        /// <param name="singleStepTolerance">The tolerance (epsilon) for comparing sameness of line segements</param>
        /// <returns>A new PolyLine2D with same segements merged.</returns>
        public static PolyLine2D ReduceComplexity(IEnumerable<Point2D> points, double singleStepTolerance)
        {
            var manifold = points.ToList();
            var n = manifold.Count;

            manifold = ReduceComplexitySingleStep(manifold, singleStepTolerance).ToList();
            var n1 = manifold.Count;

            while (n1 != n)
            {
                n = n1;
                manifold = ReduceComplexitySingleStep(manifold, singleStepTolerance).ToList();
                n1 = manifold.Count;
            }

            return new PolyLine2D(manifold);
        }

        /// <summary>
        /// Get the point at a fractional distance along the curve.  For instance, fraction=0.5 will return
        /// the point halfway along the length of the polyline.
        /// </summary>
        /// <param name="fraction">The fractional length at which to compute the point</param>
        /// <returns>A point a fraction of the way along the line.</returns>
        public Point2D GetPointAtFractionAlongCurve(double fraction)
        {
            if (fraction > 1 || fraction < 0)
            {
                throw new ArgumentException("fraction must be between 0 and 1");
            }

            return this.GetPointAtLengthFromStart(fraction * this.Length);
        }

        /// <summary>
        /// Get the point at a specified distance along the curve.  A negative argument will return the first point,
        /// an argument greater than the length of the curve will return the last point.
        /// </summary>
        /// <param name="lengthFromStart">The distance from the first point along the curve at which to return a point</param>
        /// <returns>A point which is the specified distance along the line</returns>
        public Point2D GetPointAtLengthFromStart(double lengthFromStart)
        {
            var length = this.Length;
            if (lengthFromStart >= length)
            {
                return this.Last();
            }

            if (lengthFromStart <= 0)
            {
                return this.First();
            }

            double cumulativeLength = 0;
            var i = 0;
            while (true)
            {
                var nextLength = cumulativeLength + this[i].DistanceTo(this[i + 1]);
                if (cumulativeLength <= lengthFromStart && nextLength > lengthFromStart)
                {
                    var leftover = lengthFromStart - cumulativeLength;
                    var direction = this[i].VectorTo(this[i + 1]);
                    direction.Normalize();
                    return this[i] + (direction * leftover);
                }
                else
                {
                    cumulativeLength = nextLength;
                    i++;
                }
            }
        }

        /// <summary>
        /// Returns the closest point on the polyline to the given point.
        /// </summary>
        /// <param name="p">a point</param>
        /// <returns>A point which is the closest to the given point but still on the line.</returns>
        public Point2D ClosestPointTo(Point2D p)
        {
            var minError = double.MaxValue;
            var closest = default(Point2D);

            for (var i = 0; i < this.Count - 1; i++)
            {
                var segment = new LineSegment2D(this[i], this[i + 1]);
                var projected = segment.ClosestPointTo(p);
                var error = p.DistanceTo(projected);
                if (error < minError)
                {
                    minError = error;
                    closest = projected;
                }
            }

            return closest;
        }

        /// <inheritdoc />
        public IEnumerator<Point2D> GetEnumerator()
        {
            return this.points.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Reduce the complexity of a manifold of points represented as an IEnumerable of Point2D objects.
        /// This algorithm goes through each point in the manifold and computes the error that would be introduced
        /// from the original if that point were removed.  Then it removes nonadjacent points to produce a
        /// reduced size manifold.
        /// </summary>
        /// <param name="points">A list of points</param>
        /// <param name="tolerance">Tolerance (Epislon) to apply to determine if segments are to be merged.</param>
        /// <returns>A new list of points minus any segment which was merged.</returns>
        private static IEnumerable<Point2D> ReduceComplexitySingleStep(IEnumerable<Point2D> points, double tolerance)
        {
            var manifold = points.ToList();
            var errorByIndex = new double[manifold.Count];

            // At this point we will loop through the list of points (excluding the first and the last) and
            // examine every adjacent triplet.  The middle point is tested against the segment created by
            // the two end points, and the error that would result in its deletion is computed as the length
            // of the point's projection onto the segment.
            for (var i = 1; i < manifold.Count - 1; i++)
            {
                // TODO: simplify this to remove all of the value copying
                var v0 = manifold[i - 1];
                var v1 = manifold[i];
                var v2 = manifold[i + 1];
                var projected = new LineSegment2D(v0, v2).ClosestPointTo(v1);

                var error = v1.VectorTo(projected).Length;
                errorByIndex[i] = error;
            }

            // Now go through the list of errors and remove nonadjacent points with less than the error tolerance
            var thinnedPoints = new List<Point2D>();
            var preserveMe = 0;
            for (var i = 0; i < errorByIndex.Length - 1; i++)
            {
                if (i == preserveMe)
                {
                    thinnedPoints.Add(manifold[i]);
                }
                else
                {
                    if (errorByIndex[i] < tolerance)
                    {
                        preserveMe = i + 1;
                    }
                    else
                    {
                        thinnedPoints.Add(manifold[i]);
                    }
                }
            }

            thinnedPoints.Add(manifold.Last());

            return thinnedPoints;
        }

        /// <summary>
        /// Computes the length of the polyline by summing the lengths of the individual segments
        /// </summary>
        /// <returns>The length of the line</returns>
        private double GetPolyLineLength()
        {
            double length = 0;
            for (var i = 0; i < this.points.Count - 1; ++i)
            {
                length += this[i].DistanceTo(this[i + 1]);
            }

            return length;
        }
    }
}
