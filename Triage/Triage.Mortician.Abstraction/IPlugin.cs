using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Abstraction
{
    public interface IPlugin
    {
        Task Setup();
    }

    public interface IObjectInspector
    {
        bool CanInspect(ClrObject clrObject, ClrRuntime runtime, DataTarget dataTarget);
        IHeapObject Insepct(ClrObject clrObject, ClrRuntime runtime, DataTarget dataTarget);
    }

    public interface IDumpObjectRepository
    {
        IHeapObject Get(ulong address);           
    }
}
