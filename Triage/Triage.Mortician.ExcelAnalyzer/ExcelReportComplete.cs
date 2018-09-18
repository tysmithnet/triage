// ***********************************************************************
// Assembly         : Triage.Mortician.ExcelAnalyzer
// Author           : @tysmithnet
// Created          : 01-14-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 01-15-2018
// ***********************************************************************
// <copyright file="ExcelReportComplete.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using Triage.Mortician.Core;

namespace Triage.Mortician.ExcelAnalyzer
{
    /// <summary>
    ///     A message that indicates that an excel report has been saved to disk
    /// </summary>
    /// <seealso cref="Message" />
    /// <inheritdoc />
    /// <seealso cref="T:Triage.Mortician.Message" />
    public class ExcelReportComplete : Message
    {
        /// <summary>
        ///     Gets or sets the report file.
        /// </summary>
        /// <value>The report file.</value>
        public string ReportFile { get; protected internal set; }
    }
}