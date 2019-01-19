namespace GameManager.Env.Enum
{
    public enum Moment
    {
        Voting,         // Vote du jour 
        HunterDead,     // Vengeance du Chasseur

        Election,       // Election du maire 
        Cupid,           // Cupidon tour 1

        EndNight,

        NightPhase2, // Action de la Witch
        NightPhase1, // Wolfs & Petite fille & Voyantes  

        End
    }


    public enum TimeMoment
    {
        Day,
        Night
    }
}