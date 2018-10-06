// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrInfoAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Diagnostics.Runtime;
using Mortician.Core.ClrMdAbstractions;
using ClrFlavor = Mortician.Core.ClrMdAbstractions.ClrFlavor;
using VersionInfo = Mortician.Core.ClrMdAbstractions.VersionInfo;

namespace Mortician.Adapters
{
    /// <summary>
    ///     Class ClrInfoAdapter.
    /// </summary>
    /// <seealso cref="Mortician.Core.ClrMdAbstractions.IClrInfo" />
    internal class ClrInfoAdapter : BaseAdapter, IClrInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrInfoAdapter" /> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <exception cref="ArgumentNullException">info</exception>
        /// <inheritdoc />
        public ClrInfoAdapter(IConverter converter, ClrInfo info) : base(converter)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            LocalMatchingDac = Info.LocalMatchingDac;
        }

        /// <summary>
        ///     The information
        /// </summary>
        internal ClrInfo Info;

        /// <summary>
        ///     IComparable.  Sorts the object by version.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>-1 if less, 0 if equal, 1 if greater.</returns>
        /// <inheritdoc />
        public int CompareTo(object obj) => Info.CompareTo(obj);

        /// <summary>
        ///     Creates a runtime from the given Dac file on disk.
        /// </summary>
        /// <returns>IClrRuntime.</returns>
        /// <inheritdoc />
        public IClrRuntime CreateRuntime() => Converter.Convert(Info.CreateRuntime());

        /// <summary>
        ///     Creates a runtime from a given IXClrDataProcess interface.  Used for debugger plugins.
        /// </summary>
        /// <param name="clrDataProcess">The color data process.</param>
        /// <returns>IClrRuntime.</returns>
        /// <inheritdoc />
        public IClrRuntime CreateRuntime(object clrDataProcess) =>
            Converter.Convert(Info.CreateRuntime(clrDataProcess));

        /// <summary>
        ///     Creates a runtime from the given Dac file on disk.
        /// </summary>
        /// <param name="dacFilename">A full path to the matching mscordacwks for this process.</param>
        /// <param name="ignoreMismatch">Whether or not to ignore mismatches between</param>
        /// <returns>IClrRuntime.</returns>
        /// <inheritdoc />
        public IClrRuntime CreateRuntime(string dacFilename, bool ignoreMismatch = false) =>
            Converter.Convert(Info.CreateRuntime(dacFilename, ignoreMismatch));

        public override void Setup()
        {
            DacInfo = Converter.Convert(Info.DacInfo);
            Flavor = Converter.Convert(Info.Flavor);
            ModuleInfo = Converter.Convert(Info.ModuleInfo);
            Version = Converter.Convert(Info.Version);
        }

        /// <summary>
        ///     Returns module information about the Dac needed create a ClrRuntime instance for this runtime.
        /// </summary>
        /// <value>The dac information.</value>
        /// <inheritdoc />
        public IDacInfo DacInfo { get; internal set; }

        /// <summary>
        ///     The type of CLR this module represents.
        /// </summary>
        /// <value>The flavor.</value>
        /// <inheritdoc />
        public ClrFlavor Flavor { get; internal set; }

        /// <summary>
        ///     Returns the location of the local dac on your machine which matches this version of Clr, or null
        ///     if one could not be found.
        /// </summary>
        /// <value>The local matching dac.</value>
        /// <inheritdoc />
        public string LocalMatchingDac { get; internal set; }

        /// <summary>
        ///     Returns module information about the ClrInstance.
        /// </summary>
        /// <value>The module information.</value>
        /// <inheritdoc />
        public IModuleInfo ModuleInfo { get; internal set; }

        /// <summary>
        ///     The version number of this runtime.
        /// </summary>
        /// <value>The version.</value>
        /// <inheritdoc />
        public VersionInfo Version { get; internal set; }
    }
}