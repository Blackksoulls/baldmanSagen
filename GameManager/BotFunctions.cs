﻿using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using GameManager.Env;
using GameManager.Env.Enum;
using GameManager.Env.Extentions;
using GameManager.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameManager
{
    public class BotFunctions
    {
        public static DiscordMessage DailyVotingMessage { get; private set; }
        public static DiscordMessage deadVotingMessage { get; private set; }
        public static Dictionary<ulong, int> scores;
        public static bool Attendre
        {
            get => Attendre;
            set => Attendre = value;
        }



        public static async Task DeadVote(int nbTry = 1)
        {
            Attendre = true;
            var embed = new DiscordEmbedBuilder
            {
                Title = Global.Game.Texts.Polls.DeadVoteMessage,
                Color = Color.PollColor
            };
            deadVotingMessage =
                await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());

            foreach (var personnage in Global.Game.PersonnagesList.FindAll(personnage => personnage.Alive))
            {
                Console.WriteLine($"Personnage : {personnage.Me.Username} -> {personnage.Emoji.Name}");
                await deadVotingMessage.CreateReactionAsync(personnage.Emoji);
            }

            if (nbTry == 1) Global.Client.MessageReactionAdded += ClientOnMessageReactionAdded;


            Console.WriteLine("Le temps est fini");
            deadVotingMessage = await Global.Game.DiscordChannels[GameChannel.TownText]
                .GetMessageAsync(deadVotingMessage.Id);

            while (Attendre)
            {
                await Task.Delay(100);}
            Attendre = false;
            var vote = await BotCommands.GetVotes(deadVotingMessage);
            var voted = (await BotCommands.GetVotes(DailyVotingMessage)).Voted();
            
            foreach (var(user, emoji) in vote)
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
            
            Global.Client.MessageReactionAdded -= ClientOnMessageReactionAdded;
        }


        public static async Task DailyVote()
        {
            /*while (!Attendre){
                Task.Delay(100).GetAwaiter().GetResult();
            }*/

            foreach (var personnage in Global.Game.PersonnagesList.FindAll(p => p.Alive))
            {
                Global.Game.Score.ModifyPoint(personnage.Id, 1);
            }

/*            foreach (var(id,score) in scores)
            {
                foreach(var perso in Global.Game.PersonnagesList.FindAll(p => p.Alive))
                {
                    if(perso.Id == id)
                    {
                        scores[id] += 1;
                    }
                }
            }*/
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

            //Global.Client.MessageReactionAdded += ClientOnMessageReactionAdded;


            await Task.Delay(Global.Config.DayVoteTime);


            Console.WriteLine("Le temps pour voter le jour est fini");


            
            var votes = await BotCommands.GetVotes(DailyVotingMessage);
            var player = votes.Voted();


            if (player != null)
            {
                var p = Global.Game.PersonnagesList.Find(personnage => personnage.Id == player.Id);
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
            Console.WriteLine("PREV");
            Global.Game.CheckVictory();
            Console.WriteLine("POST");

            //Global.Client.MessageReactionAdded -= ClientOnMessageReactionAdded;
            //Attendre = false;
            Console.WriteLine("A");
            await Task.Delay(10000);
            Console.WriteLine("B");

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