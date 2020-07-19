using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_trail : MonoBehaviour
{
    // vars to find object to follow
    public GameObject player;
    public player_store player_Store;
    public GameObject sphere_to_follow;

    // vars for follow distance
    private int followDistance = 2;
    public float criticalDistance = 0.2f;
    private List<Vector3> storedPositions;

    // vars for motion
    [SerializeField, Range(0f, 1000f)]
    public float maxAcceleration = 500f;
    [SerializeField, Range(0f,100f)]
    public float maxSpeed = 40f;
    private Vector3 _pos;
    private Boolean DistanceReached = false;
    private int desiredCount = 0;
    private Vector3 Velocity;

    // vars for collider
    private bool activateCollider;
    private SphereCollider myCollider;

    void Start()
    {
        //maxAcceleration *= StaticInfo.game_difficulty;
        //maxSpeed *= StaticInfo.game_difficulty;
        //if (StaticInfo.game_difficulty_int > 3)
        //{
        //    followDistance = 1;
        //
        //else if (StaticInfo.game_difficulty_int > 7)
       // {
       //     followDistance = 0;
        //}
            // recieve current spheres and attach self.
        player = GameObject.Find("Player");
        player_Store = player.GetComponent<player_store>();
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
            myCollider = gameObject.GetComponent<SphereCollider>();
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
                SceneManager.LoadScene(sceneName: "Menu");
            }
            
        }
    }
}
