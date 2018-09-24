// ***********************************************************************
// Assembly         : Triage.Mortician.Analyzers
// Author           : @tysmithnet
// Created          : 12-17-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 12-19-2017
// ***********************************************************************
// <copyright file="UniqueStacksMessage.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Triage.Mortician.Core;

namespace Triage.Mortician.Analyzers
{
    /// <summary>
    ///     A message that will contain information on the unique managed stack traces in the dump
    /// </summary>
    /// <seealso cref="Message" />
    /// <inheritdoc />
    /// <seealso cref="T:Triage.Mortician.Message" />
    public class UniqueStacksMessage : Message
    {
        /// <summary>
        ///     Gets or sets the unique stack frame rollup records.
        /// </summary>
        /// <value>The unique stack frame rollup records.</value>
        public IReadOnlyList<UniqueStackFrameRollupRecord> UniqueStackFrameRollupRecords
        {
            get;
            protected internal set;
        }
    }
}