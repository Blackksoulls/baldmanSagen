using BotDiscord.Env;
using BotDiscord.Env.Enum;
using BotDiscord.Env.Extentions;
using BotDiscord.Locale;
using DSharpPlus;
using DSharpPlus.Entities;

namespace BotDiscord.Roles
{
    #region GameRole's Classes

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
            return $"{Global.Game.Texts.GameRoles.WolfName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.WolfName;
        }
    }


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


    public class Witch : Citizen
    {
        public Witch(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }


        public override string ToString()
        {
            return Global.Game.Texts.GameRoles.WitchToString + " \n " + Global.Game.Texts.GameRoles.TownFriendly;
        }

        public override string GotKilled()
        {
            return $"{Global.Game.Texts.GameRoles.WitchName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.WitchName;
        }
    }

    public class LittleGirl : Citizen
    {
        public LittleGirl(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }


        public override string ToString()
        {
            return Global.Game.Texts.GameRoles.LittleGirlToString + " \n " + Global.Game.Texts.GameRoles.TownFriendly;
        }

        public override string GotKilled()
        {
            return $"{Global.Game.Texts.GameRoles.LittleGirlName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.LittleGirlName;
        }
    }

    public class Hunter : Citizen
    {
        public Hunter(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.GameRoles.HunterToString + " \n " + Global.Game.Texts.GameRoles.TownFriendly;
        }

        public override string GotKilled()
        {
            return $"{Global.Game.Texts.GameRoles.HunterName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.HunterName;
        }
    }

    public class Cupidon : Citizen
    {
        public Cupidon(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.GameRoles.CupidToString + " \n " + Global.Game.Texts.GameRoles.TownFriendly;
        }

        public override string GotKilled()
        {
            return Language.FirstDieMessages(Global.Game, Me) + Global.Game.Texts.GameRoles.CupidName;
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.CupidName;
        }
    }

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
    }

    public class TalkativeSeer : Seer
    {
        public TalkativeSeer(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.GameRoles.SeerToString + " \n " +
                   Global.Game.Texts.GameRoles.TalkativeSeerToString + " \n " +
                   Global.Game.Texts.GameRoles.TownFriendly;
        }

        public override string GotKilled()
        {
            return $"{Global.Game.Texts.GameRoles.TalkativeSeerName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.GameRoles.TalkativeSeerName;
        }
    }

    #endregion

    public abstract class Personnage
    {
        public DiscordMember Me { get; }
        public bool Alive { get; set; }
        public Effect Effect = Effect.None;

        public DiscordChannel ChannelT { get; set; }
        public DiscordChannel ChannelV { get; set; }

        public DiscordGuildEmoji Emoji;


        protected Personnage(DiscordMember me, DiscordGuildEmoji emoji)
        {
            Me = me;
            Emoji = emoji;
            Alive = true;


            ChannelV = Global.Game.Guild.CreateChannelAsync(Me.Username.RemoveSpecialChars(), ChannelType.Voice,
                Global.Game.DiscordChannels[GameChannel.PersoGroup]).GetAwaiter().GetResult();

            ChannelT = Global.Game.Guild.CreateChannelAsync(Me.Username.RemoveSpecialChars(), ChannelType.Text,
                Global.Game.DiscordChannels[GameChannel.PersoGroup]).GetAwaiter().GetResult();


            ChannelT.AddOverwriteAsync(Me, GameBuilder.UsrPerms);
            ChannelV.AddOverwriteAsync(Me, GameBuilder.UsrPerms);

            Global.Game.DiscordChannels[GameChannel.TownText].AddOverwriteAsync(Me, GameBuilder.UsrPerms);
            Global.Game.DiscordChannels[GameChannel.TownVoice].AddOverwriteAsync(Me, GameBuilder.UsrPerms);
            var embed = new DiscordEmbedBuilder
            {
                Title = Global.Game.Texts.GameRoles.RoleString,
                Color = Color.InfoColor
            };
            embed.AddField("Role", ToString());
            ChannelT.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();
            me.PlaceInAsync(ChannelV);
        }

        public virtual string GotKilled()
        {
            return $"{Me.DisplayName} {Global.Game.Texts.Annoucement.DeadMessagePublic}";
        }

        public abstract string GetClassName();
    }
}