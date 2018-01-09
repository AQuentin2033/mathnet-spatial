﻿namespace MathNet.Spatial.Internals
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using MathNet.Spatial.Euclidean2D;

    /// <summary>
    /// An avl node for convex hull representing a quadrant
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "By design")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1306:FieldNamesMustBeginWithLowerCaseLetter", Justification = "Reviewed.")]
    internal abstract class Quadrant : AvlTreeSet<Point2D>
    {
        /// <summary>
        /// The first point
        /// </summary>
        public Point2D FirstPoint;

        /// <summary>
        /// The last point
        /// </summary>
        public Point2D LastPoint;

        /// <summary>
        /// The root point
        /// </summary>
        public Point2D RootPoint;

        /// <summary>
        /// The current node
        /// </summary>
        protected AvlNode<Point2D> CurrentNode = null;

        /// <summary>
        /// A list of points
        /// </summary>
        protected Point2D[] ListOfPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="Quadrant"/> class.
        /// </summary>
        /// <param name="listOfPoint">a list of points</param>
        /// <param name="comparer">Comparer is only used to add the second point (the last point, which is compared against the first one).</param>
        internal Quadrant(Point2D[] listOfPoint, IComparer<Point2D> comparer)
            : base(comparer)
        {
            this.ListOfPoint = listOfPoint;
        }

        /// <summary>
        /// An enum for the side
        /// </summary>
        internal enum Side
        {
            /// <summary>
            /// Unkown side
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// left side
            /// </summary>
            Left = 1,

            /// <summary>
            /// right side
            /// </summary>
            Right = 2
        }

        /// <summary>
        /// Prepares the node
        /// </summary>
        public void Prepare()
        {
            if (!this.ListOfPoint.Any())
            {
                // There is no points at all. Hey don't try to crash me.
                return;
            }

            // Begin : General Init
            this.Add(this.FirstPoint);
            if (this.FirstPoint.Equals(this.LastPoint))
            {
                return; // Case where for weird distribution like triangle or diagonal. This quadrant will have no point
            }

            this.Add(this.LastPoint);
        }

        /// <summary>
        /// Tell if should try to add and where. -1 ==> Should not add.
        /// </summary>
        /// <param name="point">A point</param>
        internal abstract void ProcessPoint(ref Point2D point);

        /// <summary>
        /// Initialize every values needed to extract values that are parts of the convex hull.
        /// This is where the first pass of all values is done the get maximum in every directions (x and y).
        /// </summary>
        protected abstract void SetQuadrantLimits();

        /// <summary>
        /// To know if to the right. It is meaninful when p1 is first and p2 is next.
        /// </summary>
        /// <param name="p1">The first point</param>
        /// <param name="p2">The second point</param>
        /// <param name="pointtToCheck">The point to check</param>
        /// <returns>Equivalent of tracing a line from p1 to p2 and tell if ptToCheck
        /// is to the right or left of that line taking p1 as reference point.</returns>
        //// [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected bool IsPointToTheRightOfOthers(Point2D p1, Point2D p2, Point2D pointtToCheck)
        {
            return ((p2.X - p1.X) * (pointtToCheck.Y - p1.Y)) - ((p2.Y - p1.Y) * (pointtToCheck.X - p1.X)) < 0;
        }

        /// <summary>
        /// Checks if point should be in quadrant
        /// </summary>
        /// <param name="pt">a point</param>
        /// <returns>True if it is a good quadrant for the point; otherwise false</returns>
        //// [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract bool IsGoodQuadrantForPoint(Point2D pt);

        /// <summary>
        /// Called after insertion in order to see if the newly added point invalidate one
        /// or more neighbors and if so, remove it/them from the tree.
        /// </summary>
        /// <param name="pointPrevious">Previous point</param>
        /// <param name="pointNew">New point</param>
        /// <param name="pointNext">Next point</param>
        //// [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1002:SemicolonsMustBeSpacedCorrectly", Justification = "Reviewed.")]
        protected void InvalidateNeighbors(AvlNode<Point2D> pointPrevious, AvlNode<Point2D> pointNew, AvlNode<Point2D> pointNext)
        {
            bool invalidPoint;

            if (pointPrevious != null)
            {
                AvlNode<Point2D> previousPrevious = pointPrevious.GetPreviousNode();
                for (; ;)
                {
                    if (previousPrevious == null)
                    {
                        break;
                    }

                    invalidPoint = !this.IsPointToTheRightOfOthers(previousPrevious.Item, pointNew.Item, pointPrevious.Item);
                    if (!invalidPoint)
                    {
                        break;
                    }

                    Point2D pointPrevPrev = previousPrevious.Item;
                    this.RemoveNode(pointPrevious);
                    pointPrevious = this.GetNode(pointPrevPrev);
                    previousPrevious = pointPrevious.GetPreviousNode();
                }
            }

            // Invalidate next(s)
            if (pointNext != null)
            {
                AvlNode<Point2D> nextNext = pointNext.GetNextNode();
                for (; ;)
                {
                    if (nextNext == null)
                    {
                        break;
                    }

                    invalidPoint = !this.IsPointToTheRightOfOthers(pointNew.Item, nextNext.Item, pointNext.Item);
                    if (!invalidPoint)
                    {
                        break;
                    }

                    Point2D pointNextNext = nextNext.Item;
                    this.RemoveNode(pointNext);
                    pointNext = this.GetNode(pointNextNext);
                    nextNext = pointNext.GetNextNode();
                }
            }
        }
    }
}
