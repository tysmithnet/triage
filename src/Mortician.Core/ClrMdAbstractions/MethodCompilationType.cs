// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="MethodCompilationType.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Enum MethodCompilationType
    /// </summary>
    public enum MethodCompilationType
    {
        /// <summary>
        ///     Method is not yet JITed and no NGEN image exists.
        /// </summary>
        None,

        /// <summary>
        ///     Method was JITed.
        /// </summary>
        Jit,

        /// <summary>
        ///     Method was NGEN'ed (pre-JITed).
        /// </summary>
        Ngen
    }
}