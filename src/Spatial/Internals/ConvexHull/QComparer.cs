namespace MathNet.Spatial.Internals
{
    using System;
    using System.Collections.Generic;
    using MathNet.Spatial.Euclidean2D;

    /// <summary>
    /// A comparer for convex hull's use of an Avl tree
    /// </summary>
    internal class QComparer : IComparer<Point2D>
    {
        /// <summary>
        /// A function to compare with
        /// </summary>
        private readonly Func<Point2D, Point2D, int> comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="QComparer"/> class.
        /// </summary>
        /// <param name="comparer">a function to use for comparing</param>
        public QComparer(Func<Point2D, Point2D, int> comparer)
        {
            this.comparer = comparer;
        }

        /// <summary>
        /// Compares two points using the provided function
        /// </summary>
        /// <param name="pt1">the first point</param>
        /// <param name="pt2">the second point</param>
        /// <returns>A value of -1 if less than, a value of 1 is greater than; otherwise a value of 0</returns>
        public int Compare(Point2D pt1, Point2D pt2)
        {
            return this.comparer(pt1, pt2);
        }
    }
}
