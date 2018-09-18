// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="ClrFlavor.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// Returns the "flavor" of CLR this module represents.
    /// </summary>
    public enum ClrFlavor
    {
        /// <summary>
        /// This is the full version of CLR included with windows.
        /// </summary>
        Desktop = 0,

        /// <summary>
        /// This originally was for Silverlight and other uses of "coreclr", but now
        /// there are several flavors of coreclr, some of which are no longer supported.
        /// </summary>
        [Obsolete]
        CoreCLR = 1,

        /// <summary>
        /// Used for .Net Native.
        /// </summary>
        [Obsolete(".Net Native support is being split out of this library into a different one.")]
        Native = 2,

        /// <summary>
        /// For .Net Core
        /// </summary>
        Core = 3
    }
}
