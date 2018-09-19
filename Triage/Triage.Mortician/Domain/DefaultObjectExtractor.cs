// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="DefaultObjectExtractor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Triage.Mortician.Core;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Domain
{
    /// <summary>
    ///     Default object extractor
    ///     Used when all else fails
    /// </summary>
    /// <seealso cref="IDumpObjectExtractor" />
    public class DefaultObjectExtractor : IDumpObjectExtractor
    {
        /// <summary>
        ///     Determines whether this instance can extract from the provided object
        /// </summary>
        /// <param name="clrObject">The object to try to get values from</param>
        /// <param name="clrRuntime">The clr runtime being used</param>
        /// <returns><c>true</c> if this instance can extract from the object; otherwise, <c>false</c>.</returns>
        public bool CanExtract(IClrObject clrObject, IClrRuntime clrRuntime)
        {
            return true;
        }

        /// <summary>
        ///     Extracts data from the provided object
        /// </summary>
        /// <param name="clrObject">The object.</param>
        /// <param name="clrRuntime">The runtime.</param>
        /// <returns>Extracted dump object</returns>
        public DumpObject Extract(IClrObject clrObject, IClrRuntime clrRuntime)
        {
            var address = clrObject.Address;
            var gen = clrRuntime.Heap.GetGeneration(address);
            var size = clrObject.Size;
            var name = clrObject.Type.Name;
            var dumpObject = new DumpObject(address, name, size, gen);
            return dumpObject;
        }
    }
}