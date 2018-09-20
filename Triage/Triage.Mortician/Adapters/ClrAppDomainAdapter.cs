// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrAppDomainAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ClrAppDomainAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrAppDomain" />
    internal class ClrAppDomainAdapter : BaseAdapter, IClrAppDomain
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrAppDomainAdapter" /> class.
        /// </summary>
        /// <param name="appDomain">The application domain.</param>
        /// <exception cref="ArgumentNullException">appDomain</exception>
        /// <inheritdoc />
        public ClrAppDomainAdapter(IConverter converter, ClrMd.ClrAppDomain appDomain) : base(converter)
        {
            AppDomain = appDomain ?? throw new ArgumentNullException(nameof(appDomain));
            
        }

        /// <summary>
        ///     The application domain
        /// </summary>
        internal ClrMd.ClrAppDomain AppDomain;

        /// <summary>
        ///     Address of the AppDomain.
        /// </summary>
        /// <value>The address.</value>
        /// <inheritdoc />
        public ulong Address => AppDomain.Address;

        /// <summary>
        ///     Returns the base directory for this AppDomain.  This may return null if the targeted runtime does
        ///     not support enumerating this information.
        /// </summary>
        /// <value>The application base.</value>
        /// <inheritdoc />
        public string ApplicationBase => AppDomain.ApplicationBase;

        /// <summary>
        ///     Returns the config file used for the AppDomain.  This may be null if there was no config file
        ///     loaded, or if the targeted runtime does not support enumerating that data.
        /// </summary>
        /// <value>The configuration file.</value>
        /// <inheritdoc />
        public string ConfigurationFile => AppDomain.ConfigurationFile;

        /// <summary>
        ///     The AppDomain's ID.
        /// </summary>
        /// <value>The identifier.</value>
        /// <inheritdoc />
        public int Id => AppDomain.Id;

        /// <summary>
        ///     Returns a list of modules loaded into this AppDomain.
        /// </summary>
        /// <value>The modules.</value>
        /// <inheritdoc />
        public IList<IClrModule> Modules { get; internal set; }

        /// <summary>
        ///     The name of the AppDomain, as specified when the domain was created.
        /// </summary>
        /// <value>The name.</value>
        /// <inheritdoc />
        public string Name => AppDomain.Name;

        /// <summary>
        ///     Gets the runtime associated with this ClrAppDomain.
        /// </summary>
        /// <value>The runtime.</value>
        /// <inheritdoc />
        public IClrRuntime Runtime { get; internal set; }

        /// <inheritdoc />
        public override void Setup()
        {
            Modules = AppDomain.Modules.Select(Converter.Convert).ToList();
            Runtime = Converter.Convert(AppDomain.Runtime);
        }
    }
}