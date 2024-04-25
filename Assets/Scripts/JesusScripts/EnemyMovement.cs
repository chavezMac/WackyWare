using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float verticalMovementRange = 1f; // Range of vertical movement
    public float verticalMovementSpeed = 1f; // Speed of vertical movement
    private Vector3 startPosition; // Starting position of the enemy

    void Start()
    {
        startPosition = transform.position; // Store the starting position
    }

    void Update()
    {
        // Calculate vertical movement using a sine wave function
        float verticalMovement = Mathf.Sin(Time.time * verticalMovementSpeed) * verticalMovementRange;
        transform.position = startPosition + Vector3.up * verticalMovement;
    }
}
