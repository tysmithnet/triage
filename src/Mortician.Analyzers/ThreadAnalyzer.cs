// ***********************************************************************
// Assembly         : Mortician.Analyzers
// Author           : @tysmithnet
// Created          : 12-17-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="ThreadAnalyzer.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mortician.Core;

namespace Mortician.Analyzers
{
    /// <summary>
    ///     Analyzer that will provide information on the threads in the memory dump
    /// </summary>
    /// <seealso cref="IAnalyzer" />
    /// <inheritdoc />
    /// <seealso cref="T:Mortician.IAnalyzer" />
    [Export(typeof(IAnalyzer))]
    public class ThreadAnalyzer : IAnalyzer
    {
        /// <summary>
        ///     The stack frame results
        /// </summary>
        internal List<StackFrameRollupRecord> StackFrameResultsInternal = new List<StackFrameRollupRecord>();

        /// <summary>
        ///     The unique stack frame results
        /// </summary>
        internal List<UniqueStackFrameRollupRecord> UniqueStackFrameResultsInternal =
            new List<UniqueStackFrameRollupRecord>();

        /// <summary>
        ///     Performs the analysis
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task that when complete will signal the completion of the setup procedure</returns>
        /// <inheritdoc />
        public Task Process(CancellationToken cancellationToken)
        {
            if (StackFrameResultsInternal.Any())
                EventHub.Broadcast(new StackFrameBreakdownMessage
                {
                    Records = StackFrameResultsInternal
                });

            if (UniqueStackFrameResultsInternal.Any())
                EventHub.Broadcast(new UniqueStacksMessage
                {
                    UniqueStackFrameRollupRecords = UniqueStackFrameResultsInternal
                });

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Performs any necessary setup prior to processing
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task that when complete will signal the completion of the setup procedure</returns>
        /// <inheritdoc />
        public Task Setup(CancellationToken cancellationToken)
        {
            var uniqueStacksTask = Task.Run(() =>
            {
                var result = DumpThreadRepository.Threads
                    .Where(t => t.ManagedStackFrames != null)
                    .GroupBy(t => string.Join("\n", t.ManagedStackFrames.Select(f => f.DisplayString)))
                    .Where(g => !string.IsNullOrWhiteSpace(g.Key))
                    .OrderByDescending(g => g.Count())
                    .ThenByDescending(g => g.Key.Length)
                    .Select(g => new UniqueStackFrameRollupRecord
                    {
                        StackTrace = g.Key,
                        Threads = g.ToList()
                    });
                UniqueStackFrameResultsInternal.AddRange(result);
            }, cancellationToken);

            var stackFrameRollupTask = Task.Run(() =>
            {
                var results = DumpThreadRepository.Threads
                    .Where(t => t.ManagedStackFrames != null)
                    .SelectMany(t => t.ManagedStackFrames)
                    .GroupBy(f => f.DisplayString)
                    .Select(g => new StackFrameRollupRecord
                    {
                        DisplayString = g.Key,
                        ModuleName = g.First().ModuleName,
                        Count = g.Count()
                    })
                    .OrderByDescending(x => x.Count);
                StackFrameResultsInternal.AddRange(results);
            }, cancellationToken);

            return Task.WhenAll(uniqueStacksTask, stackFrameRollupTask);
        }

        /// <summary>
        ///     Gets or sets the dump thread repository.
        /// </summary>
        /// <value>The dump thread repository.</value>
        [Import]
        internal IDumpThreadRepository DumpThreadRepository { get; set; }

        /// <summary>
        ///     Gets or sets the event hub.
        /// </summary>
        /// <value>The event hub.</value>
        [Import]
        internal IEventHub EventHub { get; set; }
    }
}