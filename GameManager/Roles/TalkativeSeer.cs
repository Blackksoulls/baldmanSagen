using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using GameManager.Env;
using GameManager.Env.Enum;
using GameManager.Env.Extentions;
using GameManager.Locale;

namespace GameManager.Roles
{
    public class TalkativeSeer : Seer
    {
        public TalkativeSeer(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return
                $"{Global.Game.Texts.GameRoles.TalkativeSeerName} \n {Global.Game.Texts.GameRoles.TalkativeSeerToString} \n {Global.Game.Texts.GameRoles.TownFriendly}";
        }

        public override string GotKilled()
        {
            return Language.FirstDieMessages(Global.Game, Me) + $"{Global.Game.Texts.GameRoles.TalkativeSeerName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.TalkativeSeerName;
        }

        public static async Task TalkativeSeerAction()
        {
            try
            {
                var tSeer = Global.Game.PersonnagesList.Find(p => p.GetType() == typeof(TalkativeSeer));
                if (tSeer != null)
                {
                    var msg = await tSeer.ChannelT.SendMessageAsync(Global.Game.Texts.GameRoles.SeerAskMsg);

                    foreach (var player in Global.Game.PersonnagesList.FindAll(p => p.Alive && p.GetType() != typeof(Seer))) await msg.CreateReactionAsync(player.Emoji);

                    await Task.Delay(Global.Config.DayVoteTime);

                    var target = (await BotCommands.GetVotes(msg)).Voted();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = Color.InfoColor,
                    };


                    if (target != null)
                    {
                        Game.WriteDebug("Voir : " + target.Me.Username);

                        embed.Title = $"{Global.Game.Texts.GameRoles.TalkativeSeerMessage} {target.GetClassName()}";

                    }
                    else
                    {
                        embed.Title = $"{Global.Game.Texts.GameRoles.DidNothing}";
                    }

                    await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}