using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    public interface IObjectInspector
    {
        bool CanInspect(ClrObject clrObject, ClrRuntime clrRuntime);
        HeapObject Inspect(ClrObject clrObject, ClrRuntime clrRuntime);
    }
}