using GameManager.Env.Extentions;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameManager.Env.Enum;
using GameManager.Roles;

namespace GameManager.Env
{
    public static class Global
    {
        public static Game Game { get; set; }
        public static Dictionary<PublicRole, DiscordRole> Roles { get; set; }

        public static DiscordClient Client { get; set; }

        public static GameConfig Config { get; set; } = JsonConvert.DeserializeObject<GameConfig>(
            File.ReadAllText(Path.Combine(Program.GetPath(2), "Config", "game-settings.json"), Encoding.UTF8));

        public static bool InGame = false;
    }


    public static class GameBuilder
    {
        public static Permissions UsrPerms = CreatePerms(Permissions.AccessChannels, Permissions.AddReactions,
            Permissions.SendMessages, Permissions.UseVoiceDetection, Permissions.Speak);

        public static async Task CreatePersonnages(List<DiscordMember> players)
        {
            try
            {
                var roles = CreateRoles(players.Count);
                var rand = new Random(DateTime.Now.Millisecond);


                Global.Game.PersonnagesList = new List<Personnage>();

                var letter = 'a';

                while (players.Count != 0)
                {
                    var nbRand = rand.Next(roles.Count);
                    DiscordGuildEmoji emoji;
                    try
                    {
                        //Image image;


                        // Save image to stream.
                        Stream stream2;
                        var name = players[0].Username.RemoveSpecialChars();

                        Console.WriteLine(name + " : " + players[0].AvatarUrl);

                        if (players[0].AvatarUrl == players[0].DefaultAvatarUrl)
                        {
                            // image = Image.FromFile();
                            stream2 = new FileStream($"..//Images//UserIcons//{letter}.png", FileMode.Open);
                            letter = (char) (Convert.ToUInt32(letter) + 1);
                        }
                        else
                        {
                            string path = Path.Combine(Program.GetPath(-2), "Images", "UserIcons", $"{name}.png");
                            new WebClient().DownloadFile(players[0].AvatarUrl.Replace("size=1024", "size=256"),
                                path);
                            stream2 = new FileStream(path, FileMode.Open);
                        }

                        //image.Save(stream, ImageFormat.Png);

                        emoji = await Global.Game.Guild.CreateEmojiAsync(name, stream2);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Erreur Emoji");
                        Console.WriteLine(e);
                        emoji = DiscordEmoji.FromName(Global.Client, ":yum:") as DiscordGuildEmoji;
                    }


                    switch (roles[nbRand])
                    {
                        case GameRole.Citizen:
                            Global.Game.PersonnagesList.Add(new Citizen(players[0], emoji));
                            break;
                        case GameRole.Hunter:
                            Global.Game.PersonnagesList.Add(new Hunter(players[0], emoji));
                            break;
                        case GameRole.Cupid:
                            Global.Game.PersonnagesList.Add(new Cupidon(players[0], emoji));
                            break;
                        case GameRole.Witch:
                            Global.Game.PersonnagesList.Add(new Witch(players[0], emoji));
                            break;
                        case GameRole.Savior:
                            Global.Game.PersonnagesList.Add(new Salvator(players[0], emoji));
                            break;
                        case GameRole.Seer:
                            Global.Game.PersonnagesList.Add(new Seer(players[0], emoji));
                            break;
                        case GameRole.TalkativeSeer:
                            Global.Game.PersonnagesList.Add(new TalkativeSeer(players[0], emoji));
                            break;
                        case GameRole.LittleGirl:
                            Global.Game.PersonnagesList.Add(new LittleGirl(players[0], emoji));
                            break;

                        case GameRole.Wolf:
                            Global.Game.PersonnagesList.Add(new Wolf(players[0], emoji));
                            break;
                    }

                    roles.RemoveAt(nbRand);
                    players.RemoveAt(0);
                }


                /**
                 * param name="players": all players of the game
                 * summary: Remove AccessChannel right from everyone
                 */
                foreach (var dm in players)
                {
                    await Global.Game.DiscordChannels[GameChannel.BotVoice]
                        .AddOverwriteAsync(dm, Permissions.None, Permissions.AccessChannels);
                    await Global.Game.DiscordChannels[GameChannel.BotText]
                        .AddOverwriteAsync(dm, Permissions.None, Permissions.AccessChannels);
                }
            }
            catch (Exception ex1)
            {
                Console.WriteLine(ex1);
            }
        }


