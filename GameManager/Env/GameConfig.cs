using Newtonsoft.Json;

namespace GameManager.Env
{
    public class GameConfig
    {
        [JsonProperty("join_time")] public int JoinTime { get; set; }
        [JsonProperty("night_action_time")] public int NightActionTime { get; set; }
        [JsonProperty("day_vote_time")] public int DayVoteTime { get; set; }
        [JsonProperty("preset")] public int Preset { get; set; }

        [JsonProperty("night_phase_2_time")] public int NightPhase2 { get; set; }
        [JsonProperty("night_phase_1_time")] public int NightPhase1 { get; set; }

        [JsonProperty("quick_action_time")]  public int QuickActionTime { get; set; } // Chasseur 

        [JsonProperty("wait_after_death_time")] public int WaitAfterDeathTime { get; set; }
        [JsonProperty("refresh_remaining_time")] public double RefreshingTime { get; set; }
    }
}