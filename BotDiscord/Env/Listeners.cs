using System.Linq;
using System.Threading.Tasks;
using BotDiscord.Env.Enum;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;


namespace BotDiscord.Env
{
    public static class Listeners
    {
        public static  async Task PreventMultiVote(MessageReactionAddEventArgs e)
        {
            if (!e.User.IsBot)
            {
                foreach (var otherEmoji in (await Global.Game.Guild.GetEmojisAsync()).ToList().FindAll(em => em.Id != e.Emoji.Id))
                    // On check parmi toutes les autres émojies s'il y une émojie du joueur 
                    await e.Message.DeleteReactionAsync(otherEmoji, e.User, $"{e.User.Username} already voted");

            }
        }


        public static async Task PreventSpectatorFromVote(MessageReactionAddEventArgs e)
        {
            if (e.User.GetMember().Roles.Contains(Global.Roles[PublicRole.Spectator]))
            {
                if (e.Channel.Id != Global.Game.DiscordChannels[GameChannel.GraveyardText].Id)
                {
                    await e.Message.DeleteReactionAsync(e.Emoji, e.User, $"User {e.User.Username} wanted to vote in a no Graveyard channel");
                }
            }
        }




    }
}