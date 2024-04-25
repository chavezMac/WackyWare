using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Transform player; 
    public float moveSpeed = 5f; 
    public float detectionRadius = 10f; 
    public LayerMask playerLayer; 

    private bool isPlayerDetected = false; 
    private Vector3 initialPosition; 
    private Vector3 lastKnownPlayerPosition; 

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (!isPlayerDetected)
        {
            
            MoveTo(initialPosition);
        }
        else
        {
            
            MoveTo(lastKnownPlayerPosition);
        }

        
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        if (colliders.Length > 0)
        {
          
            isPlayerDetected = true;
            lastKnownPlayerPosition = player.position;
        }
        else
        {
           
            isPlayerDetected = false;
        }
    }

    private void MoveTo(Vector3 targetPosition)
    {
      
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
    }
}