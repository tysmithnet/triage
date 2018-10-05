// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-24-2018
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
using Serilog;
using Mortician.Core;
using Slog = Serilog.Log;

namespace Mortician
{
    /// <summary>
    ///     Class AnalyzerTaskFactory.
    /// </summary>
    /// <seealso cref="Mortician.Core.IAnalyzerTaskFactory" />
    /// <seealso cref="IAnalyzerTaskFactory" />
    [Export(typeof(IAnalyzerTaskFactory))]
    public class AnalyzerTaskFactory : IAnalyzerTaskFactory
    {
#if DEBUG
        internal Action StartTestingAction { get; set; }
#endif
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
#if DEBUG
                StartTestingAction?.Invoke();
#endif
                if (cancellationToken.IsCancellationRequested)
                    return;
                var isSetup = false;
                try
                {
                    await analyzer.Setup(cancellationToken);
                    isSetup = true;
                }
                catch (Exception e)
                {
                    Log.Error(e, "Anayler Setup Exception: {FullName} thew {FullName1} - {Message}",
                        analyzer.GetType().FullName, e.GetType().FullName, e.Message);
                }

                if (!isSetup)
                    return;

                if (cancellationToken.IsCancellationRequested)
                    return;
                try
                {
                    await analyzer.Process(cancellationToken);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Analyzer Process Exception: {FullName} threw {FullName1} - {Message}",
                        analyzer.GetType().FullName, e.GetType().FullName, e.Message);
                }
            }, cancellationToken));
            return Task.WhenAll(tasks);
        }

        internal ILogger Log { get; } = Slog.ForContext<AnalyzerTaskFactory>();
    }
}