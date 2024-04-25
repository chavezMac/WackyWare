using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Log a message whenever a collision occurs
        UnityEngine.Debug.Log("Collision Detected!");

        // Check if the collision involves an object tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Handle collision with enemy
            UnityEngine.Debug.Log("Player collided with an enemy!");

            // Destroy the enemy GameObject
            Destroy(collision.gameObject);
        }
    }
}
