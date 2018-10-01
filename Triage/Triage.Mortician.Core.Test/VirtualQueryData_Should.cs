using FluentAssertions;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class VirtualQueryData_Should : BaseTest
    {
        [Fact]
        public void Require_Create_An_Instance_With_Required_Parameters()
        {
            // arrange
            var sut = new VirtualQueryData(0x1337, 0x42);

            // act
            // assert
            sut.BaseAddress.Should().Be(0x1337);
            sut.Size.Should().Be(0x42);
        }
    }
}