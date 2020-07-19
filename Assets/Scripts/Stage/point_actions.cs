using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point_actions : MonoBehaviour
{
    public Vector3 RotateAmount = new Vector3(90,180,360);
    public int Cutspeed = 2;
    public GameObject if_point_obj;
    public Game_UI_Script _ui;
    public player_store player_Store;
    // Start is called before the first frame update
    private void Start()
    {       
        player_Store = GameObject.Find("Player").GetComponent<player_store>();
        _ui = GameObject.Find("UI_Panel").GetComponent<Game_UI_Script>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateAmount * Time.deltaTime / Cutspeed);
    }

    void OnTriggerEnter (Collider collision)
    {

        if (collision.gameObject.name == "Player")
        {
            Destroy(this.gameObject);
            StaticInfo.score += 5 * StaticInfo.game_difficulty_int;
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
}
