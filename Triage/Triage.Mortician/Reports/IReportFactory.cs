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
        string DisplayName { get; }

        /// <summary>
        ///     Generate the report artifact
        /// </summary>
        /// <returns>IReport.</returns>
        IReport Process();

        /// <summary>
        ///     Prepare to generate report artifacts. This typically means using the debugger
        ///     interface to run some commands and store those results. This method will be called
        ///     on the main thread serially, and so it is not necessary to lock the debugger.
        /// </summary>
        /// <param name="debugger">The debugger.</param>
        /// <returns>IReport.</returns>
        void Setup(DebuggerProxy debugger);
    }
}