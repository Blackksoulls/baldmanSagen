using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BotDiscord.Env;
using DSharpPlus.Entities;

namespace BotDiscord.Roles
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
            return $"{Global.Game.Texts.GameRoles.SeerName}";
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
                    Game.WriteDebug("JE S'APPELLE VOYANTE");
                    var msg = await seer.ChannelT.SendMessageAsync(Global.Game.Texts.GameRoles.SeerAskMsg);

                    foreach (var player in (Global.Game.PersonnagesList.FindAll(p =>
                        p.Alive && p.GetType() != typeof(Seer))))
                    {
                        await msg.CreateReactionAsync(player.Emoji);
                    }

                    await Task.Delay(Global.Config.DayVoteTime);
                    var react = msg.Reactions.ToList()
                        .FindAll(reaction => reaction.Count == msg.Reactions.Max(x => x.Count));
                    await msg.ModifyAsync(content: $"{msg.Reactions.ToList().Count}");
                    var embed = new DiscordEmbedBuilder
                    {
                        Color = Color.InfoColor
                    };
                    var text = "42";
                    if (react.Count > 0)
                    {
                        var target = Global.Game.PersonnagesList.Find(p => p.Emoji.Id == react[0].Emoji.Id);
                        Game.WriteDebug("Voir : " + target.Me.Username);
                        embed.Title = text = $"1337 {Global.Game.Texts.GameRoles.SeerRecMsg} {target.GetClassName()}";
                    }
                    else
                    {
                        embed.Title = text = $"42 {Global.Game.Texts.GameRoles.DidNothing}";
                    }

                    await seer.ChannelT.SendMessageAsync(text, embed: embed.Build());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}