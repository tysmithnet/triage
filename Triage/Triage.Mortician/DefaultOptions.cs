// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-17-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 01-15-2018
// ***********************************************************************
// <copyright file="DefaultOptions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using CommandLine;

namespace Triage.Mortician
{
    /// <summary>
    ///     The command line options for the default running options
    /// </summary>
    [Verb("run", HelpText = "Run mortician on the provided dump file")]
    public class DefaultOptions
    {
        /// <summary>
        ///     Gets or sets the dump file.
        /// </summary>
        /// <value>The dump file.</value>
        [Option('d', "dumpfile", HelpText = "The dump file to operate on", SetName = "LocalFile")]
        public string DumpFile { get; set; }
    }
}