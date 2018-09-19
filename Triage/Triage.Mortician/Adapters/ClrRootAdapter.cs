// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrRootAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ClrRootAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrRoot" />
    internal class ClrRootAdapter : IClrRoot
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrRootAdapter" /> class.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <exception cref="ArgumentNullException">root</exception>
        /// <inheritdoc />
        public ClrRootAdapter(ClrRoot root)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
            AppDomain = Converter.Convert(root.AppDomain);
            Kind = Converter.Convert(root.Kind);
            StackFrame = Converter.Convert(root.StackFrame);
            Thread = Converter.Convert(root.Thread);
            Type = Converter.Convert(root.Type);
        }

        /// <summary>
        ///     The root
        /// </summary>
        internal ClrRoot Root;

        /// <summary>
        ///     The address of the root in the target process.
        /// </summary>
        /// <value>The address.</value>
        /// <inheritdoc />
        public ulong Address => Root.Address;

        /// <summary>
        ///     If the root can be identified as belonging to a particular AppDomain this is that AppDomain.
        ///     It an be null if there is no AppDomain associated with the root.
        /// </summary>
        /// <value>The application domain.</value>
        /// <inheritdoc />
        public IClrAppDomain AppDomain { get; }

        /// <summary>
        ///     Returns true if Object is an "interior" pointer.  This means that the pointer may actually
        ///     point inside an object instead of to the start of the object.
        /// </summary>
        /// <value><c>true</c> if this instance is interior; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsInterior => Root.IsInterior;

        /// <summary>
        ///     Returns true if the root "pins" the object, preventing the GC from relocating it.
        /// </summary>
        /// <value><c>true</c> if this instance is pinned; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPinned => Root.IsPinned;

        /// <summary>
        ///     Unfortunately some versions of the APIs we consume do not give us perfect information.  If
        ///     this property is true it means we used a heuristic to find the value, and it might not
        ///     actually be considered a root by the GC.
        /// </summary>
        /// <value><c>true</c> if this instance is possible false positive; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPossibleFalsePositive => Root.IsPossibleFalsePositive;

        /// <summary>
        ///     A GC Root also has a Kind, which says if it is a strong or weak root
        /// </summary>
        /// <value>The kind.</value>
        /// <inheritdoc />
        public GcRootKind Kind { get; }

        /// <summary>
        ///     The name of the root.
        /// </summary>
        /// <value>The name.</value>
        /// <inheritdoc />
        public string Name => Root.Name;

        /// <summary>
        ///     The object on the GC heap that this root keeps alive.
        /// </summary>
        /// <value>The object.</value>
        /// <inheritdoc />
        public ulong Object => Root.Object;

        /// <summary>
        ///     Returns the stack frame associated with this stack root.
        /// </summary>
        /// <value>The stack frame.</value>
        /// <inheritdoc />
        public IClrStackFrame StackFrame { get; }

        /// <summary>
        ///     If the root has a thread associated with it, this will return that thread.
        /// </summary>
        /// <value>The thread.</value>
        /// <inheritdoc />
        public IClrThread Thread { get; }

        /// <summary>
        ///     The type of the object this root points to.  That is, ClrHeap.GetObjectType(ClrRoot.Object).
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public IClrType Type { get; }

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        [Import]
        internal IConverter Converter { get; set; }
    }
}