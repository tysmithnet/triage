using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    [Export(typeof(IHeapObjectExtractor))]
    public class StringExtractor : IHeapObjectExtractor
    {
        public bool CanExtract(ClrObject clrObject, ClrRuntime clrRuntime)
        {
            return clrObject.Type?.Name == "System.String";
        }

        public HeapObject Extract(ClrObject clrObject, ClrRuntime clrRuntime)
        {
            string value = (string)clrObject.Type.GetValue(clrObject.Address);
            var heapObject = new StringHeapObject(clrObject.Address, "System.String", value);
            return heapObject;
        }
    }
}