using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;

namespace EventManager
{
    public class EventManager
    {
        private static Config config;
        private static void Main(string[] args)
        {
            new EventManager().AsyncMain().GetAwaiter().GetResult();
        }

        public async Task AsyncMain()
        {

            string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var strs = strPath.Split(Path.DirectorySeparatorChar);
            var str = "";
            for (var i = 0; i < strs.Length - 2; i++)
            {
                str = Path.Combine(str, strs[i]);
            }

            Console.WriteLine(str);
            

            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Path.Combine(str, "Config", "config.json")));
            var client = new DiscordClient(new DiscordConfiguration {LogLevel = LogLevel.Debug, Token = config.Token});

            client.MessageReactionAdded += PreventMultiVote;

            try
            {
                await client.ConnectAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            client.MessageCreated += PleaseInviteMe;

            Console.WriteLine("Bot Event : GOO0D");
            await Task.Delay(-1);


        }

        private async Task PleaseInviteMe(MessageCreateEventArgs e)
        {
            Console.WriteLine($"Message reçu de {e.Author.Username} : {e.Message.Content}" );

            if (!e.Author.IsBot && e.Message.Content.Contains("!go"))
            {
                Console.WriteLine("SALOPE");
                await e.Message.RespondAsync(
                    $"https://discordapp.com/oauth2/authorize?client_id={config.Id}&scope=bot&permissions=0");
            }
        }

        private async Task PreventMultiVote(MessageReactionAddEventArgs e)
        {
            if (!e.User.IsBot)
                foreach (var otherEmoji in (await e.Channel.Guild.GetEmojisAsync()).ToList()
                    .FindAll(em => em.Id != e.Emoji.Id))
                    await e.Message.DeleteReactionAsync(otherEmoji, e.User, $"{e.User.Username} already voted");



        }

        public class Config
        {
            public string Token { get; set; }
            public string Id { get; set; }
        }


    }
}