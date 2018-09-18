// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrThread.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IClrThread
    /// </summary>
    public interface IClrThread
    {
        /// <summary>
        ///     Enumerates the GC references (objects) on the stack.  This is equivalent to
        ///     EnumerateStackObjects(true).
        /// </summary>
        /// <returns>An enumeration of GC references on the stack as the GC sees them.</returns>
        IEnumerable<IClrRoot> EnumerateStackObjects();

        /// <summary>
        ///     Enumerates the GC references (objects) on the stack.
        /// </summary>
        /// <param name="includePossiblyDead">
        ///     Include all objects found on the stack.  Passing
        ///     false attempts to replicate the behavior of the GC, reporting only live objects.
        /// </param>
        /// <returns>An enumeration of GC references on the stack as the GC sees them.</returns>
        IEnumerable<IClrRoot> EnumerateStackObjects(bool includePossiblyDead);

        /// <summary>
        ///     Enumerates a stack trace for a given thread.  Note this method may loop infinitely in the case of
        ///     stack corruption or other stack unwind issues which can happen in practice.  When enumerating frames
        ///     out of this method you should be careful to either set a maximum loop count, or to ensure the stack
        ///     unwind is making progress by ensuring that ClrStackFrame.StackPointer is making progress (though it
        ///     is expected that sometimes two frames may return the same StackPointer in some corner cases).
        /// </summary>
        /// <returns>An enumeration of stack frames.</returns>
        IEnumerable<IClrStackFrame> EnumerateStackTrace();

        /// <summary>
        ///     The address of the underlying datastructure which makes up the Thread object.  This
        ///     serves as a unique identifier.
        /// </summary>
        /// <value>The address.</value>
        ulong Address { get; }

        /// <summary>
        ///     The AppDomain the thread is running in.
        /// </summary>
        /// <value>The application domain.</value>
        ulong AppDomain { get; }

        /// <summary>
        ///     Returns the object this thread is blocked waiting on, or null if the thread is not blocked.
        /// </summary>
        /// <value>The blocking objects.</value>
        IList<IBlockingObject> BlockingObjects { get; }

        /// <summary>
        ///     Returns the exception currently on the thread.  Note that this field may be null.  Also note
        ///     that this is basically the "last thrown exception", and may be stale...meaning the thread could
        ///     be done processing the exception but a crash dump was taken before the current exception was
        ///     cleared off the field.
        /// </summary>
        /// <value>The current exception.</value>
        IClrException CurrentException { get; }

        /// <summary>
        ///     The suspension state of the thread according to the runtime.
        /// </summary>
        /// <value>The gc mode.</value>
        GcMode GcMode { get; }

        /// <summary>
        ///     Returns true if this thread was aborted.
        /// </summary>
        /// <value><c>true</c> if this instance is aborted; otherwise, <c>false</c>.</value>
        bool IsAborted { get; }

        /// <summary>
        ///     Returns true if an abort was requested for this thread (such as Thread.Abort, or AppDomain unload).
        /// </summary>
        /// <value><c>true</c> if this instance is abort requested; otherwise, <c>false</c>.</value>
        bool IsAbortRequested { get; }

        /// <summary>
        ///     Returns true if the thread is alive in the process, false if this thread was recently terminated.
        /// </summary>
        /// <value><c>true</c> if this instance is alive; otherwise, <c>false</c>.</value>
        bool IsAlive { get; }

        /// <summary>
        ///     Returns true if this thread is a background thread.  (That is, if the thread does not keep the
        ///     managed execution environment alive and running.)
        /// </summary>
        /// <value><c>true</c> if this instance is background; otherwise, <c>false</c>.</value>
        bool IsBackground { get; }

        /// <summary>
        ///     Returns true if the Clr runtime called CoIntialize for this thread.
        /// </summary>
        /// <value><c>true</c> if this instance is co initialized; otherwise, <c>false</c>.</value>
        bool IsCoInitialized { get; }

        /// <summary>
        ///     Returns if this thread is the debugger helper thread.
        /// </summary>
        /// <value><c>true</c> if this instance is debugger helper; otherwise, <c>false</c>.</value>
        bool IsDebuggerHelper { get; }

        /// <summary>
        ///     Returns true if the debugger has suspended the thread.
        /// </summary>
        /// <value><c>true</c> if this instance is debug suspended; otherwise, <c>false</c>.</value>
        bool IsDebugSuspended { get; }

        /// <summary>
        ///     Returns true if this is the finalizer thread.
        /// </summary>
        /// <value><c>true</c> if this instance is finalizer; otherwise, <c>false</c>.</value>
        bool IsFinalizer { get; }

        /// <summary>
        ///     Returns if this thread is a GC thread.  If the runtime is using a server GC, then there will be
        ///     dedicated GC threads, which this will indicate.  For a runtime using the workstation GC, this flag
        ///     will only be true for a thread which is currently running a GC (and the background GC thread).
        /// </summary>
        /// <value><c>true</c> if this instance is gc; otherwise, <c>false</c>.</value>
        bool IsGC { get; }

        /// <summary>
        ///     Returns true if the GC is attempting to suspend this thread.
        /// </summary>
        /// <value><c>true</c> if this instance is gc suspend pending; otherwise, <c>false</c>.</value>
        bool IsGCSuspendPending { get; }

        /// <summary>
        ///     Returns true if the thread is a COM multithreaded apartment.
        /// </summary>
        /// <value><c>true</c> if this instance is MTA; otherwise, <c>false</c>.</value>
        bool IsMTA { get; }

        /// <summary>
        ///     Returns true if this thread is currently the thread shutting down the runtime.
        /// </summary>
        /// <value><c>true</c> if this instance is shutdown helper; otherwise, <c>false</c>.</value>
        bool IsShutdownHelper { get; }

        /// <summary>
        ///     Returns true if this thread is in a COM single threaded apartment.
        /// </summary>
        /// <value><c>true</c> if this instance is sta; otherwise, <c>false</c>.</value>
        bool IsSTA { get; }

        /// <summary>
        ///     Returns if this thread currently suspending the runtime.
        /// </summary>
        /// <value><c>true</c> if this instance is suspending ee; otherwise, <c>false</c>.</value>
        bool IsSuspendingEE { get; }

        /// <summary>
        ///     Returns true if this thread is a threadpool IO completion port.
        /// </summary>
        /// <value><c>true</c> if this instance is threadpool completion port; otherwise, <c>false</c>.</value>
        bool IsThreadpoolCompletionPort { get; }

        /// <summary>
        ///     Returns true if this is the threadpool gate thread.
        /// </summary>
        /// <value><c>true</c> if this instance is threadpool gate; otherwise, <c>false</c>.</value>
        bool IsThreadpoolGate { get; }

        /// <summary>
        ///     Returns true if this thread is a threadpool timer thread.
        /// </summary>
        /// <value><c>true</c> if this instance is threadpool timer; otherwise, <c>false</c>.</value>
        bool IsThreadpoolTimer { get; }

        /// <summary>
        ///     Returns true if this is a threadpool wait thread.
        /// </summary>
        /// <value><c>true</c> if this instance is threadpool wait; otherwise, <c>false</c>.</value>
        bool IsThreadpoolWait { get; }

        /// <summary>
        ///     Returns true if this is a threadpool worker thread.
        /// </summary>
        /// <value><c>true</c> if this instance is threadpool worker; otherwise, <c>false</c>.</value>
        bool IsThreadpoolWorker { get; }

        /// <summary>
        ///     Returns true if this thread was created, but not started.
        /// </summary>
        /// <value><c>true</c> if this instance is unstarted; otherwise, <c>false</c>.</value>
        bool IsUnstarted { get; }

        /// <summary>
        ///     Returns true if the user has suspended the thread (using Thread.Suspend).
        /// </summary>
        /// <value><c>true</c> if this instance is user suspended; otherwise, <c>false</c>.</value>
        bool IsUserSuspended { get; }

        /// <summary>
        ///     The number of managed locks (Monitors) the thread has currently entered but not left.
        ///     This will be highly inconsistent unless the process is stopped.
        /// </summary>
        /// <value>The lock count.</value>
        uint LockCount { get; }

        /// <summary>
        ///     The managed thread ID (this is equivalent to System.Threading.Thread.ManagedThreadId
        ///     in the target process).
        /// </summary>
        /// <value>The managed thread identifier.</value>
        int ManagedThreadId { get; }

        /// <summary>
        ///     The OS thread id for the thread.
        /// </summary>
        /// <value>The os thread identifier.</value>
        uint OSThreadId { get; }

        /// <summary>
        ///     Gets the runtime associated with this thread.
        /// </summary>
        /// <value>The runtime.</value>
        IClrRuntime Runtime { get; }

        /// <summary>
        ///     The base of the stack for this thread, or 0 if the value could not be obtained.
        /// </summary>
        /// <value>The stack base.</value>
        ulong StackBase { get; }

        /// <summary>
        ///     The limit of the stack for this thread, or 0 if the value could not be obtained.
        /// </summary>
        /// <value>The stack limit.</value>
        ulong StackLimit { get; }

        /// <summary>
        ///     Returns the managed stack trace of the thread.  Note that this property may return incomplete
        ///     data in the case of a bad stack unwind or if there is a very large number of methods on the stack.
        ///     (This is usually caused by a stack overflow on the target thread, stack corruption which leads to
        ///     a bad stack unwind, or other inconsistent state in the target debuggee.)
        ///     Note: This property uses a heuristic to attempt to detect bad unwinds to stop enumerating
        ///     frames by inspecting the stack pointer and instruction pointer of each frame to ensure the stack
        ///     walk is "making progress".  Additionally we cap the number of frames returned by this method
        ///     as another safegaurd.  This means we may not have all frames even if the stack walk was making
        ///     progress.
        ///     If you want to ensure that you receive an un-clipped stack trace, you should use EnumerateStackTrace
        ///     instead of this property, and be sure to handle the case of repeating stack frames.
        /// </summary>
        /// <value>The stack trace.</value>
        IList<IClrStackFrame> StackTrace { get; }

        /// <summary>
        ///     The TEB (thread execution block) address in the process.
        /// </summary>
        /// <value>The teb.</value>
        ulong Teb { get; }
    }
}