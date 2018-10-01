using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Triage.Mortician.Core;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class SettingsJsonConverter.
    /// </summary>
    /// <seealso cref="ISettings" />
    internal class SettingsJsonConverter : JsonConverter<IEnumerable<ISettings>>
    {
        /// <summary>
        ///     Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">
        ///     The existing value of object being read. If there is no existing value then <c>null</c>
        ///     will be used.
        /// </param>
        /// <param name="hasExistingValue">The existing value has a value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public override IEnumerable<ISettings> ReadJson(JsonReader reader, Type objectType,
            IEnumerable<ISettings> existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (!(JToken.ReadFrom(reader) is JObject rootObject))
                throw new SerializationException(
                    "Settings file must contain a single object, which property names of the settings types");
            var settings = new List<ISettings>();
            foreach (var prop in rootObject)
            {
                var className = prop.Key;
                var settingsType = Type.GetType(className);
                if (settingsType == null)
                    continue;
                try
                {
                    var restored = (ISettings) JsonConvert.DeserializeObject(prop.Value.ToString(), settingsType);
                    settings.Add(restored);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Unable to retore settings for {@ClassName}", className);
                }
            }

            return settings;
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, IEnumerable<ISettings> value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            foreach (var settings in value)
            {
                var type = settings.GetType();
                var settingsValue = JObject.FromObject(settings);
                writer.WritePropertyName(type.FullName);
                writer.WriteToken(new JTokenReader(settingsValue));
            }

            writer.WriteEndObject();
        }
    }
}