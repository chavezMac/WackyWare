using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's Transform component
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset from the player's position

    void LateUpdate()
    {
        if (target != null)
        {
            // Update the camera's position to follow the player with the specified offset
            transform.position = target.position + offset;
        }
    }
}
