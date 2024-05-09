using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyingObjectController : MonoBehaviour
{
    public float hitForce = 40f; // The force applied to the object when hit
    public float spinForce = 30f; // The force applied to make the object spin

    private void Start()
    {
        if (QualitySettings.GetQualityLevel() == 0)
        {
            Rigidbody rbody = GetComponent<Rigidbody>();
            BoxCollider box = GetComponent<BoxCollider>();
            Destroy(rbody);
            Destroy(box);
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WodzillaTail"))
        {
            Rigidbody rbody = GetComponent<Rigidbody>();
            
            rbody.constraints = RigidbodyConstraints.None;
            // Calculate the direction away from Godzilla at a 45-degree angle upwards
            Vector3 hitDirection = (transform.position - other.transform.position).normalized;
            hitDirection += Vector3.up; // Add a vertical component to make the object move upwards

            // Calculate the force vector based on the hit direction and force magnitude
            Vector3 force = hitDirection.normalized * hitForce;

            // Apply the force to the object
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            // Apply a random spin force
            Vector3 randomSpin = Random.insideUnitSphere * spinForce;
            rbody.AddTorque(randomSpin, ForceMode.Impulse);
            Destroy(this.gameObject,6);
        }
    }
}