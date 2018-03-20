using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VideoNotifications.Settings {

    internal static class SettingsManager {

        /// <summary>
        /// Configuration settings.
        /// </summary>
        public static Configuration Configuration = new Configuration();

        /// <summary>
        /// Load settings from the disk at <see cref="Constants.SettingsFile" />.
        /// </summary>
        public static void Load() {
            if (!File.Exists(Constants.SettingsFile)) {
                LoggingManager.Log.Warn($"Settings file was not found at '{Constants.SettingsFile}', Creating default one.");
                Save();
            }

            using (StreamReader jsonFile = File.OpenText(Constants.SettingsFile)) {
                JsonSerializer jsonSerializer = new JsonSerializer();
                Configuration = (Configuration)jsonSerializer.Deserialize(jsonFile, typeof(Configuration));
            }

            LoggingManager.Log.Info("Settings Loaded.");
        }

        /// <summary>
        /// Save settings to the disk at <see cref="Constants.SettingsFile" />.
        /// </summary>
        public static void Save() {
            Directory.CreateDirectory(Constants.WorkingDirectory);

            using (StreamWriter streamWriter = new StreamWriter(Constants.SettingsFile))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter)) {
                DefaultContractResolver contractResolver = new DefaultContractResolver {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };

                JsonSerializer jsonSerializer = new JsonSerializer() {
                    ContractResolver = contractResolver,
                    NullValueHandling = NullValueHandling.Include,
                    Formatting = Formatting.Indented
                };

                jsonSerializer.Serialize(jsonWriter, Configuration, typeof(Configuration));
            }

            LoggingManager.Log.Info("Settings saved.");
        }

    }

}
