using System;
using FluentAssertions;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class ManagedCodeLocation_Should : BaseTest
    {
        [Fact]
        public void Should_Construct_Without_Error()
        {
            // arrange
            Action sut = () => new ManagedCodeLocation(0x0, 0x0, "");

            // act
            // assert
            sut.Should().NotThrow();
		}
    }
}