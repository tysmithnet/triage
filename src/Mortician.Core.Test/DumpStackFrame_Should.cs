using System;
using FluentAssertions;
using Mortician.Core.ClrMdAbstractions;
using Testing.Common;
using Xunit;

namespace Mortician.Core.Test
{
    public class DumpStackFrame_Should : BaseTest
    {
        [Fact]
        public void Should_Construct_Without_Error()
        {
            // arrange
            Action sut = () => new DumpStackFrame();

            // act
            // assert
            sut.Should().NotThrow();
		}

        [Fact]
        public void Exhibit_Entity_Equality()
        {
            // arrange
            var a = new DumpStackFrame()
            {
                StackPointer = 0x0
            };
            var b = new DumpStackFrame()
            {
                StackPointer = 0x0
            };
            var c = new DumpStackFrame()
            {
                StackPointer = 0x1
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