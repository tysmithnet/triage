using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using SpreadsheetLight;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician.Analyzers
{
    [Export(typeof(IExcelAnalyzer))]
    public class ThreadExcelAnalyzer : IExcelAnalyzer
    {
        protected ILog Log = LogManager.GetLogger(typeof(ThreadExcelAnalyzer));

        [Import]
        public IDumpThreadRepository DumpThreadRepository { get; set; }

        public Task Setup(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Contribute(SLDocument sharedDocument)
        {
            sharedDocument.SelectWorksheet("Unique Stacks");
            var groups = DumpThreadRepository.Get()
                .GroupBy(t => string.Join("\n", t.StackFrames.Select(s => s.DisplayString)))
                .OrderByDescending(g => g.Count())
                .ThenByDescending(g => g.Key.Length);

            int curStackRow = 1;
            int minNumLinesPerStack = 2;
            int threadsColumn = 12;
            foreach (var group in groups)
            {
                int max = minNumLinesPerStack;
                sharedDocument.SetCellValue(curStackRow, 1, "Stack:");
                sharedDocument.SetCellValue(curStackRow, threadsColumn, "Threads:");

                int stackIndex = 0;
                foreach(var line in group.First().StackFrames.Select(x => x.DisplayString))
                {
                    sharedDocument.SetCellValue(curStackRow + 1 + stackIndex, 1, line);
                    stackIndex++;
                }

                if (stackIndex - 1 > max)
                    max = stackIndex - 1;

                int threadIndex = 0;
                foreach (var thread in group.OrderByDescending(t => t.KernelModeTime + t.UserModeTime))
                {
                    sharedDocument.SetCellValue(curStackRow + 1 + threadIndex, threadsColumn,
                        $"{thread.DebuggerIndex}:{thread.OsId:x}");
                    threadIndex++;
                }

                if (threadIndex - 1 > max)
                    max = threadIndex - 1;
                           
                curStackRow += max + 5;
            }
        }
    }
}
