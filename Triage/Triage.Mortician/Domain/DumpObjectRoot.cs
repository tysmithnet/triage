using System;

namespace Triage.Mortician.Domain
{
    /// <summary>
    ///     An object that represents a GC root discovered in the memory dump
    ///     A GC root is a reference like entity that keeps an object alive in memory
    /// </summary>
    public class DumpObjectRoot
    {
        /// <summary>
        ///     Gets or sets the address of this object root
        /// </summary>
        /// <value>
        ///     The address.
        /// </value>
        public ulong Address { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the name of this object root
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this object root points not to the
        ///     object header, but to some location inside the object
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is interior pointer; otherwise, <c>false</c>.
        /// </value>
        public bool IsInteriorPointer { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the object being kept in memory is
        ///     preventing from being relocated during the compaction phase of GC
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is pinned; otherwise, <c>false</c>.
        /// </value>
        public bool IsPinned { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is possible false positive.
        ///     This comes from the CLRMd engine that uses heuristics to determine if the object
        ///     being kept alive can in fact be garbage collected
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is possible false positive; otherwise, <c>false</c>.
        /// </value>
        public bool IsPossibleFalsePositive { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this object root is a static variable
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is static variable; otherwise, <c>false</c>.
        /// </value>
        public bool IsStaticVariable { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this object root is a thread static variable
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is thread static variable; otherwise, <c>false</c>.
        /// </value>
        public bool IsThreadStaticVariable { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this object root is a local variable in a method
        ///     that is still executing
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is local variable; otherwise, <c>false</c>.
        /// </value>
        public bool IsLocalVar { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this gc root is a strong reference
        ///     Strong references prevent objects from being garbage collected
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is strong handle; otherwise, <c>false</c>.
        /// </value>
        public bool IsStrongHandle { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this gc root is a weak reference
        ///     Weak references do not prevent GC, and are commonly found in caching structures
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is weak handle; otherwise, <c>false</c>.
        /// </value>
        public bool IsWeakHandle { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this object root is strong pinning handle.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is strong pinning handle; otherwise, <c>false</c>.
        /// </value>
        public bool IsStrongPinningHandle { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this object root is the finalizer queue
        ///     waiting to finalize it
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is finalizer queue; otherwise, <c>false</c>.
        /// </value>
        public bool IsFinalizerQueue { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this object root is pointing to an area of memory
        ///     used for overlapped io
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is asynchronous io pinning; otherwise, <c>false</c>.
        /// </value>
        public bool IsAsyncIoPinning { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the managed to which this root points if possible, null otherwise
        /// </summary>
        /// <value>
        ///     The rooted object.
        /// </value>
        public DumpObject RootedObject { get; protected internal set; }

        /// <summary>
        ///     Gets the stack frame that is keeping where this root can be found
        /// </summary>
        /// <value>
        ///     The stack frame.
        /// </value>
        /// <exception cref="NotImplementedException">You still need to implement stack frame in object root</exception>
        public DumpStackFrame StackFrame =>
            throw new NotImplementedException("You still need to implement stack frame in object root");

        /// <summary>
        ///     Gets or sets the thread that this object root belongs to if possible, null otherwise
        /// </summary>
        /// <value>
        ///     The thread.
        /// </value>
        public DumpThread Thread { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the app domain where this root exists if it is associated with an app domain, null otherwise
        /// </summary>
        /// <value>
        ///     The application domain.
        /// </value>
        public DumpAppDomain AppDomain { get; protected internal set; }
    }
}