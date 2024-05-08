using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private AudioSource audioSource;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }
   
   // Trigger the particle system when the player enters the trigger zone
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered the trigger zone");
        if (other.gameObject.CompareTag("Player"))
        {
            particleSystem.Play();
            audioSource.Play();
            
            //Turn gameObject green
            GetComponent<Renderer>().material.color = Color.green;
            //Change the tag of the gameObject
            gameObject.tag = "PickedUp";
        }
    }
}
