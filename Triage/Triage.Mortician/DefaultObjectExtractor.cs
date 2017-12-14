using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    /// <summary>
    ///     Default object extractor
    ///     Used when all else fails
    /// </summary>
    public class DefaultObjectExtractor
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