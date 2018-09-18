namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// The type of GCRoot that a ClrRoot represnts.
    /// </summary>
    public enum GCRootKind
    {
        /// <summary>
        /// The root is a static variable.
        /// </summary>
        StaticVar,

        /// <summary>
        /// The root is a thread static.
        /// </summary>
        ThreadStaticVar,

        /// <summary>
        /// The root is a local variable (or compiler generated temporary variable).
        /// </summary>
        LocalVar,

        /// <summary>
        /// The root is a strong handle.
        /// </summary>
        Strong,

        /// <summary>
        /// The root is a weak handle.
        /// </summary>
        Weak,

        /// <summary>
        /// The root is a strong pinning handle.
        /// </summary>
        Pinning,

        /// <summary>
        /// The root comes from the finalizer queue.
        /// </summary>
        Finalizer,

        /// <summary>
        /// The root is an async IO (strong) pinning handle.
        /// </summary>
        AsyncPinning,

        /// <summary>
        /// The max value of this enum.
        /// </summary>
        Max = AsyncPinning
    }

    public interface IClrRoot
    {
        /// <summary>
        /// A GC Root also has a Kind, which says if it is a strong or weak root
        /// </summary>
        GCRootKind Kind { get; }

        /// <summary>
        /// The name of the root. 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The type of the object this root points to.  That is, ClrHeap.GetObjectType(ClrRoot.Object).
        /// </summary>
        IClrType Type { get; }

        /// <summary>
        /// The object on the GC heap that this root keeps alive.
        /// </summary>
        ulong Object { get; }

        /// <summary>
        /// The address of the root in the target process.
        /// </summary>
        ulong Address { get; }

        /// <summary>
        /// If the root can be identified as belonging to a particular AppDomain this is that AppDomain.
        /// It an be null if there is no AppDomain associated with the root.  
        /// </summary>
        IClrAppDomain AppDomain { get; }

        /// <summary>
        /// If the root has a thread associated with it, this will return that thread.
        /// </summary>
        IClrThread Thread { get; }

        /// <summary>
        /// Returns true if Object is an "interior" pointer.  This means that the pointer may actually
        /// point inside an object instead of to the start of the object.
        /// </summary>
        bool IsInterior { get; }

        /// <summary>
        /// Returns true if the root "pins" the object, preventing the GC from relocating it.
        /// </summary>
        bool IsPinned { get; }

        /// <summary>
        /// Unfortunately some versions of the APIs we consume do not give us perfect information.  If
        /// this property is true it means we used a heuristic to find the value, and it might not
        /// actually be considered a root by the GC.
        /// </summary>
        bool IsPossibleFalsePositive { get; }

        /// <summary>
        /// Returns the stack frame associated with this stack root.
        /// </summary>
        IClrStackFrame StackFrame { get; }

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        string ToString();
    }
}