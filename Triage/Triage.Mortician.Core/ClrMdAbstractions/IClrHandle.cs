// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrHandle.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// Interface IClrHandle
    /// </summary>
    public interface IClrHandle
    {
        /// <summary>
        /// The address of the handle itself.  That is, *ulong == Object.
        /// </summary>
        /// <value>The address.</value>
        ulong Address { get; set; }

        /// <summary>
        /// The Object the handle roots.
        /// </summary>
        /// <value>The object.</value>
        ulong Object { get; set; }

        /// <summary>
        /// The the type of the Object.
        /// </summary>
        /// <value>The type.</value>
        IClrType Type { get; set; }

        /// <summary>
        /// Whether the handle is strong (roots the object) or not.
        /// </summary>
        /// <value><c>true</c> if this instance is strong; otherwise, <c>false</c>.</value>
        bool IsStrong { get; }

        /// <summary>
        /// Whether or not the handle pins the object (doesn't allow the GC to
        /// relocate it) or not.
        /// </summary>
        /// <value><c>true</c> if this instance is pinned; otherwise, <c>false</c>.</value>
        bool IsPinned { get; }

        /// <summary>
        /// Gets the type of handle.
        /// </summary>
        /// <value>The type of the handle.</value>
        HandleType HandleType { get; set; }

        /// <summary>
        /// If this handle is a RefCount handle, this returns the reference count.
        /// RefCount handles with a RefCount &gt; 0 are strong.
        /// NOTE: v2 CLR CANNOT determine the RefCount.  We always set the RefCount
        /// to 1 in a v2 query since a strong RefCount handle is the common case.
        /// </summary>
        /// <value>The reference count.</value>
        uint RefCount { get; set; }

        /// <summary>
        /// Set only if the handle type is a DependentHandle.  Dependent handles add
        /// an extra edge to the object graph.  Meaning, this.Object now roots the
        /// dependent target, but only if this.Object is alive itself.
        /// NOTE: CLRs prior to v4.5 cannot obtain the dependent target.  This field will
        /// be 0 for any CLR prior to v4.5.
        /// </summary>
        /// <value>The dependent target.</value>
        ulong DependentTarget { get; set; }

        /// <summary>
        /// The type of the dependent target, if non 0.
        /// </summary>
        /// <value>The type of the dependent.</value>
        IClrType DependentType { get; set; }

        /// <summary>
        /// The AppDomain the handle resides in.
        /// </summary>
        /// <value>The application domain.</value>
        IClrAppDomain AppDomain { get; set; }

        /// <summary>
        /// ToString override.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        string ToString();
    }
}