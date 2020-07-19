using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Res_Scaling : MonoBehaviour
{
    private decimal targetRes = Convert.ToDecimal(16.0 / 9.0); // 1.7
    private decimal currentRes;         // eg. 1.5
    // Start is called before the first frame update
    void Start()
    {
        currentRes = Convert.ToDecimal(Camera.main.aspect);
        float scale_factor = Convert.ToSingle(targetRes / currentRes);
        float x_pos = transform.position.x;
        float scaling_x = (scale_factor * x_pos - x_pos) * -1 * 0.8f;
        Vector3 scaled_position = new Vector3(scaling_x+transform.position.x, transform.position.y, transform.position.z);
        transform.position = scaled_position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
