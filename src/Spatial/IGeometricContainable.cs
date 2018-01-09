namespace MathNet.Spatial
{
    using System;

    /// <summary>
    /// Objects declaring IGeometricContainable are containable within other geometric objects
    /// </summary>
    /// <typeparam name="T">A containable type</typeparam>
    public interface IGeometricContainable<T>
    {
        /// <summary>
        /// Gets the geometric parent of the object
        /// </summary>
        IGeometricContainer Parent { get; }

        /// <summary>
        /// Gets a value indicating whether the object can be made unmodifiable.
        /// </summary>
        bool CanFreeze { get; }

        /// <summary>
        /// Gets a value indicating whether the object is currently modifiable.
        /// </summary>
        bool IsFrozen { get; }

        /// <summary>
        /// Creates a modifiable clone of the object making deep copies of the objects values
        /// </summary>
        /// <returns>A modifiable clone of the current object. The cloned object's IsFrozen property is false even if the source's IsFrozen property is true.</returns>
        /// <remarks>
        /// Move a Freezable Between Threads
        /// <para>This method can be useful for moving an object between threads. First, make the object unmodifiable by calling its Freeze method.Now another thread can access the object and make a local Clone that it can access.
        /// </para>
        /// </remarks>
        T Clone();

        /// <summary>
        /// Makes the current object unmodifiable and sets its IsFrozen property to true.
        /// </summary>
        /// <exception cref="InvalidOperationException">The object cannot be made unmodifiable. </exception>
        /// <remarks>
        /// To avoid the possibility of an InvalidOperationException when calling this method, check the CanFreeze property to determine whether the object can be made unmodifiable before calling this method.
        /// </remarks>
        void Freeze();
    }
}