        public static List<GameRole> CreateRoles(int nbPlayer)
        {
            var roleList = new List<GameRole>();

            for (var i = 0; i < nbPlayer; i++)
                switch (i)
                {
                    case 1:
                        roleList.Add(GameRole.Seer);
                        break;
                    case 2:
                        roleList.Add(GameRole.Wolf);
                        break;
                    case 3:
                        roleList.Add(GameRole.Citizen);
                        break;
                    case 4:
                        roleList.Add(GameRole.Wolf);
                        break;
                    case 5:
                        roleList.Add(GameRole.Savior);
                        roleList.Add(GameRole.Citizen);
                        roleList.Add(GameRole.Wolf);
                        break;
                    case 6:
                        roleList.Add(GameRole.LittleGirl);
                        break;
                    case 7:
                        roleList.Add(GameRole.Witch);
                        break;
                    case 8:
                        roleList.Add(GameRole.Hunter);
                        break;
                    case 9:
                        roleList.Add(GameRole.Wolf);
                        break;
                    case 10:
                        roleList.Add(GameRole.Cupid);
                        break;
                    default:
                        roleList.Add(i % 3 == 0 ? GameRole.Wolf : GameRole.Citizen);
                        break;
                    /*case 1:
                        roleList.Add(GameRole.Citizen);
                        break;
                    case 2:
                        roleList.Add(GameRole.Wolf);
                        break;
                    case 3:
                        roleList.Add(GameRole.Seer);
                        break;
                    case 4:
                        roleList.Add(GameRole.Wolf);
                        break;
                    case 5:
                        roleList.Add(GameRole.Savior);
                        roleList.Add(GameRole.Citizen);
                        roleList.Add(GameRole.Wolf);
                        break;
                    case 6:
                        roleList.Add(GameRole.LittleGirl);
                        break;
                    case 7:
                        roleList.Add(GameRole.Witch);
                        break;
                    case 8:
                        roleList.Add(GameRole.Hunter);
                        break;
                    case 9:
                        roleList.Add(GameRole.Wolf);
                        break;
                    case 10:
                        roleList.Add(GameRole.Cupid);
                        break;
                    default:
                        roleList.Add(i % 3 == 0 ? GameRole.Wolf : GameRole.Citizen);
                        break;*/
                }

            return roleList;
        }

        public static void Debug(Game game)
        {
            if (Global.Game.PersonnagesList is null)
            {
                Console.WriteLine("Il n'y a aucun personnage joueur dans le jeu");
            }
            else
            {
                var i = 0;
                foreach (var p in Global.Game.PersonnagesList)
                {
                    Console.WriteLine(i + " : " + p);
                    i++;
                }
            }
        }

        public static async Task CreateDiscordRoles()
        {
            #region Roles

            Global.Roles = new Dictionary<PublicRole, DiscordRole>();
            Game.WriteDebug(Global.Roles);
            Game.WriteDebug(Global.Game);
            Game.WriteDebug(Global.Game.Guild);
            Game.WriteDebug(Global.Game.Texts.DiscordRoles);
            Game.WriteDebug(Global.Game.Texts.DiscordRoles.BotName);

            var adminRole = await Global.Game.Guild.CreateRoleAsync(Global.Game.Texts.DiscordRoles.BotName,
                Permissions.Administrator, Color.AdminColor, true, true, "GameRole Bot");
            Global.Roles.Add(PublicRole.Admin, adminRole);


            var playerPerms = CreatePerms(Permissions.SendMessages, Permissions.ReadMessageHistory,
                Permissions.AddReactions);

            var playerRole = await Global.Game.Guild.CreateRoleAsync(Global.Game.Texts.DiscordRoles.Player, playerPerms,
                Color.PlayerColor, true, true, "GameRole Joueur");
            Global.Roles.Add(PublicRole.Player, playerRole);


            var spectPerms = CreatePerms(Permissions.AccessChannels, Permissions.ReadMessageHistory);
            RevokePerm(spectPerms, Permissions.ManageEmojis);
            var spectRole = await Global.Game.Guild.CreateRoleAsync(Global.Game.Texts.DiscordRoles.Spectator,
                spectPerms, Color.SpectColor, true, false, "GameRole spectateur");

            Global.Roles.Add(PublicRole.Spectator, spectRole);


            var gameCreatorPerms = CreatePerms(Permissions.ManageGuild);
            var gameCreatorRole = await Global.Game.Guild.CreateRoleAsync("GameCreatorTODO", gameCreatorPerms);
            Global.Roles.Add(PublicRole.GameCreator, gameCreatorRole);


            await Global.Game.Guild.EveryoneRole.ModifyAsync(x => x.Permissions = Permissions.None);

            #endregion
        }

        public static async Task Spectator_ReactionAdd(MessageReactionAddEventArgs e)
        {
            if (e.User.GetMember().Roles.Contains(Global.Roles[PublicRole.Spectator]))
            {
                await e.Message.DeleteReactionAsync(e.Emoji, e.User, $"Spectator {e.User.Username} can't vote");
            }
        }

        public static async Task Spectator_ReactionRem(MessageReactionRemoveEventArgs e)
        {
            if (e.User.GetMember().Roles.Contains(Global.Roles[PublicRole.Spectator]))
            {
                await e.Message.DeleteReactionAsync(e.Emoji, e.User, $"Spectator {e.User.Username} can't vote");
            }
        }

        public static Permissions CreatePerms(params Permissions[] perms)
        {
            return GrantPerm(Permissions.None, perms);
        }


        public static Permissions GrantPerm(Permissions p, params Permissions[] grant)
        {
            foreach (var pg in grant) p |= pg;

            return p;
        }

        public static Permissions RevokePerm(Permissions p, params Permissions[] grant)
        {
            foreach (var pg in grant) p &= ~pg;

            return p;
        }

        public static DiscordMember GetMember(DiscordGuild guild, DiscordUser usr)
        {
            return guild.GetMemberAsync(usr.Id).GetAwaiter().GetResult();
        }

        public static DiscordMember GetMember(this DiscordUser usr)
        {
            return Global.Game.Guild.GetMemberAsync(usr.Id).GetAwaiter().GetResult();
        }
    }
}