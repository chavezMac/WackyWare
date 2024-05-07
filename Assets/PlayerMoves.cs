using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    public Transform cam;
    public float speed = 5f;
    public float jumpForce = 8f;
    public Rigidbody rb;
    public LayerMask groundMask;
    float distanceToGround = 0.5f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;
       

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.World);
        }

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
