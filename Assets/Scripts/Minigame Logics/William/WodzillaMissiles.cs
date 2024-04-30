using System;
using UnityEngine;

public class WodzillaMissiles : MonoBehaviour
{
    public GameObject godzilla;
    public GameObject explosion;
    public float missileSpeed = 10f;
    public float turnSpeed = 2.5f;
    private CapsuleCollider targetCol;
    private Rigidbody rb;
    
    void Start()
    {
        godzilla = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        targetCol = godzilla.GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, 360f * Time.deltaTime);
        Vector3 target = targetCol.transform.position + targetCol.center;
        // Check if Godzilla exists and is active
        if (godzilla != null && godzilla.activeSelf)
        {
            // Calculate the direction vector towards Godzilla
            Vector3 direction = (target - transform.position).normalized;

            // Rotate the missile towards Godzilla using Slerp for smooth rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // Move the missile forward
            rb.velocity = transform.forward * missileSpeed;
        }
        else
        {
            // If Godzilla is null or inactive, keep moving the missile forward in its current direction
            rb.velocity = transform.forward * missileSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<WodzillaController>().TakeDamage(1);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject exp = Instantiate(this.explosion, transform.position, Quaternion.identity);
        Destroy(exp,6f);
        //play sound
        
    }
}
