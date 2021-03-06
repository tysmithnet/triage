﻿// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ModuleInfoAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Diagnostics.Runtime;
using Mortician.Core.ClrMdAbstractions;
using VersionInfo = Mortician.Core.ClrMdAbstractions.VersionInfo;

namespace Mortician.Adapters
{
    /// <summary>
    ///     Class ModuleInfoAdapter.
    /// </summary>
    /// <seealso cref="Mortician.Core.ClrMdAbstractions.IModuleInfo" />
    internal class ModuleInfoAdapter : BaseAdapter, IModuleInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ModuleInfoAdapter" /> class.
        /// </summary>
        /// <param name="moduleInfo">The module information.</param>
        /// <exception cref="ArgumentNullException">moduleInfo</exception>
        /// <inheritdoc />
        public ModuleInfoAdapter(IConverter converter, ModuleInfo moduleInfo) : base(converter)
        {
            ModuleInfo = moduleInfo ?? throw new ArgumentNullException(nameof(moduleInfo));
            FileName = ModuleInfo.FileName;
            FileSize = ModuleInfo.FileSize;
            ImageBase = ModuleInfo.ImageBase;
            IsManaged = ModuleInfo.IsManaged;
            IsRuntime = ModuleInfo.IsRuntime;
            TimeStamp = ModuleInfo.TimeStamp;
        }

        /// <summary>
        ///     The module information
        /// </summary>
        internal ModuleInfo ModuleInfo;

        /// <summary>
        ///     Returns a PEFile from a stream constructed using instance fields of this object.
        ///     If the PEFile cannot be constructed correctly, null is returned
        /// </summary>
        /// <returns>IPEFile.</returns>
        /// <inheritdoc />
        public IPeFile GetPEFile() => Converter.Convert(ModuleInfo.GetPEFile());

        public override void Setup()
        {
            Pdb = Converter.Convert(ModuleInfo.Pdb);
            Version = Converter.Convert(ModuleInfo.Version);
        }

        /// <summary>
        ///     The filename of the module on disk.
        /// </summary>
        /// <value>The name of the file.</value>
        /// <inheritdoc />
        public string FileName { get; internal set; }

        /// <summary>
        ///     The filesize of the image.
        /// </summary>
        /// <value>The size of the file.</value>
        /// <inheritdoc />
        public uint FileSize { get; internal set; }

        /// <summary>
        ///     The base address of the object.
        /// </summary>
        /// <value>The image base.</value>
        /// <inheritdoc />
        public ulong ImageBase { get; internal set; }

        /// <summary>
        ///     Whether the module is managed or not.
        /// </summary>
        /// <value><c>true</c> if this instance is managed; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsManaged { get; internal set; }

        /// <summary>
        ///     Returns true if this module is a native (non-managed) .Net runtime module.
        /// </summary>
        /// <value><c>true</c> if this instance is runtime; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsRuntime { get; internal set; }

        /// <summary>
        ///     The PDB associated with this module.
        /// </summary>
        /// <value>The PDB.</value>
        /// <inheritdoc />
        public IPdbInfo Pdb { get; set; }

        /// <summary>
        ///     The build timestamp of the image.
        /// </summary>
        /// <value>The time stamp.</value>
        /// <inheritdoc />
        public uint TimeStamp { get; internal set; }

        /// <summary>
        ///     The version information for this file.
        /// </summary>
        /// <value>The version.</value>
        /// <inheritdoc />
        public VersionInfo Version { get; set; }
    }
}