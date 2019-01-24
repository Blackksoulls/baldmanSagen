using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;

namespace EventManager
{
    public class EventManager
    {
        private static Config _config;

        private static void Main(string[] args)
        {
            new EventManager().AsyncMain().GetAwaiter().GetResult();
        }

        public async Task AsyncMain()
        {
            var strPath = Assembly.GetExecutingAssembly().Location;
            var strs = strPath.Split(Path.DirectorySeparatorChar);
            var str = "";
            for (var i = 0; i < strs.Length - 2; i++)
            {
                str = Path.Combine(str, strs[i]);
            }

            Console.WriteLine(str);


            _config = JsonConvert.DeserializeObject<Config>(
                File.ReadAllText(Path.Combine(str, "Config", "config.json")));
            var client = new DiscordClient(new DiscordConfiguration {LogLevel = LogLevel.Debug, Token = _config.Token});


            try
            {
                await client.ConnectAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            client.MessageReactionAdded += PreventMultiVote;
            client.MessageCreated += PleaseInviteMe;

            Console.WriteLine("Bot Event : GOO0D");
            await Task.Delay(-1);
        }

        private async Task PleaseInviteMe(MessageCreateEventArgs e)
        {
            Console.WriteLine($"Message reçu de {e.Author.Username} : {e.Message.Content}");

            if (!e.Author.IsBot && e.Message.Content.Contains("!go"))
            {
                await (await e.Guild.GetMemberAsync(e.Author.Id)).SendMessageAsync(
                    $"https://discordapp.com/oauth2/authorize?client_id={_config.Id}&scope=bot&permissions=0");
                Console.WriteLine($"Asked to invite in private {e.Author.Username}");
            }
        }

        private async Task PreventMultiVote(MessageReactionAddEventArgs e)
        {
            if (!e.User.IsBot)
            {
                Console.WriteLine("Je Check une reaction d'un joueur !");
                foreach (var otherEmoji in (await e.Channel.Guild.GetEmojisAsync()).ToList()
                    .FindAll(em => em.Id != e.Emoji.Id))
                {
                    await e.Message.DeleteReactionAsync(otherEmoji, e.User, $"{e.User.Username} already voted");
                    Console.WriteLine($"Suppression de l'émoji de {e.User.Username}");
                }
            }
        }

        public class Config
        {
            public string Token { get; set; }
            public string Id { get; set; }
        }
    }
}