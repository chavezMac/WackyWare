using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem[] explosion; 
    public delegate void EnemyDied(bool died , Vector3 pos);
    public static event EnemyDied onEnemyDied;
    

    private void Start()
    {
        
        
    }

    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        //gameObject.GetComponent<AudioSource>().Play();
        Destroy(collision.gameObject);
        //this event lets everyone know that the enemy died 
        onEnemyDied.Invoke(true , gameObject.transform.position);
        Destroy(gameObject );
      Debug.Log("Ouch!");
    }
}
