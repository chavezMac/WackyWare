using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_micchavez : MonoBehaviour
{
    public CameraZoneSwitch cameraZoneSwitch;
    public Transform cameraTransform; // Reference to the camera transform
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float jumpGrace;

    private int count = 0;

    private CharacterController characterController;
    private float ySpeed;
    private float? lastGroundedTime;
    private float? jumpPressedTime;

    public AudioSource audioSource;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;

    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get the forward and right vectors of the camera
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Project movement direction onto the camera plane
        Vector3 movementDirection = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

        //Calculate forward direction value
        float forward = Vector3.Dot(movementDirection, cameraForward);
        animator.SetFloat("Forward", forward);

        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if(characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if(Input.GetButtonDown("Jump"))
        {
            jumpPressedTime = Time.time;
        }

        if(Time.time - lastGroundedTime < jumpGrace)
        {
            ySpeed = -0.5f;
            if(Time.time - jumpPressedTime < jumpGrace)
            {
                ySpeed = jumpSpeed;
                jumpPressedTime = null;
                lastGroundedTime = null;
            }
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }        
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("PickUp")) {
            //change the color of the object
            other.GetComponent<Renderer>().material.color = Color.red;
            audioSource.Play();
            //change the tag of the object
            other.gameObject.tag = "PickedUp";
        }

        
    }
}
