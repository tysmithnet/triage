using System;
using System.Diagnostics;

namespace Triage.Mortician.Test.IntegrationTests.Scenarios
{
    public static class ExtensionMethods
    {
        public static bool Is32BitProcess(this Process proc)
        {
            var is32BitProcess = IntPtr.Size == 4;
            // if machine is 32 bit then all procs are 32 bit
            if (NativeMethods.IsWow64Process(NativeMethods.GetCurrentProcess(), out var fIsRunningUnderWow64)
                && fIsRunningUnderWow64)
                if (NativeMethods.IsWow64Process(proc.Handle, out fIsRunningUnderWow64)
                    && fIsRunningUnderWow64)
                    is32BitProcess = true;
                else
                    is32BitProcess = false;
            return is32BitProcess;
        }
    }
}