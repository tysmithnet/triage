using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpObject_Should
    {
        [Fact]
        public void Correctly_Add_A_Reference_Object()
        {
            // arrange
            var dumpObject = new DumpObject(0x1337, "Dogs.Corgi.Duke", 0x42, 1);

            // act
            // assert
            dumpObject.Address.Should().Be(0x1337);
            dumpObject.ToShortDescription().Should().Be($"Dogs.Corgi.Duke : {0x42} : 0x0000000000001337");
        }
    }
}
