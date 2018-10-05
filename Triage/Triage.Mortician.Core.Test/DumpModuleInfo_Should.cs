using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpModuleInfo_Should : BaseTest
    {
        [Fact]
        public void Exhibit_Value_Equality()
        {
            // arrange
            var a = new DumpModuleInfo()
            {
                FileName = "a",
                FileSize = 0x10,
                ImageBase = 0x400,
                IsManaged = true,
                IsRuntime = false,
                TimeStamp = 0x400,
                Version = new VersionInfo(1,1,1,1)
            };
            var b = new DumpModuleInfo()
            {
                FileName = "a",
                FileSize = 0x10,
                ImageBase = 0x400,
                IsManaged = true,
                IsRuntime = false,
                TimeStamp = 0x400,
                Version = new VersionInfo(1, 1, 1, 1)
            };
            var c = new DumpModuleInfo()
            {
                FileName = "b",
                FileSize = 0x10,
                ImageBase = 0x400,
                IsManaged = true,
                IsRuntime = false,
                TimeStamp = 0x400,
                Version = new VersionInfo(1, 1, 1, 1)
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
