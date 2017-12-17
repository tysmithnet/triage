using System;
using System.Collections.Generic;
using System.IO;
using Common.Logging;
using Newtonsoft.Json;

namespace Triage.Mortician
{
    internal static class Settings
    {
        public static ILog Log { get; set; } = LogManager.GetLogger(typeof(Settings));

        public static Dictionary<string, string> GetSettings()
        {
            Dictionary<string, string> settings;

            try
            {
                var configText =
                    File.ReadAllText("mortician.config.json"); // todo: abstract so we can have multiple configs
                settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(configText);
            }
            catch (Exception e) when (e is IOException || e is JsonException)
            {
                Log.Warn($"Did not find valid config file: {e.Message}");
                settings = new Dictionary<string, string>();
            }

            return settings;
        }

        public static void SaveSettings(Dictionary<string, string> existingSettings)
        {
            var serializer = new JsonSerializer {Formatting = Formatting.Indented};
            using (var tw = new StringWriter())
            {
                serializer.Serialize(tw, existingSettings);
                File.WriteAllText("mortician.config.json", tw.ToString());
            }
        }
    }
}