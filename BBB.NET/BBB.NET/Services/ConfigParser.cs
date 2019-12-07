using System;
using System.IO;
using Newtonsoft.Json;
namespace ConfigParser
{
    public class Main
    {
        public static (string Prefix, string Version, string Color, string Directory, string Token) ParseConfig()
        {
            try
            {
                dynamic jToken = Newtonsoft.Json.Linq.JToken.Parse(File.ReadAllText("config.json"));
                return (jToken.prefix, jToken.version, jToken.color, jToken.directory, jToken.token);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"No config file found; We aren't gonna be operating today my friend.\n" +
                    $"The config file goes in the working directory, which is - {Directory.GetCurrentDirectory()}");
                throw;
            }
        }
    }
}