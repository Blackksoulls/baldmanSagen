using GameManager.Env;
using GameManager.Roles;
using DSharpPlus.Entities;

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
        return $"{Global.Game.Texts.GameRoles.CitizenName}";
    }

    public override string GetClassName()
    {
        return Global.Game.Texts.GameRoles.CitizenName;
    }
}