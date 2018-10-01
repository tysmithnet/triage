using FluentAssertions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpObject_Should : BaseTest
    {
        [Fact]
        public void Correctly_Add_A_Reference_Object()
        {
            // arrange
            var first = new DumpObject(0x1337, "Dogs.Corgi.Duke", 0x42, 1);
            var second = new DumpObject(0x1338, "Dogs.Corgi.Brady", 0x42, 2);
            first.AddReference(second);
            second.AddReferencer(first);

            // act
            // assert
            first.References.Should().HaveCount(1);
            second.Referencers.Should().HaveCount(1);
        }

        [Fact]
        public void Create_A_Short_Description_Correctly()
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