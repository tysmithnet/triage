using System;
using System.Linq;
using FluentAssertions;
using Moq;
using Mortician.Reports.DumpDomain;
using Testing.Common;
using Xunit;

namespace Mortician.Test
{
    public class DumpDomainReportFactory_Should : BaseTest
    {
        private const string HAPPY_PATH = @"PDB symbol for clr.dll not loaded
--------------------------------------
System Domain:      00007ffc206d04d0
LowFrequencyHeap:   00007ffc206d0a48
HighFrequencyHeap:  00007ffc206d0ad8
StubHeap:           00007ffc206d0b68
Stage:              OPEN
Name:               None
--------------------------------------
Shared Domain:      00007ffc206cff00
LowFrequencyHeap:   00007ffc206d0a48
HighFrequencyHeap:  00007ffc206d0ad8
StubHeap:           00007ffc206d0b68
Stage:              OPEN
Name:               None
Assembly:           0000014c430a0ba0 [C:\WINDOWS\Microsoft.Net\assembly\GAC_64\mscorlib\v4.0_4.0.0.0__b77a5c561934e089\mscorlib.dll]
ClassLoader:        0000014c430a0cc0
  Module Name
00007ffc072b1000            C:\WINDOWS\Microsoft.Net\assembly\GAC_64\mscorlib\v4.0_4.0.0.0__b77a5c561934e089\mscorlib.dll

--------------------------------------
Domain 1:           0000014c4303a470
LowFrequencyHeap:   0000014c4303ac68
HighFrequencyHeap:  0000014c4303acf8
StubHeap:           0000014c4303ad88
Stage:              OPEN
SecurityDescriptor: 0000014c43024490
Name:               TestApplications.Console.exe
Assembly:           0000014c430a0ba0 [C:\WINDOWS\Microsoft.Net\assembly\GAC_64\mscorlib\v4.0_4.0.0.0__b77a5c561934e089\mscorlib.dll]
ClassLoader:        0000014c430a0cc0
SecurityDescriptor: 0000014c43033050
  Module Name
00007ffc072b1000            C:\WINDOWS\Microsoft.Net\assembly\GAC_64\mscorlib\v4.0_4.0.0.0__b77a5c561934e089\mscorlib.dll

Assembly:           0000014c430ae9a0 [C:\projects\triage\Triage\TestApplications.Console\bin\Debug\TestApplications.Console.exe]
ClassLoader:        0000014c430aeac0
SecurityDescriptor: 0000014c43033320
  Module Name
00007ffbc0684118            C:\projects\triage\Triage\TestApplications.Console\bin\Debug\TestApplications.Console.exe

Assembly:           0000014c430b71d0 [C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Configuration\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Configuration.dll]
ClassLoader:        0000014c430bece0
SecurityDescriptor: 0000014c430c2570
  Module Name
00007ffbfc491000            C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Configuration\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Configuration.dll

Assembly:           0000014c430b23e0 [C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System\v4.0_4.0.0.0__b77a5c561934e089\System.dll]
ClassLoader:        0000014c430beef0
SecurityDescriptor: 0000014c430c10d0
  Module Name
00007ffbcf6c1000            C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System\v4.0_4.0.0.0__b77a5c561934e089\System.dll

Assembly:           0000014c430b70b0 [C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Core\v4.0_4.0.0.0__b77a5c561934e089\System.Core.dll]
ClassLoader:        0000014c430be8c0
SecurityDescriptor: 0000014c430c1760
  Module Name
00007ffbcdc81000            C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Core\v4.0_4.0.0.0__b77a5c561934e089\System.Core.dll

Assembly:           0000014c430c6c50 [C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Xml\v4.0_4.0.0.0__b77a5c561934e089\System.Xml.dll]
ClassLoader:        0000014c430bead0
SecurityDescriptor: 0000014c430c1850
  Module Name
00007ffbccea1000            C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Xml\v4.0_4.0.0.0__b77a5c561934e089\System.Xml.dll";

        [Fact]
        public void Save_The_Report_Output_On_Setup()
        {
            // arrange
            var sut = new DumpDomainReportFactory();
            var proxy = new Mock<IDebuggerProxy>();
            proxy.Setup(debuggerProxy => debuggerProxy.Execute("!dumpdomain", It.IsAny<TimeSpan?>())).Returns(HAPPY_PATH);

            // act
            sut.Setup(proxy.Object);

            // assert
            sut.RawOutput.Should().Be(HAPPY_PATH);
        }

