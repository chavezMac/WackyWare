using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public GameObject bullet;
  public string inputAxis;
  public float speed;
  public Transform shottingOffset;
  public Trail2D trail;
  private GameObject[] shipTails;
  private float ogSpeed;
  private float ogTailLength;
  private bool killedPlanet;
  public GameObject explosion;
  private Vector3 explosionLocation = new Vector3(); 
  public GameManger GameManger;
  
  private void Start()
  {
    killedPlanet = false;
    shipTails = GameObject.FindGameObjectsWithTag("ShipTail");
    //subscribes to the method in enemy whihc allows us to know when enemy died
    Enemy.onEnemyDied += EnemyOnEnemyDied;
    ogSpeed = speed;
    ogTailLength = shipTails[0].transform.localScale.y;
  }

  private void OnDestroy()
  {
    Enemy.onEnemyDied -= EnemyOnEnemyDied;
  }

  void EnemyOnEnemyDied(bool died , Vector3 pos)
  {
    killedPlanet = died;
    explosionLocation = pos; 
    //Debug.Log("Player Recieved EnemyDied Event");
  }
    // Update is called once per frame

    void Update()
    {
      if (killedPlanet)
      {
        GameManger.ballonsToShootDown--;
        killedPlanet = false;
        Instantiate(explosion,explosionLocation, Quaternion.identity);
        gameObject.GetComponent<AudioSource>().Play();

       

      }
      float direction = Input.GetAxis(inputAxis);
      Vector3 newPosition = new Vector3();

      /*
      if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))

      {
        //newPosition = transform.position + new Vector3(direction, 0, 0) * ogSpeed * Time.deltaTime;
        shipTails[0].GetComponent<Trail2D>().on = true;
        shipTails[1].GetComponent<Trail2D>().on = true;

      }
      else
      {
        //newPosition = transform.position + new Vector3(direction, 0, 0) * ogSpeed * Time.deltaTime;
        shipTails[0].GetComponent<Trail2D>().on =false;
        shipTails[1].GetComponent<Trail2D>().on = false;
      }
      */

      if (Input.GetKey(KeyCode.LeftShift))
      {
        speed = ogSpeed + 2f;
        newPosition = transform.position + new Vector3(direction, 0, 0) * speed * Time.deltaTime;
        shipTails[0].GetComponent<Trail2D>().on = true;
        shipTails[1].GetComponent<Trail2D>().on = true;
        shipTails[0].transform.localScale = new Vector3(0.0006f, 0.0008f, 0.0006f);
        shipTails[1].transform.localScale = new Vector3(0.0006f, 0.0008f, 0.0006f);
        shipTails[0].GetComponent<Trail2D>().width = 4f;
        shipTails[1].GetComponent<Trail2D>().width = 4f;
      }
      else
      {
        newPosition = transform.position + new Vector3(direction, 0, 0) * ogSpeed * Time.deltaTime;
        shipTails[0].transform.localScale = new Vector3(0.0006f, 0.0006f, 0.0006f);
        shipTails[1].transform.localScale = new Vector3(0.0006f, 0.0006f, 0.0006f);
        shipTails[0].GetComponent<Trail2D>().on = false;
        shipTails[1].GetComponent<Trail2D>().on = false;
        shipTails[0].GetComponent<Trail2D>().width = 2f;
        shipTails[1].GetComponent<Trail2D>().width = 2f;
      }
      transform.position = newPosition;
      
      if (Input.GetKeyDown(KeyCode.Space))
      {
        //playerShooting.PlayOneShot(ShotClip);
        
        //GetComponent<Animator>().SetTrigger("Shoot Trigger");
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        Destroy(shot, 3f);
        
      }
    }
}
