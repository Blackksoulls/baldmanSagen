using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Exceptions;
using GameManager.Env.Enum;
using GameManager.Env.Extentions;
using GameManager.Locale;
using GameManager.Roles;
using Newtonsoft.Json;

namespace GameManager.Env
{
    public class Game
    {
        public Dictionary<GameChannel, DiscordChannel> DiscordChannels;
        public Language Texts;
        public List<Personnage> PersonnagesList;
        public Victory Victory = Victory.None;
        public ulong GuildId;
        public DiscordGuild Guild;
        public Stack<Moment> Moments { get; set; }
        public List<Personnage> NightTargets { get; set; }

        public DiscordUser Creator { get; set; }

        public int Laps;

        public TimeManagement Time;

        public Score Score;
        public static bool Attendre { get; set; }


        public static DiscordMessage DailyVotingMessage { get; private set; }
        public static DiscordMessage DeadVotingMessage { get; private set; }


        public Game(string lang)
        {
            SetLanguage(lang);
            Score = Score.Load();
            Time = new TimeManagement();
        }


        public void SetLanguage(string lang) => Texts = JsonConvert.DeserializeObject<Language>(
            File.ReadAllText
            (Path.Combine(Program.GetPath(-2), "Locale", lang, "lang.json"),
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
                        Global.Game.Creator = e.User;

                        var msgs = (await e.Channel.GetMessagesAsync(10)).ToList()
                            .FindAll(m => m.Author == e.Client.CurrentUser || m.Content.Contains("!go"));
                        if (msgs.Count > 0)
                        {
                            await e.Channel.DeleteMessagesAsync(msgs);
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }

                    Console.WriteLine(Texts);
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

                    #region TIME

                    var statsChannel = await Guild.CreateChannelAsync(Global.Game.Texts.Channels.Stats, ChannelType.Category);

                    var timeChannel = await Guild.CreateChannelAsync(Global.Game.Texts.Channels.TimeBeforeNext,ChannelType.Voice,  parent:statsChannel);
                    await timeChannel.ModifyPositionAsync(0);
                    await timeChannel.AddOverwriteAsync(timeChannel.Guild.EveryoneRole, Permissions.AccessChannels);
                    DiscordChannels.Add(GameChannel.Time, timeChannel);

                    #endregion TIME

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


                    var temp = 0;
                    while (Global.Game.Guild.Members.Count(m => m.IsBot) < 2)
                    {
                        Console.WriteLine("PLOP");
                        if (temp % 20 == 0)
                        {
                            var embed1 = new DiscordEmbedBuilder
                            {
                                Color = Color.InfoColor,
                                Title = Global.Game.Texts.Annoucement.NeedEventBot,
                                Description =
                                    $"{Global.Game.Creator.Mention} {Global.Game.Texts.Annoucement.NeedEventBotTooltype}"
                            };
                            await DiscordChannels[GameChannel.BotText].SendMessageAsync(embed: embed1.Build());
                        }

                        await Task.Delay(500);
                        temp++;
                    }

                    foreach (var usr in botVChannel.Users.ToList())
                    {
                        await usr.RevokeRoleAsync(Global.Roles[PublicRole.Spectator]);
                        await usr.GrantRoleAsync(Global.Roles[PublicRole.Player]);
                        players.Add(usr);
                        WriteDebug(usr.Username);
                    }

                    Global.Client.GuildMemberAdded -= StartMember;
                    //Global.Client.MessageReactionAdded += Listeners.PreventMultiVote;
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


                    // Score / perms
                    foreach (var p in PersonnagesList)
                    {
                        if (Global.Game.Score.AddPlayer(p.Id))
                        {
                            WriteDebug($"Bank create for {p.Me.Username}");
                        }
                        else
                        {
                            WriteDebug($"{p.Me.Username} already is in bank");
                        }

                        var usr = GameBuilder.GetMember(Guild, p.Me);

                        await DiscordChannels[GameChannel.BotVoice].AddOverwriteAsync(usr, Permissions.None,
                            GameBuilder.CreatePerms(Permissions.AccessChannels, Permissions.UseVoice));
                    }

                    Global.Game.Score.Save();


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

                        // TODO Uncomment after test
                        await Global.Game.Guild.DeleteAsync();
                    }
                    else
                    {
                        e.Client.VoiceStateUpdated += Listeners.KillLeaver;
                    }

                    Global.Game.NightTargets = new List<Personnage>();

                    embed = new DiscordEmbedBuilder()
                    {
                        Title = "Scores"
                    };

                    while (Victory == Victory.None && Victory != Victory.NotPlayable) await PlayAsync();
                    // Sauvegarde de fin 

                    foreach( var personnage in Global.Game.PersonnagesList)
                    {
                        Score.ModifyPoint(personnage.Id , 10);
                        embed.AddField(personnage.Me.DisplayName, $"{Score.GetScore(personnage.Id)} point(s)");
                    }


                    await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());     
                        
