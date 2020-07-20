using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Trail : MonoBehaviour
{
    // vars to find object to follow
    public GameObject player;
    public Player_Store player_Store;
    public GameObject sphere_to_follow;

    // vars for follow distance
    public float criticalDistance = 0.2f;
    private List<Vector3> storedPositions;
    private Boolean DistanceReached = false;

    // vars for collider
    private bool activateCollider;

    void Start()
    {
        player = GameObject.Find("Player");
        player_Store = player.GetComponent<Player_Store>();
        if (player_Store.Tail_Objs.Count != 0)
        {
            sphere_to_follow = player_Store.Tail_Objs[player_Store.Tail_Objs.Count - 1];
        }
        else
        {
            sphere_to_follow = player;
        }
        player_Store.Tail_Objs.Add(gameObject);

        // intialise collision?
        if (player_Store.Tail_Objs.Count > 1)
        {
            activateCollider = true;
        }

        // initialise distancing frames list
        storedPositions = new List<Vector3>();

        

    }


    void FixedUpdate()
    {
        Vector3 spherePos = sphere_to_follow.transform.position;
        double Distance = Math.Abs(Math.Sqrt(Math.Pow(sphere_to_follow.transform.position.x - transform.position.x, 2) + Math.Pow(sphere_to_follow.transform.position.y - transform.position.y, 2)));
        storedPositions.Add(spherePos);
        if ((Math.Abs(sphere_to_follow.transform.position.x - transform.position.x) < criticalDistance && sphere_to_follow.transform.position.y == transform.position.y) || 
            (Math.Abs(sphere_to_follow.transform.position.y - transform.position.y) < criticalDistance && sphere_to_follow.transform.position.x == transform.position.x))
        {
            return;
        } 
        if (Distance > criticalDistance || DistanceReached == true)
        {
            DistanceReached = true;
            transform.position = storedPositions[0];
            storedPositions.RemoveAt(0);
        }
        if (Distance > criticalDistance+0.5)
        {
            transform.position = storedPositions[0];
            storedPositions.RemoveAt(0);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (activateCollider == true)
            {
                StaticInfo.Score = 0;
                SceneManager.LoadScene(sceneName: "Menu");
            }
            
        }
    }
}
