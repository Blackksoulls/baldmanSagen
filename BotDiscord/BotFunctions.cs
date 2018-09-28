using System;
using System.Collections.Generic;
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
    internal class BotFunctions
    {
        public static DiscordMessage DailyVotingMessage;
        public static readonly int TimeToVote = 20;

        public static async Task DailyVote(int nbTry = 1)
        {

            var embed = new DiscordEmbedBuilder()
            {
                Title = Global.Game.Texts.Annoucement.DailyVoteMessage,
                Color = Color.PollColor
            };
            DailyVotingMessage =
                await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());

            var startTime = DateTime.Now;

            foreach (var personnage in Global.Game.PersonnagesList.FindAll(personnage => personnage.Alive))
            {
                Console.WriteLine($"Personnage : {personnage.Me.Username} -> {personnage.Emoji.Name}");
                await DailyVotingMessage.CreateReactionAsync(personnage.Emoji);
            }

            if (nbTry == 1)
            {
                Global.Client.MessageReactionAdded += ClientOnMessageReactionAdded;
            }

           
            await Task.Delay(TimeToVote * 1000);
            

            Console.WriteLine("Le temps est fini");
            DailyVotingMessage =
                await Global.Game.DiscordChannels[GameChannel.TownText].GetMessageAsync(DailyVotingMessage.Id);
            foreach (var discordReaction in DailyVotingMessage.Reactions)
            {
                Console.WriteLine($"Reaction : {discordReaction.Emoji.Name} : {discordReaction.Count}");
            }


            var emoji = DailyVotingMessage.Reactions
                .First(x => x.Count == DailyVotingMessage.Reactions.Max(y => y.Count) && x.Count >= 2).Emoji;

            if (emoji == null)
            {
                if (nbTry == 1)
                {
                    embed = new DiscordEmbedBuilder()
                    {
                        Title = Global.Game.Texts.Polls.NoTownKill,
                        Color = Color.InfoColor
                    };
                    await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed);
                }

                else
                {
                    await DailyVote(2);
                }
            }
            else
            {
                var p = Global.Game.PersonnagesList.Find(personnage => personnage.Emoji.Id == emoji.Id);
                await MakeDeath(p);
                embed = new DiscordEmbedBuilder()
                {
                    Title = Global.Game.Texts.Annoucement.DeadMessagePrivate,
                    Color = Color.InfoColor
                };
                await p.ChannelT.SendMessageAsync(embed: embed.Build());

                embed = new DiscordEmbedBuilder()
                {
                    Title = $"{p.GotKilled()}",
                    Color = Color.PollColor
                };
                await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build()); //HERE
            }
            Global.Game.CheckVictory();

            Global.Client.MessageReactionAdded -= ClientOnMessageReactionAdded;


        }


        private static async Task ClientOnMessageReactionAdded(MessageReactionAddEventArgs e)
        {
            var present = false;
            foreach (var personnage in Global.Game.PersonnagesList.FindAll(p => p.Alive))
            {
                if (e.Emoji == personnage.Emoji)
                {
                    present = true;
                }
            }

            if (!present ||
                (GameBuilder.GetMember(Global.Game.Guild, e.User)).Roles.Contains(Global.Roles[CustomRoles.Spectator]))
            {
                await DailyVotingMessage.DeleteReactionAsync(e.Emoji, e.User);
                return;
            }


            if (!e.User.IsBot && !GameBuilder.GetMember(Global.Game.Guild, e.User).Roles
                    .Contains(Global.Roles[CustomRoles.Spectator]))
            {
                foreach (var otherEmoji in (await Global.Game.Guild.GetEmojisAsync()))
                {
                    if (otherEmoji.Name != e.Emoji.Name)
                    {
                        await DailyVotingMessage.DeleteReactionAsync(otherEmoji, e.User,
                            $"{e.User.Username} already voted");
                    }
                }
            }
        }

        private static async Task OnReactionAddedCupidon(MessageReactionAddEventArgs e)
        {
            var game = Global.Game;
            var present = false;
            foreach (var personnage in game.PersonnagesList.FindAll(p => p.Alive))
            {
                if (e.Emoji == personnage.Emoji)
                {
                    present = true;
                }
            }

            if (!present ||
                (GameBuilder.GetMember(game.Guild, e.User)).Roles.Contains(Global.Roles[CustomRoles.Spectator]))
            {
                await DailyVotingMessage.DeleteReactionAsync(e.Emoji, e.User);
                return;
            }

            var cnt = 0;

            if (!e.User.IsBot && !GameBuilder.GetMember(game.Guild, e.User).Roles
                    .Contains(Global.Roles[CustomRoles.Spectator]))
            {
                foreach (var otherEmoji in (await game.Guild.GetEmojisAsync()))
                {
                    cnt++;
                    if (otherEmoji.Name != e.Emoji.Name && cnt > 1)
                    {
                        await DailyVotingMessage.DeleteReactionAsync(otherEmoji, e.User,
                            $"{e.User.Username} already voted");
                        cnt = 0; // 0  car on ajoute 1 avant le test
                    }
                }
            }
        }

        internal static async Task HunterDeath()
        {
            var hunter = Global.Game.PersonnagesList.Find(p => p.GetType() == typeof(Hunter));
            var message = await hunter.ChannelT.SendMessageAsync(Global.Game.Texts.GameRoles.HunterDeathQuestion);


            foreach (var emoji in (await Global.Game.Guild.GetEmojisAsync()).ToList()
                .FindAll(emo => emo.Id != hunter.Emoji.Id))
            {
                await message.CreateReactionAsync(emoji);
            }

            await Task.Delay(10 * 1000);

            var react = message.Reactions.First(reaction => reaction.Count == message.Reactions.Max(x => x.Count));
            var target = Global.Game.PersonnagesList.Find(p => p.Emoji.Id == react.Emoji.Id);
            await Global.Game.Kill(target);
            var embed = new DiscordEmbedBuilder()
            {
                Title =
                    $"{hunter.Me.Username} {Global.Game.Texts.Annoucement.PublicHunterMessage} {target.Me.Username}",
                Color = Color.DeadColor
            };
            await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
        }

        internal static async Task SeerAction()
        {
            var seer = Global.Game.PersonnagesList.Find(p => p.GetType() == typeof(Seer));
            if (seer != null)
            {
                var msg = await seer.ChannelT.SendMessageAsync(Global.Game.Texts.GameRoles.SeerAskMsg);

                foreach (var emoji in (await Global.Game.Guild.GetEmojisAsync()).ToList()
                    .FindAll(emo => emo.Id != seer.Emoji.Id))
                {
                    await msg.CreateReactionAsync(emoji);
                }

                await Task.Delay(TimeToVote);
                var react = msg.Reactions.ToList()
                    .FindAll(reaction => reaction.Count == msg.Reactions.Max(x => x.Count));

                var embed = new DiscordEmbedBuilder()
                {
                    Color = Color.InfoColor
                };

                if (react.Count > 0)
                {
                    var target = Global.Game.PersonnagesList.Find(p => p.Emoji.Id == react[0].Emoji.Id);
                    embed.Title = $"{Global.Game.Texts.GameRoles.SeerRecMsg} {target.GetClassName()}";
                }
                else
                {
                    embed.Title = $"{Global.Game.Texts.GameRoles.DidNothing}";
                }

                await seer.ChannelT.SendMessageAsync(embed: embed.Build());



            }
        }

        internal static async Task WitchMoment()
        {
            var witch = Global.Game.PersonnagesList.Find(p => p.GetType() == typeof(Witch));

            if (witch != null)
            {
                var witchCh = witch.ChannelT;
                var embed = new DiscordEmbedBuilder()
                {
                    Color = Color.PollColor,
                    Title = $"{Global.Game.NightTargets[0]} {Global.Game.Texts.GameRoles.WitchSaveMsg}"
                };

                var healMsg = await witchCh.SendMessageAsync(embed: embed.Build());
                await healMsg.CreateReactionAsync(DiscordEmoji.FromName(Global.Client, ":thumbsup:"));
                await healMsg.CreateReactionAsync(DiscordEmoji.FromName(Global.Client, ":thumbsdown:"));

                Global.Client.MessageReactionAdded += ClientOnMessageReactionAdded;

                await Task.Delay(TimeToVote / 2);
                healMsg = await witchCh.GetMessageAsync(healMsg.Id);
                if (healMsg.GetReactionsAsync(DiscordEmoji.FromName(Global.Client, ":thumbsup:")).GetAwaiter()
                        .GetResult()
                        .Count == 2)
                {
                    Global.Game.NightTargets.Clear();
                }

                embed = new DiscordEmbedBuilder()
                {
                    Color = Color.PollColor,
                    Title = Global.Game.Texts.GameRoles.WitchKillMsg,
                };
                var killMsg = await witchCh.SendMessageAsync(embed: embed.Build());
                foreach (var emoji in Global.Game.Guild.GetEmojisAsync().GetAwaiter().GetResult().ToList()
                    .FindAll(e => e.Id != witch.Emoji.Id))
                {
                    await killMsg.CreateReactionAsync(emoji);
                }

                Global.Client.MessageReactionAdded += ClientOnMessageReactionAdded;

                await Task.Delay(TimeToVote / 2);
                killMsg = await witchCh.GetMessageAsync(killMsg.Id);
                Global.Game.NightTargets.Add(Global.Game.PersonnagesList.Find(p =>
                    p.Emoji == killMsg.Reactions.ToList().Find(r => r.Count == 2).Emoji));
            }
        }

        internal static async Task WolfVote()
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


        internal static async Task CupidonChoice()
        {
            var channel = Global.Game.PersonnagesList.Find(p => p.GetType() == typeof(Cupidon)).ChannelT;

            if (channel != null)
            {
                var embed = new DiscordEmbedBuilder()
                {
                    Color = Color.PollColor,
                    Title = Global.Game.Texts.GameRoles.CupidMessage
                };
                var msg = await channel.SendMessageAsync(embed: embed.Build());
                Global.Client.MessageReactionAdded += OnReactionAddedCupidon;

                await Task.Delay(TimeToVote);

                msg = await channel.GetMessageAsync(msg.Id);

                var react = msg.Reactions.ToList()
                    .FindAll(reaction => reaction.Count == msg.Reactions.Max(x => x.Count));

                var target = new[]
                {
                    Global.Game.PersonnagesList.Find(p => p.Emoji.Id == react[0].Emoji.Id),
                    Global.Game.PersonnagesList.Find(p => p.Emoji.Id == react[1].Emoji.Id)
                };

                foreach (var personnage in target)
                {
                    personnage.Effect = Effect.Lover;
                }

                Global.Client.MessageReactionAdded -= OnReactionAddedCupidon;
            }
        }


        public static async Task EndNight()
        {
            Game.WriteDebug("Fin de la nuit");

            foreach (var p in Global.Game.PersonnagesList)
            {
                Permissions Sends = GameBuilder.CreatePerms(Permissions.SendMessages, Permissions.SendTtsMessages);
                await Global.Game.DiscordChannels[GameChannel.WolfText].AddOverwriteAsync(p.Me, deny: Sends);
            }


            foreach (var target in Global.Game.NightTargets)
            {
                var embed = new DiscordEmbedBuilder()
                {
                    Title = $"{Global.Game.Texts.Annoucement.DeadMessagePrivate}",
                    Color = Color.DeadColor
                };
                Game.WriteDebug("MEUTRE DE " + target.Me.Username);
                await target.ChannelT.SendMessageAsync(embed: embed.Build());//HERE
                await MakeDeath(target);
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


            if (p.GetType() == typeof(Hunter))
            {
                Global.Game.Moments.Push(Moment.HunterDead);
            }

            if (p.Effect == Effect.Lover)
            {
                var loved = Global.Game.PersonnagesList.Find(p2 => p2.Effect == Effect.Lover && p != p2);
                var embed = new DiscordEmbedBuilder()
                {
                    Title = $"{loved.Me.Username} {Global.Game.Texts.Annoucement.LoveSuicide}",
                    Color = Color.LoveColor
                };
                await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
                await Global.Game.Kill(loved);
            }
        }

        public static Task LittleGirlAction()
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        public static async Task NightAnnoucement()
        {
            var embed = new DiscordEmbedBuilder()
            {
                Title = Global.Game.Texts.Annoucement.NightAnnoucement,
                Color = Color.InfoColor
            };
            await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
            var sends = GameBuilder.CreatePerms(Permissions.SendMessages, Permissions.SendTtsMessages); 
            foreach (var p in Global.Game.PersonnagesList.FindAll(p => p.Alive))
            {
                if (p.GetType() != typeof(Wolf))
                {
                    await p.Me.PlaceInAsync(p.ChannelV);
                }
                else
                {
                    await p.Me.PlaceInAsync(Global.Game.DiscordChannels[GameChannel.WolfVoice]);
                    await Global.Game.DiscordChannels[GameChannel.WolfText].AddOverwriteAsync(p.Me, deny: sends);
                }
            }
        }

        public static async Task DayAnnoucement()
        {
            var embed = new DiscordEmbedBuilder()
            {
                Title = Global.Game.Texts.Annoucement.DayAnnoucement,
                Color = Color.InfoColor
            };
            await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
            foreach (var p in Global.Game.PersonnagesList.FindAll(p => p.Alive))
            {
                await p.Me.PlaceInAsync(Global.Game.DiscordChannels[GameChannel.TownVoice]);
            }
        }
    }
}