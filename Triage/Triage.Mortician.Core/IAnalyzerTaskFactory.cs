// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IAnalyzerTaskFactory.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Interface IAnalyzerTaskFactory
    /// </summary>
    public interface IAnalyzerTaskFactory
    {
        /// <summary>
        ///     Starts the analyzers.
        /// </summary>
        /// <param name="analyzers">The analyzers.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        Task StartAnalyzers(IEnumerable<IAnalyzer> analyzers, CancellationToken cancellationToken);
    }
}