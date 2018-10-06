// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-19-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrModuleAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Mortician.Core.ClrMdAbstractions;

namespace Mortician.Adapters
{
    /// <summary>
    ///     Class ClrModuleAdapter.
    /// </summary>
    /// <seealso cref="Mortician.Core.ClrMdAbstractions.IClrModule" />
    internal class ClrModuleAdapter : BaseAdapter, IClrModule
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrModuleAdapter" /> class.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <exception cref="ArgumentNullException">module</exception>
        /// <inheritdoc />
        public ClrModuleAdapter(IConverter converter, ClrModule module) : base(converter)
        {
            Module = module ?? throw new ArgumentNullException(nameof(module));
            AssemblyId = Module.AssemblyId;
            AssemblyName = Module.AssemblyName;
            FileName = Module.FileName;
            ImageBase = Module.ImageBase;
            IsDynamic = Module.IsDynamic;
            IsFile = Module.IsFile;
            MetadataAddress = Module.MetadataAddress;
            MetadataImport = Module.MetadataImport;
            MetadataLength = Module.MetadataLength;
            Name = Module.Name;
            Size = Module.Size;
        }

        /// <summary>
        ///     The module
        /// </summary>
        internal ClrModule Module;

        /// <summary>
        ///     Enumerate all types defined by this module.
        /// </summary>
        /// <returns>IEnumerable&lt;IClrType&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <inheritdoc />
        public IEnumerable<IClrType> EnumerateTypes() => Module.EnumerateTypes().Select(Converter.Convert);

        /// <summary>
        ///     Attempts to obtain a ClrType based on the name of the type.  Note this is a "best effort" due to
        ///     the way that the dac handles types.  This function will fail for Generics, and types which have
        ///     never been constructed in the target process.  Please be sure to null-check the return value of
        ///     this function.
        /// </summary>
        /// <param name="name">The name of the type.  (This would be the EXACT value returned by ClrType.Name.</param>
        /// <returns>The requested ClrType, or null if the type doesn't exist or couldn't be constructed.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <inheritdoc />
        public IClrType GetTypeByName(string name) => Converter.Convert(Module.GetTypeByName(name));

        public override void Setup()
        {
            AppDomains = Module.AppDomains.Select(Converter.Convert).ToList();
            DebuggingMode = Module.DebuggingMode;
            PdbInfo = Converter.Convert(Module.Pdb);
            Runtime = Converter.Convert(Module.Runtime);
        }

        /// <summary>
        ///     Returns a list of all AppDomains this module is loaded into.  Please note that unlike
        ///     ClrRuntime.AppDomains, this list may include the shared AppDomain.
        /// </summary>
        /// <value>The application domains.</value>
        /// <inheritdoc />
        public IList<IClrAppDomain> AppDomains { get; internal set; }

        /// <summary>
        ///     Returns an identifier to uniquely represent this assembly.  This value is not used by any other
        ///     function in ClrMD, but can be used to group modules by their assembly.  (Do not use AssemblyName
        ///     for this, as reflection and other special assemblies can share the same name, but actually be
        ///     different.)
        /// </summary>
        /// <value>The assembly identifier.</value>
        /// <inheritdoc />
        public ulong AssemblyId { get; internal set; }

        /// <summary>
        ///     Returns the name of the assembly that this module is defined in.
        /// </summary>
        /// <value>The name of the assembly.</value>
        /// <inheritdoc />
        public string AssemblyName { get; internal set; }

        /// <summary>
        ///     The debugging attributes for this module.
        /// </summary>
        /// <value>The debugging mode.</value>
        /// <inheritdoc />
        public DebuggableAttribute.DebuggingModes DebuggingMode { get; internal set; }

        /// <summary>
        ///     Returns the filename of where the module was loaded from on disk.  Undefined results if
        ///     IsPEFile returns false.
        /// </summary>
        /// <value>The name of the file.</value>
        /// <inheritdoc />
        public string FileName { get; internal set; }

        /// <summary>
        ///     Returns the base of the image loaded into memory.  This may be 0 if there is not a physical
        ///     file backing it.
        /// </summary>
        /// <value>The image base.</value>
        /// <inheritdoc />
        public ulong ImageBase { get; internal set; }

        /// <summary>
        ///     Returns true if this module was created through Reflection.Emit (and thus has no associated
        ///     file).
        /// </summary>
        /// <value><c>true</c> if this instance is dynamic; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsDynamic { get; internal set; }

        /// <summary>
        ///     Returns true if this module is an actual PEFile on disk.
        /// </summary>
        /// <value><c>true</c> if this instance is file; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsFile { get; internal set; }

        /// <summary>
        ///     The location of metadata for this module in the process's memory.  This is useful if you
        ///     need to manually create IMetaData* objects.
        /// </summary>
        /// <value>The metadata address.</value>
        /// <inheritdoc />
        public ulong MetadataAddress { get; internal set; }

        /// <summary>
        ///     The IMetaDataImport interface for this module.  Note that this API does not provide a
        ///     wrapper for IMetaDataImport.  You will need to wrap the API yourself if you need to use this.
        /// </summary>
        /// <value>The metadata import.</value>
        /// <inheritdoc />
        public object MetadataImport { get; internal set; }

        /// <summary>
        ///     The length of the metadata for this module.
        /// </summary>
        /// <value>The length of the metadata.</value>
        /// <inheritdoc />
        public ulong MetadataLength { get; internal set; }

        /// <summary>
        ///     Returns the name of the module.
        /// </summary>
        /// <value>The name.</value>
        /// <inheritdoc />
        public string Name { get; internal set; }

        /// <summary>
        ///     Returns the pdb information for this module.
        /// </summary>
        /// <value>The PDB.</value>
        /// <inheritdoc />
        public IPdbInfo PdbInfo { get; internal set; }

        /// <summary>
        ///     Gets the runtime which contains this module.
        /// </summary>
        /// <value>The runtime.</value>
        /// <inheritdoc />
        public IClrRuntime Runtime { get; internal set; }

        /// <summary>
        ///     Returns the size of the image in memory.
        /// </summary>
        /// <value>The size.</value>
        /// <inheritdoc />
        public ulong Size { get; internal set; }
    }
}