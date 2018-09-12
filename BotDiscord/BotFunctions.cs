using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotDiscord.Env;
using BotDiscord.Env.Enum;
using BotDiscord.Roles;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace BotDiscord
{
    internal class BotFunctions
    {
        public static DiscordMessage DailyVotingMessage;
        public static readonly int TimeToVote = 10_000;

        public static async Task DailyVote(Game game)
        {
            var embed = new DiscordEmbedBuilder()
            {
                Title = game.Texts.DailyVoteMessage,
                Color = Color.PollColor
            };
            DailyVotingMessage = await game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);

            var startTime = DateTime.Now;

            foreach (var personnage in game.PersonnagesList.FindAll(personnage => personnage.Alive))
            {
                Console.WriteLine($"Personnage : {personnage.Me.Username} -> {personnage.Emoji.Name}");
                await DailyVotingMessage.CreateReactionAsync(personnage.Emoji).ConfigureAwait(true);
            }
            //Global.currGame = 
            Global.Client.MessageReactionAdded += ClientOnMessageReactionAdded;

            while (DateTime.Now < startTime.AddSeconds(TimeToVote))
            {
                await Task.Delay(500).ConfigureAwait(true);
            }

            Console.WriteLine("Le temps est fini");
            DailyVotingMessage = await game.DiscordChannels[GameChannel.TownText].GetMessageAsync(DailyVotingMessage.Id).ConfigureAwait(true);
            foreach (var discordReaction in DailyVotingMessage.Reactions)
            {
                Console.WriteLine($"Reaction : {discordReaction.Emoji.Name} : {discordReaction.Count}");
            }

            try
            {
                var emoji = DailyVotingMessage.Reactions.First(x => x.Count == DailyVotingMessage.Reactions.Max(y => y.Count)).Emoji;
                var p = game.PersonnagesList.Find(personnage => personnage.Emoji.Id == emoji.Id);
                await MakeDeath(game, p).ConfigureAwait(true);
                embed = new DiscordEmbedBuilder()
                {
                    Title = game.Texts.DeadMessagePrivate,
                    Color = Color.InfoColor
                };
                await p.ChannelT.SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);

                embed = new DiscordEmbedBuilder()
                {
                    Title = $"{p.GotKilled()}",
                    Color = Color.PollColor
                };
                await game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            Global.Client.MessageReactionAdded -= ClientOnMessageReactionAdded;
        }

        private static Task ClientOnMessageReactionAdded(MessageReactionAddEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static async Task ClientOnMessageReactionAdded(MessageReactionAddEventArgs e, Game game)
        {
            var present = false;
            foreach (var personnage in game.PersonnagesList.FindAll(p => p.Alive))
            {
                if (e.Emoji == personnage.Emoji)
                {
                    present = true;
                }
            }

            if (!present || (GameBuilder.GetMember(game.Guild, e.User)).Roles.Contains(Global.Roles[CustomRoles.Spectator]))
            {
                await DailyVotingMessage.DeleteReactionAsync(e.Emoji, e.User).ConfigureAwait(true);
                return;
            }


            if (!e.User.IsBot && !GameBuilder.GetMember(game.Guild, e.User).Roles.Contains(Global.Roles[CustomRoles.Spectator]))
            {
                foreach (var otherEmoji in (await game.Guild.GetEmojisAsync().ConfigureAwait(true)))
                {
                    if (otherEmoji.Name != e.Emoji.Name)
                    {
                        await DailyVotingMessage.DeleteReactionAsync(otherEmoji, e.User,
                            $"{e.User.Username} already voted").ConfigureAwait(true);
                    }
                }
            }
        }

        private static async Task OnReactionAddedCupidon(MessageReactionAddEventArgs e)
        {

            Game Game = Global.Games[Global.currGame];
            var present = false;
            foreach (var personnage in Game.PersonnagesList.FindAll(p => p.Alive))
            {
                if (e.Emoji == personnage.Emoji)
                {
                    present = true;
                }
            }

            if (!present || (GameBuilder.GetMember(Game.Guild, e.User)).Roles.Contains(Global.Roles[CustomRoles.Spectator]))
            {
                await DailyVotingMessage.DeleteReactionAsync(e.Emoji, e.User).ConfigureAwait(true);
                return;
            }

            var cnt = 0;

            if (!e.User.IsBot && !GameBuilder.GetMember(Game.Guild, e.User).Roles.Contains(Global.Roles[CustomRoles.Spectator]))
            {
                foreach (var otherEmoji in (await Game.Guild.GetEmojisAsync().ConfigureAwait(true)))
                {
                    cnt++;
                    if (otherEmoji.Name != e.Emoji.Name && cnt > 1)
                    {
                        await DailyVotingMessage.DeleteReactionAsync(otherEmoji, e.User,
                            $"{e.User.Username} already voted").ConfigureAwait(true);
                        cnt = 0; // 0  car on ajoute 1 avant le test
                    }
                }
            }
        }

        internal static async Task HunterDeath(Game game)
        {
            var hunter = game.PersonnagesList.Find(p => p.GetType() == typeof(Hunter));
            var message = await hunter.ChannelT.SendMessageAsync(game.Texts.HunterDeathQuestion).ConfigureAwait(true);


            foreach (var emoji in (await game.Guild.GetEmojisAsync().ConfigureAwait(true)).ToList()
                .FindAll(emo => emo.Id != hunter.Emoji.Id))
            {
                await message.CreateReactionAsync(emoji).ConfigureAwait(true);
            }

            await Task.Delay(10 * 1000).ConfigureAwait(true);

            var react = message.Reactions.First(reaction => reaction.Count == message.Reactions.Max(x => x.Count));
            var target = game.PersonnagesList.Find(p => p.Emoji.Id == react.Emoji.Id);
            await game.Kill(target).ConfigureAwait(true);
            var embed = new DiscordEmbedBuilder()
            {
                Title = $"{hunter.Me.Username} {game.Texts.PublicHunterMessage} {target.Me.Username}",
                Color = Color.DeadColor
            };
            await game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);
        }

        internal static async Task SeerAction(Game game)
        {
            var seer = game.PersonnagesList.Find(p => p.GetType() == typeof(Seer));
            if (seer != null)
            {
                var msg = await seer.ChannelT.SendMessageAsync(game.Texts.SeerAskMsg).ConfigureAwait(true);

                foreach (var emoji in (await game.Guild.GetEmojisAsync().ConfigureAwait(true)).ToList()
                    .FindAll(emo => emo.Id != seer.Emoji.Id))
                {
                    await msg.CreateReactionAsync(emoji).ConfigureAwait(true);
                }

                await Task.Delay(TimeToVote).ConfigureAwait(true);
                var react = msg.Reactions.First(reaction => reaction.Count == msg.Reactions.Max(x => x.Count));
                var target = game.PersonnagesList.Find(p => p.Emoji.Id == react.Emoji.Id);
                var embed = new DiscordEmbedBuilder()
                {
                    Title = $"{game.Texts.SeerRecMsg} {target.GetClassName()}",
                    Color = Color.InfoColor
                };
                await seer.ChannelT.SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);
            }
        }

        internal static async Task WitchMoment(Game game)
        {
            var witch = game.PersonnagesList.Find(p => p.GetType() == typeof(Witch));

            if (witch != null)
            {
                var witchCh = witch.ChannelT;
                var embed = new DiscordEmbedBuilder()
                {
                    Color = Color.PollColor,
                    Title = $"{game.NightTargets[0]} {game.Texts.WitchSaveMsg}"
                };

                var healMsg = await witchCh.SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);
                await healMsg.CreateReactionAsync(DiscordEmoji.FromName(Global.Client, ":thumbsup:")).ConfigureAwait(true);
                await healMsg.CreateReactionAsync(DiscordEmoji.FromName(Global.Client, ":thumbsdown:")).ConfigureAwait(true);

                Global.Client.MessageReactionAdded += ClientOnMessageReactionAdded;

                await Task.Delay(TimeToVote / 2).ConfigureAwait(true);
                healMsg = await witchCh.GetMessageAsync(healMsg.Id).ConfigureAwait(true);
                if (healMsg.GetReactionsAsync(DiscordEmoji.FromName(Global.Client, ":thumbsup:")).GetAwaiter().GetResult()
                        .Count == 2)
                {
                    game.NightTargets.Clear();
                }

                embed = new DiscordEmbedBuilder()
                {
                    Color = Color.PollColor,
                    Title = game.Texts.WitchKillMsg,
                };
                var killMsg = await witchCh.SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);
                foreach (var emoji in game.Guild.GetEmojisAsync().GetAwaiter().GetResult().ToList()
                    .FindAll(e => e.Id != witch.Emoji.Id))
                {
                    await killMsg.CreateReactionAsync(emoji).ConfigureAwait(true);
                }

                Global.Client.MessageReactionAdded += ClientOnMessageReactionAdded;

                await Task.Delay(TimeToVote / 2).ConfigureAwait(true);
                killMsg = await witchCh.GetMessageAsync(killMsg.Id).ConfigureAwait(true);
                game.NightTargets.Add(game.PersonnagesList.Find(p =>
                    p.Emoji == killMsg.Reactions.ToList().Find(r => r.Count == 2).Emoji));
            }
        }

        internal static async Task WolfVote(Game game)
        {
            game.NightTargets = new List<Personnage>();
            var embed = new DiscordEmbedBuilder()
            {
                Color = Color.PollColor,
                Title = game.Texts.NightlyWolfMessage
            };

            var msg = await game.DiscordChannels[GameChannel.WolfText].SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);

            foreach (var personnage in game.PersonnagesList.FindAll(p => p.GetType() != typeof(Wolf) && p.Alive))
            {
                await msg.CreateReactionAsync(personnage.Emoji).ConfigureAwait(true);
            }

            await Task.Delay(TimeToVote).ConfigureAwait(true);
            msg = await game.DiscordChannels[GameChannel.WolfText].GetMessageAsync(msg.Id).ConfigureAwait(true);
            var react = msg.Reactions.First(reaction => reaction.Count == msg.Reactions.Max(x => x.Count));
            var target = game.PersonnagesList.Find(p => p.Emoji.Id == react.Emoji.Id);

            game.NightTargets.Add(target);
        }


        internal static async Task CupidonChoice(Game game)
        {
            var channel = game.PersonnagesList.Find(p => p.GetType() == typeof(Cupidon)).ChannelT;

            if (channel != null)
            {
                var embed = new DiscordEmbedBuilder()
                {
                    Color = Color.PollColor,
                    Title = game.Texts.CupidMessage
                };

                var msg = await channel.SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);
                Global.Client.MessageReactionAdded += OnReactionAddedCupidon;

                await Task.Delay(TimeToVote).ConfigureAwait(true);

                msg = await channel.GetMessageAsync(msg.Id).ConfigureAwait(true);

                var react = msg.Reactions.ToList()
                    .FindAll(reaction => reaction.Count == msg.Reactions.Max(x => x.Count));

                var target = new[]
                {
                    game.PersonnagesList.Find(p => p.Emoji.Id == react[0].Emoji.Id),
                    game.PersonnagesList.Find(p => p.Emoji.Id == react[1].Emoji.Id)
                };

                foreach (var personnage in target)
                {
                    personnage.Effect = Effect.Lover;
                }

                Global.Client.MessageReactionAdded -= OnReactionAddedCupidon;
            }
        }


        public static async Task EndNight(Game game)
        {
            foreach (var target in game.NightTargets)
            {
                var embed = new DiscordEmbedBuilder()
                {
                    Title = $"{game.Texts.DeadMessagePrivate}",
                    Color = Color.DeadColor
                };
                await target.ChannelT.SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);
                await MakeDeath(game, target).ConfigureAwait(true);
            }

            game.CheckVictory();
        }

        public static async Task Elections(Game game)
        {
            throw new NotImplementedException();
        }

        public static async Task MakeDeath(Game game, Personnage p)
        {
            await game.Kill(p).ConfigureAwait(true);


            if (p.GetType() == typeof(Hunter))
            {
                game.Moments.Push(Moment.HunterDead);
            }

            if (p.Effect == Effect.Lover)
            {
                var loved = game.PersonnagesList.Find(p2 => p2.Effect == Effect.Lover && p != p2);
                var embed = new DiscordEmbedBuilder()
                {
                    Title = $"{loved.Me.Username} {game.Texts.LoveSuicide}",
                    Color = Color.LoveColor
                };
                await game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);
                await game.Kill(loved).ConfigureAwait(true);
            }
        }

        public static Task LittleGirlAction(Game Game)
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        public static async Task NightAnnoucement(Game game)
        {
            var embed = new DiscordEmbedBuilder()
            {
                Title = game.Texts.NightAnnoucement,
                Color = Color.InfoColor
            };
            await game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);
            foreach (var p in game.PersonnagesList.FindAll(p => p.Alive))
            {
                if (p.GetType() != typeof(Wolf))
                {
                    await p.Me.PlaceInAsync(p.ChannelV).ConfigureAwait(true);
                }
                else
                {
                    await p.Me.PlaceInAsync(game.DiscordChannels[GameChannel.WolfVoice]).ConfigureAwait(true);
                }
            }
        }

        public static async Task DayAnnoucement(Game game)
        {
            var embed = new DiscordEmbedBuilder()
            {
                Title = game.Texts.DayAnnoucement,
                Color = Color.InfoColor
            };
            await game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);
            foreach (var p in game.PersonnagesList.FindAll(p => p.Alive))
            {
                await p.Me.PlaceInAsync(game.DiscordChannels[GameChannel.TownVoice]).ConfigureAwait(true);
            }
        }
    }
}