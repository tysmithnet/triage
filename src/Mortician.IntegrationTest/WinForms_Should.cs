// ***********************************************************************
// Assembly         : Mortician.IntegrationTest
// Author           : @tysmithnet
// Created          : 09-25-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-03-2018
// ***********************************************************************
// <copyright file="WinForms_Should.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Mortician.Core;
using Mortician.Core.ClrMdAbstractions;
using Mortician.IntegrationTest.Scenarios;
using Testing.Common;
using Xunit;

namespace Mortician.IntegrationTest
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    ///     Class WinForms_Should.
    /// </summary>
    /// <seealso cref="Testing.Common.BaseTest" />
    public class WinForms_Should : BaseTest
    {
        /// <summary>
        ///     Class TestAnalyzer.
        /// </summary>
        /// <seealso cref="Mortician.Core.IAnalyzer" />
        [Export(typeof(IAnalyzer))]
        [Export]
        internal class TestAnalyzer : IAnalyzer
        {
            /// <summary>
            ///     Performs the analysis
            /// </summary>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A Task that when complete will signal the completion of the setup procedure</returns>
            /// <inheritdoc />
            public Task Process(CancellationToken cancellationToken)
            {
                if (ObjectRepo.Objects.First(x => x.FullTypeName.Contains("System.Windows.Forms.Button")) is
                    ButtonDumpObject button) ButtonText = button.Text;
                return Task.CompletedTask;
            }

            /// <summary>
            ///     Performs any necessary setup prior to processing
            /// </summary>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A Task that when complete will signal the completion of the setup procedure</returns>
            /// <inheritdoc />
            public Task Setup(CancellationToken cancellationToken) => Task.CompletedTask;

            /// <summary>
            ///     Gets or sets the application domain repo.
            /// </summary>
            /// <value>The application domain repo.</value>
            [Import]
            public IDumpAppDomainRepository AppDomainRepo { get; set; }

            /// <summary>
            ///     Gets or sets the button text.
            /// </summary>
            /// <value>The button text.</value>
            public string ButtonText { get; set; }

            /// <summary>
            ///     Gets or sets the dump information repo.
            /// </summary>
            /// <value>The dump information repo.</value>
            [Import]
            public IDumpInformationRepository DumpInfoRepo { get; set; }

            /// <summary>
            ///     Gets or sets the module repo.
            /// </summary>
            /// <value>The module repo.</value>
            [Import]
            public IDumpModuleRepository ModuleRepo { get; set; }

            /// <summary>
            ///     Gets or sets the object repo.
            /// </summary>
            /// <value>The object repo.</value>
            [Import]
            public IDumpObjectRepository ObjectRepo { get; set; }

            /// <summary>
            ///     Gets or sets the thread repo.
            /// </summary>
            /// <value>The thread repo.</value>
            [Import]
            public IDumpThreadRepository ThreadRepo { get; set; }

            /// <summary>
            ///     Gets or sets the type repo.
            /// </summary>
            /// <value>The type repo.</value>
            [Import]
            public IDumpTypeRepository TypeRepo { get; set; }
        }

        /// <summary>
        ///     Class ButtonExtractor.
        /// </summary>
        /// <seealso cref="Mortician.Core.IDumpObjectExtractor" />
        [Export(typeof(IDumpObjectExtractor))]
        internal class ButtonExtractor : IDumpObjectExtractor
        {
            /// <summary>
            ///     Determines whether this instance can extract from the provided object
            /// </summary>
            /// <param name="clrObject">The object to try to get values from</param>
            /// <param name="clrRuntime">The clr runtime being used</param>
            /// <returns><c>true</c> if this instance can extract from the object; otherwise, <c>false</c>.</returns>
            /// <inheritdoc />
            public bool CanExtract(IClrObject clrObject, IClrRuntime clrRuntime) =>
                clrObject.Type.Name == "System.Windows.Forms.Button";

            /// <summary>
            ///     Determines whether this instance can extract the specified address.
            /// </summary>
            /// <param name="address">The address.</param>
            /// <param name="clrRuntime">The color runtime.</param>
            /// <returns><c>true</c> if this instance can extract the specified address; otherwise, <c>false</c>.</returns>
            /// <inheritdoc />
            public bool CanExtract(ulong address, IClrRuntime clrRuntime)
            {
                var type = clrRuntime.Heap.GetObjectType(address);
                return type.Name == "System.Windows.Forms.Button";
            }

            /// <summary>
            ///     Extracts data from the provided object
            /// </summary>
            /// <param name="clrObject">The object.</param>
            /// <param name="clrRuntime">The runtime.</param>
            /// <returns>Extracted dump object</returns>
            /// <inheritdoc />
            public DumpObject Extract(IClrObject clrObject, IClrRuntime clrRuntime)
            {
                var text = clrObject.GetStringField("text");
                return new ButtonDumpObject(clrObject.Address, clrObject.Type.Name, clrObject.Size,
                    clrRuntime.Heap.GetGeneration(clrObject.Address), text);
            }

            /// <summary>
            ///     Extracts the specified address.
            /// </summary>
            /// <param name="address">The address.</param>
            /// <param name="clrRuntime">The color runtime.</param>
            /// <returns>DumpObject.</returns>
            /// <inheritdoc />
            public DumpObject Extract(ulong address, IClrRuntime clrRuntime)
            {
                var type = clrRuntime.Heap.GetObjectType(address);
                var textField = type.GetFieldByName("text");
                var text = (string) textField.GetValue(address);
                return new ButtonDumpObject(address, type.Name, type.GetSize(address),
                    clrRuntime.Heap.GetGeneration(address), text);
            }
        }

        /// <summary>
        ///     Class ButtonDumpObject.
        /// </summary>
        /// <seealso cref="Mortician.Core.DumpObject" />
        internal class ButtonDumpObject : DumpObject
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="ButtonDumpObject" /> class.
            /// </summary>
            /// <param name="address">The address.</param>
            /// <param name="fullTypeName">Full name of the type.</param>
            /// <param name="size">The size.</param>
            /// <param name="gen">The gen.</param>
            /// <param name="text">The text.</param>
            /// <inheritdoc />
            public ButtonDumpObject(ulong address, string fullTypeName, ulong size, int gen, string text) : base(
                address, fullTypeName, size, gen)
            {
                Text = text;
            }

            /// <summary>
            ///     Gets or sets the text.
            /// </summary>
            /// <value>The text.</value>
            public string Text { get; set; }
        }

        /// <summary>
        ///     Extracts the text from a button.
        /// </summary>
        [Fact]
        public void Extract_The_Text_From_A_Button()
        {
            // arrange
            var program = new Program();
            var dumpFile = Scenario.WinForms.GetDumpFile();
            var options = new Options
            {
                AdditionalTypes = new[]
                {
                    typeof(ButtonExtractor),
                    typeof(TestAnalyzer)
                },
                RunOptions = new RunOptions()
                {
                    DumpLocation = dumpFile.FullName
                },
                SettingsFile = "Settings/Mortician_Should.json"
            };

            // act
            CompositionContainer cc = null;
            var result = program.DefaultExecution(options, container => cc = container);
            var analyzer = cc.GetExportedValue<TestAnalyzer>();

            // assert
            result.Should().Be(0);
            analyzer.ButtonText.Should().Be("Hello World");
        }
    }
}