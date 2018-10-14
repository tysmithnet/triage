using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Mortician.Core;
using Testing.Common;
using Xunit;

namespace Mortician.Analyzers.Test
{
    public class ThreadAnalyzer_Should
    {
        [Fact]
        public void Identify_Unique_Stacks()
        {
            // arrange
            var repo = new DumpThreadRepositoryBuilder()
                .WithThreads(new DumpThread[]
                {
                    new DumpThread()
                    {
                        ManagedStackFramesInternal =
                        {
                            new DumpStackFrame()
                            {
                                DisplayString = "Foo()",
                                ModuleName = "A"
                            },
                            new DumpStackFrame()
                            {
                                DisplayString = "Bar()",
                                ModuleName = "B"
                            },
                            new DumpStackFrame()
                            {
                                DisplayString = "Baz()",
                                ModuleName = "C"
                            },
                            new DumpStackFrame()
                            {
                                DisplayString = "Faz()",
                                ModuleName = "D"
                            },
                        }
                    },
                    new DumpThread()
                    {
                        ManagedStackFramesInternal =
                        {
                            new DumpStackFrame()
                            {
                                DisplayString = "Foo()",
                                ModuleName = "A"
                            },
                            new DumpStackFrame()
                            {
                                DisplayString = "Bar()",
                                ModuleName = "B"
                            },
                            new DumpStackFrame()
                            {
                                DisplayString = "Baz()",
                                ModuleName = "C"
                            },
                            new DumpStackFrame()
                            {
                                DisplayString = "Faz()",
                                ModuleName = "D"
                            },
                        }
                    },
                    new DumpThread()
                    {
                        ManagedStackFramesInternal =
                        {
                            new DumpStackFrame()
                            {
                                DisplayString = "Foo()",
                                ModuleName = "A"
                            },
                            new DumpStackFrame()
                            {
                                DisplayString = "Bar()",
                                ModuleName = "B"
                            },
                            new DumpStackFrame()
                            {
                                DisplayString = "Baz()",
                                ModuleName = "C"
                            },
                        }
                    },
                })
                .Build();
            var eventHub = new EventHub();
            var analyzer = new ThreadAnalyzer()
            {
                DumpThreadRepository = repo,
                EventHub = eventHub
            };
            var stackObs = eventHub.Get<StackFrameBreakdownMessage>();
            var uniqueObs = eventHub.Get<UniqueStacksMessage>();

            // act
            analyzer.Setup(CancellationToken.None).Wait();
            analyzer.Process(CancellationToken.None).Wait();
            eventHub.Shutdown();

            // assert
            analyzer.UniqueStackFrameResultsInternal.Should().HaveCount(2);
            analyzer.StackFrameResultsInternal.Should().HaveCount(4);
            stackObs.SingleAsync().Wait().Records.Should().HaveCount(4);
            uniqueObs.SingleAsync().Wait().UniqueStackFrameRollupRecords.Should().HaveCount(2);
        }
    }
}
