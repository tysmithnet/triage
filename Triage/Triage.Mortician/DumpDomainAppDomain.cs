// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-21-2018
// ***********************************************************************
// <copyright file="DumpDomainAppDomain.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class DumpDomainAppDomain. This class cannot be inherited.
    /// </summary>
    public sealed class DumpDomainAppDomain
    {
        /// <summary>
        ///     The assemblies internal
        /// </summary>
        internal IList<DumpDomainAssembly> AssembliesInternal = new List<DumpDomainAssembly>();

        /// <summary>
        ///     Gets the address.
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; internal set; }

        /// <summary>
        ///     Gets the assemblies.
        /// </summary>
        /// <value>The assemblies.</value>
        public IEnumerable<DumpDomainAssembly> Assemblies => AssembliesInternal;

        /// <summary>
        ///     Gets the high frequency heap.
        /// </summary>
        /// <value>The high frequency heap.</value>
        public ulong HighFrequencyHeap { get; internal set; }

        /// <summary>
        ///     Gets the index.
        /// </summary>
        /// <value>The index.</value>
        public uint Index { get; internal set; }

        /// <summary>
        ///     Gets the low frequency heap.
        /// </summary>
        /// <value>The low frequency heap.</value>
        public ulong LowFrequencyHeap { get; internal set; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; internal set; }

        /// <summary>
        ///     Gets the security descriptor.
        /// </summary>
        /// <value>The security descriptor.</value>
        public ulong SecurityDescriptor { get; internal set; }

        /// <summary>
        ///     Gets the stage.
        /// </summary>
        /// <value>The stage.</value>
        public AppDomainStage Stage { get; internal set; }

        /// <summary>
        ///     Gets the stub heap.
        /// </summary>
        /// <value>The stub heap.</value>
        public ulong StubHeap { get; internal set; }
    }
}