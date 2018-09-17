using System;
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
            Global.Game.DiscordChannels[GameChannel.WolfText].AddOverwriteAsync(me, GameBuilder.UsrPerms);
            Global.Game.DiscordChannels[GameChannel.WolfVoice].AddOverwriteAsync(me, GameBuilder.UsrPerms);
        }

        public override string ToString()
        {
            return Global.Game.Texts.WolfToString;
        }

        public override string GotKilled()
        {
            return base.GotKilled() + $"{Global.Game.Texts.WolfName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.WolfName;
        }
    }


    public class Citizen : Personnage
    {
        public Citizen(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.CitizenToString;
        }

        public override string GotKilled()
        {
            return base.GotKilled() + $"{Global.Game.Texts.CitizenName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.CitizenName;
        }
    }


    public class Salvator : Citizen
    {
        public Salvator(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.SaviorToString + " \n " + Global.Game.Texts.TownFriendly;
        }

        public override string GotKilled()
        {
            return base.GotKilled() + $"{Global.Game.Texts.SaviorName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.SaviorName;
        }
    }


    public class Witch : Citizen
    {
        public Witch(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }


        public override string ToString()
        {
            return Global.Game.Texts.WitchToString + " \n " + Global.Game.Texts.TownFriendly;
        }

        public override string GotKilled()
        {
            return base.GotKilled() + $"{Global.Game.Texts.WitchName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.WitchName;
        }
    }

    public class LittleGirl : Citizen
    {
        public LittleGirl(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }


        public override string ToString()
        {
            return Global.Game.Texts.LittleGirlToString + " \n " + Global.Game.Texts.TownFriendly;
        }

        public override string GotKilled()
        {
            return base.GotKilled() + $"{Global.Game.Texts.LittleGirlName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.LittleGirlName;
        }
    }

    public class Hunter : Citizen
    {
        public Hunter(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.HunterToString + " \n " + Global.Game.Texts.TownFriendly;
        }

        public override string GotKilled()
        {
            return base.GotKilled() + $"{Global.Game.Texts.HunterName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.HunterName;
        }
    }

    public class Cupidon : Citizen
    {
        public Cupidon(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.CupidToString + " \n " + Global.Game.Texts.TownFriendly;
        }

        public override string GotKilled()
        {
            return Language.FirstDieMessages(Global.Game, Me) + Global.Game.Texts.CupidName;
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.CupidName;
        }
    }

    public class Seer : Citizen
    {
        public Seer(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.SeerToString + " \n " + Global.Game.Texts.TownFriendly;
        }

        public override string GotKilled()
        {
            return base.GotKilled() + $"{Global.Game.Texts.SeerName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.SeerName;
        }
    }

    public class TalkativeSeer : Seer
    {
        public TalkativeSeer(DiscordMember me, DiscordGuildEmoji emoji) : base(me, emoji)
        {
        }

        public override string ToString()
        {
            return Global.Game.Texts.SeerToString + " \n " + Global.Game.Texts.TalkativeSeerToString + " \n " +
                   Global.Game.Texts.TownFriendly;
        }

        public override string GotKilled()
        {
            return base.GotKilled() + $"{Global.Game.Texts.TalkativeSeerName}";
        }

        public override string GetClassName()
        {
            return Global.Game.Texts.TalkativeSeerName;
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
            var name = Me.Username.RemoveSpecialChars() ?? "jesaispasquoi";


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
                Title = Global.Game.Texts.RoleString,
                Color = Color.InfoColor
            };
            embed.AddField("Role", ToString());
            ChannelT.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();
            me.PlaceInAsync(ChannelV);
        }

        public virtual string GotKilled()
        {
            return Me.DisplayName + Global.Game.Texts.DeadMessagePublic;
        }

        public abstract string GetClassName();
    }
}