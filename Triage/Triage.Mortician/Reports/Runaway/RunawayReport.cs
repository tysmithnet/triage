// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-26-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-26-2018
// ***********************************************************************
// <copyright file="RunawayReport.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Triage.Mortician.Reports.Runaway
{
    /// <summary>
    ///     Class RunawayReport.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Reports.IReport" />
    [Export(typeof(IReport))]
    [Export]
    public class RunawayReport : IReport
    {
        /// <summary>
        ///     Gets or sets the runaway lines.
        /// </summary>
        /// <value>The runaway lines.</value>
        public IList<RunawayLine> RunawayLines { get; internal set; } = new List<RunawayLine>();

        /// <inheritdoc />
        public string RawOutput { get; internal set; }
    }
}