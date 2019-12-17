using System;
using System.Threading.Tasks;
using System.IO;
using Discord;
using Discord.WebSocket;
using BBB.NET.Base;
using Discord.Net;

namespace BBB.NET
{
    public class Program 
    {
        public static DiscordSocketClient client = null;

        public static CommandConsole commandConsole = null;

        public const string configFile = "config.json";
        public static Config config = null;

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            if (!File.Exists(configFile))
            {
                Config.Create(configFile);
                Logger.Log("Please fill out the configuration and then try running the bot again.");

                Console.ReadKey();

                Environment.Exit(0);
            }

            // Load the configuration file
            config = Config.Load(configFile);

            // Update the configuration file
            config.Save(configFile);

            client = new DiscordSocketClient();

            //client.Log += Log;

            try
            {
                await client.LoginAsync(TokenType.Bot, config.Token);
            }
            catch (HttpException)
            {
                Logger.Log("Failed to authenticate with Discord server", LogType.Error);
                Environment.Exit(-1);
            }

            await client.LoginAsync(TokenType.Bot, config.Token); 
            await client.StartAsync();
            client.Connected += Client_Connected;
            client.Ready += Client_Ready;

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task Client_Connected()
        {
            Logger.Log("Client connected, getting ready.", LogType.Info);
            return Task.CompletedTask;
        }

        private Task Client_Ready()
        {
            Logger.Log("Ready!", LogType.Info);

            commandConsole = new CommandConsole();

            return Task.CompletedTask;
        }
    }
}
