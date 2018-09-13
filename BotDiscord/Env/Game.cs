using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public int Id;
        
        public static int Ids;

        public Game(CommandContext e, string lang)
        {
            Id = Ids;
            Ids++;
            Global.currGame = Id;
        }


        public void SetLanguage(string lang)
        {
            // ReSharper disable once PossibleNullReferenceException
            Texts = JsonConvert.DeserializeObject<Language>(File.ReadAllText(
                $@"..//Locale/{lang}/lang.json",
                Encoding.UTF8));
        }





        public async Task CreateGuild(CommandContext e, string lang = "fr")
        {
            try
            {
                Global.Client = e.Client;
                SetLanguage(lang);
           
                var msgs = (await e.Guild.GetDefaultChannel().GetMessagesAsync(10).ConfigureAwait(true)).ToList()
                    .FindAll(m => m.Author == e.Client.CurrentUser || m.Content.Contains("!go"));
                if (msgs.Count > 0)
                {
                    await e.Guild.GetDefaultChannel().DeleteMessagesAsync(msgs).ConfigureAwait(true);

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }


            while (Guild == null)
                try
                {
                    Guild = e.Client.CreateGuildAsync("Loup Garou").GetAwaiter().GetResult();
                    GuildId = Guild.Id;
                    await Guild.ModifyAsync(x => x.SystemChannel = null).ConfigureAwait(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            await GameBuilder.CreateDiscordRoles(this).ConfigureAwait(true);

            await GameBuilder.GetMember(Guild, Global.Client.CurrentUser).GrantRoleAsync(Global.Roles[CustomRoles.Admin]).ConfigureAwait(true);

            await (await Guild.GetAllMembersAsync().ConfigureAwait(true)).First().ModifyAsync(m => m.Nickname = Texts.BotName).ConfigureAwait(true);

            Console.WriteLine("Guild Created");

            DiscordChannels = new Dictionary<GameChannel, DiscordChannel>();

            Console.WriteLine("Delatation faite");

            await e.TriggerTypingAsync().ConfigureAwait(true);

            var generalChannel = Guild.GetDefaultChannel();
            await generalChannel.ModifyAsync(x => x.Name = "Bot").ConfigureAwait(true);
            DiscordChannels.Add(GameChannel.BotText, generalChannel);

            var botVChannel = await Guild.CreateChannelAsync("Bot", ChannelType.Voice, generalChannel.Parent).ConfigureAwait(true);
            DiscordChannels.Add(GameChannel.BotVoice, botVChannel);
            e.Client.GuildMemberAdded += NewGuildMember;
            e.Client.GuildMemberAdded += StartMember;


            var inv = await generalChannel.CreateInviteAsync().ConfigureAwait(true);

            var msgInv = await e.RespondAsync(inv.ToString()).ConfigureAwait(true);

            var embed = new DiscordEmbedBuilder
            {
                Title = Texts.BotWantPlay,
                Color = Color.PollColor
            };
            var askMessage = await generalChannel.SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);
            var emoji = DiscordEmoji.FromName(e.Client, ":thumbsup:");
            await askMessage.CreateReactionAsync(emoji).ConfigureAwait(true);


            var players = new List<DiscordMember>();


            try
            {
                var timeToJoin = 10;
                await Task.Delay(timeToJoin * 1000).ConfigureAwait(true);

                var users = await (await Guild.GetDefaultChannel().GetMessageAsync(askMessage.Id).ConfigureAwait(true))
                    .GetReactionsAsync(emoji).ConfigureAwait(true);

                foreach (var usr in users)
                    if (!usr.IsBot)
                    {
                        var dm = await Guild.GetMemberAsync(usr.Id).ConfigureAwait(true);
                        await dm.RevokeRoleAsync(Global.Roles[CustomRoles.Spectator]).ConfigureAwait(true);
                        await dm.GrantRoleAsync(Global.Roles[CustomRoles.Player]).ConfigureAwait(true);
                        players.Add(dm);
                    }

                // DEBUG
                foreach (var discordMember in players) WriteDebug($"Il y a {discordMember.Username} dans le jeu");

                e.Client.GuildMemberAdded -= StartMember;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


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

                    foreach (var dm in channelsToRemove) await dm.DeleteAsync().ConfigureAwait(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


            Console.WriteLine("Supr fini");

            await RoleAssignment(msgInv, e, players).ConfigureAwait(true);

            foreach (var p in PersonnagesList)
            {
                WriteDebug($"Y : {p.Me.Username}");

                DiscordMember usr = GameBuilder.GetMember(Guild, p.Me);

                await DiscordChannels[GameChannel.BotVoice].AddOverwriteAsync(usr, Permissions.None,
                    GameBuilder.CreatePerms(Permissions.AccessChannels, Permissions.UseVoice)).ConfigureAwait(true);
            }


            if (PersonnagesList.Count < 2)
            {
                Victory = Victory.NotPlayable;
                embed = new DiscordEmbedBuilder
                {
                    Title = $"{Texts.NotEnoughPlayer} {PersonnagesList.Count}",
                    Color = Color.InfoColor
                };
                await DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);
            }

            while (Victory == Victory.None && Victory != Victory.NotPlayable) await PlayAsync().ConfigureAwait(true);
        }

        public async Task RoleAssignment(DiscordMessage msgInv, CommandContext e, List<DiscordMember> players)
        {
            try
            {
                // Création de tous les channels sans Droit
                Game.WriteDebug(Global.currGame);
                var chsPerso = await Global.Games[Global.currGame].Guild
                    .CreateChannelAsync(Global.Games[Global.currGame].Texts.PersoGroup, ChannelType.Category).ConfigureAwait(true);
                Global.Games[Global.currGame].DiscordChannels.Add(GameChannel.PersoGroup, chsPerso);

                var wolfGrpChannel =
                    await Global.Games[Global.currGame].Guild
                        .CreateChannelAsync(Global.Games[Global.currGame].Texts.WolvesChannel, ChannelType.Category).ConfigureAwait(true);
                var townGrpChannel = await Global.Games[Global.currGame].Guild
                    .CreateChannelAsync(Global.Games[Global.currGame].Texts.TownChannel, ChannelType.Category).ConfigureAwait(true);


                var townTChannel =
                    await Guild.CreateChannelAsync(Texts.TownChannel, ChannelType.Text, townGrpChannel).ConfigureAwait(true);
                var townVChannel =
                    await Guild.CreateChannelAsync(Texts.TownChannel, ChannelType.Voice, townGrpChannel).ConfigureAwait(true);
                DiscordChannels.Add(GameChannel.TownText, townTChannel);
                DiscordChannels.Add(GameChannel.TownVoice, townVChannel);


                var wolfTChannel =
                    await Guild.CreateChannelAsync(Texts.WolvesChannel, ChannelType.Text, wolfGrpChannel).ConfigureAwait(true);
                var wolfVChannel =
                    await Guild.CreateChannelAsync(Texts.WolvesChannel, ChannelType.Voice, wolfGrpChannel).ConfigureAwait(true);
                DiscordChannels.Add(GameChannel.WolfText, wolfTChannel);
                DiscordChannels.Add(GameChannel.WolfVoice, wolfVChannel);

                var graveyardGrpChannel =
                    await Guild.CreateChannelAsync(Texts.GraveyardChannel, ChannelType.Category).ConfigureAwait(true);
                var graveyardTChannel = await Guild.CreateChannelAsync(Texts.GraveyardChannel,
                    ChannelType.Text, graveyardGrpChannel).ConfigureAwait(true);
                var graveyardVChannel = await Guild.CreateChannelAsync(Texts.GraveyardChannel,
                    ChannelType.Voice, graveyardGrpChannel).ConfigureAwait(true);

                await graveyardTChannel.AddOverwriteAsync(Global.Roles[CustomRoles.Spectator], GameBuilder.UsrPerms).ConfigureAwait(true);

                DiscordChannels.Add(GameChannel.GraveyardText, graveyardTChannel);
                DiscordChannels.Add(GameChannel.GraveyardVoice, graveyardVChannel);

                foreach (var discordMember in Guild.Members)
                    if (discordMember.Roles.Contains(Global.Roles[CustomRoles.Spectator]))
                        await graveyardVChannel.AddOverwriteAsync(discordMember,
                            GameBuilder.CreatePerms(Permissions.UseVoiceDetection, Permissions.UseVoice,
                                Permissions.Speak)).ConfigureAwait(true);

                await GameBuilder.CreatePersonnages(this, players).ConfigureAwait(true);

                await (await e.Channel.GetMessageAsync(msgInv.Id).ConfigureAwait(true)).ModifyAsync((await townTChannel.CreateInviteAsync().ConfigureAwait(true))
                    .ToString()).ConfigureAwait(true);
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task NewGuildMember(GuildMemberAddEventArgs e)
        {
            await e.Member.GrantRoleAsync(Global.Roles[CustomRoles.Spectator]).ConfigureAwait(true);
        }

        private async Task StartMember(GuildMemberAddEventArgs e)
        {
            var p =
                GameBuilder.CreatePerms(Permissions.AccessChannels, Permissions.UseVoice, Permissions.Speak);
            await DiscordChannels[GameChannel.BotVoice].AddOverwriteAsync(e.Member, p).ConfigureAwait(true);
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
            // Si il n'y a pas de loup = la ville gagne 
            var nbWolves = PersonnagesList.FindAll(p => p.GetType() == typeof(Wolf) && p.Alive).Count;
            if (nbWolves == 0)
            {
                Victory = Victory.Town;
                DiscordChannels[GameChannel.TownText].SendMessageAsync(Texts.TownVictory);
            }

            // Si il n'y a que des loups = les loups gagne 
            if (nbWolves == PersonnagesList.FindAll(p => p.Alive).Count)
            {
                Victory = Victory.Wolf;
                var embed = new DiscordEmbedBuilder
                {
                    Title = Texts.WolfVictory,
                    Color = Color.WolfColor,
                    ImageUrl = "https://f4.bcbits.com/img/a3037005253_16.jpg"
                };
                DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
            }

            // On check si les amoureux sont les seuls restant 
            if (PersonnagesList.FindAll(p => p.Effect == Effect.Lover && p.Alive).Count ==
                PersonnagesList.FindAll(p2 => p2.Alive).Count)
            {
                Victory = Victory.Lovers;
                var embed = new DiscordEmbedBuilder
                {
                    Title = Texts.LoverVictory,
                    Color = Color.LoveColor
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
                            await BotFunctions.DailyVote(this).ConfigureAwait(true);
                            break;

                        case Moment.HunterDead:
                            await BotFunctions.HunterDeath(this).ConfigureAwait(true);
                            break;

                        case Moment.EndNight:
                            await BotFunctions.EndNight(this).ConfigureAwait(true);
                            await BotFunctions.DayAnnoucement(this).ConfigureAwait(true);
                            break;

                        case Moment.NightPhase1:
                            await BotFunctions.NightAnnoucement(this).ConfigureAwait(true);
                            await BotFunctions.WolfVote(this).ConfigureAwait(true);
                            await BotFunctions.SeerAction(this).ConfigureAwait(true);
                            await BotFunctions.LittleGirlAction(this).ConfigureAwait(true);
                            break;

                        case Moment.NightPhase2:
                            await BotFunctions.WitchMoment(this).ConfigureAwait(true);
                            break;

                        case Moment.Election:
                            await BotFunctions.Elections(this).ConfigureAwait(true);
                            break;

                        case Moment.Cupid:
                            await BotFunctions.CupidonChoice(this).ConfigureAwait(true);
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

        public void Ending()
        {
        }


        public async Task Kill(Personnage p)
        {
            try
            {
                p.Alive = false;
                await p.Me.PlaceInAsync(DiscordChannels[GameChannel.GraveyardVoice]).ConfigureAwait(true);
                var embed = new DiscordEmbedBuilder
                {
                    Color = Color.DeadColor,
                    Title = $"{p.Me.Username} {Texts.DeadMessagePublic} {p.GetClassName()}"
                };

                await DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build()).ConfigureAwait(true);


                foreach (var discordChannel in DiscordChannels.Values)
                    await discordChannel.AddOverwriteAsync(p.Me, Permissions.AccessChannels).ConfigureAwait(true);

                await p.Me.RevokeRoleAsync(Global.Roles[CustomRoles.Player]).ConfigureAwait(true);
                await p.Me.GrantRoleAsync(Global.Roles[CustomRoles.Spectator]).ConfigureAwait(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}