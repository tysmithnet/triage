using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    [Export(typeof(IDumpObjectExtractor))]
    public class StringExtractor : IDumpObjectExtractor
    {
        public bool CanExtract(ClrObject clrObject, ClrRuntime clrRuntime)
        {
            return clrObject.Type?.Name == "System.String";
        }

        public DumpObject Extract(ClrObject clrObject, ClrRuntime clrRuntime)
        {   
            string value = (string)clrObject.Type.GetValue(clrObject.Address);
            var heapObject = new StringDumpObject(clrObject.Address, "System.String", clrObject.Size, value);
            return heapObject;
        }
    }
}