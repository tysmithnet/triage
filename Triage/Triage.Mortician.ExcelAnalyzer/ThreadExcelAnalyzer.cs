// ***********************************************************************
// Assembly         : Triage.Mortician.ExcelAnalyzer
// Author           : @tysmithnet
// Created          : 01-15-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="ThreadExcelAnalyzer.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using SpreadsheetLight;
using Triage.Mortician.Analyzers;
using Triage.Mortician.Core;
using Slog = Serilog.Log;

namespace Triage.Mortician.ExcelAnalyzer
{
    /// <summary>
    ///     Represents an object that is capable of reporting on the threads from the memory dump
    /// </summary>
    /// <seealso cref="Triage.Mortician.ExcelAnalyzer.IExcelAnalyzer" />
    /// <inheritdoc />
    /// <seealso cref="T:Triage.Mortician.Analyzers.IExcelAnalyzer" />
    [Export(typeof(IExcelAnalyzer))]
    public class ThreadExcelAnalyzer : IExcelAnalyzer
    {
        /// <summary>
        ///     Contributes the specified shared document.
        /// </summary>
        /// <param name="sharedDocument">The shared document.</param>
        /// <inheritdoc />
        public void Contribute(SLDocument sharedDocument)
        {
            if (UniqueStacksMessage != null)
                PopulateUniqueStacks(sharedDocument);
            else
                Log.Information("No unique stack message received");

            if (StackFrameBreakdownMessage != null)
                PopulateManagedStackFrames(sharedDocument);
            else
                Log.Information("No stack frame breakdown message received");
        }

        /// <summary>
        ///     Performs any required setup like number crunching etc.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task, that when complete will signal the setup completion</returns>
        /// <inheritdoc />
        public async Task Setup(CancellationToken cancellationToken)
        {
            var stackFrameBreakdownTask = EventHub.Get<StackFrameBreakdownMessage>()
                .FirstOrDefaultAsync().ToTask(cancellationToken);
            var uniqueStackTask = EventHub.Get<UniqueStacksMessage>().FirstOrDefaultAsync().ToTask(cancellationToken);
            await Task.WhenAll(stackFrameBreakdownTask, uniqueStackTask);

            StackFrameBreakdownMessage = stackFrameBreakdownTask.Result;
            UniqueStacksMessage = uniqueStackTask.Result;
        }

        /// <summary>
        ///     Populates the managed stack frames.
        /// </summary>
        /// <param name="sharedDocument">The shared document.</param>
        private void PopulateManagedStackFrames(SLDocument sharedDocument)
        {
            sharedDocument.SelectWorksheet("Stack Frames");
            var row = 2;
            foreach (var record in StackFrameBreakdownMessage.Records)
            {
                sharedDocument.SetCellValue(row, 1, record.DisplayString);
                sharedDocument.SetCellValue(row, 2, record.ModuleName);
                sharedDocument.SetCellValue(row, 3, record.Count);
                row++;
            }
        }

        /// <summary>
        ///     Populates the unique stacks.
        /// </summary>
        /// <param name="sharedDocument">The shared document.</param>
        private void PopulateUniqueStacks(SLDocument sharedDocument)
        {
            sharedDocument.SelectWorksheet("Unique Stacks");
            var frameRollupRecords = UniqueStacksMessage.UniqueStackFrameRollupRecords;

            var curStackRow = 1;
            var minNumLinesPerStack = 2;
            var threadsColumn = 12;
            foreach (var frameRollupRecord in frameRollupRecords)
            {
                var max = minNumLinesPerStack;
                sharedDocument.SetCellValue(curStackRow, 1, "Stack:");
                sharedDocument.SetCellValue(curStackRow, threadsColumn, "Threads:");

                var stackIndex = 0;
                foreach (var line in frameRollupRecord.Threads.First().ManagedStackFrames.Select(f => f.DisplayString))
                {
                    sharedDocument.SetCellValue(curStackRow + 1 + stackIndex, 1, line);
                    stackIndex++;
                }

                if (stackIndex - 1 > max)
                    max = stackIndex - 1;

                var threadIndex = 0;
                foreach (var thread in frameRollupRecord.Threads.OrderByDescending(t =>
                    t.KernelModeTime + t.UserModeTime))
                {
                    sharedDocument.SetCellValue(curStackRow + 1 + threadIndex, threadsColumn,
                        $"{thread.DebuggerIndex}:{thread.OsId:x} ({thread.KernelModeTime + thread.UserModeTime})");
                    threadIndex++;
                }

                if (threadIndex - 1 > max)
                    max = threadIndex - 1;

                curStackRow += max + 5;
            }
        }

        /// <summary>
        ///     Gets or sets the event hub.
        /// </summary>
        /// <value>The event hub.</value>
        [Import]
        protected internal IEventHub EventHub { get; set; }

        /// <summary>
        ///     Gets or sets the stack frame breakdown message.
        /// </summary>
        /// <value>The stack frame breakdown message.</value>
        protected internal StackFrameBreakdownMessage StackFrameBreakdownMessage { get; set; }

        /// <summary>
        ///     Gets or sets the unique stacks message.
        /// </summary>
        /// <value>The unique stacks message.</value>
        protected internal UniqueStacksMessage UniqueStacksMessage { get; set; }

        /// <summary>
        ///     Gets the log.
        /// </summary>
        /// <value>The log.</value>
        internal ILogger Log { get; } = Slog.ForContext<ThreadExcelAnalyzer>();
    }
}