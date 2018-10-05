// ***********************************************************************
// Assembly         : Mortician.Analyzers
// Author           : @tysmithnet
// Created          : 12-17-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 12-19-2017
// ***********************************************************************
// <copyright file="UniqueStackFrameRollupRecord.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Mortician.Core;

namespace Mortician.Analyzers
{
    /// <summary>
    ///     Represents a summary of a particular managed call stack and the threads that share this same call stacks
    /// </summary>
    public class UniqueStackFrameRollupRecord
    {
        /// <summary>
        ///     Gets or sets the stack trace.
        /// </summary>
        /// <value>The stack trace.</value>
        public string StackTrace { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the threads.
        /// </summary>
        /// <value>The threads.</value>
        public IReadOnlyList<DumpThread> Threads { get; protected internal set; }
    }
}