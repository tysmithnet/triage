using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    public interface IHeapObjectExtractor
    {
        bool CanExtract(ClrObject clrObject, ClrRuntime clrRuntime);
        HeapObject Extract(ClrObject clrObject, ClrRuntime clrRuntime);
    }
}