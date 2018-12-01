using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotDiscord.Env;
using BotDiscord.Env.Enum;
using BotDiscord.Env.Extentions;
using DSharpPlus;
using DSharpPlus.Entities;

namespace BotDiscord.Roles
{
    public class Wolf : Personnage
    {
        public Wolf(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
            Global.Game.DiscordChannels[GameChannel.WolfText]
                .AddOverwriteAsync(me, GameBuilder.UsrPerms.Grant(Permissions.AccessChannels));
            Global.Game.DiscordChannels[GameChannel.WolfVoice].AddOverwriteAsync(me, GameBuilder.UsrPerms);
        }

        public override string ToString()
        {
            return Global.Game.Texts.GameRoles.WolfToString;
        }

        public override string GotKilled()
        {
            return $"{Global.Game.Texts.GameRoles.WolfName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.WolfName;
        }

        public static async Task WolfVote()
        {
            Global.Game.NightTargets = new List<Personnage>();
            var embed = new DiscordEmbedBuilder
            {
                Color = Color.PollColor,
                Title = Global.Game.Texts.Annoucement.NightlyWolfMessage
            };

            var msg = await Global.Game.DiscordChannels[GameChannel.WolfText].SendMessageAsync(embed: embed.Build());

            foreach (var personnage in Global.Game.PersonnagesList.FindAll(p => p.GetType() != typeof(Wolf) && p.Alive))
            {
                await msg.CreateReactionAsync(personnage.Emoji);
            }

            await Task.Delay(Global.Config.DayVoteTime);

            var targetMember = (await BotCommands.GetVotes(msg)).Voted();

            if (targetMember != null)
            {
                var targetPersonnage = Global.Game.PersonnagesList.Find(p => p.Me == targetMember);
                Global.Game.NightTargets.Add(targetPersonnage);
            }
            else
            {
                embed = new DiscordEmbedBuilder
                {
                    Color = Color.InfoColor,
                    Title = Global.Game.Texts.Polls.NoWolfKill
                };
                await Global.Game.DiscordChannels[GameChannel.WolfText].SendMessageAsync(embed: embed.Build());
            }
        }
    }
}