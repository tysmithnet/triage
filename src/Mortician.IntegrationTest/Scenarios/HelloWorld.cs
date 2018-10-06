using System.Configuration;

// ReSharper disable once CheckNamespace
namespace Mortician.IntegrationTest.Scenarios
{
    public abstract partial class Scenario
    {
        public static HelloWorldScenario HelloWorld { get; } = new HelloWorldScenario();

        // ReSharper disable once InconsistentNaming
        public class HelloWorldScenario : Scenario
        {
            /// <inheritdoc />
            public override bool IsLibrary { get; } = false;

            /// <inheritdoc />
            public override string X64ExeLocation { get; } = ConfigurationManager.AppSettings["HelloWorld.exe_x64"];

            /// <inheritdoc />
            public override string X64ScenarioDumpFile { get; } =
                ConfigurationManager.AppSettings["HelloWorld.dmp_x64"];

            /// <inheritdoc />
            public override string X86ExeLocation { get; } = ConfigurationManager.AppSettings["HelloWorld.exe_x86"];

            /// <inheritdoc />
            public override string X86ScenarioDumpFile { get; } =
                ConfigurationManager.AppSettings["HelloWorld.dmp_x86"];
        }
    }
}