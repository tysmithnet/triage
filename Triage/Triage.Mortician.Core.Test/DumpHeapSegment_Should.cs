using System;
using FluentAssertions;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpHeapSegment_Should : BaseTest
    {
        [Fact]
        public void Should_Construct_Without_Error()
        {
            // arrange
            Action sut = () => new DumpHeapSegment();

            // act
            // assert
            sut.Should().NotThrow();
		}
    }
}