using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    internal class RepositoryFactory
    {
        public ILog Log = LogManager.GetLogger(typeof(RepositoryFactory));
        public CompositionContainer CompositionContainer { get; set; }
        public DataTarget DataTarget { get; set; }

        public RepositoryFactory(CompositionContainer compositionContainer, DataTarget dataTarget)
        {
            CompositionContainer = compositionContainer ?? throw new ArgumentNullException(nameof(compositionContainer));
            DataTarget = dataTarget ?? throw new ArgumentNullException(nameof(dataTarget));
        }

        public void RegisterRepositories()
        {
            var heapObjectExtractors = CompositionContainer.GetExportedValues<IDumpObjectExtractor>().ToList();

            var rt = DataTarget.ClrVersions.Single().CreateRuntime();
            var stopWatch = Stopwatch.StartNew();
            var dumpObjectRepository = new DumpObjectRepository(rt, heapObjectExtractors);
            Log.Trace(
                $"DumpObjectRepository created in {TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).ToString()}");

            var debuggerProxy = new DebuggerProxy(DataTarget.DebuggerInterface);
            stopWatch.Restart();
            var dumpThreadRepository = new DumpThreadRepository(rt, debuggerProxy, dumpObjectRepository);
            Log.Trace(
                $"DumpThreadRepository created in {TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).ToString()}");
              
            CompositionContainer.ComposeExportedValue<IDumpObjectRepository>(dumpObjectRepository);
            CompositionContainer.ComposeExportedValue<IDebuggerProxy>(debuggerProxy);
            CompositionContainer.ComposeExportedValue<IDumpThreadRepository>(dumpThreadRepository);
        }
    }
}
