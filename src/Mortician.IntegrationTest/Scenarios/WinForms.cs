using System.Configuration;

namespace Mortician.IntegrationTest.Scenarios
{
    public abstract partial class Scenario
    {
        public static readonly WinFormsScenario WinForms = new WinFormsScenario();

        public class WinFormsScenario : Scenario
        {
            /// <inheritdoc />
            public override bool IsLibrary { get; } = false;

            /// <inheritdoc />
            public override string X64ExeLocation { get; } = ConfigurationManager.AppSettings["WinForms.exe_x64"];

            /// <inheritdoc />
            public override string X64ScenarioDumpFile { get; } = ConfigurationManager.AppSettings["WinForms.dmp_x64"];

            /// <inheritdoc />
            public override string X86ExeLocation { get; } = ConfigurationManager.AppSettings["WinForms.exe_x86"];

            /// <inheritdoc />
            public override string X86ScenarioDumpFile { get; } = ConfigurationManager.AppSettings["WinForms.dmp_x86"];
        }
    }
}