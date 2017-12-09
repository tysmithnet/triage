using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Abstraction
{
    public interface IObjectInspector
    {
        bool CanInspect(ClrObject clrObject, ClrRuntime runtime, DataTarget dataTarget);
        IDumpObject Insepct(ClrObject clrObject, ClrRuntime runtime, DataTarget dataTarget);
    }
}