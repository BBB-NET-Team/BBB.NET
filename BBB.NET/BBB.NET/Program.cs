﻿using System;
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
        public (string Prefix, string Version, string Color, string Directory, string Token) ParseConfig()
        {
            try
            {
                dynamic jToken = Newtonsoft.Json.Linq.JToken.Parse(File.ReadAllText("config.json"));
                return (jToken.prefix, jToken.version, jToken.color, jToken.directory, jToken.token);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No config file found; We aren't gonna be operating today my friend.");
                Console.WriteLine($"The config file goes in the working directory, which is - {Directory.GetCurrentDirectory()}");
                throw;
            }
        }
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;
            var token = ParseConfig();
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
