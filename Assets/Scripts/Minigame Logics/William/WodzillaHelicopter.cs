using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class WodzillaHelicopter : MonoBehaviour
{
    public GameObject mainBlades;
    public float mainBladesSpeed = 500f;
    public float hp = 50;
    
    public GameObject godzilla;
    public float moveSpeed = 18;
    public float minDistance = 50f;
    public float backingDistance = 25f;
    private NavMeshAgent agent;
    public bool withinFiringDistance = false;
    public Vector3 destination;
    public float distanceToGodzilla;
    
    public const float firingCooldown = 3f;
    private float timeUntilFiring;
    private bool chargingWeapons = false;

    public ParticleSystem glowLeft;
    public ParticleSystem glowRight;
    public Light lightLeft;
    public Light lightRight;

    public GameObject explosion;
    public GameObject missiles;

    // public GameObject debugSphere;

    void Start()
    {
        godzilla = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        timeUntilFiring = firingCooldown/3f;
        agent.speed = moveSpeed;
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
        // AI pathing
        if (godzilla != null)
        {
            // Calculate the distance between the helicopter and Godzilla
            distanceToGodzilla = Vector3.Distance(transform.position, godzilla.transform.position);

            // If the distance is greater than the minimum distance, move towards Godzilla
            if (distanceToGodzilla > minDistance)
            {
                destination = godzilla.transform.position;
                agent.SetDestination(destination);
                withinFiringDistance = false;
                agent.speed = moveSpeed;
            }
            // If the distance is less than or equal to the backing distance, back away
            else if(distanceToGodzilla <= backingDistance)
            {
                withinFiringDistance = true;
                // Calculate the direction away from Godzilla
                Vector3 directionAwayFromGodzilla = transform.position - godzilla.transform.position;

                // Normalize the direction vector
                directionAwayFromGodzilla.Normalize();

                // Calculate the new destination away from Godzilla
                destination = transform.position + directionAwayFromGodzilla * backingDistance;
                destination.y = transform.position.y;

                // Set the new destination for the helicopter
                agent.SetDestination(destination);
                agent.speed = moveSpeed;
            }
            else
            {
                agent.speed = 0;
                withinFiringDistance = true;
                destination = godzilla.transform.position;
                agent.SetDestination(destination);
            }
            // debugSphere.transform.SetPositionAndRotation(destination,Quaternion.identity);
        }
    }

    private void Shoot()
    {
        if (withinFiringDistance)
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
        Vector3 direction = godzilla.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 2f);
    }

    private void FireWeapons()
    {
        chargingWeapons = false;
        timeUntilFiring = firingCooldown/2;
        GameObject missile = Instantiate(this.missiles, transform.position, quaternion.identity);
        Destroy(missile,7f);
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
