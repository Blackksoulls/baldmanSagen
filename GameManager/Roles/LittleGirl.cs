using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using GameManager.Env;
using GameManager.Locale;
using DSharpPlus.VoiceNext;
using GameManager.Env.Enum;

namespace GameManager.Roles
{
    public class LittleGirl : Citizen
    {
        public LittleGirl(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }


        public void ActivateLittleGirl()
        {
            var client = Global.Client;
            var vNext = client.GetVoiceNext();
             vNext.ConnectAsync(Global.Game.DiscordChannels[GameChannel.WolfVoice]).GetAwaiter().GetResult();
            // if (vNext == null)
            // client.DebugLogger.LogMessage(LogLevel.Info, "BotApp", , DateTime.Now);
        }

        public override string ToString()
        {
            return Global.Game.Texts.GameRoles.LittleGirlToString + " \n " + Global.Game.Texts.GameRoles.TownFriendly;
        }

        public override string GotKilled()
        {
            return Language.FirstDieMessages(Global.Game, Me) + $"{Global.Game.Texts.GameRoles.LittleGirlName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.LittleGirlName;
        }

        public static Task LittleGirlAction()
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}