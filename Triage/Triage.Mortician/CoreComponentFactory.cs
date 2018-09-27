using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician
{
    internal class CoreComponentFactory
    {
        public CompositionContainer CompositionContainer { get; set; }
        public FileInfo DumpFile { get; set; }
        public IDataTarget DataTarget { get; set; }
        public IClrRuntime Runtime { get; set; }
        public IConverter Converter { get; set; } = new Converter();

        /// <inheritdoc />
        public CoreComponentFactory(CompositionContainer compositionContainer, FileInfo dumpFile)
        {
            CompositionContainer =
                compositionContainer ?? throw new ArgumentNullException(nameof(compositionContainer));
            DumpFile = dumpFile ?? throw new ArgumentNullException(nameof(dumpFile));
            DataTarget = Converter.Convert(Microsoft.Diagnostics.Runtime.DataTarget.LoadCrashDump(dumpFile.FullName));
            Runtime = DataTarget.ClrVersions.Single().CreateRuntime();
        }

        public void Setup()
        {
            CreateObjects();
        }

        private void CreateObjects()
        {
            foreach (var enumerateObject in Runtime.Heap.EnumerateObjects())
            {
                
            }
        }

        public void RegisterRepositories(DefaultOptions options)
        {
            
        }
    }
}