using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Triage.Mortician.Core;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Mortician.IntegrationTest.Scenarios;
using Xunit;

namespace Triage.Mortician.IntegrationTest
{
    public class WinForms_Should
    {
        [Export(typeof(IAnalyzer))]
        [Export]
        internal class TestAnalyzer : IAnalyzer
        {
            /// <inheritdoc />
            public Task Process(CancellationToken cancellationToken)
            {
                if (ObjectRepo.Get().First(x => x.FullTypeName.Contains("System.Windows.Forms.Button")) is
                    ButtonDumpObject button) ButtonText = button.Text;
                return Task.CompletedTask;
            }

            /// <inheritdoc />
            public Task Setup(CancellationToken cancellationToken) => Task.CompletedTask;

            [Import]
            public IDumpAppDomainRepository AppDomainRepo { get; set; }

            public string ButtonText { get; set; }

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

        [Export(typeof(IDumpObjectExtractor))]
        internal class ButtonExtractor : IDumpObjectExtractor
        {
            /// <inheritdoc />
            public bool CanExtract(IClrObject clrObject, IClrRuntime clrRuntime) =>
                clrObject.Type.Name == "System.Windows.Forms.Button";

            /// <inheritdoc />
            public DumpObject Extract(IClrObject clrObject, IClrRuntime clrRuntime)
            {
                var text = clrObject.GetStringField("text");
                return new ButtonDumpObject(clrObject.Address, clrObject.Type.Name, clrObject.Size,
                    clrRuntime.Heap.GetGeneration(clrObject.Address), text);
            }
        }

        internal class ButtonDumpObject : DumpObject
        {
            /// <inheritdoc />
            public ButtonDumpObject(ulong address, string fullTypeName, ulong size, int gen, string text) : base(
                address, fullTypeName, size, gen)
            {
                Text = text;
            }

            public string Text { get; set; }
        }

        [Fact]
        public void Extract_The_Text_From_A_Button()
        {
            // arrange
            var dumpFile = Scenario.WinForms.GetDumpFile();
            var options = new DefaultOptions
            {
                AdditionalTypes = new[]
                {
                    typeof(ButtonExtractor),
                    typeof(TestAnalyzer)
                },
                DumpFile = dumpFile.FullName,
                SettingsFile = "Settings/Mortician_Should.json"
            };

            // act
            CompositionContainer cc = null;
            var result = Program.DefaultExecution(options, container => cc = container);
            var analyzer = cc.GetExportedValue<TestAnalyzer>();

            // assert
            result.Should().Be(0);
            analyzer.ButtonText.Should().Be("Hello World");
        }
    }
}