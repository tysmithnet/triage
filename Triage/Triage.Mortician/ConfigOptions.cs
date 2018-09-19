// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-17-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="ConfigOptions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using CommandLine;

namespace Triage.Mortician
{
    /// <summary>
    ///     The command line options for configuring mortician
    /// </summary>
    [Verb("config", HelpText = "Configure mortician")]
    public class ConfigOptions
    {
        /// <summary>
        ///     Gets or sets the keys.
        /// </summary>
        /// <value>The keys.</value>
        [Option('k', "key", HelpText = "The key to use when retrieving this setting", SetName = "Upsert")]
        public IEnumerable<string> Keys { get; set; }

        /// <summary>
        ///     Gets or sets the keys to delete.
        /// </summary>
        /// <value>The keys to delete.</value>
        [Option('d', "delete", HelpText = "Delete keys from the settings")]
        public IEnumerable<string> KeysToDelete { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [should list].
        /// </summary>
        /// <value><c>true</c> if [should list]; otherwise, <c>false</c>.</value>
        [Option('l', "list", HelpText = "List the current settings", SetName = "List")]
        public bool ShouldList { get; set; }

        /// <summary>
        ///     Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        [Option('v', "value", HelpText = "The value of the setting", SetName = "Upsert")]
        public IEnumerable<string> Values { get; set; }
    }
}