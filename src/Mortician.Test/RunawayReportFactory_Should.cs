using System;
using System.Linq;
using FluentAssertions;
using Moq;
using Mortician.Reports.EeStack;
using Mortician.Reports.Runaway;
using Testing.Common;
using Xunit;

namespace Mortician.Test
{
    public class RunawayReportFactory_Should : BaseTest
    {
        private const string HAPPY_PATH = @" User Mode Time
  Thread       Time
   0:2024      0 days 0:00:00.015
   6:375c      0 days 0:00:00.000
   5:68a0      0 days 0:00:00.000
   4:24e0      0 days 0:00:00.000
   3:61f4      0 days 0:00:00.000
   2:540       0 days 0:00:00.000
   1:6460      0 days 0:00:00.000
 Kernel Mode Time
  Thread       Time
   0:2024      1 days 0:00:00.046
   6:375c      0 days 0:00:00.000
   5:68a0      0 days 0:00:00.000
   4:24e0      0 days 0:00:00.000
   3:61f4      0 days 0:00:00.000
   2:540       0 days 0:00:00.000
   1:6460      0 days 0:00:00.000
";

        [Fact]
        public void Collect_Both_UserTime_And_KernelTime()
        {
            // arrange
            var sut = new RunawayReportFactory();

            // act
            var report = sut.ProcessOutput(HAPPY_PATH);

            // assert
            report.RunawayLines.Should().HaveCount(7);
            report.RunawayLines.First().UserModeTime.Should().Be(TimeSpan.FromMilliseconds(15));
            report.RunawayLines.First().KernelModeTime.Should()
                .Be(TimeSpan.FromDays(1).Add(TimeSpan.FromMilliseconds(46)));
            report.RunawayLines.First().TotalTime.Should().Be(TimeSpan.FromDays(1).Add(TimeSpan.FromMilliseconds(61)));
        }

        [Fact]
        public void Create_The_Report_When_Processed()
        {
            // arrange
            var sut = new RunawayReportFactory();
            var mock = new Mock<IDebuggerProxy>();
            mock.Setup(proxy => proxy.Execute("!runaway 3", It.IsAny<TimeSpan?>())).Returns(HAPPY_PATH);

            // act
            sut.Setup(mock.Object);
            var report = (RunawayReport)sut.Process();

            // assert
            report.RunawayLines.Should().HaveCount(7);
        }
    }
}