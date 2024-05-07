using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    public float speed = 5f;
    public float jumpForce =8f;
    public Rigidbody rb;
    public RaycastHit hit;
    float distanceToGround = 0.5f;
    bool isGrounded ;
    // Start is called before the first frame update
    
    void Start () {
       
         rb = GetComponent<Rigidbody>(); 
     }
   

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        // Update the position of the player based on the input
        // if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        // {
        //     position.x -= speed * Time.deltaTime;
        // }
        //  if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        //  {
        //    position.x += speed * Time.deltaTime;
        // }
        position.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        if ( Input.GetKeyDown(KeyCode.Space))
            {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

            if (Physics.Raycast(transform.position, Vector3.down, out hit, distanceToGround, 1 >> 8))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);
        transform.position = position;
    }


}
