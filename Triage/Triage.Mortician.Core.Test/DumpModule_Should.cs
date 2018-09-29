using System;
using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpModule_Should
    {
        [Fact]
        public void Set_Member_Values_Correctly()
        {
            // arrange
            var module = new DumpModule()
            {
                Key = new DumpModuleKey(0x1337, "AssemblyName"),

            };

            // act
            // assert
            module.Key.AssemblyId.Should().Be(0x1337);
            module.Key.Name.Should().Be("AssemblyName");
            module.DebuggingMode.Should().Be(DebuggableAttribute.DebuggingModes.Default);
            module.FileName.Should().Be(@"C:\temp\assembly.dll");
            module.ImageBase = 0x4000;
            module.IsFile = true;
            module.IsDynamic = false;
            module.Name.Should().Be("Assembly");
            module.PdbInfo.FileName.Should().Be(@"C:\temp\assembly.pdb");
            module.Size.Should().Be(0x42);
            module.PdbInfo.Guid.Should().Be(Guid.Empty);
        }

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
    }
}