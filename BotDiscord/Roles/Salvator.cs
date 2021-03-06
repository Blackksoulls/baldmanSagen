using BotDiscord.Env;
using DSharpPlus.Entities;

namespace BotDiscord.Roles
{
    public class Salvator : Citizen
    {
        public Salvator(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.GameRoles.SaviorToString + " \n " + Global.Game.Texts.GameRoles.TownFriendly;
        }

        public override string GotKilled()
        {
            return $"{Global.Game.Texts.GameRoles.SaviorName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.SaviorName;
        }
    }
}