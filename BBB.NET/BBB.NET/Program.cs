using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Newtonsoft.Json;
namespace BBB.NET
{
    public class Program // uwu i love you goshunjin-sama~ <3
    {
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
        private DiscordSocketClient _client;
        public (string Prefix, string Version, string Color, string Directory, string Token) ParseConfig()
        {
            try
            {
                var json = File.ReadAllText("config.json");
                dynamic jToken = Newtonsoft.Json.Linq.JToken.Parse(json);
                return (jToken.prefix, jToken.version, jToken.color, jToken.directory, jToken.token);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No config file found; We ain't gonna be running today");
                throw;
            }
        }
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;
            var token = ParseConfig();
            await _client.LoginAsync(TokenType.Bot,
                token.Token); // TODO: get from a configuration file. As we all know, hardcoding shit is a BAAAAAAAADDDDDDDD idea.
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
