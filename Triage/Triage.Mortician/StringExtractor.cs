using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
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
            var chars = value.ToCharArray()
                .Take(Convert.ToInt32(ConfigurationManager.AppSettings["string_value_preview_length"])).ToArray();
            var preview = new string(chars);
            var heapObject = new StringDumpObject(clrObject.Address, "System.String", clrObject.Size, value, clrRuntime.Heap.GetGeneration(clrObject.Address));

            return heapObject;
        }
    }
}