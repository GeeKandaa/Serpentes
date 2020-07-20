using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Store : MonoBehaviour
{
    public List<GameObject> Tail_Objs;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Tail_Objs = new List<GameObject>();
    }

}
