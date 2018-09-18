// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="ClrStackFrameType.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     The type of frame the ClrStackFrame represents.
    /// </summary>
    public enum ClrStackFrameType
    {
        /// <summary>
        ///     Indicates this stack frame is unknown
        /// </summary>
        Unknown = -1,

        /// <summary>
        ///     Indicates this stack frame is a standard managed method.
        /// </summary>
        ManagedMethod = 0,

        /// <summary>
        ///     Indicates this stack frame is a special stack marker that the Clr runtime leaves on the stack.
        ///     Note that the ClrStackFrame may still have a ClrMethod associated with the marker.
        /// </summary>
        Runtime = 1
    }
}