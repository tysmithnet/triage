using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrThreadAdapter : IClrThread
    {
        /// <inheritdoc />
        public ClrThreadAdapter(Microsoft.Diagnostics.Runtime.ClrThread thread)
        {
            _thread = thread ?? throw new ArgumentNullException(nameof(thread));
        }

        internal Microsoft.Diagnostics.Runtime.ClrThread _thread;

        /// <inheritdoc />
        public IEnumerable<IClrRoot> EnumerateStackObjects()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrRoot> EnumerateStackObjects(bool includePossiblyDead)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrStackFrame> EnumerateStackTrace()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong Address { get; }

        /// <inheritdoc />
        public ulong AppDomain { get; }

        /// <inheritdoc />
        public IList<IBlockingObject> BlockingObjects { get; }

        /// <inheritdoc />
        public IClrException CurrentException { get; }

        /// <inheritdoc />
        public GcMode GcMode { get; }

        /// <inheritdoc />
        public bool IsAborted { get; }

        /// <inheritdoc />
        public bool IsAbortRequested { get; }

        /// <inheritdoc />
        public bool IsAlive { get; }

        /// <inheritdoc />
        public bool IsBackground { get; }

        /// <inheritdoc />
        public bool IsCoInitialized { get; }

        /// <inheritdoc />
        public bool IsDebuggerHelper { get; }

        /// <inheritdoc />
        public bool IsDebugSuspended { get; }

        /// <inheritdoc />
        public bool IsFinalizer { get; }

        /// <inheritdoc />
        public bool IsGC { get; }

        /// <inheritdoc />
        public bool IsGCSuspendPending { get; }

        /// <inheritdoc />
        public bool IsMTA { get; }

        /// <inheritdoc />
        public bool IsShutdownHelper { get; }

        /// <inheritdoc />
        public bool IsSTA { get; }

        /// <inheritdoc />
        public bool IsSuspendingEE { get; }

        /// <inheritdoc />
        public bool IsThreadpoolCompletionPort { get; }

        /// <inheritdoc />
        public bool IsThreadpoolGate { get; }

        /// <inheritdoc />
        public bool IsThreadpoolTimer { get; }

        /// <inheritdoc />
        public bool IsThreadpoolWait { get; }

        /// <inheritdoc />
        public bool IsThreadpoolWorker { get; }

        /// <inheritdoc />
        public bool IsUnstarted { get; }

        /// <inheritdoc />
        public bool IsUserSuspended { get; }

        /// <inheritdoc />
        public uint LockCount { get; }

        /// <inheritdoc />
        public int ManagedThreadId { get; }

        /// <inheritdoc />
        public uint OSThreadId { get; }

        /// <inheritdoc />
        public IClrRuntime Runtime { get; }

        /// <inheritdoc />
        public ulong StackBase { get; }

        /// <inheritdoc />
        public ulong StackLimit { get; }

        /// <inheritdoc />
        public IList<IClrStackFrame> StackTrace { get; }

        /// <inheritdoc />
        public ulong Teb { get; }
    }
}