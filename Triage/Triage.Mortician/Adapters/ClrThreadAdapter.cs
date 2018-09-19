using System;
using System.Collections.Generic;
using System.Linq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrThreadAdapter : IClrThread
    {
        /// <inheritdoc />
        public ClrThreadAdapter(Microsoft.Diagnostics.Runtime.ClrThread thread)
        {
            Thread = thread ?? throw new ArgumentNullException(nameof(thread));
            BlockingObjects = thread.BlockingObjects.Select(Converter.Convert).ToList();
            CurrentException = Converter.Convert(thread.CurrentException);
            GcMode = Converter.Convert(thread.GcMode);
            Runtime = Converter.Convert(thread.Runtime);
            StackTrace = thread.StackTrace.Select(Converter.Convert).ToList();
        }

        internal Microsoft.Diagnostics.Runtime.ClrThread Thread;

        /// <inheritdoc />
        public IEnumerable<IClrRoot> EnumerateStackObjects() => Thread.EnumerateStackObjects().Select(Converter.Convert);

        /// <inheritdoc />
        public IEnumerable<IClrRoot> EnumerateStackObjects(bool includePossiblyDead) => Thread.EnumerateStackObjects(includePossiblyDead).Select(Converter.Convert);

        /// <inheritdoc />
        public IEnumerable<IClrStackFrame> EnumerateStackTrace() => Thread.EnumerateStackTrace().Select(Converter.Convert);

        /// <inheritdoc />
        public ulong Address => Thread.Address;

        /// <inheritdoc />
        public ulong AppDomain => Thread.AppDomain;

        /// <inheritdoc />
        public IList<IBlockingObject> BlockingObjects { get; }

        /// <inheritdoc />
        public IClrException CurrentException { get; }

        /// <inheritdoc />
        public GcMode GcMode { get; }

        /// <inheritdoc />
        public bool IsAborted => Thread.IsAborted;

        /// <inheritdoc />
        public bool IsAbortRequested => Thread.IsAbortRequested;

        /// <inheritdoc />
        public bool IsAlive => Thread.IsAlive;

        /// <inheritdoc />
        public bool IsBackground => Thread.IsBackground;

        /// <inheritdoc />
        public bool IsCoInitialized => Thread.IsCoInitialized;

        /// <inheritdoc />
        public bool IsDebuggerHelper => Thread.IsDebuggerHelper;

        /// <inheritdoc />
        public bool IsDebugSuspended => Thread.IsDebugSuspended;

        /// <inheritdoc />
        public bool IsFinalizer => Thread.IsFinalizer;

        /// <inheritdoc />
        public bool IsGC => Thread.IsGC;

        /// <inheritdoc />
        public bool IsGCSuspendPending => Thread.IsGCSuspendPending;

        /// <inheritdoc />
        public bool IsMTA => Thread.IsMTA;

        /// <inheritdoc />
        public bool IsShutdownHelper => Thread.IsShutdownHelper;

        /// <inheritdoc />
        public bool IsSTA => Thread.IsSTA;

        /// <inheritdoc />
        public bool IsSuspendingEE => Thread.IsSuspendingEE;

        /// <inheritdoc />
        public bool IsThreadpoolCompletionPort => Thread.IsThreadpoolCompletionPort;

        /// <inheritdoc />
        public bool IsThreadpoolGate => Thread.IsThreadpoolGate;

        /// <inheritdoc />
        public bool IsThreadpoolTimer => Thread.IsThreadpoolTimer;

        /// <inheritdoc />
        public bool IsThreadpoolWait => Thread.IsThreadpoolWait;

        /// <inheritdoc />
        public bool IsThreadpoolWorker => Thread.IsThreadpoolWorker;

        /// <inheritdoc />
        public bool IsUnstarted => Thread.IsUnstarted;

        /// <inheritdoc />
        public bool IsUserSuspended => Thread.IsUserSuspended;

        /// <inheritdoc />
        public uint LockCount => Thread.LockCount;

        /// <inheritdoc />
        public int ManagedThreadId => Thread.ManagedThreadId;

        /// <inheritdoc />
        public uint OSThreadId => Thread.OSThreadId;

        /// <inheritdoc />
        public IClrRuntime Runtime { get; }

        /// <inheritdoc />
        public ulong StackBase => Thread.StackBase;

        /// <inheritdoc />
        public ulong StackLimit => Thread.StackLimit;

        /// <inheritdoc />
        public IList<IClrStackFrame> StackTrace { get; }

        /// <inheritdoc />
        public ulong Teb => Thread.Teb;
    }
}