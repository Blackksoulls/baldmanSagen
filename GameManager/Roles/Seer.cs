using System;
using System.Threading.Tasks;
using GameManager.Env.Extentions;
using DSharpPlus.Entities;
using GameManager.Env;
using GameManager.Locale;

namespace GameManager.Roles
{
    public class Seer : Citizen
    {
        public Seer(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.GameRoles.SeerToString + " \n " + Global.Game.Texts.GameRoles.TownFriendly;
        }

        public override string GotKilled()
        {
            return Language.FirstDieMessages(Global.Game, Me) + $"{Global.Game.Texts.GameRoles.SeerName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.SeerName;
        }

        public static async Task SeerAction()
        {
            try
            {
                var seer = Global.Game.PersonnagesList.Find(p => p.GetType() == typeof(Seer));
                if (seer != null)
                {
                    var msg = await seer.ChannelT.SendMessageAsync(Global.Game.Texts.GameRoles.SeerAskMsg);

                    foreach (var player in Global.Game.PersonnagesList.FindAll(p => p.Alive && p.GetType() != typeof(Seer))) await msg.CreateReactionAsync(player.Emoji);

                    await Task.Delay(Global.Config.DayVoteTime);

                    var target = (await BotCommands.GetVotes(msg)).Voted();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = Color.InfoColor,
                        Title = $"{Global.Game.Texts.GameRoles.SeerEmbedTitle} {target.Me.Username}"
                    };


                    if (target != null)
                    {
                        Game.WriteDebug("Voir : " + target.Me.Username);
                        
                        embed.Description = $"{Global.Game.Texts.GameRoles.SeerRecMsg} {target.GetClassName()}";

                    }
                    else
                    {
                        embed.Title = $"{Global.Game.Texts.GameRoles.DidNothing}";
                    }

                    await seer.ChannelT.SendMessageAsync(embed: embed.Build());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}