// ***********************************************************************
// Assembly         : Triage.Mortician.Analyzers
// Author           : @tysmithnet
// Created          : 12-17-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 12-19-2017
// ***********************************************************************
// <copyright file="StackFrameBreakdownMessage.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Triage.Mortician.Core;

namespace Triage.Mortician.Analyzers
{
    /// <summary>
    ///     A message that communicates the distribution of stack frames found in the memor dump
    /// </summary>
    /// <seealso cref="Message" />
    /// <inheritdoc />
    /// <seealso cref="T:Triage.Mortician.Message" />
    public class StackFrameBreakdownMessage : Message
    {
        /// <summary>
        ///     Gets or sets the records.
        /// </summary>
        /// <value>The records.</value>
        public IReadOnlyList<StackFrameRollupRecord> Records { get; protected internal set; }
    }
}