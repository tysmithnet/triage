using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Triage.Mortician.Core;
using Triage.Mortician.IntegrationTest.IntegrationTests.Scenarios;
using Xunit;

namespace Triage.Mortician.IntegrationTest
{
    public class Mortician_Should
    {
        internal class MyAnalyzer : IAnalyzer
        {
            /// <inheritdoc />
            public Task Process(CancellationToken cancellationToken)
            {
                AppDomainRepo.Get().Count().Should().BeGreaterThan(0);
                return Task.CompletedTask;
            }

            /// <inheritdoc />
            public Task Setup(CancellationToken cancellationToken) => Task.CompletedTask;

            [Import]
            public IDumpAppDomainRepository AppDomainRepo { get; set; }

            [Import]
            public IDumpInformationRepository DumpInfoRepo { get; set; }

            [Import]
            public IDumpModuleRepository ModuleRepo { get; set; }

            [Import]
            public IDumpObjectRepository ObjectRepo { get; set; }

            [Import]
            public IDumpThreadRepository ThreadRepo { get; set; }

            [Import]
            public IDumpTypeRepository TypeRepo { get; set; }
        }

        [Fact]
        public void Not_Fail_When_Loading_Dump()
        {
            // arrange
            var dumpFile = Scenario.HelloWorld.GetDumpFile();
            var options = new DefaultOptions
            {
                DumpFile = dumpFile.FullName,
                SettingsFile = "Settings/Mortician_Should.json"
            };

            // act
            var result = Program.DefaultExecution(options, container =>
            {
                var analyzer = new MyAnalyzer();
                container.ComposeParts(analyzer);
                container.ComposeExportedValue<IAnalyzer>(analyzer);
                return container;
            });

            // assert
            result.Should().Be(0);
        }
    }
}