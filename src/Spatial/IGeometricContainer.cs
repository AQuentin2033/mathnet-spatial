namespace MathNet.Spatial
{
    using MathNet.Spatial.Euclidean2D;

    /// <summary>
    /// Objects which implement IGeometricContainer contain other geometric objects
    /// </summary>
    public interface IGeometricContainer
    {
        /// <summary>
        /// Gets the coordinate system of the container
        /// </summary>
        CoordinateSystem CoordinateSystem { get; }
    }
}
