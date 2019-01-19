using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using GameManager.Env;
using GameManager.Env.Enum;
using GameManager.Env.Extentions;
using GameManager.Locale;

namespace GameManager.Roles
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
            return Language.FirstDieMessages(Global.Game, Me) + $"{Global.Game.Texts.GameRoles.WolfName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.WolfName;
        }

        public static async Task WolfVote()
        {
            Global.Game.NightTargets.Clear(); // On vide la liste des gens à manger

            var embed = new DiscordEmbedBuilder
            {
                Color = Color.PollColor,
                Title = Global.Game.Texts.Polls.NightlyWolfMessage
            };

            var msg = await Global.Game.DiscordChannels[GameChannel.WolfText].SendMessageAsync(embed: embed.Build());

            foreach (var personnage in Global.Game.PersonnagesList.FindAll(p => p.GetType() != typeof(Wolf) && p.Alive))
            {
                await msg.CreateReactionAsync(personnage.Emoji);
            }

            await Task.Delay(Global.Config.NightPhase1);

            var target = (await BotCommands.GetVotes(msg)).Voted();

            if (target != null)
            {
                Global.Game.NightTargets.Add(target);
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