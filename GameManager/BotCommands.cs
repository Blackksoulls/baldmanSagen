using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using GameManager.Env;
using GameManager.Env.Enum;
using GameManager.Env.Extentions;

namespace GameManager
{
    public class BotCommands : BaseCommandModule
    {

    
        [Command("Reload"), Aliases("r")]
        public async Task Reload()
        {
            Global.Game = null;
            //Global.Client = null;
            //Global.Config = null;
            Global.InGame = false;
            Global.Roles = null;
        }

        [Command("game"), Aliases("go")]
        [Description("Available langages: 'fr', 'en', 'es', 'de', 'ja'")]
        public async Task CreateGame(CommandContext e, [Description("Language for the bot")] string lang = "fr")
        {
            try
            {
                await e.TriggerTypingAsync();
                Game.WriteDebug("GO ! ");
                await Task.Run(() =>
                {
                    Global.Game = new Game(lang);
                    Global.Game.CreateGuild(e).GetAwaiter().GetResult();
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        [Command("ping"), Description("")]
        public async Task Ping(CommandContext e)
        {
            await e.TriggerTypingAsync();
            await e.RespondAsync($"{e.User.Mention} Pong ({e.Client.Ping}ms)");
        }



        [Command("disconnect"), Aliases("dis", "dc")]
        public async Task Disconnect(CommandContext e)
        {
            if (Global.Game != null) await Global.Game.Guild.DeleteAsync();
            await e.Client.UpdateStatusAsync(userStatus: UserStatus.Invisible);
            e.Client.DebugLogger.LogMessage(LogLevel.Info, "AlphaBot", "Disconnecting from Discord", DateTime.Now);
            await e.Client.DisconnectAsync();
            Thread.Sleep(1000);
            Environment.Exit(0);
        }



        public static async Task<Dictionary<DiscordMember, DiscordGuildEmoji>> GetVotes(DiscordMessage m)
        {
            var d = new Dictionary<DiscordMember, DiscordGuildEmoji>();
            foreach (var discordReaction in (await Global.Game.Guild.GetEmojisAsync()))
            {
                foreach (var discordUserReact in (await m.GetReactionsAsync(discordReaction)))
                {
                    if (!discordUserReact.IsCurrent)
                        d.Add(discordUserReact.GetMember(), discordReaction);
                }
            }

            //Game.WriteDebug($"Debug");
            //foreach (var (key, value) in d)
            //{
            //    Game.WriteDebug($"\t{key} : {value}");
            //}

            return d;
        }

        [Command("delete")]
        public async Task Delete(CommandContext e)
        {
            foreach (var discordChannel in e.Guild.Channels)
            {
                await discordChannel.DeleteAsync();
            }
        }

        [Command("admin")]
        public async Task AdminTask(CommandContext e)
        {
            var adminRole = e.Guild.CreateRoleAsync("ADMIN", Permissions.Administrator).GetAwaiter().GetResult();

            await GameBuilder.GetMember(e.Guild, e.User).GrantRoleAsync(adminRole);
        }



        [Command("score"), Aliases("s")]
        public async Task Scores(CommandContext e)
        {
            Console.WriteLine("Score !!!");
            try
            {
                Console.WriteLine(PathExtension.RootBot());
                var s = Score.Load();
                await e.RespondAsync(s.ToString());

                s.ModifyPoint(1, 100);
                s.Save();
                

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}