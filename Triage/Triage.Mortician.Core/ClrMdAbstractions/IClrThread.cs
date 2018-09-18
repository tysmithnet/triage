using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    public interface IClrThread
    {
        /// <summary>
        /// Gets the runtime associated with this thread.
        /// </summary>
        IClrRuntime Runtime { get; }

        /// <summary>
        /// The suspension state of the thread according to the runtime.
        /// </summary>
        GcMode GcMode { get; }

        /// <summary>
        /// Returns true if this is the finalizer thread.
        /// </summary>
        bool IsFinalizer { get; }

        /// <summary>
        /// The address of the underlying datastructure which makes up the Thread object.  This
        /// serves as a unique identifier.
        /// </summary>
        ulong Address { get; }

        /// <summary>
        /// Returns true if the thread is alive in the process, false if this thread was recently terminated.
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// The OS thread id for the thread.
        /// </summary>
        uint OSThreadId { get; }

        /// <summary>
        /// The managed thread ID (this is equivalent to System.Threading.Thread.ManagedThreadId
        /// in the target process).
        /// </summary>
        int ManagedThreadId { get; }

        /// <summary>
        /// The AppDomain the thread is running in.
        /// </summary>
        ulong AppDomain { get; }

        /// <summary>
        /// The number of managed locks (Monitors) the thread has currently entered but not left.
        /// This will be highly inconsistent unless the process is stopped.
        /// </summary>
        uint LockCount { get; }

        /// <summary>
        /// The TEB (thread execution block) address in the process.
        /// </summary>
        ulong Teb { get; }

        /// <summary>
        /// The base of the stack for this thread, or 0 if the value could not be obtained.
        /// </summary>
        ulong StackBase { get; }

        /// <summary>
        /// The limit of the stack for this thread, or 0 if the value could not be obtained.
        /// </summary>
        ulong StackLimit { get; }

        /// <summary>
        /// Returns the managed stack trace of the thread.  Note that this property may return incomplete
        /// data in the case of a bad stack unwind or if there is a very large number of methods on the stack.
        /// (This is usually caused by a stack overflow on the target thread, stack corruption which leads to
        /// a bad stack unwind, or other inconsistent state in the target debuggee.)
        /// 
        /// Note: This property uses a heuristic to attempt to detect bad unwinds to stop enumerating
        /// frames by inspecting the stack pointer and instruction pointer of each frame to ensure the stack
        /// walk is "making progress".  Additionally we cap the number of frames returned by this method
        /// as another safegaurd.  This means we may not have all frames even if the stack walk was making
        /// progress.
        /// 
        /// If you want to ensure that you receive an un-clipped stack trace, you should use EnumerateStackTrace
        /// instead of this property, and be sure to handle the case of repeating stack frames.
        /// </summary>
        IList<IClrStackFrame> StackTrace { get; }

        /// <summary>
        /// Returns the exception currently on the thread.  Note that this field may be null.  Also note
        /// that this is basically the "last thrown exception", and may be stale...meaning the thread could
        /// be done processing the exception but a crash dump was taken before the current exception was
        /// cleared off the field.
        /// </summary>
        IClrException CurrentException { get; }

        /// <summary>
        /// Returns if this thread is a GC thread.  If the runtime is using a server GC, then there will be
        /// dedicated GC threads, which this will indicate.  For a runtime using the workstation GC, this flag
        /// will only be true for a thread which is currently running a GC (and the background GC thread).
        /// </summary>
        bool IsGC { get; }

        /// <summary>
        /// Returns if this thread is the debugger helper thread.
        /// </summary>
        bool IsDebuggerHelper { get; }

        /// <summary>
        /// Returns true if this thread is a threadpool timer thread.
        /// </summary>
        bool IsThreadpoolTimer { get; }

        /// <summary>
        /// Returns true if this thread is a threadpool IO completion port.
        /// </summary>
        bool IsThreadpoolCompletionPort { get; }

        /// <summary>
        /// Returns true if this is a threadpool worker thread.
        /// </summary>
        bool IsThreadpoolWorker { get; }

        /// <summary>
        /// Returns true if this is a threadpool wait thread.
        /// </summary>
        bool IsThreadpoolWait { get; }

        /// <summary>
        /// Returns true if this is the threadpool gate thread.
        /// </summary>
        bool IsThreadpoolGate { get; }

        /// <summary>
        /// Returns if this thread currently suspending the runtime.
        /// </summary>
        bool IsSuspendingEE { get; }

        /// <summary>
        /// Returns true if this thread is currently the thread shutting down the runtime.
        /// </summary>
        bool IsShutdownHelper { get; }

        /// <summary>
        /// Returns true if an abort was requested for this thread (such as Thread.Abort, or AppDomain unload).
        /// </summary>
        bool IsAbortRequested { get; }

        /// <summary>
        /// Returns true if this thread was aborted.
        /// </summary>
        bool IsAborted { get; }

        /// <summary>
        /// Returns true if the GC is attempting to suspend this thread.
        /// </summary>
        bool IsGCSuspendPending { get; }

        /// <summary>
        /// Returns true if the user has suspended the thread (using Thread.Suspend).
        /// </summary>
        bool IsUserSuspended { get; }

        /// <summary>
        /// Returns true if the debugger has suspended the thread.
        /// </summary>
        bool IsDebugSuspended { get; }

        /// <summary>
        /// Returns true if this thread is a background thread.  (That is, if the thread does not keep the
        /// managed execution environment alive and running.)
        /// </summary>
        bool IsBackground { get; }

        /// <summary>
        /// Returns true if this thread was created, but not started.
        /// </summary>
        bool IsUnstarted { get; }

        /// <summary>
        /// Returns true if the Clr runtime called CoIntialize for this thread.
        /// </summary>
        bool IsCoInitialized { get; }

        /// <summary>
        /// Returns true if this thread is in a COM single threaded apartment.
        /// </summary>
        bool IsSTA { get; }

        /// <summary>
        /// Returns true if the thread is a COM multithreaded apartment.
        /// </summary>
        bool IsMTA { get; }

        /// <summary>
        /// Returns the object this thread is blocked waiting on, or null if the thread is not blocked.
        /// </summary>
        IList<IBlockingObject> BlockingObjects { get; }

        /// <summary>
        /// Enumerates the GC references (objects) on the stack.  This is equivalent to
        /// EnumerateStackObjects(true).
        /// </summary>
        /// <returns>An enumeration of GC references on the stack as the GC sees them.</returns>
        IEnumerable<IClrRoot> EnumerateStackObjects();

        /// <summary>
        /// Enumerates the GC references (objects) on the stack.
        /// </summary>
        /// <param name="includePossiblyDead">Include all objects found on the stack.  Passing
        /// false attempts to replicate the behavior of the GC, reporting only live objects.</param>
        /// <returns>An enumeration of GC references on the stack as the GC sees them.</returns>
        IEnumerable<IClrRoot> EnumerateStackObjects(bool includePossiblyDead);

        /// <summary>
        /// Enumerates a stack trace for a given thread.  Note this method may loop infinitely in the case of
        /// stack corruption or other stack unwind issues which can happen in practice.  When enumerating frames
        /// out of this method you should be careful to either set a maximum loop count, or to ensure the stack
        /// unwind is making progress by ensuring that ClrStackFrame.StackPointer is making progress (though it
        /// is expected that sometimes two frames may return the same StackPointer in some corner cases).
        /// </summary>
        /// <returns>An enumeration of stack frames.</returns>
        IEnumerable<IClrStackFrame> EnumerateStackTrace();
    }
}