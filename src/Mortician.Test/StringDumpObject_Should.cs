using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Testing.Common;
using Xunit;

namespace Mortician.Test
{
    public class StringDumpObject_Should : BaseTest
    {
        [Fact]
        public void Have_The_Correct_Short_Description()
        {
            // arrange
            var sut = new StringDumpObject(0, "System.String", 0, "012345678901234567890123456789", 1);

            // act
            var desc = sut.ToShortDescription();

            // assert
            desc.Should().NotBeNull();
            desc.Should().Be("System.String : 0 : 0x0000000000000000 - 012345678901234567890123");
        }
    }
}
