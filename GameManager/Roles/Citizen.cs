using DSharpPlus.Entities;
using GameManager.Env;
using GameManager.Locale;
using GameManager.Roles;

public class Citizen : Personnage
{
    public Citizen(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
    {
    }

    public override string ToString()
    {
        return Global.Game.Texts.GameRoles.CitizenToString;
    }

    public override string GotKilled()
    {
        return Language.FirstDieMessages(Global.Game, Me) + $"{Global.Game.Texts.GameRoles.CitizenName}";
    }

    public override string GetClassName()
    {
        return Global.Game.Texts.GameRoles.CitizenName;
    }
}