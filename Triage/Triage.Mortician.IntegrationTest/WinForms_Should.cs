using System.ComponentModel.Composition;
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
        internal class TestAnalyzer : IAnalyzer
        {
            /// <inheritdoc />
            public Task Process(CancellationToken cancellationToken)
            {
                var button = ObjectRepo.Get().First(x => x.FullTypeName.Contains("System.Windows.Forms.Button")) as ButtonDumpObject;
                if (button != null)
                {
                    ButtonText = button.Text;
                }
                return Task.CompletedTask;
            }

            /// <inheritdoc />
            public Task Setup(CancellationToken cancellationToken) => Task.CompletedTask;

            public string ButtonText { get; set; }

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

        internal class ButtonExtractor : IDumpObjectExtractor
        {
            /// <inheritdoc />
            public bool CanExtract(IClrObject clrObject, IClrRuntime clrRuntime)
            {
                return clrObject.Type.Name == "System.Windows.Forms.Button";
            }

            /// <inheritdoc />
            public DumpObject Extract(IClrObject clrObject, IClrRuntime clrRuntime)
            {
                var text = clrObject.GetStringField("text");
                return new ButtonDumpObject(clrObject.Address, clrObject.Type.Name, clrObject.Size, clrRuntime.Heap.GetGeneration(clrObject.Address), text);
            }
        }

        internal class ButtonDumpObject : DumpObject
        {
            public string Text { get; set; }
            /// <inheritdoc />
            public ButtonDumpObject(ulong address, string fullTypeName, ulong size, int gen, string text) : base(address, fullTypeName, size, gen)
            {
                Text = text;
            }
        }

        [Fact]
        public void Perform_Basic_Startup_Without_Failure()
        {
            // arrange
            var dumpFile = Scenario.WinForms.GetDumpFile();
            var options = new DefaultOptions
            {
                DumpFile = dumpFile.FullName,
                SettingsFile = "Settings/Mortician_Should.json"
            };
            var analyzer = new TestAnalyzer();

            // act
            var result = Program.DefaultExecution(options, container =>
            {
                container.ComposeParts(analyzer);
                container.ComposeExportedValue<IDumpObjectExtractor>(new ButtonExtractor());
                container.ComposeExportedValue<IAnalyzer>(analyzer);
                return container;
            });

            // assert
            result.Should().Be(0);
            analyzer.ButtonText.Should().Be("Hello World");
        }
    }
}