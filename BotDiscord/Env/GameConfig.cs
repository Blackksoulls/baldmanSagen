using Newtonsoft.Json;

namespace BotDiscord.Env
{
    public class GameConfig
    {
        [JsonProperty("join_time")] public int JoinTime { get; set; }
        [JsonProperty("night_action_time")] public int NightActionTime { get; set; }
        [JsonProperty("day_vote_time")] public int DayVoteTime { get; set; }
        [JsonProperty("preset")] public int Preset { get; set; }
    }
}