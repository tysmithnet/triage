using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    /// <inheritdoc />
    /// <summary>
    ///     DumpObjectExtractor capable of parsing System.String objects
    /// </summary>
    /// <seealso cref="T:Triage.Mortician.IDumpObjectExtractor" />
    [Export(typeof(IDumpObjectExtractor))]
    public class StringExtractor : IDumpObjectExtractor
    {
        /// <inheritdoc />
        /// <summary>
        ///     Determines whether this instance can extract from the provided object
        /// </summary>
        /// <param name="clrObject">The object to try to get values from</param>
        /// <param name="clrRuntime">The clr runtime being used</param>
        /// <returns>
        ///     <c>true</c> if this instance can extract from the object; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExtract(ClrObject clrObject, ClrRuntime clrRuntime)
        {
            return clrObject.Type?.Name == "System.String";
        }

        /// <inheritdoc />
        /// <summary>
        ///     Extracts data from the provided object
        /// </summary>
        /// <param name="clrObject">The object.</param>
        /// <param name="clrRuntime">The runtime.</param>
        /// <returns>
        ///     Extracted dump object
        /// </returns>
        public DumpObject Extract(ClrObject clrObject, ClrRuntime clrRuntime)
        {
            var value = (string) clrObject.Type.GetValue(clrObject.Address);
            var heapObject = new StringDumpObject(clrObject.Address, "System.String", clrObject.Size, value,
                clrRuntime.Heap.GetGeneration(clrObject.Address));

            return heapObject;
        }
    }
}