public static class StaticInfo
{
    public static string player_id { get; set; }

    private static int Score;
    public static int score 
    {
        get
        {
            return Score;
        }
        set
        {
            if (value > Highscore)
            {
                Highscore = value;
            }
            Score = value;
        }
    }
    
    private static int Highscore;
    public static int highscore 
    {
        get
        {
            return Highscore;
        } 
        set 
        {
            if (value > Highscore)
            {
                Highscore = value;
            }
        } }
    public static float game_difficulty { get; set; }
    public static int game_difficulty_int { get; set; }
}

