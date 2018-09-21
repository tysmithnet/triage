using System.Configuration;

// ReSharper disable once CheckNamespace
namespace Triage.Mortician.IntegrationTest.IntegrationTests.Scenarios
{
    public partial class Scenario
    {
        public static readonly HelloWorldScenario HelloWorld = new HelloWorldScenario();

        // ReSharper disable once InconsistentNaming
        public class HelloWorldScenario : IntegrationTest.Scenarios.Scenario
        {
            /// <inheritdoc />
            public override bool IsLibrary { get; } = false;

            /// <inheritdoc />
            public override string X64ExeLocation { get; } = ConfigurationManager.AppSettings["HelloWorld.exe_x64"];

            /// <inheritdoc />
            public override string X64ScenarioDumpFile { get; } = ConfigurationManager.AppSettings["HelloWorld.dmp_x64"];

            /// <inheritdoc />
            public override string X86ExeLocation { get; } = ConfigurationManager.AppSettings["HelloWorld.exe_x86"];

            /// <inheritdoc />
            public override string X86ScenarioDumpFile { get; } = ConfigurationManager.AppSettings["HelloWorld.dmp_x86"];
        }
    }
}