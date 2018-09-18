namespace Microsoft.Diagnostics.Runtime
{
    public interface IClrInterface
    {
        /// <summary>
        /// The typename of the interface.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The interface that this interface inherits from.
        /// </summary>
        ClrInterface BaseInterface { get; }

        /// <summary>
        /// Display string for this interface.
        /// </summary>
        /// <returns>Display string for this interface.</returns>
        string ToString();

        /// <summary>
        /// Equals override.
        /// </summary>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>True if this interface equals another.</returns>
        bool Equals(object obj);

        /// <summary>
        /// GetHashCode override.
        /// </summary>
        /// <returns>A hashcode for this object.</returns>
        int GetHashCode();
    }
}