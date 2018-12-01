using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BotDiscord.Roles;
using DSharpPlus.Entities;

namespace BotDiscord.Env.Extentions
{
    public static class StringExtention
    {
        public static string RemoveSpecialChars(this string str) => Regex.Replace(str, "[^a-zA-Z0-9]", "");
    }

    public static class DictionnaryExtension
    {
        public static DiscordMember Voted(this Dictionary<DiscordMember, DiscordGuildEmoji> dico)
        {

            var counter = new Dictionary<DiscordGuildEmoji, int>();
            
            foreach (var (_, value) in dico)
            {
                if (counter.TryAdd(value, 1) == false)
                {
                    counter[value] += 1;
                }
            }

            // On recupère le max
            DiscordGuildEmoji max = null;
            var count = 0;
            foreach (var (key, value) in counter)
            {
                if (max == null)
                {
                    max = key;
                    count = value;
                }
                else
                {
                    if (count < value)
                    {
                        max = key;
                        count = value;
                    }
                }
            }

            var per = Global.Game.PersonnagesList.Find(p => p.Emoji == max);

            return count == 0 ? null : per.Me;
        }
    }
}