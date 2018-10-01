// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-26-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DumpHandle.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Class DumpHandle.
    /// </summary>
    public class DumpHandle
    {
        /// <summary>
        ///     Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; set; }

        /// <summary>
        ///     Gets or sets the application domain.
        /// </summary>
        /// <value>The application domain.</value>
        public DumpAppDomain AppDomain { get; set; }

        /// <summary>
        ///     Gets or sets the dependent target.
        /// </summary>
        /// <value>The dependent target.</value>
        public ulong DependentTarget { get; set; }

        /// <summary>
        ///     Gets or sets the type of the dependent.
        /// </summary>
        /// <value>The type of the dependent.</value>
        public DumpType DependentType { get; set; }

        /// <summary>
        ///     Gets or sets the type of the handle.
        /// </summary>
        /// <value>The type of the handle.</value>
        public HandleType HandleType { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is pinned.
        /// </summary>
        /// <value><c>true</c> if this instance is pinned; otherwise, <c>false</c>.</value>
        public bool IsPinned { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is strong.
        /// </summary>
        /// <value><c>true</c> if this instance is strong; otherwise, <c>false</c>.</value>
        public bool IsStrong { get; set; }

        /// <summary>
        ///     Gets or sets the object address.
        /// </summary>
        /// <value>The object address.</value>
        public ulong ObjectAddress { get; set; }

        /// <summary>
        ///     Gets or sets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public DumpType ObjectType { get; set; }

        /// <summary>
        ///     Gets or sets the reference count.
        /// </summary>
        /// <value>The reference count.</value>
        public uint RefCount { get; set; }
    }
}