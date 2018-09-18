namespace Triage.Mortician.Core.ClrMdAbstractions
{
    public interface IClrHandle
    {
        /// <summary>
        /// The address of the handle itself.  That is, *ulong == Object.
        /// </summary>
        ulong Address { get; set; }

        /// <summary>
        /// The Object the handle roots.
        /// </summary>
        ulong Object { get; set; }

        /// <summary>
        /// The the type of the Object.
        /// </summary>
        IClrType Type { get; set; }

        /// <summary>
        /// Whether the handle is strong (roots the object) or not.
        /// </summary>
        bool IsStrong { get; }

        /// <summary>
        /// Whether or not the handle pins the object (doesn't allow the GC to
        /// relocate it) or not.
        /// </summary>
        bool IsPinned { get; }

        /// <summary>
        /// Gets the type of handle.
        /// </summary>
        HandleType HandleType { get; set; }

        /// <summary>
        /// If this handle is a RefCount handle, this returns the reference count.
        /// RefCount handles with a RefCount > 0 are strong.
        /// NOTE: v2 CLR CANNOT determine the RefCount.  We always set the RefCount
        ///       to 1 in a v2 query since a strong RefCount handle is the common case.
        /// </summary>
        uint RefCount { get; set; }

        /// <summary>
        /// Set only if the handle type is a DependentHandle.  Dependent handles add
        /// an extra edge to the object graph.  Meaning, this.Object now roots the
        /// dependent target, but only if this.Object is alive itself.
        /// NOTE: CLRs prior to v4.5 cannot obtain the dependent target.  This field will
        ///       be 0 for any CLR prior to v4.5.
        /// </summary>
        ulong DependentTarget { get; set; }

        /// <summary>
        /// The type of the dependent target, if non 0.
        /// </summary>
        IClrType DependentType { get; set; }

        /// <summary>
        /// The AppDomain the handle resides in.
        /// </summary>
        IClrAppDomain AppDomain { get; set; }

        /// <summary>
        /// ToString override.
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}