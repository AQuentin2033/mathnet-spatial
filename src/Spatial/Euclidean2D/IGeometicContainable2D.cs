namespace MathNet.Spatial.Euclidean2D
{
    /// <summary>
    /// An interface representing a 2D geometrically containable object
    /// </summary>
    /// <typeparam name="T">A containable type</typeparam>
    public interface IGeometicContainable2D<T> : IGeometricContainable<T>
    {
        /// <summary>
        /// Gets the coordinates of the origin of the object
        /// </summary>
        Point2D Origin { get; }
    }
}
