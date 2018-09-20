using Triage.Mortician.IntegrationTest.IntegrationTests.Scenarios;
using Xunit;

namespace Triage.Mortician.IntegrationTest.IntegrationTests
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
