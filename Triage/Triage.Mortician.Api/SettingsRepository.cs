using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician.Api
{
    [Export(typeof(ISettingsRepository))]
    internal class SettingsRepository : ISettingsRepository
    {
        public Dictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
        public string Get(string key)
        {
            return Settings[key];
        }

        public void Set(string key, string value)
        {
            if (!Settings.ContainsKey(key))
                Settings.Add(key, value);
            else
                Settings[key] = value;
        }
    }
}