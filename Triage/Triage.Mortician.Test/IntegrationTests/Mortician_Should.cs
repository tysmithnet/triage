using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Triage.Mortician.Test.IntegrationTests.Scenarios;
using Xunit;

namespace Triage.Mortician.Test.IntegrationTests
{
    public class Mortician_Should
    {
        [Fact]
        public void Not_Fail_When_Loading_Dump()
        {
            var dumpFile = Scenario.HelloWorld.GetDumpFile();
        }
    }
}
