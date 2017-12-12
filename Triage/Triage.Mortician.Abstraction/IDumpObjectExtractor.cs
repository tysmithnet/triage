using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Abstraction
{
    /// <summary>
    ///     Represents an object cabable of extracting data from a ClrObject
    /// </summary>
    public interface IDumpObjectExtractor
    {
        /// <summary>
        ///     Determines whether this instance can extract from the provided object
        /// </summary>
        /// <param name="clrObject">The object to try to get values from</param>
        /// <param name="clrRuntime">The clr runtime being used</param>
        /// <returns>
        ///     <c>true</c> if this instance can extract from the object; otherwise, <c>false</c>.
        /// </returns>
        bool CanExtract(ClrObject clrObject, ClrRuntime clrRuntime);


        /// <summary>
        ///     Extracts data from the provided object
        /// </summary>
        /// <param name="clrObject">The object.</param>
        /// <param name="clrRuntime">The runtime.</param>
        /// <returns>Extracted dump object</returns>
        IDumpObject Extract(ClrObject clrObject, ClrRuntime clrRuntime);
    }
}