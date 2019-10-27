using System;
using System.Net.Http;
using System.Threading.Tasks;
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
            var json = System.IO.File.ReadAllText("config.json");
            dynamic jToken = Newtonsoft.Json.Linq.JToken.Parse(json);
            string prefix = jToken.prefix;
            string version = jToken.version;
            string color = jToken.color;
            string directory = jToken.directory;
            string token = jToken.token;
            return (prefix, version, color, directory, token);
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
