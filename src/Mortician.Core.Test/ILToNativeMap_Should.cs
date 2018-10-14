using FluentAssertions;
using Mortician.Core.ClrMdAbstractions;
using Testing.Common;
using Xunit;

namespace Mortician.Core.Test
{
    public class ILToNativeMap_Should : BaseTest
    {
        [Fact]
        public void Have_The_Correct_To_String()
        {
            // arrange
            var sut = new ILToNativeMap
            {
                ILOffset = 0x42,
                StartAddress = 0x1337,
                EndAddress = 0x1338
            };

            // act
            // assert
            sut.ToString().Should().Be("42 - [1337-1338]");
        }
    }
}