// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="Architecture.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     The architecture of a process.
    /// </summary>
    public enum Architecture
    {
        /// <summary>
        ///     Unknown.  Should never be exposed except in case of error.
        /// </summary>
        Unknown,

        /// <summary>
        ///     x86.
        /// </summary>
        X86,

        /// <summary>
        ///     x64
        /// </summary>
        Amd64,

        /// <summary>
        ///     ARM
        /// </summary>
        Arm
    }
}