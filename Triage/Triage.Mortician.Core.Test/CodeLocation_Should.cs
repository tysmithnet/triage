using System;
using FluentAssertions;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class CodeLocation_Should : BaseTest
    {
        [Fact]
        public void Should_Construct_Without_Error()
        {
            // arrange
            Action sut0 = () => new CodeLocation("", "", 0);
            Action sut1 = () => new CodeLocation();

            // act
            // assert
            sut0.Should().NotThrow();
            sut1.Should().NotThrow();
		}

        [Fact]
        public void Exhibit_Value_Equality()
        {
            var a = new CodeLocation("a", "a", 0);
            var a0 = new CodeLocation("a", "b", 0);
            var a1 = new CodeLocation("b", "a", 0);
            var b = new CodeLocation("a", "a", 0);
            var c = new CodeLocation("a", "a", 1);

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