using UnityEngine;

public class JumpMovement2 : MonoBehaviour
{
   
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float jumpGrace;

    private int count = 0;

    private CharacterController characterController;
    private float ySpeed;
    private float? lastGroundedTime;
    private float? jumpPressedTime;

    [Header("Animator")]
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

      
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

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

        float forward = Vector3.Dot(velocity, transform.forward);
        animator.SetFloat("Forward", forward);

        characterController.Move(velocity * Time.deltaTime);

        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }        
    }

     void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("PickUp")) {
            // other.gameObject.SetActive(false);
            count = count + 1;
             //change the tag of the object
            other.gameObject.tag = "PickedUp";
        }       
    }
}