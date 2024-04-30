using System;
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
    
    public const float firingCooldown = 4f;
    private float timeUntilFiring;
    private bool chargingWeapons = false;

    public ParticleSystem glowLeft;
    public ParticleSystem glowRight;
    public Light lightLeft;
    public Light lightRight;

    public GameObject explosion;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timeUntilFiring = firingCooldown/2f;
    }

    void Update()
    {
        // Rotate the main blades
        mainBlades.transform.Rotate(Vector3.up, mainBladesSpeed * Time.deltaTime);
        
        AIPathing();
        Shoot();
        RotateTowardsGodzilla();
    }

    private void AIPathing()
    {
        //AI pathing
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
    }

    private void Shoot()
    {
        if (agent.isStopped == false)
        {
            timeUntilFiring -= Time.deltaTime;
        }

        if (!chargingWeapons)
        {
            glowLeft.gameObject.SetActive(false);
            glowRight.gameObject.SetActive(false);
            lightLeft.intensity = 0;
            lightRight.intensity = 0;
        }

        if (timeUntilFiring <= -firingCooldown)
        {
            FireWeapons();
            return;
        }

        if (timeUntilFiring <= 0 && !chargingWeapons)
        {
            glowLeft.gameObject.SetActive(true);
            glowRight.gameObject.SetActive(true);
            chargingWeapons = true;
        }
        if (chargingWeapons)
        {
            float simSpeed = Mathf.Lerp(0,1f,Mathf.Abs(timeUntilFiring)/firingCooldown);
            var sizeCurve = new ParticleSystem.MinMaxCurve(10 * simSpeed, 20 * simSpeed);
            var glowRightMain = glowRight.main;
            glowRightMain.simulationSpeed = simSpeed;
            glowRightMain.startSize = sizeCurve;
            var glowLeftMain = glowLeft.main;
            glowLeftMain.simulationSpeed = simSpeed;
            glowLeftMain.startSize = sizeCurve;
            lightRight.intensity = simSpeed;
            lightLeft.intensity = simSpeed;
        }
    }

    void RotateTowardsGodzilla()
    {
        Vector3 direction = godzilla.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 2f);
    }

    private void FireWeapons()
    {
        chargingWeapons = false;
        timeUntilFiring = firingCooldown/2;
        Debug.Log("Firing missiles!");
        //shoot missiles
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
