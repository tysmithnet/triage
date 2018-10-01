using FluentAssertions;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class VersionInfo_Should : BaseTest
    {
        [Fact]
        public void Return_The_Correct_Version_String()
        {
            // arrange
            var versionInfo = new VersionInfo(1, 2, 3, 4);

            // act
            var s = versionInfo.ToString();

            // assert
            s.Should().Be("v1.2.3.4");
        }
    }
}