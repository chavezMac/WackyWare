using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    public Transform cam;
    public float speed = 5f;
    public float runThreshold = 0.5f; // Magnitude threshold for triggering run animation
    public float runSpeedMultiplier = 2f; // Multiplier for run speed
    public float jumpForce = 8f;
    public float jumpSpeed;
    public Rigidbody rb;
    public LayerMask groundMask;
    float distanceToGround = 0.05f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    private float ySpeed;

    [Header("Animator")]
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;
        ySpeed += Physics.gravity.y * Time.deltaTime;
        UnityEngine.Debug.DrawRay(transform.position, Vector3.down * (distanceToGround + 0.1f), Color.green);


        // Check if the movement magnitude is above the threshold
        bool isRunning = direction.magnitude > runThreshold;

        // Calculate movement speed
        float currentSpeed = isRunning ? speed * runSpeedMultiplier : speed;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.World);

            // Set motion for animator
            animator.SetFloat("Speed", 0.5f); // Use float literal instead of double

        }
        else
        {
            // If not moving, set idle motion
            animator.SetFloat("Speed", 0f); // Ensure float value

        }





        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            UnityEngine.Debug.Log("spacebar is pressed");
            ySpeed = jumpSpeed;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            animator.SetFloat("Speed", 1f); // Ensure float value

        }
    }

    bool IsGrounded()
    {
        // Define a raycast hit variable to store information about the hit, if any
        RaycastHit hit;

        // Perform the raycast
        bool grounded = Physics.Raycast(transform.position, Vector3.down, out hit, distanceToGround + 0.6f, groundMask);

        // Check if the raycast hit something and if that something is not the current gameObject
        // This is to prevent detecting the object itself as the ground
        if (grounded && hit.collider.gameObject != gameObject)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


}
