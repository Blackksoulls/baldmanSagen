using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Linq;
using System.Threading.Tasks;
using GameManager.Env;
using GameManager.Env.Enum;
using GameManager.Locale;

namespace GameManager.Roles
{
    public class Cupidon : Citizen
    {
        public Cupidon(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.GameRoles.CupidToString + " \n " + Global.Game.Texts.GameRoles.TownFriendly;
        }

        public override string GotKilled()
        {
            return Language.FirstDieMessages(Global.Game, Me) + Global.Game.Texts.GameRoles.CupidName;
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.CupidName;
        }

        public static async Task CupidonChoice()
        {
            var channel = Global.Game.PersonnagesList.Find(p => p.GetType() == typeof(Cupidon)).ChannelT;

            if (channel != null)
            {
                var embed = new DiscordEmbedBuilder
                {
                    Color = Color.PollColor,
                    Title = Global.Game.Texts.GameRoles.CupidMessage
                };
                var msg = await channel.SendMessageAsync(embed: embed.Build());
                Global.Client.MessageReactionAdded += OnReactionAddedCupidon;

                await Task.Delay(Global.Config.DayVoteTime);

                msg = await channel.GetMessageAsync(msg.Id);

                var react = msg.Reactions.ToList()
                    .FindAll(reaction => reaction.Count == msg.Reactions.Max(x => x.Count));

                var target = new[]
                {
                    Global.Game.PersonnagesList.Find(p => p.Emoji.Id == react[0].Emoji.Id),
                    Global.Game.PersonnagesList.Find(p => p.Emoji.Id == react[1].Emoji.Id)
                };

                foreach (var personnage in target)
                {
                    personnage.Effect = Effect.Lover;
                }

                Global.Client.MessageReactionAdded -= OnReactionAddedCupidon;
            }
        }

        private static async Task OnReactionAddedCupidon(MessageReactionAddEventArgs e)
        {
            var game = Global.Game;
            var present = false;
            foreach (var personnage in game.PersonnagesList.FindAll(p => p.Alive))
            {
                if (e.Emoji == personnage.Emoji)
                {
                    present = true;
                }
            }

            if (!present ||
                (GameBuilder.GetMember(game.Guild, e.User)).Roles.Contains(Global.Roles[PublicRole.Spectator]))
            {
                await BotFunctions.DailyVotingMessage.DeleteReactionAsync(e.Emoji, e.User);
                return;
            }

            var cnt = 0;

            if (!e.User.IsBot && !GameBuilder.GetMember(game.Guild, e.User).Roles
                    .Contains(Global.Roles[PublicRole.Spectator]))
            {
                foreach (var otherEmoji in (await game.Guild.GetEmojisAsync()))
                {
                    cnt++;
                    if (otherEmoji.Name != e.Emoji.Name && cnt > 1)
                    {
                        await BotFunctions.DailyVotingMessage.DeleteReactionAsync(otherEmoji, e.User,
                            $"{e.User.Username} already voted");
                        cnt = 0; // 0  car on ajoute 1 avant le test
                    }
                }
            }
        }
    }
}