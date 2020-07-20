public static class StaticInfo
{
    public static string Player_Id { get; set; }

    private static int _Score;
    public static int Score 
    {
        get
        {
            return _Score;
        }
        set
        {
            if (value > _Highscore)
            {
                _Highscore = value;
            }
            _Score = value;
        }
    }
    
    private static int _Highscore;
    public static int Highscore 
    {
        get
        {
            return _Highscore;
        } 
        set 
        {
            if (value > Highscore)
            {
                _Highscore = value;
            }
        } }
    public static bool Game_Difficulty { get; set; }
    public static int Game_Difficulty_Int { get; set; }
}

