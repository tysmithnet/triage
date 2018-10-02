using System;
using System.Diagnostics;
using FluentAssertions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpModule_Should : BaseTest
    {
        [Fact]
        public void Return_The_Correct_AppDomains()
        {
            // arrange
            var module = new DumpModule();
            var domain = new DumpAppDomain();

            // act
            module.AppDomainsInternal.Add(domain);

            // assert
            module.AppDomains.Should().HaveCount(1);
        }

        [Fact]
        public void Add_A_AppDomain_Only_Once()
        {
            // arrange
            var sut = new DumpModule(new DumpModuleKey(0, ""));
            var appDomain = new DumpAppDomain();

            // act
            sut.AddAppDomain(appDomain);
            sut.AddAppDomain(appDomain);

            // assert
            sut.AppDomains.Should().HaveCount(1);
        }

        [Fact]
        public void Add_A_Type_Only_Once()
        {
            // arrange
            var sut = new DumpModule(new DumpModuleKey(0, ""));
            var type = new DumpType();

            // act
            sut.AddType(type);
            sut.AddType(type);

            // assert
            sut.Types.Should().HaveCount(1);
        }

        [Fact]
        public void Set_Member_Values_Correctly()
        {
            // arrange
            var module = new DumpModule(new DumpModuleKey(0x1337, "AssemblyName"))
            {
                DebuggingMode = DebuggableAttribute.DebuggingModes.Default,
                FileName = @"C:\temp\assembly.dll",
                ImageBase = 0x4000,
                IsFile = true,
                IsDynamic = false,
                Size = 0x42
            };

            // act
            // assert
            module.Key.AssemblyId.Should().Be(0x1337);
            module.AssemblyId.Should().Be(0x1337);
            module.Key.Name.Should().Be("AssemblyName");
            module.DebuggingMode.Should().Be(DebuggableAttribute.DebuggingModes.Default);
            module.FileName.Should().Be(@"C:\temp\assembly.dll");
            module.ImageBase = 0x4000;
            module.IsFile = true;
            module.IsDynamic = false;
            module.Name.Should().Be("AssemblyName");
            module.Size.Should().Be(0x42);
        }

        [Fact]
        public void Exhibit_Entity_Equality()
        {
            // arrange
            var a = new DumpModule(new DumpModuleKey(0, "0"));
            var b = new DumpModule(new DumpModuleKey(0, "0"));
            var c = new DumpModule(new DumpModuleKey(1, "0"));

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