using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeMovement_micchavez : MonoBehaviour
{
    public float speed = 100f;

    private Rigidbody rb;

    private int count;

    private float movementX;
    private float movementZ;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        
    }

    
    private void FixedUpdate() {
        movementX = Input.GetAxis("Vertical");
        movementZ = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(movementX, 0, movementZ);
        rb.AddForce(movement * speed);
    }

}
