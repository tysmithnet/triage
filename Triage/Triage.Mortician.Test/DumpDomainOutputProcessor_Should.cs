using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Triage.Mortician.Test
{
    public class DumpDomainOutputProcessor_Should
    {
        [Fact]
        public void Correctly_Identify_The_Number_Of_Domains()
        {
            // arrange
            var processor = new DumpDomainOutputProcessor();

            // act
            var report = processor.ProcessOutput(HELLO_WORLD);

            // assert
            report.AppDomainsInternal.Count.Should().Be(3);
            report.SystemDomain.Address.Should().Be(0x00007ffc206d04d0);
            report.SystemDomain.LowFrequencyHeap.Should().Be(0x00007ffc206d0a48);
            report.SystemDomain.HighFrequencyHeap.Should().Be(0x00007ffc206d0ad8);
            report.SystemDomain.StubHeap.Should().Be(0x00007ffc206d0b68);
            report.SystemDomain.Stage.Should().Be(AppDomainStage.Open);
            report.SystemDomain.Name.Should().Be("None");
        }

        #region Sample !dumpdomain output

        public const string HELLO_WORLD = @"--------------------------------------
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
Name:               Triage.TestApplications.Console.exe
Assembly:           0000014c430a0ba0 [C:\WINDOWS\Microsoft.Net\assembly\GAC_64\mscorlib\v4.0_4.0.0.0__b77a5c561934e089\mscorlib.dll]
ClassLoader:        0000014c430a0cc0
SecurityDescriptor: 0000014c43033050
  Module Name
00007ffc072b1000            C:\WINDOWS\Microsoft.Net\assembly\GAC_64\mscorlib\v4.0_4.0.0.0__b77a5c561934e089\mscorlib.dll

Assembly:           0000014c430ae9a0 [C:\projects\triage\Triage\Triage.TestApplications.Console\bin\Debug\Triage.TestApplications.Console.exe]
ClassLoader:        0000014c430aeac0
SecurityDescriptor: 0000014c43033320
  Module Name
00007ffbc0684118            C:\projects\triage\Triage\Triage.TestApplications.Console\bin\Debug\Triage.TestApplications.Console.exe

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


        #endregion
    }
}
