// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="DacInfoAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;
using Architecture = Triage.Mortician.Core.ClrMdAbstractions.Architecture;
using VersionInfo = Triage.Mortician.Core.ClrMdAbstractions.VersionInfo;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class DacInfoAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IDacInfo" />
    internal class DacInfoAdapter : BaseAdapter, IDacInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DacInfoAdapter" /> class.
        /// </summary>
        /// <param name="dacInfo">The dac information.</param>
        /// <exception cref="ArgumentNullException">dacInfo</exception>
        /// <inheritdoc />
        public DacInfoAdapter(IConverter converter, DacInfo dacInfo) : base(converter)
        {
            DacInfo = dacInfo ?? throw new ArgumentNullException(nameof(dacInfo));
        }

        /// <summary>
        ///     The dac information
        /// </summary>
        internal DacInfo DacInfo;

        public override void Setup()
        {
            Pdb = Converter.Convert(DacInfo.Pdb);
            TargetArchitecture = Converter.Convert(DacInfo.TargetArchitecture);
            Version = Converter.Convert(DacInfo.Version);
        }

        /// <summary>
        ///     The filename of the module on disk.
        /// </summary>
        /// <value>The name of the file.</value>
        /// <inheritdoc />
        public string FileName => DacInfo.FileName;

        /// <summary>
        ///     The filesize of the image.
        /// </summary>
        /// <value>The size of the file.</value>
        /// <inheritdoc />
        public uint FileSize => DacInfo.FileSize;

        /// <summary>
        ///     The base address of the object.
        /// </summary>
        /// <value>The image base.</value>
        /// <inheritdoc />
        public ulong ImageBase => DacInfo.ImageBase;

        /// <summary>
        ///     Whether the module is managed or not.
        /// </summary>
        /// <value><c>true</c> if this instance is managed; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsManaged => DacInfo.IsManaged;

        /// <summary>
        ///     Returns true if this module is a native (non-managed) .Net runtime module.
        /// </summary>
        /// <value><c>true</c> if this instance is runtime; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsRuntime => DacInfo.IsRuntime;

        /// <summary>
        ///     The PDB associated with this module.
        /// </summary>
        /// <value>The PDB.</value>
        /// <inheritdoc />
        public IPdbInfo Pdb { get; set; }

        /// <summary>
        ///     The platform-agnostice filename of the dac dll
        /// </summary>
        /// <value>The name of the platform agnostic file.</value>
        /// <inheritdoc />
        public string PlatformAgnosticFileName { get; set; }

        /// <summary>
        ///     The architecture (x86 or amd64) being targeted
        /// </summary>
        /// <value>The target architecture.</value>
        /// <inheritdoc />
        public Architecture TargetArchitecture { get; set; }

        /// <summary>
        ///     The build timestamp of the image.
        /// </summary>
        /// <value>The time stamp.</value>
        /// <inheritdoc />
        public uint TimeStamp => DacInfo.TimeStamp;

        /// <summary>
        ///     The version information for this file.
        /// </summary>
        /// <value>The version.</value>
        /// <inheritdoc />
        public VersionInfo Version { get; set; }
    }
}