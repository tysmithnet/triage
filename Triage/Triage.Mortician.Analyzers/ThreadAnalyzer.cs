using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Triage.Mortician.Repository;

namespace Triage.Mortician.Analyzers
{
    [Export(typeof(IAnalyzer))]
    public class ThreadAnalyzer : IAnalyzer
    {
        protected internal List<StackFrameRollupRecord> StackFrameResultsInternal = new List<StackFrameRollupRecord>();

        protected internal List<UniqueStackFrameRollupRecord> UniqueStackFrameResultsInternal =
            new List<UniqueStackFrameRollupRecord>();

        [Import]
        public EventHub EventHub { get; set; }

        [Import]
        public DumpThreadRepository DumpThreadRepository { get; set; }

        public Task Setup(CancellationToken cancellationToken)
        {
            var task1 = Task.Run(() =>
            {
                var result = DumpThreadRepository.Get()
                    .Where(t => t.ManagedStackFrames != null)
                    .GroupBy(t => string.Join("\n", t.ManagedStackFrames.Select(f => f.DisplayString)))
                    .Where(g => !string.IsNullOrWhiteSpace(g.Key))
                    .OrderByDescending(g => g.Count())
                    .ThenByDescending(g => g.Key.Length)
                    .Select(g => new UniqueStackFrameRollupRecord
                    {
                        DisplayString = g.Key,
                        Threads = g.ToList()
                    });
                UniqueStackFrameResultsInternal.AddRange(result);
            }, cancellationToken);

            var task2 = Task.Run(() =>
            {
                var results = DumpThreadRepository.Get().Where(t => t.ManagedStackFrames != null)
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

            return Task.WhenAll(task1, task2);
        }

        public Task Process(CancellationToken cancellationToken)
        {
            if(StackFrameResultsInternal.Any())
                EventHub.Broadcast(new StackFrameBreakdownMessage
                {
                    Records = StackFrameResultsInternal
                });

            if(UniqueStackFrameResultsInternal.Any())
                EventHub.Broadcast(new UniqueStacksMessage
                {
                    UniqueStackFrameRollupRecords = UniqueStackFrameResultsInternal
                });                                                                

            return Task.CompletedTask;
        }
    }
}