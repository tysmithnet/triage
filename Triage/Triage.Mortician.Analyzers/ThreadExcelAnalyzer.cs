using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using SpreadsheetLight;
using Triage.Mortician.Repository;

namespace Triage.Mortician.Analyzers
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents an object that is capable of reporting on the threads from the memory dump
    /// </summary>
    /// <seealso cref="T:Triage.Mortician.Analyzers.IExcelAnalyzer" />
    [Export(typeof(IExcelAnalyzer))]
    public class ThreadExcelAnalyzer : IExcelAnalyzer
    {
        /// <summary>
        ///     The log
        /// </summary>
        protected ILog Log = LogManager.GetLogger(typeof(ThreadExcelAnalyzer));

        [Import]
        public EventHub EventHub { get; set; }

        protected internal StackFrameBreakdownMessage StackFrameBreakdownMessage { get; set; }
        protected internal UniqueStacksMessage UniqueStacksMessage { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Performs any required setup like number crunching etc.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A Task, that when complete will signal the setup completion
        /// </returns>
        public async Task Setup(CancellationToken cancellationToken)
        {
            var stackFrameBreakdownTask = EventHub.Get<StackFrameBreakdownMessage>()
                .FirstOrDefaultAsync().ToTask(cancellationToken);
            var uniqueStackTask = EventHub.Get<UniqueStacksMessage>().FirstOrDefaultAsync().ToTask(cancellationToken);
            await Task.WhenAll(stackFrameBreakdownTask, uniqueStackTask);

            StackFrameBreakdownMessage = stackFrameBreakdownTask.Result;
            UniqueStacksMessage = uniqueStackTask.Result;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Contributes the specified shared document.
        /// </summary>
        /// <param name="sharedDocument">The shared document.</param>
        public void Contribute(SLDocument sharedDocument)
        {
            if(UniqueStacksMessage != null)
                PopulateUniqueStacks(sharedDocument);
            else
                Log.Trace("No unique stack message received");

            if(StackFrameBreakdownMessage != null)
                PopulateManagedStackFrames(sharedDocument);
            else
                Log.Trace("No stack frame breakdown message received");
        }

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
                foreach (var thread in frameRollupRecord.Threads.OrderByDescending(t => t.KernelModeTime + t.UserModeTime))
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
    }
}