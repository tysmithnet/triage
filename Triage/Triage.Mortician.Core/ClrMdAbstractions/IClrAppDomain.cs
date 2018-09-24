// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrAppDomain.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IClrAppDomain
    /// </summary>
    public interface IClrAppDomain
    {
        /// <summary>
        ///     To string override.
        /// </summary>
        /// <returns>The name of this AppDomain.</returns>
        string ToString();

        /// <summary>
        ///     Address of the AppDomain.
        /// </summary>
        /// <value>The address.</value>
        ulong Address { get; }

        /// <summary>
        ///     Returns the base directory for this AppDomain.  This may return null if the targeted runtime does
        ///     not support enumerating this information.
        /// </summary>
        /// <value>The application base.</value>
        string ApplicationBase { get; }

        /// <summary>
        ///     Returns the config file used for the AppDomain.  This may be null if there was no config file
        ///     loaded, or if the targeted runtime does not support enumerating that data.
        /// </summary>
        /// <value>The configuration file.</value>
        string ConfigurationFile { get; }

        /// <summary>
        ///     The AppDomain's ID.
        /// </summary>
        /// <value>The identifier.</value>
        int Id { get; }

        /// <summary>
        ///     Returns a list of modules loaded into this AppDomain.
        /// </summary>
        /// <value>The modules.</value>
        IList<IClrModule> Modules { get; }

        /// <summary>
        ///     The name of the AppDomain, as specified when the domain was created.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        ///     Gets the runtime associated with this ClrAppDomain.
        /// </summary>
        /// <value>The runtime.</value>
        IClrRuntime Runtime { get; }
    }
}