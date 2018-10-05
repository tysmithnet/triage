// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-13-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="IDumpObjectExtractor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
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
        /// <returns><c>true</c> if this instance can extract from the object; otherwise, <c>false</c>.</returns>
        bool CanExtract(IClrObject clrObject, IClrRuntime clrRuntime);

        /// <summary>
        ///     Extracts data from the provided object
        /// </summary>
        /// <param name="clrObject">The object.</param>
        /// <param name="clrRuntime">The runtime.</param>
        /// <returns>Extracted dump object</returns>
        DumpObject Extract(IClrObject clrObject, IClrRuntime clrRuntime);
    }
}