using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Mortician.Core;
using Testing.Common;
using Xunit;

namespace Mortician.Test
{
    public class AnalyzerTaskFactory_Should : BaseTest
    {
        [Fact]
        public async Task Return_A_Task_That_Throws_If_The_Cancellation_Token_Is_Set()
        {
            // arrange
            var fac = new AnalyzerTaskFactory();
            var analyzers = new[] {new Mock<IAnalyzer>().Object};
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel(true);

            // act
            var task = fac.StartAnalyzers(analyzers, cancellationTokenSource.Token);

            // assert
            try
            {
                await Task.Delay(1000);
                await task;
            }
            catch (Exception e)
            {
                e.Should().BeOfType<TaskCanceledException>();
            }
        }

        [Fact]
        public void Not_Throw_If_Setup_Fails()
        {
            // arrange
            var sut = new AnalyzerTaskFactory();
            var badAnalyzer = new Mock<IAnalyzer>();
            badAnalyzer.Setup(analyzer => analyzer.Setup(CancellationToken.None)).Throws<FileNotFoundException>();
            Action mightThrow = () => sut.StartAnalyzers(new[] {badAnalyzer.Object}, CancellationToken.None);

            // act
            
            // assert
            mightThrow.Should().NotThrow();
        }

        [Fact]
        public void Not_Throw_If_Processing_Fails()
        {
            // arrange
            var sut = new AnalyzerTaskFactory();
            var badAnalyzer = new Mock<IAnalyzer>();
            badAnalyzer.Setup(analyzer => analyzer.Process(CancellationToken.None)).Throws<FileNotFoundException>();
            Action throws = () => sut.StartAnalyzers(new[] { badAnalyzer.Object }, CancellationToken.None);

            // act

            // assert
            throws.Should().NotThrow();
        }

        private class SetupFails : IAnalyzer
        {
            public bool SetupCalled { get; set; }
            public bool ProcessCalled { get; set; }
            /// <inheritdoc />
            public Task Process(CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc />
            public Task Setup(CancellationToken cancellationToken)
            {
                SetupCalled = true;
                throw new FileNotFoundException();
            }
        }

        [Fact]
        public void Wait_For_All_Tasks_To_Finish()
        {
            // arrange
            var sut = new AnalyzerTaskFactory();
            var cts = new CancellationTokenSource();
            var slowTime = DateTime.MinValue;
            var fastTime = DateTime.MinValue;

            Func<CancellationToken, Task<object>> slow = async ct =>
            {
                await Task.Delay(1000);
                slowTime = DateTime.Now;
                return null;
            };
            var slowAnalyzer = new Mock<IAnalyzer>();
            slowAnalyzer.Setup(analyzer => analyzer.Setup(It.IsAny<CancellationToken>())).Returns(slow);
            slowAnalyzer.Setup(analyzer => analyzer.Process(It.IsAny<CancellationToken>())).Returns(slow);

            Func<CancellationToken, Task<object>> fast = async ct =>
            {
                await Task.Delay(100);
                fastTime = DateTime.Now;
                return null;
            };
            var fastAnalyzer = new Mock<IAnalyzer>();
            fastAnalyzer.Setup(analyzer => analyzer.Setup(It.IsAny<CancellationToken>())).Returns(fast);
            fastAnalyzer.Setup(analyzer => analyzer.Process(It.IsAny<CancellationToken>())).Returns(fast);


            // act
            sut.StartAnalyzers(new[] { slowAnalyzer.Object, fastAnalyzer.Object }, cts.Token).Wait();
            
            // assert
            slowTime.Should().NotBe(DateTime.MinValue);
            fastTime.Should().NotBe(DateTime.MinValue);
            (slowTime > fastTime).Should().BeTrue();
        }

        [Fact]
        public void Not_Do_Anything_If_Immediately_Cancelled()
        {
            // arrange
            var sut = new AnalyzerTaskFactory();
            var cts = new CancellationTokenSource();
            var badAnalyzer = new Mock<IAnalyzer>();
            var normalAnalyzer = new Mock<IAnalyzer>();

            sut.StartTestingAction = cts.Cancel;
            badAnalyzer.Setup(analyzer => analyzer.Setup(It.IsAny<CancellationToken>())).Verifiable();
            badAnalyzer.Setup(analyzer => analyzer.Process(It.IsAny<CancellationToken>())).Verifiable();

            // act
            try
            {
                sut.StartAnalyzers(new[] { badAnalyzer.Object, normalAnalyzer.Object }, cts.Token).Wait();
            }
            catch (Exception)
            {
                ;
            }
            
            // assert
            badAnalyzer.Verify(analyzer => analyzer.Setup(cts.Token), Times.Exactly(0));
            badAnalyzer.Verify(analyzer => analyzer.Process(cts.Token), Times.Exactly(0));
        }

        [Fact]
        public void Not_Process_If_Cancelled_Before_Processing_Starts()
        {
            // arrange
            var sut = new AnalyzerTaskFactory();
            var cts = new CancellationTokenSource();
            var badAnalyzer = new Mock<IAnalyzer>();
            badAnalyzer.Setup(analyzer => analyzer.Setup(It.IsAny<CancellationToken>()))
                .Callback(() =>
                {
                    cts.Cancel();
                });
            badAnalyzer.Setup(analyzer => analyzer.Process(It.IsAny<CancellationToken>())).Verifiable();

            // act
            bool cancelled = false;
            try
            {
                sut.StartAnalyzers(new[] { badAnalyzer.Object }, cts.Token).Wait();
            }
            catch (Exception)
            {
                cancelled = true;
            }

            // assert
            badAnalyzer.Verify(analyzer => analyzer.Setup(cts.Token), Times.Exactly(1));
            badAnalyzer.Verify(analyzer => analyzer.Process(cts.Token), Times.Exactly(0));
        }
    }
}