using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    //Event
    public delegate void BalloonDied();
    public static event BalloonDied onBalloonDied;
    public GameManagerScript GameManger; 
    private Rigidbody2D rb;
    public float upMovement = 3.5f; // change to your liking 
    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
   void Update()
   {
       rb.AddForce(Vector2.up * upMovement);
       //Destroy(this.gameObject, 10f);
   }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Border")) // Check the tag instead of the name
        {
            Destroy(gameObject); // Destroy the balloon
            Debug.Log("LOST");
            MinigameBroadcaster.MinigameFailed();
            //GameManger.balloonEscaped = true;
            //onBalloonDied?.Invoke(); // Invoke the event if subscribed
        }
    }
   void animationCompleteDestroy()
   {
        
       onBalloonDied.Invoke();
       Destroy(gameObject);
       
   }

}
