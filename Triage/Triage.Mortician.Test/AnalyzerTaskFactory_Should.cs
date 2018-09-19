using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Triage.Mortician.Core;
using Xunit;

namespace Triage.Mortician.Test
{
    public class AnalyzerTaskFactory_Should
    {
        [Fact]
        public async Task Return_A_Task_That_Throws_If_The_Cancellation_Token_Is_Set()
        {
            // arrange
            var fac = new AnalyzerTaskFactory();
            var analyzers = new[] {new Mock<IAnalyzer>().Object};
            var cancellationTokenSource = new CancellationTokenSource(0);

            // act
            var task = fac.StartAnalyzers(analyzers, cancellationTokenSource.Token);

            // assert
            try
            {
                await task;
            }
            catch (Exception e)
            {
                e.Should().BeOfType<TaskCanceledException>();
            }
        }
    }
}