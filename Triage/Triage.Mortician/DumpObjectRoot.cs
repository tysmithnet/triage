using System;

namespace Triage.Mortician
{
    public class DumpObjectRoot
    {
        public ulong Address { get; set; }
        public string Name { get; set; }
        public bool IsInteriorPointer { get; set; }
        public bool IsPinned { get; set; }
        public bool IsPossibleFalsePositive { get; set; }
        public bool IsStaticVariable { get; set; }
        public bool IsThreadStaticVariable { get; set; }
        public bool IsLocalVar { get; set; }
        public bool IsStrongHandle { get; set; }
        public bool IsWeakHandle { get; set; }
        public bool IsStrongPinningHandle { get; set; }
        public bool IsFinalizerQueue { get; set; }
        public bool IsAsyncIoPinning { get; set; }
        public DumpObject RootedObject { get; set; }

        public DumpStackFrame StackFrame =>
            throw new NotImplementedException("You still need to implement stack frame in object root");

        public DumpThread Thread { get; set; }
        public DumpAppDomain AppDomain { get; set; }
    }
}