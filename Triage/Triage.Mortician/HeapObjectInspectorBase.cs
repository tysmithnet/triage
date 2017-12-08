using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    public class HeapObjectInspectorBase : IHeapObjectExtractor
    {
        public virtual bool CanExtract(ClrObject clrObject, ClrRuntime clrRuntime)
        {
            return true;
        }

        public virtual HeapObject Extract(ClrObject clrObject, ClrRuntime clrRuntime)
        {
            var heapObject = new HeapObject(clrObject.Address, clrObject.Type.Name, clrObject.Size);
            return heapObject;
        }
    }
}