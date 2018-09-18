// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-17-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="Settings.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using Common.Logging;
using Newtonsoft.Json;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class capable of performing settings file specific tasks
    /// </summary>
    internal static class Settings
    {
        /// <summary>
        ///     Initializes static members of the <see cref="Settings" /> class.
        /// </summary>
        static Settings()
        {
            try
            {
                var configText =
                    File.ReadAllText("mortician.config.json"); // todo: abstract so we can have multiple configs
                SettingsInstance = JsonConvert.DeserializeObject<SettingsInternal>(configText);
            }
            catch (Exception e) when (e is IOException || e is JsonException)
            {
                Log.Warn($"Did not find valid config file: {e.Message}");
                SettingsInstance = new SettingsInternal();
            }
        }

        /// <summary>
        ///     The settings instance
        /// </summary>
        internal static SettingsInternal SettingsInstance;

        /// <summary>
        ///     Saves the settings.
        /// </summary>
        /// <param name="existingSettings">The existing settings.</param>
        public static void SaveSettings(Dictionary<string, string> existingSettings)
        {
            var serializer = new JsonSerializer {Formatting = Formatting.Indented};
            using (var tw = new StringWriter())
            {
                serializer.Serialize(tw, existingSettings);
                File.WriteAllText("mortician.config.json", tw.ToString());
            }
        }

        /// <summary>
        ///     Gets the settings.
        /// </summary>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        internal static Dictionary<string, string> GetSettings()
        {
            return SettingsInstance.GeneralSettings;
        }

        /// <summary>
        ///     Gets or sets the log.
        /// </summary>
        /// <value>The log.</value>
        public static ILog Log { get; set; } = LogManager.GetLogger(typeof(Settings));

        /// <summary>
        ///     Class SettingsInternal.
        /// </summary>
        internal class SettingsInternal
        {
            /// <summary>
            ///     Gets or sets the blacklisted assemblies.
            /// </summary>
            /// <value>The blacklisted assemblies.</value>
            [JsonProperty("blacklisted_assemblies")]
            public string[] BlacklistedAssemblies { get; set; } = new string[0];

            /// <summary>
            ///     Gets or sets the blacklisted types.
            /// </summary>
            /// <value>The blacklisted types.</value>
            [JsonProperty("blacklisted_types")]
            public string[] BlacklistedTypes { get; set; } = new string[0];

            /// <summary>
            ///     Gets or sets the general settings.
            /// </summary>
            /// <value>The general settings.</value>
            [JsonProperty("general_settings")]
            public Dictionary<string, string> GeneralSettings { get; set; } = new Dictionary<string, string>();
        }
    }
}