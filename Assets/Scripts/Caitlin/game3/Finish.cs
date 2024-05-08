using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    // When player reaches the end
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<MainGameController>().MinigameDone(true);
            Debug.Log("Finished!");
        }
    }
}
