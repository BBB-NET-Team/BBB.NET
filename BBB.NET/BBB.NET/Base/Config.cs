using System;
using System.IO;
using Newtonsoft.Json;

namespace BBB.NET.Base
{
    /// <summary>
    /// The Config class holds the bot's configuration
    /// The field are populated with default values that will be overriden by the configuration file
    /// </summary>
    public class Config
    {
        private Config() { }

        [JsonProperty("prefix")]
        public string Prefix = "bot:";

        [JsonProperty("version")]
        public string Version = "UNDEFINED";

        [JsonProperty("color")]
        public string Color = "0x00BFBF";

        [JsonProperty("token")]
        public string Token = "Please put your token here!";

        /// <summary>
        /// Load a configuration file
        /// </summary>
        /// <param name="filepath">Path to the configuaration file</param>
        /// <returns>Loaded configuration</returns>
        public static Config Load(string filepath)
        {
            if (!File.Exists(filepath))
            {
                Logger.Log($"Tried to load non-existent config file: \"{Path.GetFullPath(filepath)}\"", LogType.Error);
                return null;
            }

            string jsonString = File.ReadAllText(filepath);

            return JsonConvert.DeserializeObject<Config>(jsonString);
        }

        /// <summary>
        /// Save the current configuration file
        /// </summary>
        /// <param name="filepath">File that the configuration will be saved to</param>
        public void Save(string filepath)
        {
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);

            File.WriteAllText(filepath, jsonString);
        }

        /// <summary>
        /// Create a new configuration with default values
        /// </summary>
        /// <param name="filepath"></param>
        public static void Create(string filepath)
        {
            new Config().Save(filepath);
        }
    }
}
