// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-26-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-26-2018
// ***********************************************************************
// <copyright file="IReportFactory.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Triage.Mortician.Domain;

namespace Triage.Mortician.Reports
{
    /// <summary>
    ///     Interface IReportFactory
    /// </summary>
    public interface IReportFactory
    {
        /// <summary>
        ///     Creates the report.
        /// </summary>
        /// <param name="debugger">The debugger.</param>
        /// <returns>IReport.</returns>
        IReport CreateReport(DebuggerProxy debugger);
    }
}