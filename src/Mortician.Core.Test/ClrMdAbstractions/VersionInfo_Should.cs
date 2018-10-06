using System;
using FluentAssertions;
using Mortician.Core.ClrMdAbstractions;
using Testing.Common;
using Xunit;

namespace Mortician.Core.Test.ClrMdAbstractions
{
    public class VersionInfo_Should : BaseTest
    {
        [Fact]
        public void Should_Construct_Without_Error()
        {
            // arrange
            Action sut = () => new VersionInfo();

            // act
            // assert
            sut.Should().NotThrow();
		}

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