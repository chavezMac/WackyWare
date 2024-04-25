using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawEnemy : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the saw enemy moves
    public float respawnThreshold = -10f; // X position where the enemy respawns
    public Vector3 respawnPoint; // Respawn point for the enemy

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial respawn point to the enemy's starting position
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy to the left
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // Check if the enemy has reached the respawn threshold
        if (transform.position.x < respawnThreshold)
        {
            // Respawn the enemy at the predefined respawn point
            transform.position = respawnPoint;
        }
    }
}