using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using GameManager.Env;
using GameManager.Locale;

namespace GameManager.Roles
{
    public class Witch : Citizen
    {
        public Witch(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }


        public override string ToString()
        {
            return Global.Game.Texts.GameRoles.WitchToString + " \n " + Global.Game.Texts.GameRoles.TownFriendly;
        }

        public override string GotKilled()
        {
            return Language.FirstDieMessages(Global.Game, Me) + $"{Global.Game.Texts.GameRoles.WitchName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.WitchName;
        }

        public static async Task WitchMoment()
        {
            var witch = Global.Game.PersonnagesList.Find(p => p.GetType() == typeof(Witch));

            if (witch != null)
            {
                var witchCh = witch.ChannelT;
                var embed = new DiscordEmbedBuilder
                {
                    Color = Color.PollColor,
                    Title = $"{Global.Game.NightTargets[0]} {Global.Game.Texts.GameRoles.WitchSaveMsg}"
                };

                var healMsg = await witchCh.SendMessageAsync(embed: embed.Build());
                await healMsg.CreateReactionAsync(DiscordEmoji.FromName(Global.Client, ":thumbsup:"));
                await healMsg.CreateReactionAsync(DiscordEmoji.FromName(Global.Client, ":thumbsdown:"));

                Global.Client.MessageReactionAdded += BotFunctions.ClientOnMessageReactionAdded;

                await Task.Delay(Global.Config.DayVoteTime / 2);
                healMsg = await witchCh.GetMessageAsync(healMsg.Id);
                if (healMsg.GetReactionsAsync(DiscordEmoji.FromName(Global.Client, ":thumbsup:")).GetAwaiter()
                        .GetResult()
                        .Count == 2)
                {
                    Global.Game.NightTargets.Clear();
                }

                embed = new DiscordEmbedBuilder
                {
                    Color = Color.PollColor,
                    Title = Global.Game.Texts.GameRoles.WitchKillMsg
                };
                var killMsg = await witchCh.SendMessageAsync(embed: embed.Build());
                foreach (var emoji in Global.Game.Guild.GetEmojisAsync().GetAwaiter().GetResult().ToList()
                    .FindAll(e => e.Id != witch.Emoji.Id))
                {
                    await killMsg.CreateReactionAsync(emoji);
                }

                Global.Client.MessageReactionAdded += BotFunctions.ClientOnMessageReactionAdded;

                await Task.Delay(Global.Config.DayVoteTime / 2);
                killMsg = await witchCh.GetMessageAsync(killMsg.Id);
                Global.Game.NightTargets.Add(Global.Game.PersonnagesList.Find(p =>
                    p.Emoji == killMsg.Reactions.ToList().Find(r => r.Count == 2).Emoji));
            }
        }
    }
}