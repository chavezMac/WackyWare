using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WodzillaHelicopter : MonoBehaviour
{
    public GameObject mainBlades;
    public float mainBladesSpeed = 500f;
    public float hp = 100;
    
    public Transform godzilla;
    public float minDistance = 10f;
    public Transform[] waypoints;

    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // MoveToNextWaypoint();
    }

    void Update()
    {
        // Rotate the main blades
        mainBlades.transform.Rotate(Vector3.up, mainBladesSpeed * Time.deltaTime);
        
        if (godzilla != null)
        {
            // Set Godzilla's position as the destination
            agent.SetDestination(godzilla.position);

            // Check if the helicopter is close enough to stop moving
            if (Vector3.Distance(transform.position, godzilla.position) <= minDistance)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }

        RotateTowardsGodzilla();
    }

    void MoveToNextWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
            currentWaypointIndex++;
        }
    }

    void RotateTowardsGodzilla()
    {
        Vector3 direction = godzilla.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 2f);
    }
}
