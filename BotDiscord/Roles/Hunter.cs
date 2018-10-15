using System.Linq;
using System.Threading.Tasks;
using BotDiscord.Env;
using BotDiscord.Env.Enum;
using DSharpPlus.Entities;

namespace BotDiscord.Roles
{
    public class Hunter : Citizen
    {
        public Hunter(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.GameRoles.HunterToString + " \n " + Global.Game.Texts.GameRoles.TownFriendly;
        }

        public override string GotKilled()
        {
            return $"{Global.Game.Texts.GameRoles.HunterName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.HunterName;
        }

        public static async Task HunterDeath()
        {
            var hunter = Global.Game.PersonnagesList.Find(p => p.GetType() == typeof(Hunter));
            var message = await hunter.ChannelT.SendMessageAsync(Global.Game.Texts.GameRoles.HunterDeathQuestion);


            foreach (var emoji in (await Global.Game.Guild.GetEmojisAsync()).ToList()
                .FindAll(emo => emo.Id != hunter.Emoji.Id))
            {
                await message.CreateReactionAsync(emoji);
            }

            await Task.Delay(Global.Config.DayVoteTime);

            var react = message.Reactions.First(reaction => reaction.Count == message.Reactions.Max(x => x.Count));
            var target = Global.Game.PersonnagesList.Find(p => p.Emoji.Id == react.Emoji.Id);
            await Global.Game.Kill(target);
            var embed = new DiscordEmbedBuilder
            {
                Title =
                    $"{hunter.Me.Username} {Global.Game.Texts.Annoucement.PublicHunterMessage} {target.Me.Username}",
                Color = Color.DeadColor
            };
            await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
        }
    }
}