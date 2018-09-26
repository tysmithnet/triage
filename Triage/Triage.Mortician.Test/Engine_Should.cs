using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Triage.Mortician.Core;
using Xunit;

namespace Triage.Mortician.Test
{
    public class Engine_Should
    {
        [Fact]
        public async Task Shutdown_The_Event_Hub_When_Analyzers_Have_Finished()
        {
            // arrange
            var eventHubMock = new Mock<IEventHub>();
            var taskFactoryMock = new Mock<IAnalyzerTaskFactory>();
            taskFactoryMock
                .Setup(factory =>
                    factory.StartAnalyzers(It.IsAny<IEnumerable<IAnalyzer>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var engine = new Engine
            {
                Analyzers = new[] {new Mock<IAnalyzer>().Object},
                EventHub = eventHubMock.Object,
                AnalyzerTaskFactory = taskFactoryMock.Object
            };

            // act
            await engine.Process();

            // assert
            eventHubMock.Verify(hub => hub.Shutdown(), Times.Once);
        }

        [Fact]
        public async Task Stop_If_There_Are_No_Analyzers()
        {
            // arrange
            var engine = new Engine();

            // act
            // assert
            engine.Process().IsCompleted.Should().BeTrue();
        }

        [Fact]
        public void Wait_For_The_Observers_To_Finish_After_Analyzers_Finish()
        {
            // arrange
            var sw = Stopwatch.StartNew();
            var engine = new Engine();
            engine.AnalyzerTaskFactory = new AnalyzerTaskFactory();
            var eventHub = new EventHub();
            var analyzer = new DummyAnalyzer();
            var observer = new DummyAnalysisObserver();

            engine.Analyzers = new[] {analyzer};
            engine.AnalysisObservers = new[] {observer};
            engine.EventHub = eventHub;
            analyzer.EventHub = eventHub;
            observer.EventHub = eventHub;

            // act
            var task = engine.Process();
            task.Wait();

            // assert
            sw.ElapsedMilliseconds.Should().BeLessThan(2000);
            task.IsCompleted.Should().BeTrue();
            task.IsFaulted.Should().BeFalse();
        }

        private class DummyAnalyzer : IAnalyzer
        {
            public IEventHub EventHub { get; internal set; }

            /// <inheritdoc />
            public async Task Process(CancellationToken cancellationToken)
            {
                EventHub.Broadcast(new DummyMessage()
                {
                    Amount = 5
                });
                Thread.Sleep(333);
                await Task.Delay(333);
                Thread.Sleep(333);
                EventHub.Broadcast(new DummyMessage()
                {
                    Amount = 5
                });
            }

            /// <inheritdoc />
            public Task Setup(CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }

        private class DummyMessage : Message
        {
            public int Amount { get; set; }

            public DummyMessage()
            {
            }
        }

        private class DummyAnalysisObserver : IAnalysisObserver
        {
            public IEventHub EventHub { get; internal set; }

            /// <inheritdoc />
            public Task Process(CancellationToken cancellationToken)
            {
                var messages = EventHub.Get<DummyMessage>();
                var task = messages.Select(x => x.Amount).Sum().ToTask(cancellationToken);
                Thread.Sleep(1000);
                return task;
            }

            /// <inheritdoc />
            public Task Setup(CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }
    }
}