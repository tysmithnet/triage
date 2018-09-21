// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-21-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-21-2018
// ***********************************************************************
// <copyright file="EeStackThread.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Triage.Mortician.Reports
{
    /// <summary>
    ///     Class EeStackThread. This class cannot be inherited.
    /// </summary>
    public sealed class EeStackThread
    {
        /// <summary>
        ///     Gets the stack frames.
        /// </summary>
        /// <value>The stack frames.</value>
        public IEnumerable<EeStackFrame> StackFrames => StackFramesInternal;

        /// <summary>
        ///     Gets or sets the stack frames internal.
        /// </summary>
        /// <value>The stack frames internal.</value>
        internal IList<EeStackFrame> StackFramesInternal { get; set; } = new List<EeStackFrame>();
    }
}