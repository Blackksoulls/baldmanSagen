using System.Linq;
using System.Threading.Tasks;
using GameManager.Env.Extentions;
using DSharpPlus.Entities;
using GameManager.Env;
using GameManager.Env.Enum;
using GameManager.Locale;

namespace GameManager.Roles
{
    public class Hunter : Citizen
    {
        public Hunter(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString() => Global.Game.Texts.GameRoles.HunterToString + " \n " + Global.Game.Texts.GameRoles.TownFriendly;

        public override string GotKilled()
        {
            return Language.FirstDieMessages(Global.Game, Me) + $"{Global.Game.Texts.GameRoles.HunterName}";
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

            var target = (await BotCommands.GetVotes(message)).Voted();
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