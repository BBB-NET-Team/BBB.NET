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
    public class Program 
    {
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
        private DiscordSocketClient _client;
        
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;
            var token = ConfigParser.Main.ParseConfig();
            await _client.LoginAsync(TokenType.Bot,
                token.Token); 
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
