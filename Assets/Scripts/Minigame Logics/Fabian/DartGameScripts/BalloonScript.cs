using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name);
        
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {

            if (collision.gameObject.name == "Bullet(Clone)")
            {
               // GetComponent<Animator>().SetTrigger("gotHit");
                Destroy(collision.gameObject);
                Destroy(this.gameObject);

                

                
                
            }
        


    }
}
