// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-21-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-21-2018
// ***********************************************************************
// <copyright file="EeStackFrame.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Reports
{
    /// <summary>
    ///     Class EeStackFrame. This class cannot be inherited.
    /// </summary>
    public sealed class EeStackFrame
    {
        /// <summary>
        ///     Gets the callee.
        /// </summary>
        /// <value>The callee.</value>
        public string Callee { get; internal set; }

        /// <summary>
        ///     Gets the caller.
        /// </summary>
        /// <value>The caller.</value>
        public string Caller { get; internal set; }

        /// <summary>
        ///     Gets the child stack pointer.
        /// </summary>
        /// <value>The child stack pointer.</value>
        public ulong ChildStackPointer { get; internal set; }

        /// <summary>
        ///     Gets the return address.
        /// </summary>
        /// <value>The return address.</value>
        public ulong ReturnAddress { get; internal set; }
    }
}