        [Fact]
        public void Correctly_Identify_The_Default_Domain()
        {
            // arrange
            var sut = new DumpDomainReportFactory();
            var proxy = new Mock<IDebuggerProxy>();
            proxy.Setup(debuggerProxy => debuggerProxy.Execute("!dumpdomain", It.IsAny<TimeSpan?>())).Returns(HAPPY_PATH);

            // act
            sut.Setup(proxy.Object);
            var report = (DumpDomainReport)sut.Process();

            // assert
            report.AppDomains.Should().HaveCount(3);
            report.DefaultDomain.Address.Should().Be(0x0000014c4303a470);
            report.DefaultDomain.LowFrequencyHeap.Should().Be(0x0000014c4303ac68);
            report.DefaultDomain.HighFrequencyHeap.Should().Be(0x0000014c4303acf8);
            report.DefaultDomain.StubHeap.Should().Be(0x0000014c4303ad88);
            report.DefaultDomain.SecurityDescriptor.Should().Be(0x0000014c43024490);
            report.DefaultDomain.Stage.Should().Be(AppDomainStage.Open);
            report.DefaultDomain.Name.Should().Be("TestApplications.Console.exe");

            report.DefaultDomain.Assemblies.ElementAt(0).Address.Should().Be(0x0000014c430a0ba0);
            report.DefaultDomain.Assemblies.ElementAt(0).Location.Should().Be(
                @"C:\WINDOWS\Microsoft.Net\assembly\GAC_64\mscorlib\v4.0_4.0.0.0__b77a5c561934e089\mscorlib.dll");
            report.DefaultDomain.Assemblies.ElementAt(0).ClassLoader.Should().Be(0x0000014c430a0cc0);
            report.DefaultDomain.Assemblies.ElementAt(0).SecurityDescriptor.Should().Be(0x0000014c43033050);
            report.DefaultDomain.Assemblies.ElementAt(0).Modules.First().Address.Should().Be(0x00007ffc072b1000);
            report.DefaultDomain.Assemblies.ElementAt(0).Modules.First().Location.Should().Be(
                @"C:\WINDOWS\Microsoft.Net\assembly\GAC_64\mscorlib\v4.0_4.0.0.0__b77a5c561934e089\mscorlib.dll");

            report.DefaultDomain.Assemblies.ElementAt(1).Address.Should().Be(0x0000014c430ae9a0);
            report.DefaultDomain.Assemblies.ElementAt(1).Location.Should().Be(
                @"C:\projects\triage\Triage\TestApplications.Console\bin\Debug\TestApplications.Console.exe");
            report.DefaultDomain.Assemblies.ElementAt(1).ClassLoader.Should().Be(0x0000014c430aeac0);
            report.DefaultDomain.Assemblies.ElementAt(1).SecurityDescriptor.Should().Be(0x0000014c43033320);
            report.DefaultDomain.Assemblies.ElementAt(1).Modules.First().Address.Should().Be(0x00007ffbc0684118);
            report.DefaultDomain.Assemblies.ElementAt(1).Modules.First().Location.Should().Be(
                @"C:\projects\triage\Triage\TestApplications.Console\bin\Debug\TestApplications.Console.exe");
        }

        [Fact]
        public void Correctly_Identify_The_Shared_Domain()
        {
            // arrange
            var processor = new DumpDomainReportFactory();

            // act
            var report = processor.ProcessOutput(HAPPY_PATH);

            // assert
            report.SharedDomain.Address.Should().Be(0x00007ffc206cff00);
            report.SharedDomain.LowFrequencyHeap.Should().Be(0x00007ffc206d0a48);
            report.SharedDomain.HighFrequencyHeap.Should().Be(0x00007ffc206d0ad8);
            report.SharedDomain.StubHeap.Should().Be(0x00007ffc206d0b68);
            report.SharedDomain.Stage.Should().Be(AppDomainStage.Open);
            report.SharedDomain.Name.Should().Be("None");
            report.SharedDomain.Assemblies.Should().HaveCount(1);
            report.SharedDomain.Assemblies.First().Address.Should().Be(0x0000014c430a0ba0);
            report.SharedDomain.Assemblies.First().Location.Should().Be(
                @"C:\WINDOWS\Microsoft.Net\assembly\GAC_64\mscorlib\v4.0_4.0.0.0__b77a5c561934e089\mscorlib.dll");
            report.SharedDomain.Assemblies.First().ClassLoader.Should().Be(0x0000014c430a0cc0);
            report.SharedDomain.Assemblies.First().Modules.Should().HaveCount(1);
            report.SharedDomain.Assemblies.First().Modules.First().Address.Should().Be(0x00007ffc072b1000);
            report.SharedDomain.Assemblies.First().Modules.First().Location.Should().Be(
                @"C:\WINDOWS\Microsoft.Net\assembly\GAC_64\mscorlib\v4.0_4.0.0.0__b77a5c561934e089\mscorlib.dll");
        }

        [Fact]
        public void Correctly_Identify_The_System_Domain()
        {
            // arrange
            var processor = new DumpDomainReportFactory();

            // act
            var report = processor.ProcessOutput(HAPPY_PATH);

            // assert
            report.AppDomainsInternal.Count.Should().Be(3);
            report.SystemDomain.Address.Should().Be(0x00007ffc206d04d0);
            report.SystemDomain.LowFrequencyHeap.Should().Be(0x00007ffc206d0a48);
            report.SystemDomain.HighFrequencyHeap.Should().Be(0x00007ffc206d0ad8);
            report.SystemDomain.StubHeap.Should().Be(0x00007ffc206d0b68);
            report.SystemDomain.Stage.Should().Be(AppDomainStage.Open);
            report.SystemDomain.Name.Should().Be("None");
        }
    }
}