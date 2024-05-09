using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;
        public float speed;
        public float turnSmoothTime = 0.1f;
        private Animator animator;
        public float movementSpeed;
        public float movementAcc;

        public float jumpImpulse;

        //private float inputThreshold = 0.1f;
        public bool isGrounded; // Adjust this threshold as needed
        private float turnVwlocity;

        void Start()
        {
          
            animator = GetComponent<Animator>();
        }

        // void Update()
        // {
        //     // Get input axis values
        //     float horizontalInput = Input.GetAxis("Horizontal");
        //     float ForwardAndBackInput = Input.GetAxis("Vertical");
        //     Rigidbody rb = GetComponent<Rigidbody>();
        //     rb.velocity += Vector3.right * horizontalInput * Time.deltaTime * movementAcc;
        //     rb.velocity += Vector3.forward * ForwardAndBackInput * Time.deltaTime * movementAcc;
        //
        //
        //
        //     CapsuleCollider col = GetComponent<CapsuleCollider>();
        //     float halfHeight = col.bounds.extents.y + 0.04f;
        //     Vector3 startPoint = new Vector3(transform.transform.position.x, (col.center.y + transform.position.y),
        //         transform.position.z);
        //     Vector3 endPoint = startPoint + Vector3.down * halfHeight;
        //     isGrounded = (Physics.Raycast(startPoint, Vector3.down, halfHeight));
        //     Color lineColor = (isGrounded) ? Color.red : Color.blue;
        //     Debug.DrawLine(startPoint, endPoint, lineColor, 0f, false);
        //
        //
        //     if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        //     {
        //
        //         rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        //     }
        //
        //     if (Mathf.Abs(rb.velocity.x) > movementSpeed)
        //     {
        //         Vector3 newVel = rb.velocity;
        //         newVel.x = Math.Clamp(newVel.x, -movementSpeed, movementSpeed);
        //         rb.velocity = newVel;
        //
        //     }
        //
        //     if (Mathf.Abs(rb.velocity.z) > movementSpeed)
        //     {
        //         Vector3 newVel = rb.velocity;
        //         newVel.z = Math.Clamp(newVel.z, -movementSpeed, movementSpeed);
        //         rb.velocity = newVel;
        //     }
        //
        //     Vector3 direction = new Vector3(horizontalInput, 0f, ForwardAndBackInput).normalized;
        //     if (direction.magnitude >= 0.1f)
        //     {
        //         float targetAngel = MathF.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //         float angel = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnVwlocity,
        //             turnSmoothTime);
        //         transform.rotation = Quaternion.Euler(0f, angel, 0f);
        //         //controller.Move(direction * movementSpeed * Time.deltaTime);
        //
        //     }
            //
            // float speed = Mathf.Abs(rb.velocity.magnitude);
            // GetComponent<Animator>().SetFloat("speed", speed);
            // GetComponent<Animator>().SetBool("IsJumping", !isGrounded);


            // // Calculate movement direction
            // Vector3 movementDirection = new Vector3(horizontalInput, 0, ForwardAndBackInput);
            //
            // // Check if character is walking
            // bool isWalking = movementDirection.magnitude > inputThreshold;
            //
            // // Update animator parameters 
            // animator.SetBool("IsWalking", isWalking);
            //
            // // Move the character
            // if (isWalking)
            // {
            //     transform.Translate(movementDirection.normalized * movementSpeed * Time.deltaTime);
            // }
       // }

         void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            if (direction.magnitude >= 0.1f)
            {
                float targetAngel = MathF.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angel = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnVwlocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f,angel,0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngel, 0f) * Vector3.forward;
                controller.Move(moveDir * speed * Time.deltaTime);
                // float speed1 = MathF.Abs(controller.velocity.x);
                // GetComponent<Animator>().SetFloat("speed", speed1 );
                float speed1 = Mathf.Abs(controller.velocity.magnitude);
                GetComponent<Animator>().SetFloat("speed", speed1);
            }

            

        }
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("FabianGame3Collectibles"))
            {
     
                if (other.gameObject.name == "PortalSphere")
                {
                    other.gameObject.SetActive(false);
                    GameObject.FindGameObjectWithTag("MiniGameLogic").GetComponent<Collectibles>().hitPortal = true;
                }else{
                    speed = 10;
                    other.gameObject.SetActive(false);
                    GameObject.FindGameObjectWithTag("MiniGameLogic").GetComponent<Collectibles>().itemsCollected++;
                }



            }

        }
    }
