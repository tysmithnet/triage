using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Web
{
    public interface IObjectInspector
    {
        bool CanInspect(ClrObject clrObject, ClrRuntime clrRuntime);
        DumpObject Inspect(ClrObject clrObject, ClrRuntime clrRuntime);
    }
}