using System.Collections.Generic;
using System.Diagnostics;

namespace Triage.Mortician.Abstraction
{
    /// <summary>
    ///     Represents a module loaded into the CLR
    /// </summary>
    public interface IModule
    {
        /// <summary>
        ///     Gets the name of the assembly in which this module was defined
        /// </summary>
        /// <value>
        ///     The name of the assembly in which this module was defined
        /// </value>
        string AssemblyName { get; }

        /// <summary>
        ///     Gets the name of the file from which this module was defined, can be null
        /// </summary>
        /// <value>
        ///     The name of the file from which this module was defined
        /// </value>
        string FileName { get; }

        /// <summary>
        ///     Unique identifier for assemblies (you cannot trust AssemblyName)
        /// </summary>
        /// <value>
        ///     The assembly identifier.
        /// </value>
        ulong AssemblyId { get; }

        /// <summary>
        ///     Gets a value indicating whether this module is dynamic
        /// </summary>
        /// <value>
        ///     <c>true</c> if this module is dynamic; otherwise, <c>false</c>.
        /// </value>
        bool IsDynamic { get; }

        /// <summary>
        ///     Gets the application domains in which this module is loaded
        /// </summary>
        /// <value>
        ///     The application domains in which this module is loaded
        /// </value>
        IEnumerable<IAppDomain> AppDomains { get; }

        /// <summary>
        ///     Gets the debugging attributes for this module (optimized, edit and continue, etc)
        /// </summary>
        /// <value>
        ///     The debugging mode.
        /// </value>
        DebuggableAttribute DebuggingMode { get; }

        /// <summary>
        ///     Gets the image base for this module in memory (can be 0 if not backed by physical memory)
        /// </summary>
        /// <value>
        ///     The image base.
        /// </value>
        ulong ImageBase { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is backed by a PE file
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is backed by a file; otherwise, <c>false</c>.
        /// </value>
        bool IsFile { get; }

        /// <summary>
        ///     Gets the name for this module
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; }

        /// <summary>
        ///     Gets the size of this module in memory
        /// </summary>
        /// <value>
        ///     The size.
        /// </value>
        ulong Size { get; }

        /// <summary>
        ///     Gets the PDB file, but can be null
        /// </summary>
        /// <value>
        ///     The PDB file.
        /// </value>
        string PdbFile { get; }
    }
}