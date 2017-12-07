using System;
using System.Collections.Generic;
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

    public abstract class HeapObject
    {
        
    }

    public interface IObjectInspector
    {
        bool CanInspect(ClrObject clrobject);
        HeapObject Insepct(ClrObject clrObject);
    }
}
