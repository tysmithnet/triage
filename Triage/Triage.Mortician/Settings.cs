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
        internal static SettingsInternal SettingsInstance;

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

        public static ILog Log { get; set; } = LogManager.GetLogger(typeof(Settings));

        /// <summary>
        ///     Gets the settings.
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, string> GetSettings()
        {
            return SettingsInstance.GeneralSettings;
        }

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

        internal class SettingsInternal
        {
            [JsonProperty("general_settings")]
            public Dictionary<string, string> GeneralSettings { get; set; } = new Dictionary<string, string>();

            [JsonProperty("blacklisted_assemblies")]
            public string[] BlacklistedAssemblies { get; set; } = new string[0];

            [JsonProperty("blacklisted_types")]
            public string[] BlacklistedTypes { get; set; } = new string[0];
        }
    }
}