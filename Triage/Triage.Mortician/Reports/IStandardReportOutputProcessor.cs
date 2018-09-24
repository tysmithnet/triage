// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-21-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-21-2018
// ***********************************************************************
// <copyright file="IStandardReportOutputProcessor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Reports
{
    /// <summary>
    ///     Interface IStandardReportOutputProcessor
    /// </summary>
    /// <typeparam name="TReport">The type of the t report.</typeparam>
    public interface IStandardReportOutputProcessor<out TReport> where TReport : IReport
    {
        /// <summary>
        ///     Processes the output.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <returns>TReport.</returns>
        TReport ProcessOutput(string output);
    }
}