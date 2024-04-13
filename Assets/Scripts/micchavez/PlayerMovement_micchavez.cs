using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_micchavez : MonoBehaviour
{
    public float speed = 100f;

    private Rigidbody rb;

    private int count;

    private float movementX;
    private float movementZ;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        
    }

    
    private void FixedUpdate() {
        movementX = Input.GetAxis("Horizontal");
        movementZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(movementX, 0, movementZ);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count = count + 1;
        }
    }
}
