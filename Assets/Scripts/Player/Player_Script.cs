using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player_Script : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 20f;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 30f;

    public Renderer Stage;
    public Rect allowedArea;

    Vector3 Velocity;

    private void Start()
    {
        StaticInfo.game_difficulty_int = 2;
        // maxAcceleration *= StaticInfo.game_difficulty;
        // maxSpeed *= StaticInfo.game_difficulty;
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
            SceneManager.LoadScene(sceneName: "Menu");
        }
        transform.localPosition = NewPosition;
    }
}
