using System.Collections.Generic;

namespace Triage.Mortician.Domain
{
    /// <summary>
    ///     Represents an AppDomain that was found in the memory dump
    /// </summary>
    public class DumpAppDomain
    {
        /// <summary>
        ///     The modules that have been loaded into the app domain.
        ///     Usually there is a 1:1 relationship between modules and assemblies, but
        ///     there are numerous cases where the assembly has multiple modules. This is
        ///     the mutable internal version
        /// </summary>
        protected internal IList<DumpModule> LoadedModulesInternal = new List<DumpModule>();

        /// <summary>
        ///     Gets or sets the name of this module e.g. System.Net
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the configuration file for this module
        /// </summary>
        /// <value>
        ///     The configuration file.
        /// </value>
        public string ConfigFile { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the application base if one exists e.g. /LM/W3SVC/2/ROOT-1-13157282501912414
        /// </summary>
        /// <value>
        ///     The application base.
        /// </value>
        public string ApplicationBase { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the address of where the app domain has been loaded in memory
        /// </summary>
        /// <value>
        ///     The address.
        /// </value>
        public ulong Address { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the loaded modules.
        /// </summary>
        /// <value>
        ///     The loaded modules.
        /// </value>
        public IEnumerable<DumpModule> LoadedModules => LoadedModulesInternal;
    }
}