                    Global.Game.Score.Save();

                    await Task.Delay(Global.Config.DayVoteTime);

                    await Global.Game.Guild.DeleteAsync();

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
            WriteDebug($"D : {e.Member.Username} {Global.Game.Creator.Username}");
            if (e.Member.Username == Global.Game.Creator.Username)
            {
                await e.Member.GrantRoleAsync(Global.Roles[PublicRole.GameCreator]);
                Console.WriteLine("GRANTED");
            }
        }


        public void CreateStack()
        {
            Moments = new Stack<Moment>();
            
            Moments.Push(Moment.Voting);
            Moments.Push(Moment.EndNight); // Tue vraiment les targets 

            var nightPhase2Roles = new List<Type>(){typeof(Witch), typeof(Savior)};
            foreach (var role in nightPhase2Roles)
            {
                if (PersonnagesList.FindAll(p => p.GetType() == role).Count > 0)
                {
                    Moments.Push(Moment.NightPhase2); 
                    break;
                  
                }
            }

            Moments.Push(Moment.NightPhase1); // lg, pf, voyante 

            if (Laps == 0 && PersonnagesList.FindAll(p => p.GetType() == typeof(Cupidon)).Count >= 1)
                Moments.Push(Moment.Cupid);

            Laps++;
        }

        public void CheckVictory()
        {
            // On check si les amoureux sont les seuls restants

            if (PersonnagesList.Count <= 0) return;
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

            // S'il n'y a pas de loup = la ville gagne 
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
                            await DailyVote();
                            break;

                        case Moment.HunterDead:
                            await Hunter.HunterDeath();
                            break;

                        case Moment.EndNight:
                            await EndNight();
                            await DayAnnoucement();
                            break;

                        case Moment.NightPhase1:
                            await NightAnnoucement();
                            await Wolf.WolfVote();
                            await Seer.SeerAction();
                            await TalkativeSeer.TalkativeSeerAction();
                            //await LittleGirl.LittleGirlAction();
                            break;

                        case Moment.NightPhase2:
                            await Witch.WitchMoment();
                            await Savior.SaviorMoment();
                            break;

                        case Moment.Election:
                            await Elections();
                            break;

                        case Moment.Cupid:
                            await Cupidon.CupidonChoice();
                            break;

                        case Moment.End:
                            Global.Game.Time.Timer.Stop();
                            done = true;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                Global.Game.Score.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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
                    Color = Color.DeadColor
                };

                await p.ChannelT.SendMessageAsync(embed: embed.Build());
                foreach (var discordChannel in DiscordChannels.Values)
                    await discordChannel.AddOverwriteAsync(p.Me, Permissions.AccessChannels, Permissions.ManageEmojis);

                await p.Me.RevokeRoleAsync(Global.Roles[PublicRole.Player]);
                await p.Me.GrantRoleAsync(Global.Roles[PublicRole.Spectator]);
                await Task.Delay(Global.Config.WaitAfterDeathTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task DeadVote()
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = Global.Game.Texts.Polls.DeadVoteMessage,
                Color = Color.PollColor
            };
            DeadVotingMessage =
                await Global.Game.DiscordChannels[GameChannel.GraveyardText].SendMessageAsync(embed: embed.Build());

            foreach (var personnage in Global.Game.PersonnagesList.FindAll(personnage => personnage.Alive))
            {
                Console.WriteLine($"Personnage : {personnage.Me.Username} -> {personnage.Emoji.Name}");
                await DeadVotingMessage.CreateReactionAsync(personnage.Emoji);
            }

            Global.Client.MessageReactionAdded += Listeners.ClientOnMessageReactionAdded;

            //if (nbTry == 1) Global.Client.MessageReactionAdded += Listeners.ClientOnMessageReactionAdded;


            Console.WriteLine("Le temps est fini");
            DeadVotingMessage = await Global.Game.DiscordChannels[GameChannel.GraveyardText]
                .GetMessageAsync(DeadVotingMessage.Id);

            while (Attendre)
            {
                await Task.Delay(500);
            }

            var vote = await BotCommands.GetVotes(DeadVotingMessage);
            var voted = (await BotCommands.GetVotes(DailyVotingMessage)).Voted();

            foreach (var (user, emoji) in vote)
            {
                if (emoji.Equals(voted.Emoji))
                {
                    Global.Game.Score.ModifyPoint(user.Id, 1);
                }
                else
                {
                    Global.Game.Score.ModifyPoint(user.Id, -1);
                }
            }

