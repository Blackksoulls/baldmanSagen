using System;
using System.Linq;
using System.Threading.Tasks;
using BotDiscord.Env;
using BotDiscord.Env.Enum;
using BotDiscord.Roles;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace BotDiscord
{
    public class BotFunctions
    {
        public static DiscordMessage DailyVotingMessage { get; private set; }
        public static DiscordMessage DeadVotingMessage { get; private set; }
        public static bool Attendre
		{
			get => Attendre;
            set => Attendre = value;
        }


        public static async Task DeadVote(int nbTry = 1)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = Global.Game.Texts.Polls.DeadVoteMessage,
                Color = Color.PollColor
            };
            DeadVotingMessage =
                await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
            
            foreach (var personnage in Global.Game.PersonnagesList.FindAll(personnage => personnage.Alive))
            {
                Console.WriteLine($"Personnage : {personnage.Me.Username} -> {personnage.Emoji.Name}");
                await DeadVotingMessage.CreateReactionAsync(personnage.Emoji);
            }

            if (nbTry == 1) Global.Client.MessageReactionAdded += ClientOnMessageReactionAdded;


            Console.WriteLine("Le temps est fini");
            DeadVotingMessage =
                await Global.Game.DiscordChannels[GameChannel.TownText].GetMessageAsync(DeadVotingMessage.Id);
            //Attendre

/*            foreach (var discordReaction in (await Global.Game.Guild.GetEmojisAsync()))
            {
                foreach (var discordUserReact in (await DeadVotingMessage.GetReactionsAsync()))
                {
                    Console.WriteLine($"Reaction : {discordReaction.Emoji.Name} : {discordReaction.Count}");
                }
            }*/

            var players = DeadVotingMessage.Reactions.ToList().FindAll(x =>
                x.Count == DeadVotingMessage.Reactions.Max(y => y.Count) && x.Count >= 2);


            if (players.Count == 0)
            {
                embed = new DiscordEmbedBuilder
                {
                    Title = Global.Game.Texts.Polls.NoTownKill,
                    Color = Color.InfoColor
                };
                await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed);
            }
            else
            {
                var p = Global.Game.PersonnagesList.Find(personnage => personnage.Emoji.Id == players[0].Emoji.Id);
                await MakeDeath(p);
            }

            Global.Game.CheckVictory();

            Global.Client.MessageReactionAdded -= ClientOnMessageReactionAdded;
        }


        public static async Task DailyVote(int nbTry = 1)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = Global.Game.Texts.Polls.DailyVoteMessage,
                Color = Color.PollColor
            };
            DailyVotingMessage =
                await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
            _ = Task.Run(DeadVote);
            var startTime = DateTime.Now;

            foreach (var personnage in Global.Game.PersonnagesList.FindAll(personnage => personnage.Alive))
            {
                Console.WriteLine($"Personnage : {personnage.Me.Username} -> {personnage.Emoji.Name}");
                await DailyVotingMessage.CreateReactionAsync(personnage.Emoji);
            }

            if (nbTry == 1) Global.Client.MessageReactionAdded += ClientOnMessageReactionAdded;


            await Task.Delay(Global.Config.DayVoteTime );


            Console.WriteLine("Le temps est fini");
            DailyVotingMessage =
                await Global.Game.DiscordChannels[GameChannel.TownText].GetMessageAsync(DailyVotingMessage.Id);
            foreach (var discordReaction in DailyVotingMessage.Reactions)
            {
                Console.WriteLine($"Reaction : {discordReaction.Emoji.Name} : {discordReaction.Count}");
            }


            var players = DailyVotingMessage.Reactions.ToList().FindAll(x =>
                x.Count == DailyVotingMessage.Reactions.Max(y => y.Count) && x.Count >= 2);


            if (players.Count == 0)
            {
                embed = new DiscordEmbedBuilder
                {
                    Title = Global.Game.Texts.Polls.NoTownKill,
                    Color = Color.InfoColor
                };
                await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed);
            }
            else
            {
                var p = Global.Game.PersonnagesList.Find(personnage => personnage.Emoji.Id == players[0].Emoji.Id);
                await MakeDeath(p);
            }

            Global.Game.CheckVictory();

            Global.Client.MessageReactionAdded -= ClientOnMessageReactionAdded;
        }


        public static async Task ClientOnMessageReactionAdded(MessageReactionAddEventArgs e)
        {
            var present = false;
            foreach (var personnage in Global.Game.PersonnagesList.FindAll(p => p.Alive))
                if (e.Emoji == personnage.Emoji)
                    present = true;

            if (!present || e.User.GetMember().Roles.Contains(Global.Roles[PublicRole.Spectator]))
            {
                await DailyVotingMessage.DeleteReactionAsync(e.Emoji, e.User);
                return;
            }


            if (!e.User.IsBot && !GameBuilder.GetMember(Global.Game.Guild, e.User).Roles
                    .Contains(Global.Roles[PublicRole.Spectator]))
                foreach (var otherEmoji in await Global.Game.Guild.GetEmojisAsync())
                    if (otherEmoji.Name != e.Emoji.Name)
                        await DailyVotingMessage.DeleteReactionAsync(otherEmoji, e.User,
                            $"{e.User.Username} already voted");
        }


        public static async Task EndNight()
        {
            Game.WriteDebug("Fin de la nuit");

            foreach (var p in Global.Game.PersonnagesList.FindAll(p => p.GetType() == typeof(Wolf)))
            {
                var sends = GameBuilder.CreatePerms(Permissions.SendMessages, Permissions.SendTtsMessages);
                var others = GameBuilder.CreatePerms(Permissions.AccessChannels, Permissions.ReadMessageHistory);
                await Global.Game.DiscordChannels[GameChannel.WolfText].AddOverwriteAsync(p.Me, others, sends);
            }


            foreach (var target in Global.Game.NightTargets)
            {
                var embed = new DiscordEmbedBuilder
                {
                    Title = $"{Global.Game.Texts.Annoucement.DeadMessagePrivate}",
                    Color = Color.DeadColor
                };
                Game.WriteDebug("MEUTRE DE " + target.Me.Username);
                await target.ChannelT.SendMessageAsync(embed: embed.Build()); //PAS HERE (Message Privé)
                await MakeDeath(target); // Here
            }

            Global.Game.CheckVictory();
        }

        public static Task Elections()
        {
            throw new NotImplementedException();
        }

        public static async Task MakeDeath(Personnage p)
        {
            await Global.Game.Kill(p);


            if (p.GetType() == typeof(Hunter)) Global.Game.Moments.Push(Moment.HunterDead);

            if (p.Effect == Effect.Lover)
            {
                var loved = Global.Game.PersonnagesList.Find(p2 => p2.Effect == Effect.Lover && p != p2);
                var embed = new DiscordEmbedBuilder
                {
                    Title = $"{loved.Me.Username} {Global.Game.Texts.Annoucement.LoveSuicide}",
                    Color = Color.LoveColor
                };
                await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
                await Global.Game.Kill(loved);
            }
        }

        public static async Task NightAnnoucement()
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = Global.Game.Texts.Annoucement.NightAnnoucement,
                Color = Color.InfoColor
            };
            await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
            var sends = GameBuilder.CreatePerms(Permissions.AccessChannels, Permissions.SendMessages,
                Permissions.SendTtsMessages);
            foreach (var p in Global.Game.PersonnagesList.FindAll(p => p.Alive))
                if (p.GetType() != typeof(Wolf))
                {
                    await p.Me.PlaceInAsync(p.ChannelV);
                }
                else
                {
                    await p.Me.PlaceInAsync(Global.Game.DiscordChannels[GameChannel.WolfVoice]);
                    await Global.Game.DiscordChannels[GameChannel.WolfText].AddOverwriteAsync(p.Me, sends);
                }
        }

        public static async Task DayAnnoucement()
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = Global.Game.Texts.Annoucement.DayAnnoucement,
                Color = Color.InfoColor
            };
            await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
            foreach (var p in Global.Game.PersonnagesList.FindAll(p => p.Alive))
                await p.Me.PlaceInAsync(Global.Game.DiscordChannels[GameChannel.TownVoice]);
        }

        internal static Task DeadVote()
        {
            throw new NotImplementedException();
        }
    }
}