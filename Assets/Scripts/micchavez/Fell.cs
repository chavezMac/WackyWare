using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fell : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
   
   // Trigger the particle system when the player enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Player"))
         {
                audioSource.Play();
         }
    }
}
