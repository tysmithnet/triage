using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    [Export(typeof(IDumpObjectExtractor))]
    public class DefaultObjectExtractor : IDumpObjectExtractor
    {
        public bool CanExtract(ClrObject clrObject, ClrRuntime clrRuntime)
        {
            return true;
        }

        public DumpObject Extract(ClrObject clrObject, ClrRuntime clrRuntime)
        {
            var address = clrObject.Address;
            var gen = clrRuntime.Heap.GetGeneration(address);
            var size = clrObject.Size;
            var name = clrObject.Type.Name;            
            var dumpObject = new DumpObject(address, name, size, gen);
            return dumpObject;
        }
    }
}