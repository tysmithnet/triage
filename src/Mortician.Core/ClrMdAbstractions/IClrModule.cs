// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrModule.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Diagnostics;

namespace Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IClrModule
    /// </summary>
    public interface IClrModule
    {
        /// <summary>
        ///     Enumerate all types defined by this module.
        /// </summary>
        /// <returns>IEnumerable&lt;IClrType&gt;.</returns>
        IEnumerable<IClrType> EnumerateTypes();

        /// <summary>
        ///     Attempts to obtain a ClrType based on the name of the type.  Note this is a "best effort" due to
        ///     the way that the dac handles types.  This function will fail for Generics, and types which have
        ///     never been constructed in the target process.  Please be sure to null-check the return value of
        ///     this function.
        /// </summary>
        /// <param name="name">The name of the type.  (This would be the EXACT value returned by ClrType.Name.</param>
        /// <returns>The requested ClrType, or null if the type doesn't exist or couldn't be constructed.</returns>
        IClrType GetTypeByName(string name);

        /// <summary>
        ///     Returns a name for the assembly.
        /// </summary>
        /// <returns>A name for the assembly.</returns>
        string ToString();

        /// <summary>
        ///     Returns a list of all AppDomains this module is loaded into.  Please note that unlike
        ///     ClrRuntime.AppDomains, this list may include the shared AppDomain.
        /// </summary>
        /// <value>The application domains.</value>
        IList<IClrAppDomain> AppDomains { get; }

        /// <summary>
        ///     Returns an identifier to uniquely represent this assembly.  This value is not used by any other
        ///     function in ClrMD, but can be used to group modules by their assembly.  (Do not use AssemblyName
        ///     for this, as reflection and other special assemblies can share the same name, but actually be
        ///     different.)
        /// </summary>
        /// <value>The assembly identifier.</value>
        ulong AssemblyId { get; }

        /// <summary>
        ///     Returns the name of the assembly that this module is defined in.
        /// </summary>
        /// <value>The name of the assembly.</value>
        string AssemblyName { get; }

        /// <summary>
        ///     The debugging attributes for this module.
        /// </summary>
        /// <value>The debugging mode.</value>
        DebuggableAttribute.DebuggingModes DebuggingMode { get; }

        /// <summary>
        ///     Returns the filename of where the module was loaded from on disk.  Undefined results if
        ///     IsPEFile returns false.
        /// </summary>
        /// <value>The name of the file.</value>
        string FileName { get; }

        /// <summary>
        ///     Returns the base of the image loaded into memory.  This may be 0 if there is not a physical
        ///     file backing it.
        /// </summary>
        /// <value>The image base.</value>
        ulong ImageBase { get; }

        /// <summary>
        ///     Returns true if this module was created through Reflection.Emit (and thus has no associated
        ///     file).
        /// </summary>
        /// <value><c>true</c> if this instance is dynamic; otherwise, <c>false</c>.</value>
        bool IsDynamic { get; }

        /// <summary>
        ///     Returns true if this module is an actual PEFile on disk.
        /// </summary>
        /// <value><c>true</c> if this instance is file; otherwise, <c>false</c>.</value>
        bool IsFile { get; }

        /// <summary>
        ///     The location of metadata for this module in the process's memory.  This is useful if you
        ///     need to manually create IMetaData* objects.
        /// </summary>
        /// <value>The metadata address.</value>
        ulong MetadataAddress { get; }

        /// <summary>
        ///     The IMetaDataImport interface for this module.  Note that this API does not provide a
        ///     wrapper for IMetaDataImport.  You will need to wrap the API yourself if you need to use this.
        /// </summary>
        /// <value>The metadata import.</value>
        object MetadataImport { get; }

        /// <summary>
        ///     The length of the metadata for this module.
        /// </summary>
        /// <value>The length of the metadata.</value>
        ulong MetadataLength { get; }

        /// <summary>
        ///     Returns the name of the module.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        ///     Returns the pdb information for this module.
        /// </summary>
        /// <value>The PDB.</value>
        IPdbInfo PdbInfo { get; }

        /// <summary>
        ///     Gets the runtime which contains this module.
        /// </summary>
        /// <value>The runtime.</value>
        IClrRuntime Runtime { get; }

        /// <summary>
        ///     Returns the size of the image in memory.
        /// </summary>
        /// <value>The size.</value>
        ulong Size { get; }
    }
}