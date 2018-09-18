// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrRoot.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// Interface IClrRoot
    /// </summary>
    public interface IClrRoot
    {
        /// <summary>
        /// A GC Root also has a Kind, which says if it is a strong or weak root
        /// </summary>
        /// <value>The kind.</value>
        GcRootKind Kind { get; }

        /// <summary>
        /// The name of the root.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// The type of the object this root points to.  That is, ClrHeap.GetObjectType(ClrRoot.Object).
        /// </summary>
        /// <value>The type.</value>
        IClrType Type { get; }

        /// <summary>
        /// The object on the GC heap that this root keeps alive.
        /// </summary>
        /// <value>The object.</value>
        ulong Object { get; }

        /// <summary>
        /// The address of the root in the target process.
        /// </summary>
        /// <value>The address.</value>
        ulong Address { get; }

        /// <summary>
        /// If the root can be identified as belonging to a particular AppDomain this is that AppDomain.
        /// It an be null if there is no AppDomain associated with the root.
        /// </summary>
        /// <value>The application domain.</value>
        IClrAppDomain AppDomain { get; }

        /// <summary>
        /// If the root has a thread associated with it, this will return that thread.
        /// </summary>
        /// <value>The thread.</value>
        IClrThread Thread { get; }

        /// <summary>
        /// Returns true if Object is an "interior" pointer.  This means that the pointer may actually
        /// point inside an object instead of to the start of the object.
        /// </summary>
        /// <value><c>true</c> if this instance is interior; otherwise, <c>false</c>.</value>
        bool IsInterior { get; }

        /// <summary>
        /// Returns true if the root "pins" the object, preventing the GC from relocating it.
        /// </summary>
        /// <value><c>true</c> if this instance is pinned; otherwise, <c>false</c>.</value>
        bool IsPinned { get; }

        /// <summary>
        /// Unfortunately some versions of the APIs we consume do not give us perfect information.  If
        /// this property is true it means we used a heuristic to find the value, and it might not
        /// actually be considered a root by the GC.
        /// </summary>
        /// <value><c>true</c> if this instance is possible false positive; otherwise, <c>false</c>.</value>
        bool IsPossibleFalsePositive { get; }

        /// <summary>
        /// Returns the stack frame associated with this stack root.
        /// </summary>
        /// <value>The stack frame.</value>
        IClrStackFrame StackFrame { get; }

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        string ToString();
    }
}