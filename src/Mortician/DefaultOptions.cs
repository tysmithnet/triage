// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 12-17-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-06-2018
// ***********************************************************************
// <copyright file="DefaultOptions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using CommandLine;

namespace Mortician
{
    /// <summary>
    ///     The command line options for the default running options
    /// </summary>
    [Verb("run", HelpText = "Run mortician on the provided dump file")]
    public class DefaultOptions
    {
        /// <summary>
        ///     Gets or sets the additional assemblies.
        /// </summary>
        /// <value>The additional assemblies.</value>
        [Option("aa", HelpText = "Additional assemblies to look at when searching for plugins")]
        public string[] AdditionalAssemblies { get; set; } = new string[0];

        /// <summary>
        ///     Gets or sets the black listed assemblies.
        /// </summary>
        /// <value>The black listed assemblies.</value>
        [Option("ba", HelpText = "Blacklisted assemblies will not be loaded")]
        public string[] BlackListedAssemblies { get; set; } = new string[0];

        /// <summary>
        ///     Gets or sets the black listed types.
        /// </summary>
        /// <value>The black listed types.</value>
        [Option("bt", HelpText =
            "Blacklisted types will be considered for injection. Use this to prevent certain plugins from loading.")]
        public string[] BlackListedTypes { get; set; } = new string[0];

        /// <summary>
        ///     Gets or sets the dump file.
        /// </summary>
        /// <value>The dump file.</value>
        [Option('d', "dumpfile", HelpText = "The dump file to operate on", SetName = "LocalFile")]
        public string DumpFile { get; set; }

        /// <summary>
        ///     Gets or sets the settings file.
        /// </summary>
        /// <value>The settings file.</value>
        [Option('s', "settings", HelpText = "Settings for Mortician and its plugins")]
        public string SettingsFile { get; set; }

        /// <summary>
        ///     Gets or sets the additional types.
        /// </summary>
        /// <value>The additional types.</value>
        internal Type[] AdditionalTypes { get; set; } = new Type[0];
    }
}