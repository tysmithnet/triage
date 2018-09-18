using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Diagnostics.Runtime
{
    public interface IClrModule
    {
        /// <summary>
        /// Gets the runtime which contains this module.
        /// </summary>
        ClrRuntime Runtime { get; }

        /// <summary>
        /// Returns a list of all AppDomains this module is loaded into.  Please note that unlike
        /// ClrRuntime.AppDomains, this list may include the shared AppDomain.
        /// </summary>
        IList<ClrAppDomain> AppDomains { get; }

        /// <summary>
        /// Returns the name of the assembly that this module is defined in.
        /// </summary>
        string AssemblyName { get; }

        /// <summary>
        /// Returns an identifier to uniquely represent this assembly.  This value is not used by any other
        /// function in ClrMD, but can be used to group modules by their assembly.  (Do not use AssemblyName
        /// for this, as reflection and other special assemblies can share the same name, but actually be
        /// different.)
        /// </summary>
        ulong AssemblyId { get; }

        /// <summary>
        /// Returns the name of the module.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns true if this module was created through Reflection.Emit (and thus has no associated
        /// file).
        /// </summary>
        bool IsDynamic { get; }

        /// <summary>
        /// Returns true if this module is an actual PEFile on disk.
        /// </summary>
        bool IsFile { get; }

        /// <summary>
        /// Returns the filename of where the module was loaded from on disk.  Undefined results if
        /// IsPEFile returns false.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Returns the base of the image loaded into memory.  This may be 0 if there is not a physical
        /// file backing it.
        /// </summary>
        ulong ImageBase { get; }

        /// <summary>
        /// Returns the size of the image in memory.
        /// </summary>
        ulong Size { get; }

        /// <summary>
        /// The location of metadata for this module in the process's memory.  This is useful if you
        /// need to manually create IMetaData* objects.
        /// </summary>
        ulong MetadataAddress { get; }

        /// <summary>
        /// The length of the metadata for this module.
        /// </summary>
        ulong MetadataLength { get; }

        /// <summary>
        /// The IMetaDataImport interface for this module.  Note that this API does not provide a
        /// wrapper for IMetaDataImport.  You will need to wrap the API yourself if you need to use this.
        /// </summary>
        object MetadataImport { get; }

        /// <summary>
        /// The debugging attributes for this module.
        /// </summary>
        DebuggableAttribute.DebuggingModes DebuggingMode { get; }

        /// <summary>
        /// Returns the pdb information for this module.
        /// </summary>
        PdbInfo Pdb { get; }

        /// <summary>
        /// Enumerate all types defined by this module.
        /// </summary>
        IEnumerable<ClrType> EnumerateTypes();

        /// <summary>
        /// Attempts to obtain a ClrType based on the name of the type.  Note this is a "best effort" due to
        /// the way that the dac handles types.  This function will fail for Generics, and types which have
        /// never been constructed in the target process.  Please be sure to null-check the return value of
        /// this function.
        /// </summary>
        /// <param name="name">The name of the type.  (This would be the EXACT value returned by ClrType.Name.</param>
        /// <returns>The requested ClrType, or null if the type doesn't exist or couldn't be constructed.</returns>
        ClrType GetTypeByName(string name);

        /// <summary>
        /// Returns a name for the assembly.
        /// </summary>
        /// <returns>A name for the assembly.</returns>
        string ToString();
    }
}