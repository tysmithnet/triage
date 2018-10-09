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
using Commander.NET.Attributes;

namespace Mortician
{
    public class Options
    {
        [Parameter("-c", "--config", Description = "Location of the config file", Required = Required.No)]
        public string ConfigFile { get; set; }

        [Parameter("-s", "--settings", Description = "Location of the settings file", Required = Required.No)]
        public string SettingsFile { get; set; }

        [Command("run", Description = "Process a memory dump")]
        public RunOptions RunOptions { get; set; }

        internal Type[] AdditionalTypes { get; set; }
    }

    public class RunOptions
    {
        [Parameter("-f", "--file", Description = "File to process", Required = Required.Yes)]
        public string DumpLocation { get; set; }
    }
}