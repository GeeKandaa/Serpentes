using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class generate_points : MonoBehaviour
{
    public GameObject point_obj;
    public GameObject bonus_obj;
    private Rect levelspace;
    private List<GameObject> active_point;
    private List<GameObject> active_bonus;
    // Start is called before the first frame update
    void Start()
    {
        levelspace = new Rect(new Vector2(5, 5), gameObject.GetComponent<Renderer>().bounds.size-new Vector3(3,3));
        active_point = new List<GameObject>();
        active_bonus = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active_point.Count == 0)
        {
            Vector3 bonus_rand_pos = Vector3.zero;
            if (Random.value < 0.2 && active_bonus.Count == 0)
            {
                bonus_rand_pos = new Vector3(Random.Range(levelspace.x, levelspace.width), Random.Range(levelspace.y, levelspace.height), -0.6f);
                GameObject new_bonus_point = Instantiate(bonus_obj, bonus_rand_pos, Quaternion.identity);
                active_bonus.Add(new_bonus_point);
            }
            Vector3 rand_pos = new Vector3(Random.Range(levelspace.x, levelspace.width), Random.Range(levelspace.y, levelspace.height), -0.6f);
            double pointsDistance = Math.Abs(Math.Sqrt(Math.Pow(bonus_rand_pos.x - rand_pos.x, 2) + Math.Pow(bonus_rand_pos.y - rand_pos.y, 2)));
            while (bonus_rand_pos != Vector3.zero && pointsDistance < 2)
            {
                rand_pos = new Vector3(Random.Range(levelspace.x, levelspace.width), Random.Range(levelspace.y, levelspace.height), -0.6f);
                pointsDistance = Math.Abs(Math.Sqrt(Math.Pow(bonus_rand_pos.x - rand_pos.x, 2) + Math.Pow(bonus_rand_pos.y - rand_pos.y, 2)));
            }
            GameObject new_point = Instantiate(point_obj, rand_pos, Quaternion.identity);
            active_point.Add(new_point);
        }
        if (active_point[0] == null)
        {
            active_point.RemoveAt(0);
        }
        if (active_bonus[0] == null)
        {
            active_bonus.RemoveAt(0);
        }
    }
}
