﻿// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="AnalyzerTaskFactory.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Triage.Mortician.Core;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class AnalyzerTaskFactory.
    /// </summary>
    /// <seealso cref="IAnalyzerTaskFactory" />
    [Export(typeof(IAnalyzerTaskFactory))]
    public class AnalyzerTaskFactory : IAnalyzerTaskFactory
    {
        /// <summary>
        ///     Starts the analyzers.
        /// </summary>
        /// <param name="analyzers">The analyzers.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        public Task StartAnalyzers(IEnumerable<IAnalyzer> analyzers, CancellationToken cancellationToken)
        {
            var tasks = analyzers.Select(analyzer => Task.Run(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                var isSetup = false;
                try
                {
                    await analyzer.Setup(cancellationToken);
                    isSetup = true;
                }
                catch (Exception e)
                {
                    Log.Error(
                        $"Anayler Setup Exception: {analyzer.GetType().FullName} thew {e.GetType().FullName} - {e.Message}",
                        e);
                }

                if (!isSetup)
                    return;

                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    await analyzer.Process(cancellationToken);
                }
                catch (Exception e)
                {
                    Log.Error(
                        $"Analyzer Process Exception: {analyzer.GetType().FullName} threw {e.GetType().FullName} - {e.Message}",
                        e);
                }
            }, cancellationToken));
            return Task.WhenAll(tasks);
        }

        /// <summary>
        ///     Gets the log.
        /// </summary>
        /// <value>The log.</value>
        private ILog Log { get; } = LogManager.GetLogger<AnalyzerTaskFactory>();
    }
}