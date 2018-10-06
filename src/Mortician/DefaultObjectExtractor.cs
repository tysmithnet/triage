// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DefaultObjectExtractor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Mortician.Core;
using Mortician.Core.ClrMdAbstractions;

namespace Mortician
{
    /// <summary>
    ///     Default object extractor
    ///     Used when all else fails
    /// </summary>
    /// <seealso cref="Mortician.Core.IDumpObjectExtractor" />
    /// <seealso cref="IDumpObjectExtractor" />
    public class DefaultObjectExtractor : IDumpObjectExtractor
    {
        /// <summary>
        ///     Determines whether this instance can extract from the provided object
        /// </summary>
        /// <param name="clrObject">The object to try to get values from</param>
        /// <param name="clrRuntime">The clr runtime being used</param>
        /// <returns><c>true</c> if this instance can extract from the object; otherwise, <c>false</c>.</returns>
        public bool CanExtract(IClrObject clrObject, IClrRuntime clrRuntime) => true;

        /// <summary>
        ///     Determines whether this instance can extract the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="clrRuntime">The color runtime.</param>
        /// <returns><c>true</c> if this instance can extract the specified address; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public bool CanExtract(ulong address, IClrRuntime clrRuntime) => true;

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

        /// <summary>
        ///     Extracts the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="clrRuntime">The color runtime.</param>
        /// <returns>DumpObject.</returns>
        /// <inheritdoc />
        public DumpObject Extract(ulong address, IClrRuntime clrRuntime)
        {
            var type = clrRuntime.Heap.GetObjectType(address);
            return new DumpObject(address, type.Name, type.GetSize(address), clrRuntime.Heap.GetGeneration(address));
        }
    }
}