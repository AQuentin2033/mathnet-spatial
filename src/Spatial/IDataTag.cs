namespace MathNet.Spatial
{
    /// <summary>
    /// Interface provides a common means to attach a data reference to a geometric items
    /// </summary>
    public interface IDataTag
    {
        /// <summary>
        /// Gets or sets a user supplied data reference.
        /// </summary>
        object Tag { get; set; }
    }
}
