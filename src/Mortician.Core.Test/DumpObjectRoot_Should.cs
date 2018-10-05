using System;
using FluentAssertions;
using Testing.Common;
using Xunit;

namespace Mortician.Core.Test
{
    public class DumpObjectRoot_Should : BaseTest
    {
        [Fact]
        public void Add_A_Thread_Only_Once()
        {
            // arrange
            var sut = new DumpObjectRoot
            {
                Address = 0x4000
            };
            var thread = new DumpThread
            {
                OsId = 0x40
            };

            // act
            sut.AddThread(thread);
            sut.AddThread(thread);

            // assert
            sut.Threads.Should().HaveCount(1);
        }

        [Fact]
        public void Exhibit_Entity_Equality()
        {
            var a = new DumpObjectRoot
            {
                Address = 0,
                Name = "Something"
            };
            var b = new DumpObjectRoot
            {
                Address = 0,
                Name = "Something Else"
            };
            var c = new DumpObjectRoot
            {
                Address = 1,
                Name = "Something"
            };

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
            a.Equals((object) a).Should().BeTrue();
            a.Equals((object) b).Should().BeTrue();
            a.Equals((object) c).Should().BeFalse();
            a.Equals((object) null).Should().BeFalse();
            a.CompareTo((object) a).Should().Be(0);
            a.CompareTo((object) b).Should().Be(0);
            a.CompareTo((object) c).Should().Be(-1);
            a.CompareTo((object) null).Should().Be(1);
            (a < b).Should().BeFalse();
            (a <= b).Should().BeTrue();
            (c > a).Should().BeTrue();
            (c >= a).Should().BeTrue();
            Action throws = () => a.CompareTo("");
            throws.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Should_Construct_Without_Error()
        {
            // arrange
            Action sut = () => new DumpObjectRoot();

            // act
            // assert
            sut.Should().NotThrow();
        }
    }
}