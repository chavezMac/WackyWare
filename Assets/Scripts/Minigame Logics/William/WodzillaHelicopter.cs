using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Effects;

public class WodzillaHelicopter : MonoBehaviour
{
    public GameObject mainBlades;
    public float mainBladesSpeed = 500f;
    public float hp = 50;
    
    public Transform godzilla;
    public float minDistance = 10f;

    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    public float firingCooldown = 5f;

    public GameObject explosion;

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
    
    void RotateTowardsGodzilla()
    {
        Vector3 direction = godzilla.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 2f);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        GameObject exp = Instantiate(this.explosion, transform.position, quaternion.identity);
        Destroy(exp,6f);
        Destroy(gameObject);
    }
}
