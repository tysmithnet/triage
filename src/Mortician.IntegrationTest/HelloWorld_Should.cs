using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Mortician.Core;
using Mortician.IntegrationTest.Scenarios;
using Mortician.Reports.Runaway;
using Testing.Common;
using Xunit;

namespace Mortician.IntegrationTest
{
    public class HelloWorld_Should : BaseTest
    {
        internal class TestAnalyzer : IAnalyzer
        {
            /// <inheritdoc />
            public Task Process(CancellationToken cancellationToken)
            {
                AppDomainCount = AppDomainRepo.AppDomains.Count();
                return Task.CompletedTask;
            }

            /// <inheritdoc />
            public Task Setup(CancellationToken cancellationToken) => Task.CompletedTask;

            public int AppDomainCount { get; set; }

            [Import]
            public RunawayReport RunawayReport { get; set; }

            [Import]
            public EngineSettings EngineSettings { get; set; }

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

        // todo: more reports
        // todo: app setting for native dll? try to load in advance to avoid dll hell problems?

        [Fact]
        public void Perform_Basic_Startup_Without_Failure()
        {
            // arrange
            var program = new Program();
            var dumpFile = Scenario.HelloWorld.GetDumpFile();
            var options = new Options
            {
                RunOptions = new RunOptions()
                {
                    DumpLocation = dumpFile.FullName,
                },
                SettingsFile = "Settings/Mortician_Should.json"
            };
            var analyzer = new TestAnalyzer();

            // act
            var result = program.DefaultExecution(options, container =>
            {
                container.ComposeParts(analyzer);
                container.ComposeExportedValue<IAnalyzer>(analyzer);
                return container;
            });

            // assert
            result.Should().Be(0);
            analyzer.AppDomainCount.Should().Be(3);
            analyzer.TypeRepo.Types.FirstOrDefault(t => t.Name == "TestApplications.Console.Person").Should()
                .NotBeNull();
            analyzer.TypeRepo.Types.FirstOrDefault(t => t.Name == "TestApplications.Console.Address").Should()
                .NotBeNull();
            analyzer.ThreadRepo.Threads.Any(t =>
                    t.ManagedStackFrames.Any(f =>
                        f.DisplayString.Contains("TestApplications.Console.Program.Main")))
                .Should().BeTrue();
            analyzer.EngineSettings.TestString.Should().Be("This is a test string");
            analyzer.RunawayReport.Should().NotBeNull();
        }
    }
}