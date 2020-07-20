using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player_Script : MonoBehaviour
{
    private float maxSpeed = 10f;
    public Renderer Stage;
    private Rect allowedArea;

    private void Start()
    {
        maxSpeed += StaticInfo.Game_Difficulty_Int*2;
        allowedArea = new Rect(new Vector2(0,0), Stage.bounds.size);
    }
    private void Update()
    {
        // Player Input
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal") * -1;
        playerInput.y = Input.GetAxis("Vertical") * -1;

        if (playerInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(playerInput.y, playerInput.x) * Mathf.Rad2Deg;
            angle = Mathf.Round(angle);
            // convert angle to one of 8 directions
            float adjusted_angle = -7.5f * Mathf.Sin(angle * Mathf.Deg2Rad * 8) + angle;
            Quaternion q2 = Quaternion.AngleAxis(adjusted_angle + 90.0f, Vector3.forward);
            if (transform.rotation != Quaternion.AngleAxis(adjusted_angle + 270.0f, Vector3.forward) && transform.rotation != Quaternion.AngleAxis(adjusted_angle - 90.0f, Vector3.forward))
            {
                transform.rotation = q2;
            } 
        }
    }
    void FixedUpdate ()
    {

        // Constant motion.
        Vector3 Velocity = transform.up.normalized * maxSpeed;
        Vector3 displacement = Velocity * Time.fixedDeltaTime;
        Vector3 NewPosition = transform.localPosition + displacement;
        // Contained motion.
        if (!allowedArea.Contains(new Vector2(NewPosition.x, NewPosition.y)))
        {
            StaticInfo.Score = 0;
            SceneManager.LoadScene(sceneName: "Menu");
        }
        transform.localPosition = NewPosition;
    }
}
