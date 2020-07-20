using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_Bonus_Actions : MonoBehaviour
{
    public Vector3 RotateAmount = new Vector3(90, 180, 360);
    public int Cutspeed = 2;
    private Renderer render; 
    public Color colour;
    private Boolean forward = true;

    public GameObject timerBar;
    public GameObject if_point_obj;
    public Game_UI_Script _ui;
    public Player_Store player_Store;
    // Start is called before the first frame update
    private void Start()
    {
        render = gameObject.GetComponent<Renderer>();
        colour = render.material.color;
        player_Store = GameObject.Find("Player").GetComponent<Player_Store>();
        _ui = GameObject.Find("UI_Panel").GetComponent<Game_UI_Script>();
    }
    // Update is called once per frame
    void Update()
    {
        if (timerBar.transform.localScale.x <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Vector3 decreased_bar_scale = new Vector3(timerBar.transform.localScale.x - 0.002f, timerBar.transform.localScale.y, timerBar.transform.localScale.z);
            timerBar.transform.localScale = decreased_bar_scale;
            transform.Rotate(RotateAmount * Time.deltaTime / Cutspeed);
            if (forward)
            {
                if (colour.r < 1f)
                {
                    colour.r += 0.01f;
                }
                else if (colour.b > 0f)
                {
                    colour.b -= 0.01f;
                }
                else if (colour.g < 1f)
                {
                    colour.g += 0.01f;
                }
                else
                {
                    forward = false;
                }
            }
            else
            {
                if (colour.g > 0f)
                {
                    colour.g -= 0.01f;
                }
                else if (colour.b < 1f)
                {
                    colour.b += 0.01f;
                }
                else if (colour.r > 0f)
                {
                    colour.r -= 0.01f;
                }
                else
                {
                    forward = true;
                }
            }
            render.material.color = colour;
        }
    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.name == "Player")
        {
            Destroy(this.gameObject);
            StaticInfo.Score += 10 * StaticInfo.Game_Difficulty_Int;
            _ui.UpdateBoards();
            if (player_Store.Tail_Objs.Count > 0)
            {
                Instantiate(if_point_obj, player_Store.Tail_Objs[player_Store.Tail_Objs.Count - 1].transform.position, Quaternion.identity);

            }
            else
            {
                Instantiate(if_point_obj, GameObject.Find("Player").transform.position, Quaternion.identity);
            }
        }
    }

    private void OnDestroy()
    {
        Destroy(timerBar.gameObject);
        Destroy(transform.parent.gameObject);
    }
}