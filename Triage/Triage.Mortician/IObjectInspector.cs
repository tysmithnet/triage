﻿using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    public interface IObjectInspector
    {
        bool CanInspect(ClrObject clrObject, ClrRuntime clrRuntime);
        DumpObject Inspect(ClrObject clrObject, ClrRuntime clrRuntime);
    }
}