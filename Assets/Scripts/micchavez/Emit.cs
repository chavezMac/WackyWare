using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emit : MonoBehaviour
{
    // bool playerIsOnFinishLine = false;
    private ParticleSystem _particleSystem;
    private AudioSource audioSource;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }
   
   // Trigger the particle system when the player enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _particleSystem.Play();
            audioSource.Play();
            other.tag = "PickedUp";
        }

    }
}
