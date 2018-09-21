using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    }
}