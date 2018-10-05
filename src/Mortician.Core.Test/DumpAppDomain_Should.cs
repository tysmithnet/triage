using System;
using FluentAssertions;
using Testing.Common;
using Xunit;

namespace Mortician.Core.Test
{
    public class DumpAppDomain_Should : BaseTest
    {
        [Fact]
        public void Correctly_Add_Modules()
        {
            // arrange
            var appDomain = new DumpAppDomain();
            var module = new DumpModule();

            // act
            appDomain.AddModule(module);

            // assert
            appDomain.LoadedModules.Should().HaveCount(1);
        }

        [Fact]
        public void Add_A_Handle_Only_Once()
        {
            // arrange
            var sut = new DumpAppDomain();
            var handle = new DumpHandle();

            // act
            sut.AddHandle(handle);
            sut.AddHandle(handle);

            // assert
            sut.Handles.Should().HaveCount(1);
        }


        [Fact]
        public void Exhibit_Entity_Equality()
        {
            // arrange
            var a = new DumpAppDomain()
            {
                Address = 0,
                Name = "Something"
            };
            var b = new DumpAppDomain
            {
                Address = 0,
                Name = "Something Else"
            };
            var c = new DumpAppDomain
            {
                Address = 1,
                Name = "Something"
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