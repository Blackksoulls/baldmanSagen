using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using GameManager.Env;
using GameManager.Env.Extentions;
using GameManager.Locale;

namespace GameManager.Roles
{
    public class Witch : Citizen
    {

        public bool HealingPotion { get; set; }
        public bool PoisonPotion { get; set; }

        public Witch(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
            HealingPotion = PoisonPotion = true;
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
            var witch = (Witch) Global.Game.PersonnagesList.Find(p => p.GetType() == typeof(Witch));

            if (witch != null)
            {
                var witchCh = witch.ChannelT;
                DiscordEmbedBuilder embed;
                Personnage target;
                
                // Heal 
                if (Global.Game.NightTargets.Count > 0 && witch.HealingPotion)
                {
                    embed = new DiscordEmbedBuilder
                    {
                        Color = Color.PollColor,
                        Title = $"{Global.Game.NightTargets[0].Me.Username} {Global.Game.Texts.GameRoles.WitchSaveMsg}"
                    };

                    var healMsg = await witchCh.SendMessageAsync(embed: embed.Build());
                    await healMsg.CreateReactionAsync(DiscordEmoji.FromName(Global.Client, ":thumbsup:"));
                    await healMsg.CreateReactionAsync(DiscordEmoji.FromName(Global.Client, ":thumbsdown:"));

                    // Global.Client.MessageReactionAdded += BotFunctions.ClientOnMessageReactionAdded;

                    await Task.Delay(Global.Config.NightPhase2 / 2);
                    healMsg = await witchCh.GetMessageAsync(healMsg.Id);
                     target = (await BotCommands.GetVotes(healMsg)).Voted();
                     Global.Game.NightTargets.Remove(target);
                     Console.WriteLine("Nombre de cible : " + Global.Game.NightTargets.Count);
                    witch.HealingPotion = false;
                }

             
                // Kill
                if (witch.PoisonPotion)
                {
                    embed = new DiscordEmbedBuilder
                    {
                        Color = Color.PollColor,
                        Title = Global.Game.Texts.GameRoles.WitchKillMsg
                    };
                    var killMsg = await witchCh.SendMessageAsync(embed: embed.Build());
        
                    foreach (var personnage in Global.Game.PersonnagesList.FindAll(p => p.Alive && p.Id != witch.Id))
                    {
                        await killMsg.CreateReactionAsync(personnage.Emoji);
                    }

                    Global.Client.MessageReactionAdded += Listeners.ClientOnMessageReactionAdded;

                    await Task.Delay(Global.Config.NightPhase2 / 2);
                    killMsg = await witchCh.GetMessageAsync(killMsg.Id);
                    target = (await BotCommands.GetVotes(killMsg)).Voted();
                    Global.Game.NightTargets.Add(target);
                    witch.PoisonPotion = false;
                }

            }
        }
    }
}