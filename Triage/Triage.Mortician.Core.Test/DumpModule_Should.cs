using System;
using System.Diagnostics;
using FluentAssertions;
using Triage.Mortician.Core.ClrMdAbstractions;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpModule_Should
    {
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