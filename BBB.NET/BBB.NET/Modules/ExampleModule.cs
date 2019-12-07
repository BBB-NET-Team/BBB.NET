using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace ExampleModule
{
    public class Echo : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
        => ReplyAsync(echo);
    }
}