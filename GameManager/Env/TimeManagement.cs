using GameManager.Env.Enum;
using GameManager.Roles;
using System;
using System.Runtime.InteropServices;
using System.Timers;

namespace GameManager.Env
{
    public class TimeManagement
    {
        public Timer Timer { get; }

        private TimeMoment _moment;
        public TimeMoment Moment
        {
            get => _moment;
            set
            {
                Start = DateTime.Now;
                _moment = value;
            }
        }

        public DateTime Start;

        public TimeManagement()
        {
            Timer = new Timer(Global.Config.RefreshingTime);
            Timer.Elapsed += SetRemainingTime;
            Timer.AutoReset = true;

        }

        public async void SetRemainingTime(object sender, ElapsedEventArgs e)
        {
            // "Temps restant avant | jour | 120s" 
            var str = $"";
            var timePassed = DateTime.Now - Start;
            int nb = 0;
            switch (Moment)
            {
                case TimeMoment.Day:
                    nb = (int) Math.Round((Global.Config.DayVoteTime - timePassed.TotalMilliseconds) / 1000);
                    str += $"{Global.Game.Texts.Channels.TimeBeforeNightToString} {Global.Game.Texts.Channels.TimeBeforeNext} :";
                    break;
                case TimeMoment.Night:

                    str += $"{Global.Game.Texts.Channels.TimeBeforeDayToString } {Global.Game.Texts.Channels.TimeBeforeNext} :";
                    if (Global.Game.PersonnagesList
                            .FindAll(p => p.GetType() == typeof(Savior) || p.GetType() == typeof(Witch)).Count > 0)
                    {
                        nb = (int)Math.Round((Global.Config.NightPhase1 + Global.Config.NightPhase2 - timePassed.TotalMilliseconds) / 1000);

                        
                    }
                    else
                    {
                        nb = (int)Math.Round((Global.Config.NightPhase1 - timePassed.TotalMilliseconds) / 1000);

                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }

            await Global.Game.DiscordChannels[GameChannel.Time].ModifyAsync(a => a.Name = $"{str} {nb}");
            Game.WriteDebug("Milisecond passed ; " + nb);


        }
    }
}