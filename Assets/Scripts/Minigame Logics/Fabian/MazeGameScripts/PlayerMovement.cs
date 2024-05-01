using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string inputXAxis;
    public string inputYAxis;
    private float speed = 5;

    void Update()
    {
        float horizontalDir = Input.GetAxis(inputXAxis);
        float verticalDir = Input.GetAxis(inputYAxis);

        // Calculate the combined movement vector
        Vector3 movement = new Vector3(horizontalDir, verticalDir, 0) * speed * Time.deltaTime;

        // Update the player's position using the combined movement vector
        transform.position += movement;
    }
}
