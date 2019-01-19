using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using GameManager.Env.Enum;

namespace GameManager.Env
{
    public static class Listeners
    {
        public static async Task PreventMultiVote(MessageReactionAddEventArgs e)
        {
            if (!e.User.IsBot)
                foreach (var otherEmoji in (await Global.Game.Guild.GetEmojisAsync()).ToList()
                        .FindAll(em => em.Id != e.Emoji.Id))
                    // On check parmi toutes les autres émojies s'il y une émojie du joueur 
                    await e.Message.DeleteReactionAsync(otherEmoji, e.User, $"{e.User.Username} already voted");
        }


        public static async Task PreventSpectatorFromVote(MessageReactionAddEventArgs e)
        {
            if (e.User.GetMember().Roles.Contains(Global.Roles[PublicRole.Spectator]))
                if (e.Channel.Id != Global.Game.DiscordChannels[GameChannel.GraveyardText].Id)
                    await e.Message.DeleteReactionAsync(e.Emoji, e.User,
                        $"User {e.User.Username} wanted to vote in a no Graveyard channel");
        }

        public static async Task KillLeaver(VoiceStateUpdateEventArgs e)
        {

            if (e.Channel == null)
            {
                var foo = Global.Game.PersonnagesList.Find(p => p.Alive && p.Me.Id == e.User.GetMember().Id);
                if (foo != null)
                {
                    var embed = new DiscordEmbedBuilder
                    {
                        Title = Global.Game.Texts.Annoucement.LeaveMessage
                    };

                    await Game.MakeDeath(foo);
                    Game.WriteDebug("Meutre du leaver " + foo.Me.Username);

                    await Global.Game.DiscordChannels[GameChannel.TownText].SendMessageAsync(embed: embed.Build());
                    if(Global.Game.Victory == Victory.None)
                    { Global.Game.CheckVictory();}
                }
            }
        }

        public static async Task ClientOnMessageReactionAdded(MessageReactionAddEventArgs e)
        {
            if (!e.User.IsBot && e.User.GetMember().Roles.Contains(Global.Roles[PublicRole.Spectator]))
            {
                if(e.Channel != Global.Game.PersonnagesList.Find(p => p.Id == e.User.GetMember().Id).ChannelT) // Permet au joueur de voter dans leur propres channel même en étant mort (chasseur)
                    await Game.DailyVotingMessage.DeleteReactionAsync(e.Emoji, e.User);
            }
        }
    }
}