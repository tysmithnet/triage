// ***********************************************************************
// Assembly         : Triage.Mortician
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
using System.ComponentModel.Composition;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    /// Class ClrInfoAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrInfo" />
    internal class ClrInfoAdapter : IClrInfo
    {
        /// <summary>
        /// Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        [Import]
        internal IConverter Converter { get; set; }
        /// <summary>
        /// The information
        /// </summary>
        internal Microsoft.Diagnostics.Runtime.ClrInfo Info;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClrInfoAdapter"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <exception cref="ArgumentNullException">info</exception>
        /// <inheritdoc />
        public ClrInfoAdapter(Microsoft.Diagnostics.Runtime.ClrInfo info)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            DacInfo = Converter.Convert(info.DacInfo);
            Flavor = Converter.Convert(info.Flavor);
            ModuleInfo = Converter.Convert(info.ModuleInfo);
            Version = Converter.Convert(info.Version);
        }

        /// <summary>
        /// IComparable.  Sorts the object by version.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>-1 if less, 0 if equal, 1 if greater.</returns>
        /// <inheritdoc />
        public int CompareTo(object obj) => Info.CompareTo(obj);

        /// <summary>
        /// Creates a runtime from the given Dac file on disk.
        /// </summary>
        /// <returns>IClrRuntime.</returns>
        /// <inheritdoc />
        public IClrRuntime CreateRuntime() => Converter.Convert(Info.CreateRuntime());


        /// <summary>
        /// Creates a runtime from a given IXClrDataProcess interface.  Used for debugger plugins.
        /// </summary>
        /// <param name="clrDataProcess">The color data process.</param>
        /// <returns>IClrRuntime.</returns>
        /// <inheritdoc />
        public IClrRuntime CreateRuntime(object clrDataProcess) => Converter.Convert(Info.CreateRuntime(clrDataProcess));


        /// <summary>
        /// Creates a runtime from the given Dac file on disk.
        /// </summary>
        /// <param name="dacFilename">A full path to the matching mscordacwks for this process.</param>
        /// <param name="ignoreMismatch">Whether or not to ignore mismatches between</param>
        /// <returns>IClrRuntime.</returns>
        /// <inheritdoc />
        public IClrRuntime CreateRuntime(string dacFilename, bool ignoreMismatch = false) => Converter.Convert(Info.CreateRuntime(dacFilename, ignoreMismatch));

        /// <summary>
        /// Returns module information about the Dac needed create a ClrRuntime instance for this runtime.
        /// </summary>
        /// <value>The dac information.</value>
        /// <inheritdoc />
        public IDacInfo DacInfo { get; }

        /// <summary>
        /// The type of CLR this module represents.
        /// </summary>
        /// <value>The flavor.</value>
        /// <inheritdoc />
        public ClrFlavor Flavor { get; }

        /// <summary>
        /// Returns the location of the local dac on your machine which matches this version of Clr, or null
        /// if one could not be found.
        /// </summary>
        /// <value>The local matching dac.</value>
        /// <inheritdoc />
        public string LocalMatchingDac => Info.LocalMatchingDac;

        /// <summary>
        /// Returns module information about the ClrInstance.
        /// </summary>
        /// <value>The module information.</value>
        /// <inheritdoc />
        public IModuleInfo ModuleInfo { get; }

        /// <summary>
        /// The version number of this runtime.
        /// </summary>
        /// <value>The version.</value>
        /// <inheritdoc />
        public VersionInfo Version { get; }
    }
}