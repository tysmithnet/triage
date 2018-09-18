using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Triage.Mortician.Test
{
    public class Engine_Should
    {
        [Fact]
        public void Shutdown_The_Event_Hub_When_Analyzers_Have_Finished()
        {
            // arrange
            var eventHubMock = new Mock<IEventHub>();
            var analyzerMock = new Mock<IAnalyzer>();
            
            var engine = new Engine()
            {
                Analyzers = new [] {analyzerMock.Object},
                EventHub = eventHubMock.Object
            };

            // act


            // assert
        }
    }
}
