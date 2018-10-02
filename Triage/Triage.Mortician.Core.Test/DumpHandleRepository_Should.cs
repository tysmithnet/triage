using System;
using System.Collections.Generic;
using FluentAssertions;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpHandleRepository_Should : BaseTest
    {
        [Fact]
        public void Should_Construct_Without_Error()
        {
            // arrange
            Action sut = () => new DumpHandleRepository(new Dictionary<ulong, DumpHandle>());

            // act
            // assert
            sut.Should().NotThrow();
		}
    }
}