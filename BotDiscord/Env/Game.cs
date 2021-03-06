﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using BotDiscord.Env.Enum;
using BotDiscord.Locale;
using BotDiscord.Roles;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Exceptions;
using Newtonsoft.Json;

namespace BotDiscord.Env
{
    public class Game
    {
        public Dictionary<GameChannel, DiscordChannel> DiscordChannels;
        public Language Texts;
        public bool Wait = true;
        public List<Personnage> PersonnagesList;
        public Victory Victory = Victory.None;
        public ulong GuildId;
        public DiscordGuild Guild;
        public Stack<Moment> Moments { get; set; }
        public List<Personnage> NightTargets { get; set; }

        public int Laps;


        public Game(string lang)
        {
            SetLanguage(lang);
        }


        public void SetLanguage(string lang) => Texts = JsonConvert.DeserializeObject<Language>(
            File.ReadAllText
            ($@"..//Locale/{lang}/lang.json",
                Encoding.UTF8));


        public async Task CreateGuild(CommandContext e)
        {
            try
            {
                if (Global.InGame)
                {
                    var embed = new DiscordEmbedBuilder
                    {
                        Color = Color.InfoColor,
                        Title = Texts.Errors.AnotherGameIsPlaying
                    };
                    await e.Channel.SendMessageAsync(embed: embed.Build());
                }
                else
                {
                    try
                    {
                        Global.Client = e.Client;


                        var msgs = (await e.Guild.GetDefaultChannel().GetMessagesAsync(10)).ToList()
                            .FindAll(m => m.Author == e.Client.CurrentUser || m.Content.Contains("!go"));
                        if (msgs.Count > 0)
                        {
                            await e.Guild.GetDefaultChannel().DeleteMessagesAsync(msgs);
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }

                    Console.WriteLine(File.ReadAllText(@"..//Locale/fr/lang.json", Encoding.UTF8));
                    while (Guild == null)
                    {
                        try
                        {
                            Guild = e.Client.CreateGuildAsync("Loup Garou").GetAwaiter().GetResult();
                            GuildId = Guild.Id;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }

                    await Guild.ModifyAsync(x => x.SystemChannel = new Optional<DiscordChannel>(null));
                    await Guild.ModifyAsync(opt =>
                        opt.DefaultMessageNotifications = DefaultMessageNotifications.MentionsOnly);
                    Global.InGame = true;

                    WriteDebug("1");


                    await GameBuilder.CreateDiscordRoles(); // Role Admin, Joueur, Spectateur
                    WriteDebug("2");
                    await Global.Client.CurrentUser.GetMember().GrantRoleAsync(Global.Roles[PublicRole.Admin]);
                    WriteDebug("3");
                    await (await Guild.GetAllMembersAsync()).First()
                        .ModifyAsync(m => m.Nickname = Texts.DiscordRoles.BotName);
                    WriteDebug("4");
                    Console.WriteLine("Guild Created");
                    DiscordChannels = new Dictionary<GameChannel, DiscordChannel>();

                    await e.TriggerTypingAsync();

                    var generalChannel = Guild.GetDefaultChannel();
                    await generalChannel.ModifyAsync(x => x.Name = "Bot");
                    DiscordChannels.Add(GameChannel.BotText, generalChannel);

                    var botVChannel = await Guild.CreateChannelAsync("Bot", ChannelType.Voice, generalChannel.Parent);
                    DiscordChannels.Add(GameChannel.BotVoice, botVChannel);
                    Global.Client.GuildMemberAdded += NewGuildMember;
                    Global.Client.GuildMemberAdded += StartMember;


                    var inv = await generalChannel.CreateInviteAsync();

                    var msgInv = await e.RespondAsync(inv.ToString());

                    var embed = new DiscordEmbedBuilder
                    {
                        Title = Texts.Annoucement.BotWantPlay,
                        Color = Color.PollColor
                    };
                    await generalChannel.SendMessageAsync(embed: embed.Build());

                    var players = new List<DiscordMember>();


                    await Task.Delay(Global.Config.JoinTime);

                    foreach (var usr in botVChannel.Users)
                    {
                        await usr.RevokeRoleAsync(Global.Roles[PublicRole.Spectator]);
                        await usr.GrantRoleAsync(Global.Roles[PublicRole.Player]);
                        players.Add(usr);
                        WriteDebug(usr.Username);
                    }

                    Global.Client.GuildMemberAdded -= StartMember;
                    Global.Client.MessageReactionAdded += Listeners.PreventMultiVote;
                    Global.Client.MessageReactionAdded += Listeners.PreventSpectatorFromVote;


                    Console.WriteLine(13);
                    try
                    {
                        GameBuilder.Debug(this);
                        var channelsToRemove = new List<DiscordChannel>();
                        while (Guild.Channels.Count != DiscordChannels.Count)
                        {
                            foreach (var c in Guild.Channels)
                                try
                                {
                                    if (!DiscordChannels.ContainsValue(c)) channelsToRemove.Add(c);
                                }
                                catch (NotFoundException exception)
                                {
                                    Console.WriteLine(exception.JsonMessage);
                                }

                            foreach (var dm in channelsToRemove) await dm.DeleteAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }


                    Console.WriteLine("Supr fini");

                    await RoleAssignment(msgInv, e, players);

                    foreach (var p in PersonnagesList)
                    {
                        WriteDebug($"Y : {p.Me.Username}");

                        var usr = GameBuilder.GetMember(Guild, p.Me);

                        await DiscordChannels[GameChannel.BotVoice].AddOverwriteAsync(usr, Permissions.None,
                            GameBuilder.CreatePerms(Permissions.AccessChannels, Permissions.UseVoice));
                    }


                    if (PersonnagesList.Count < 2)
                    {
                        Victory = Victory.NotPlayable;
                        embed = new DiscordEmbedBuilder
                        {
                            Title = $"{Texts.Errors.NotEnoughPlayer} {PersonnagesList.Count}",
                            Color = Color.InfoColor
                        };
                        await DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
                        await Task.Delay(Global.Config.JoinTime);

                        await Global.Game.Guild.DeleteAsync();
                    }
                    else
                    {
                        e.Client.VoiceStateUpdated += Listeners.KillLeaver;
                    }

                    while (Victory == Victory.None && Victory != Victory.NotPlayable) await PlayAsync();
                }
            }
            catch (Exception e4)
            {
                Console.WriteLine(e4);
            }
        }

        public async Task RoleAssignment(DiscordMessage msgInv, CommandContext e, List<DiscordMember> players)
        {
            try
            {
                // Création de tous les channels sans Droit
                var chsPerso = await Global.Game.Guild
                    .CreateChannelAsync(Global.Game.Texts.Channels.PersoGroup, ChannelType.Category);
                Global.Game.DiscordChannels.Add(GameChannel.PersoGroup, chsPerso);

                var wolfGrpChannel =
                    await Global.Game.Guild.CreateChannelAsync(Global.Game.Texts.Channels.WolvesChannel,
                        ChannelType.Category);
                var townGrpChannel = await Global.Game.Guild
                    .CreateChannelAsync(Global.Game.Texts.Channels.TownChannel, ChannelType.Category);


                var townTChannel =
                    await Guild.CreateChannelAsync(Texts.Channels.TownChannel, ChannelType.Text, townGrpChannel);
                var townVChannel =
                    await Guild.CreateChannelAsync(Texts.Channels.TownChannel, ChannelType.Voice, townGrpChannel);
                DiscordChannels.Add(GameChannel.TownText, townTChannel);
                DiscordChannels.Add(GameChannel.TownVoice, townVChannel);


                var wolfTChannel =
                    await Guild.CreateChannelAsync(Texts.Channels.WolvesChannel, ChannelType.Text, wolfGrpChannel);
                var wolfVChannel =
                    await Guild.CreateChannelAsync(Texts.Channels.WolvesChannel, ChannelType.Voice, wolfGrpChannel);
                DiscordChannels.Add(GameChannel.WolfText, wolfTChannel);
                DiscordChannels.Add(GameChannel.WolfVoice, wolfVChannel);

                var graveyardGrpChannel =
                    await Guild.CreateChannelAsync(Texts.Channels.GraveyardChannel, ChannelType.Category);
                var graveyardTChannel = await Guild.CreateChannelAsync(Texts.Channels.GraveyardChannel,
                    ChannelType.Text, graveyardGrpChannel);
                var graveyardVChannel = await Guild.CreateChannelAsync(Texts.Channels.GraveyardChannel,
                    ChannelType.Voice, graveyardGrpChannel);

                await graveyardTChannel.AddOverwriteAsync(Global.Roles[PublicRole.Spectator], GameBuilder.UsrPerms);

                DiscordChannels.Add(GameChannel.GraveyardText, graveyardTChannel);
                DiscordChannels.Add(GameChannel.GraveyardVoice, graveyardVChannel);

                foreach (var discordMember in Guild.Members)
                    if (discordMember.Roles.Contains(Global.Roles[PublicRole.Spectator]))
                        await graveyardVChannel.AddOverwriteAsync(discordMember,
                            GameBuilder.CreatePerms(Permissions.UseVoiceDetection, Permissions.UseVoice,
                                Permissions.Speak));

                await GameBuilder.CreatePersonnages(players);

                await (await e.Channel.GetMessageAsync(msgInv.Id)).ModifyAsync((await townTChannel.CreateInviteAsync())
                    .ToString());

                Global.Client.MessageReactionAdded += GameBuilder.Spectator_ReactionAdd;
                Global.Client.MessageReactionRemoved += GameBuilder.Spectator_ReactionRem;
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task NewGuildMember(GuildMemberAddEventArgs e)
        {
            await e.Member.GrantRoleAsync(Global.Roles[PublicRole.Spectator]);
        }

        private async Task StartMember(GuildMemberAddEventArgs e)
        {
            var p =
                GameBuilder.CreatePerms(Permissions.AccessChannels,
                    Permissions.UseVoice,
                    Permissions.Speak,
                    Permissions.UseVoiceDetection);

            await DiscordChannels[GameChannel.BotVoice].AddOverwriteAsync(e.Member, p);
            WriteDebug($"D : {e.Member.Username}");
        }


        public void CreateStack()
        {
            Moments = new Stack<Moment>();


            Moments.Push(Moment.Voting);
            Moments.Push(Moment.EndNight); // Tue vraiment les targets 
            if (PersonnagesList.FindAll(p => p.GetType() == typeof(Witch)).Count >= 1)
                Moments.Push(Moment.NightPhase2); // Witch 

            Moments.Push(Moment.NightPhase1); // lg, pf, voyante 


            if (Laps == 1 && PersonnagesList.FindAll(p => p.GetType() == typeof(Cupidon)).Count >= 1)
                Moments.Push(Moment.Cupid);

            Laps++;
        }

        public void CheckVictory()
        {
            // On check si les amoureux sont les seuls restants
            if (PersonnagesList.FindAll(p => p.Effect == Effect.Lover && p.Alive).Count ==
                PersonnagesList.FindAll(p2 => p2.Alive).Count)
            {
                Victory = Victory.Lovers;
                var embed = new DiscordEmbedBuilder
                {
                    Title = Texts.Annoucement.LoverVictory,
                    Color = Color.LoveColor
                };
                DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
            }

            // Si il n'y a pas de loup = la ville gagne 
            var nbWolves = PersonnagesList.FindAll(p => p.GetType() == typeof(Wolf) && p.Alive).Count;
            if (nbWolves == 0)
            {
                Victory = Victory.Town;
                DiscordChannels[GameChannel.TownText].SendMessageAsync(Texts.Annoucement.TownVictory);
            }

            // Si il n'y a que des loups = les loups gagnent
            if (nbWolves == PersonnagesList.FindAll(p => p.Alive).Count)
            {
                Victory = Victory.Wolf;
                var embed = new DiscordEmbedBuilder
                {
                    Title = Texts.Annoucement.WolfVictory,
                    Color = Color.WolfColor,
                    ImageUrl = "https://f4.bcbits.com/img/a3037005253_16.jpg"
                };
                DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
            }


            if (Victory != Victory.None) Moments.Push(Moment.End);
        }


        public static void WriteDebug(object o)
        {
            Console.WriteLine("DEBUG\t" + o);
        }

        public async Task PlayAsync()
        {
            try
            {
                CreateStack();

                WriteDebug($"Laps : {Laps}");

                var done = false;

                while (Moments.Count > 0 && !done)
                {
                    WriteDebug($"Moment Active : {Moments.Peek()}");

                    foreach (var moment in Moments.ToArray()) WriteDebug($"Moment in pile : {moment}");


                    switch (Moments.Pop())
                    {
                        case Moment.Voting:
                            await BotFunctions.DailyVote();
                            break;

                        case Moment.HunterDead:
                            await Hunter.HunterDeath();
                            break;

                        case Moment.EndNight:
                            await BotFunctions.EndNight();
                            await BotFunctions.DayAnnoucement();
                            break;

                        case Moment.NightPhase1:
                            await BotFunctions.NightAnnoucement();
                            await Wolf.WolfVote();
                            await Seer.SeerAction();
                            await LittleGirl.LittleGirlAction();
                            break;

                        case Moment.NightPhase2:
                            await Witch.WitchMoment();
                            break;

                        case Moment.Election:
                            await BotFunctions.Elections();
                            break;

                        case Moment.Cupid:
                            await Cupidon.CupidonChoice();
                            break;

                        case Moment.End:
                            done = true;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task Kill(DiscordMember m)
        {
            await Kill(Global.Game.PersonnagesList.Find(p => p.Id == m.Id));
        }

        public async Task Kill(Personnage p)
        {
            try
            {
                p.Alive = false;
                await p.Me.PlaceInAsync(DiscordChannels[GameChannel.GraveyardVoice]);
                var embed = new DiscordEmbedBuilder
                {
                    Color = Color.DeadColor,
                    Title = $"{p.GotKilled()}"
                };

                await DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());

                embed = new DiscordEmbedBuilder
                {
                    Title = Global.Game.Texts.Annoucement.DeadMessagePrivate,
                    Color = Color.InfoColor
                };

                await p.ChannelT.SendMessageAsync(embed: embed.Build());
                foreach (var discordChannel in DiscordChannels.Values)
                    await discordChannel.AddOverwriteAsync(p.Me, Permissions.AccessChannels, Permissions.ManageEmojis);

                await p.Me.RevokeRoleAsync(Global.Roles[PublicRole.Player]);
                await p.Me.GrantRoleAsync(Global.Roles[PublicRole.Spectator]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}