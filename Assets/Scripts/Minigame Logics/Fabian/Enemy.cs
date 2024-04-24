using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDied();
    public static event EnemyDied onEnemyDied;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        //this event lets everyone know that the enemy died 
        onEnemyDied.Invoke();
        Destroy(gameObject);
      Debug.Log("Ouch!");
    }
}
