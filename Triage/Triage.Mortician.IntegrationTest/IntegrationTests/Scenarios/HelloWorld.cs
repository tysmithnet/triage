using System.Configuration;

namespace Triage.Mortician.IntegrationTest.IntegrationTests.Scenarios
{
    public partial class Scenario
    {
        public static readonly _HelloWorld HelloWorld = new _HelloWorld();

        // ReSharper disable once InconsistentNaming
        public class _HelloWorld : Scenario
        {
            /// <inheritdoc />
            public override bool IsLibrary { get; } = false;

            /// <inheritdoc />
            public override string[] SourceFiles { get; }

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