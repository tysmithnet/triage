using System;
using FluentAssertions;
using Testing.Common;
using Xunit;

namespace Mortician.Core.Test
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
        public void Add_A_Reference_Only_Once()
        {
            // arrange
            var first = new DumpObject(0x1337, "Dogs.Corgi.Duke", 0x42, 1);
            var second = new DumpObject(0x1338, "Dogs.Corgi.Brady", 0x42, 2);
            first.AddReference(second);
            second.AddReferencer(first);
            first.AddReference(second);

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

        [Fact]
        public void Add_A_Thread_Only_Once()
        {
            // arrange
            var sut = new DumpObject();
            var thread = new DumpThread();

            // act
            sut.AddThread(thread);
            sut.AddThread(thread);

            // assert
            sut.Threads.Should().HaveCount(1);
        }

        [Fact]
        public void Exhibit_Entity_Equality()
        {
            // arrange
            var a = new DumpObject
            {
                Address = 0,
                FullTypeName = "Something"
            };
            var b = new DumpObject
            {
                Address = 0,
                FullTypeName = "Something Else"
            };
            var c = new DumpObject
            {
                Address = 1,
                FullTypeName = "Something"
            };

            // act
            // assert
            a.GetHashCode().Should().Be(b.GetHashCode());
            a.GetHashCode().Should().NotBe(c.GetHashCode());

            a.Equals(a).Should().BeTrue();
            a.Equals(b).Should().BeTrue();
            a.Equals(c).Should().BeFalse();
            a.Equals(null).Should().BeFalse();
            a.Equals("").Should().BeFalse();
            a.CompareTo(a).Should().Be(0);
            a.CompareTo(b).Should().Be(0);
            a.CompareTo(c).Should().Be(-1);
            a.CompareTo(null).Should().Be(1);
            a.Equals((object)a).Should().BeTrue();
            a.Equals((object)b).Should().BeTrue();
            a.Equals((object)c).Should().BeFalse();
            a.Equals((object)null).Should().BeFalse();
            a.CompareTo((object)a).Should().Be(0);
            a.CompareTo((object)b).Should().Be(0);
            a.CompareTo((object)c).Should().Be(-1);
            a.CompareTo((object)null).Should().Be(1);
            (a < b).Should().BeFalse();
            (a <= b).Should().BeTrue();
            (c > a).Should().BeTrue();
            (c >= a).Should().BeTrue();
            Action throws = () => a.CompareTo("");
            throws.Should().Throw<ArgumentException>();
        }
    }
}