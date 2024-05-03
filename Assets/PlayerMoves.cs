using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 8f;
    public Rigidbody rb;
    public LayerMask groundMask;
    float distanceToGround = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalMovement, 0f, verticalMovement) * speed * Time.deltaTime;
        transform.Translate(movement);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f, groundMask);
    }
}