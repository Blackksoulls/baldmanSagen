using BotDiscord.Env;
using BotDiscord.Env.Enum;
using DSharpPlus;
using DSharpPlus.Entities;

namespace BotDiscord.Roles
{
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

        public static async Task WolfVote()
        {
            Global.Game.NightTargets = new List<Personnage>();
            var embed = new DiscordEmbedBuilder()
            {
                Color = Color.PollColor,
                Title = Global.Game.Texts.Annoucement.NightlyWolfMessage
            };

            var msg = await Global.Game.DiscordChannels[GameChannel.WolfText].SendMessageAsync(embed: embed.Build());

            foreach (var personnage in Global.Game.PersonnagesList.FindAll(p => p.GetType() != typeof(Wolf) && p.Alive))
            {
                await msg.CreateReactionAsync(personnage.Emoji);
            }

            await Task.Delay(TimeToVote * 1000);
            msg = await Global.Game.DiscordChannels[GameChannel.WolfText].GetMessageAsync(msg.Id);
            var react = msg.Reactions.ToList().FindAll(reaction =>
                reaction.Count == msg.Reactions.Max(x => x.Count) && reaction.Count >= 2);
            Game.WriteDebug(react);
            if (react.Count > 0)
            {
                var target = Global.Game.PersonnagesList.Find(p => p.Emoji.Id == react[0].Emoji.Id);
                Global.Game.NightTargets.Add(target);
            }
            else
            {
                embed = new DiscordEmbedBuilder()
                {
                    Color = Color.InfoColor,
                    Title = Global.Game.Texts.Polls.NoWolfKill
                };
                await Global.Game.DiscordChannels[GameChannel.WolfText].SendMessageAsync(embed: embed.Build());
            }
        }
    }
}