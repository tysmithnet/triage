// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-24-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-24-2018
// ***********************************************************************
// <copyright file="EeStackFrame.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Triage.Mortician.Core;

namespace Triage.Mortician.Reports.EeStack
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
        public CodeLocation Callee { get; internal set; }

        /// <summary>
        ///     Gets the caller.
        /// </summary>
        /// <value>The caller.</value>
        public CodeLocation Caller { get; internal set; }

        /// <summary>
        ///     Gets the child stack pointer.
        /// </summary>
        /// <value>The child stack pointer.</value>
        public ulong ChildStackPointer { get; internal set; }

        /// <summary>
        ///     Gets the method descriptor.
        /// </summary>
        /// <value>The method descriptor.</value>
        public ulong MethodDescriptor { get; internal set; }

        /// <summary>
        ///     Gets the return address.
        /// </summary>
        /// <value>The return address.</value>
        public ulong ReturnAddress { get; internal set; }
    }
}