            Global.Client.MessageReactionAdded -= Listeners.ClientOnMessageReactionAdded;
        }

        public static async Task DailyVote()
        {

            foreach (var personnage in Global.Game.PersonnagesList.FindAll(p => p.Alive))
            {
                Global.Game.Score.ModifyPoint(personnage.Id, 1);
            }

            var embed = new DiscordEmbedBuilder
            {
                Title = Global.Game.Texts.Polls.DailyVoteMessage,
                Color = Color.PollColor
            };
            DailyVotingMessage =
                await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());

            Attendre = true;
            var thread = new Thread(async () => await DeadVote());
            thread.Start();

            foreach (var personnage in Global.Game.PersonnagesList.FindAll(personnage => personnage.Alive))
            {
                Console.WriteLine($"Personnage : {personnage.Me.Username} -> {personnage.Emoji.Name}");
                await DailyVotingMessage.CreateReactionAsync(personnage.Emoji);
            }

            //Global.Client.MessageReactionAdded += ClientOnMessageReactionAdded;


            await Task.Delay(Global.Config.DayVoteTime);


            Console.WriteLine("Le temps pour voter le jour est fini");


            var votes = await BotCommands.GetVotes(DailyVotingMessage);
            var target = votes.Voted();


            if (target != null)
            {
                var p = Global.Game.PersonnagesList.Find(personnage => personnage.Id == target.Id);
                await MakeDeath(p);
            }
            else
            {
                embed = new DiscordEmbedBuilder
                {
                    Title = Global.Game.Texts.Polls.NoTownKill,
                    Color = Color.InfoColor
                };
                await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed);
            }

            Global.Game.CheckVictory();

            //Global.Client.MessageReactionAdded -= ClientOnMessageReactionAdded;
            Attendre = false;
            thread.Join();
            //await Task.Delay(Global.Config.DayVoteTime);
        }

        public static async Task EndNight()
        {
            WriteDebug("Fin de la nuit");

            foreach (var p in Global.Game.PersonnagesList.FindAll(p => p.GetType() == typeof(Wolf)))
            {
                var sends = GameBuilder.CreatePerms(Permissions.SendMessages, Permissions.SendTtsMessages);
                var others = GameBuilder.CreatePerms(Permissions.AccessChannels, Permissions.ReadMessageHistory);
                await Global.Game.DiscordChannels[GameChannel.WolfText].AddOverwriteAsync(p.Me, others, sends);
            }


            foreach (var target in Global.Game.NightTargets)
            {
                if (target != null)
                {
                    WriteDebug("MEUTRE DE " + target.Me.Username);
                    await MakeDeath(target);
                }
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
            Global.Game.Time.Timer.Enabled = false;
            Global.Game.Time.Moment = TimeMoment.Night;
            Global.Game.Time.Timer.Enabled = true;

            var embed = new DiscordEmbedBuilder
            {
                Title = Global.Game.Texts.Annoucement.NightAnnoucement,
                Color = Color.InfoColor
            };
            await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
            var sends = GameBuilder.CreatePerms(Permissions.AccessChannels, Permissions.SendMessages,
                Permissions.SendTtsMessages);
            foreach (var p in Global.Game.PersonnagesList.FindAll(p => p.Alive))
            {
                if (p.GetType() != typeof(Wolf))
                {
                    await p.Me.PlaceInAsync(p.ChannelV);
                }
                else
                {
                    await p.Me.PlaceInAsync(Global.Game.DiscordChannels[GameChannel.WolfVoice]);
                    await Global.Game.DiscordChannels[GameChannel.WolfText].AddOverwriteAsync(p.Me, sends);
                }

                await Global.Game.DiscordChannels[GameChannel.TownText].AddOverwriteAsync(p.Me, Permissions.None, GameBuilder.SendPerms);

            }
        }

        public static async Task DayAnnoucement()
        {

            Global.Game.Time.Timer.Enabled = false;
            Global.Game.Time.Moment = TimeMoment.Day;
            Global.Game.Time.Timer.Enabled = true;


            var embed = new DiscordEmbedBuilder
            {
                Title = Global.Game.Texts.Annoucement.DayAnnoucement,
                Color = Color.InfoColor
            };
            await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
            foreach (var p in Global.Game.PersonnagesList.FindAll(p => p.Alive))
            {
                await p.Me.PlaceInAsync(Global.Game.DiscordChannels[GameChannel.TownVoice]);
                await Global.Game.DiscordChannels[GameChannel.TownText].AddOverwriteAsync(p.Me,GameBuilder.CreatePerms(GameBuilder.SendPerms, Permissions.AccessChannels), Permissions.None);

            }
        }

      
    }
}