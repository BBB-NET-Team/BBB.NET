using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
namespace BBB.NET
{
    public class Program
    {
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            
            using (var services = ConfigureServices())
            {
                var client = services.GetRequiredService<DiscordSocketClient>();

                client.Log += LogAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;

                
                await client.LoginAsync(TokenType.Bot, "token"); // Token goes here, don't hardcode this (obviously)
                await client.StartAsync();

                // Here we initialize the logic required to register our commands.
                await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

                await Task.Delay(-1);
            }
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
