using System;
using FluentAssertions;
using Mortician.Core.ClrMdAbstractions;
using Testing.Common;
using Xunit;

namespace Mortician.Core.Test
{
    public class DumpModuleKey_Should : BaseTest
    {
        [Fact]
        public void Should_Construct_Without_Error()
        {
            // arrange
            Action sut = () => new DumpModuleKey(0x0, "");

            // act
            // assert
            sut.Should().NotThrow();
		}

        [Fact]
        public void Exhibit_Value_Equality()
        {
            var a = new DumpModuleKey(0xa, "a");
            var a0 = new DumpModuleKey(0xa, "b");
            var a1 = new DumpModuleKey(0xb, "a");
            var b = new DumpModuleKey(0xa, "a");
            var c = new DumpModuleKey(0xc, "c");

            a.GetHashCode().Should().Be(b.GetHashCode());
            a.GetHashCode().Should().NotBe(c.GetHashCode());

            a.CompareTo(a0).Should().Be(-1);
            a.CompareTo(a1).Should().Be(-1);

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