using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Web
{
    public interface IDumpObjectExtractor
    {
        bool CanExtract(ClrObject clrObject, ClrRuntime clrRuntime);
        DumpObject Extract(ClrObject clrObject, ClrRuntime clrRuntime);
    }
}