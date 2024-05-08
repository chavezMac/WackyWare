using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            //Reset the players position
            other.transform.position = new Vector3(-2f, 0f, -2.7f);
            Destroy(gameObject,2f);
        }
    }
}
