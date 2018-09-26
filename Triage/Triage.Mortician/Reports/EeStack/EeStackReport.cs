// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-21-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-26-2018
// ***********************************************************************
// <copyright file="EeStackReport.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Triage.Mortician.Reports.EeStack
{
    /// <summary>
    ///     Class EeStackReport. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Reports.IReport" />
    /// <seealso cref="IReport" />
    [Export(typeof(IReport))]
    [Export]
    public sealed class EeStackReport : IReport
    {
        /// <summary>
        ///     Gets the raw output.
        /// </summary>
        /// <value>The raw output.</value>
        /// <inheritdoc />
        public string RawOutput { get; internal set; }

        /// <summary>
        ///     Gets the threads.
        /// </summary>
        /// <value>The threads.</value>
        public IEnumerable<EeStackThread> Threads => ThreadsInternal;

        /// <summary>
        ///     Gets or sets the threads internal.
        /// </summary>
        /// <value>The threads internal.</value>
        internal IList<EeStackThread> ThreadsInternal { get; set; } = new List<EeStackThread>();
    }
}