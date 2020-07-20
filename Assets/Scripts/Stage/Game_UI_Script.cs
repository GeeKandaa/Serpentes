using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_UI_Script : MonoBehaviour
{
    public Text _scoreboard;
    public Text _Highscoreboard;
    // Start is called before the first frame update
    void Start()
    {
        UpdateBoards();
    }

    // Update is called once per frame
    public void UpdateBoards()
    {
        _scoreboard.text = StaticInfo.Score.ToString();
        _Highscoreboard.text = StaticInfo.Highscore.ToString();
    }
}
