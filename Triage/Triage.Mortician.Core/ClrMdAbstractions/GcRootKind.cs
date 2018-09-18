// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="GcRootKind.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     The type of GCRoot that a ClrRoot represnts.
    /// </summary>
    public enum GcRootKind
    {
        /// <summary>
        ///     The root is a static variable.
        /// </summary>
        StaticVar,

        /// <summary>
        ///     The root is a thread static.
        /// </summary>
        ThreadStaticVar,

        /// <summary>
        ///     The root is a local variable (or compiler generated temporary variable).
        /// </summary>
        LocalVar,

        /// <summary>
        ///     The root is a strong handle.
        /// </summary>
        Strong,

        /// <summary>
        ///     The root is a weak handle.
        /// </summary>
        Weak,

        /// <summary>
        ///     The root is a strong pinning handle.
        /// </summary>
        Pinning,

        /// <summary>
        ///     The root comes from the finalizer queue.
        /// </summary>
        Finalizer,

        /// <summary>
        ///     The root is an async IO (strong) pinning handle.
        /// </summary>
        AsyncPinning,

        /// <summary>
        ///     The max value of this enum.
        /// </summary>
        Max = AsyncPinning
    }
}