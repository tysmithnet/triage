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
using System.Collections.Generic;
using CommandLine;

namespace Mortician
{
    public class Options
    {
        // todo: document difference between config and settings
        [Option('c', "config", HelpText = "Config file to use")]
        public string ConfigFile { get; set; }

        [Option('s', "settings", HelpText = "Path to the settings file")]
        public string SettingsFile { get; set; }

        internal Type[] AdditionalTypes { get; set; }
    }

    [Verb("run")]
    public class RunOptions : Options
    {
        [Option('f',"file", Required = true, HelpText = "Dump file to process")]
        public string DumpLocation { get; set; }
    }
}