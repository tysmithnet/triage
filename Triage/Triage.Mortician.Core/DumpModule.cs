using System;
using System.Collections.Generic;
using System.Diagnostics;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     A module that was discovered in the memory dump
    /// </summary>
    public class DumpModule
    {

        internal DumpModule()
        {
        }

        internal void AddAppDomain(DumpAppDomain domain)
        {
            AppDomainsInternal.Add(domain);
        }

        internal void AddType(DumpType type)
        {
            TypesInternal.Add(type);
        }

        /// <summary>
        ///     The app domains for which this module is loaded
        /// </summary>
        protected internal List<DumpAppDomain> AppDomainsInternal = new List<DumpAppDomain>();

        /// <summary>
        ///     The types defined in this module
        /// </summary>
        protected internal List<DumpType> TypesInternal = new List<DumpType>();

        public DumpModule(DumpModuleKey dumpModuleKey)
        {
            Key = dumpModuleKey ?? throw new ArgumentNullException(nameof(dumpModuleKey));
        }

        /// <summary>
        ///     Gets or sets the application domains.
        /// </summary>
        /// <value>The application domains.</value>
        public IEnumerable<DumpAppDomain> AppDomains => AppDomainsInternal;


        /// <summary>
        ///     Gets or sets the debugging mode for this assembly (edit and continue, etc)
        /// </summary>
        /// <value>The debugging mode.</value>
        public DebuggableAttribute.DebuggingModes DebuggingMode { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the name of the file if this module is backed by a physical file, null otherwise
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the address for which this module is loaded in memory, but can be null if
        ///     it not backed by phsical memory
        /// </summary>
        /// <value>The image base.</value>
        public ulong ImageBase { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this module is a dynamic module
        /// </summary>
        /// <value><c>true</c> if this instance is dynamic; otherwise, <c>false</c>.</value>
        public bool IsDynamic { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this module comes from a file
        /// </summary>
        /// <value><c>true</c> if this modules comes from a file; otherwise, <c>false</c>.</value>
        public bool IsFile { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the name of this module
        /// </summary>
        /// <value>The name.</value>
        public string Name => Key.Name;

        public ulong AssemblyId => Key.AssemblyId;
        
        /// <summary>
        ///     Gets or sets the PDB guid if available
        /// </summary>
        /// <value>The PDB unique identifier.</value>
        public IPdbInfo PdbInfo { get; set; }

        /// <summary>
        ///     Gets or sets the size of this module
        /// </summary>
        /// <value>The size.</value>
        public ulong Size { get; protected internal set; }

        public DumpModuleKey Key { get; set; }
    }
}