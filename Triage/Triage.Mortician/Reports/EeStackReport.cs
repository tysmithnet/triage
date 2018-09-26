// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-21-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-24-2018
// ***********************************************************************
// <copyright file="EeStackReport.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Triage.Mortician.Reports
{
    /// <summary>
    ///     Class EeStackReport. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Reports.IReport" />
    /// <seealso cref="IReport" />
    public sealed class EeStackReport : IReport
    {
        /// <summary>
        ///     Gets the current frame.
        /// </summary>
        /// <value>The current frame.</value>
        public EeStackFrame CurrentFrame { get; internal set; }

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