using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using GameManager.Env;
using GameManager.Env.Extentions;
using GameManager.Locale;

namespace GameManager.Roles
{
    public class Savior : Citizen
    {
        public Personnage LastTarget { get; set; }

        public Savior(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.GameRoles.SaviorToString + " \n " + Global.Game.Texts.GameRoles.TownFriendly;
        }

        public override string GotKilled()
        {
            return Language.FirstDieMessages(Global.Game, Me) + $"{Global.Game.Texts.GameRoles.SaviorName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.SaviorName;
        }

        public static async Task SaviorMoment()
        {
            var savior = (Savior) Global.Game.PersonnagesList.Find(p => p.GetType() == typeof(Savior) && p.Alive);
            Game.WriteDebug("Coucou Savior est tu là ?");

            if (savior != null)
            {
            Game.WriteDebug("Salvator trouvé !");
                var saviorCh = savior.ChannelT;

                var embed = new DiscordEmbedBuilder
                {
                    Color = Color.PollColor,
                    Title = Global.Game.Texts.GameRoles.SaviorSave
                };
                var saveMsg = await saviorCh.SendMessageAsync(embed: embed.Build());
                Game.WriteDebug("Message envoyé");
                foreach (var personnage in Global.Game.PersonnagesList.FindAll(p => p.Alive && p != savior.LastTarget))
                {
                    await saveMsg.CreateReactionAsync(personnage.Emoji);
                }

                Global.Client.MessageReactionAdded += Listeners.ClientOnMessageReactionAdded;

                await Task.Delay(Global.Config.NightPhase2);
                saveMsg = await saviorCh.GetMessageAsync(saveMsg.Id);
                var target = (await BotCommands.GetVotes(saveMsg)).Voted();
                if (Global.Game.NightTargets.Contains(target))
                {
                    Global.Game.NightTargets.Remove(target);
                    Game.WriteDebug($"NightTarget count : {Global.Game.NightTargets.Count}");
                }
                savior.LastTarget = target;
            }
        }
    }
}