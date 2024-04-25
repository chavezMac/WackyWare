using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class WodzillaController : MonoBehaviour
{
    public WodzillaTail tail;
    public float moveSpeed = 10f; // Movement speed
    public float turnSpeed = 2f; // Turning speed
    public Animator anim;
    
    public GameObject laserPrefab;
    public Transform leftEye;
    public Transform rightEye;
    public LayerMask layerMask;

    public AudioSource roar;
    public AudioSource laser;
    public AudioSource stomp;
    public AudioSource impact;
    public AudioClip[] footstepSounds; // Array of footstep sound effects
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input for movement
        float verticalInput = -Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate movement and rotation
        Vector3 moveDirection = transform.forward * verticalInput * moveSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, horizontalInput * turnSpeed * Time.deltaTime, 0f);

        // Apply movement and rotation
        transform.Translate(moveDirection, Space.World);
        transform.rotation *= turnRotation;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.Play("WodzillaSpin");
            tail.isActive = true;
        }
        
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            ShootLaser(leftEye);
            ShootLaser(rightEye);
        }
    }

    public void EndTailAttack()
    {
        tail.isActive = false;
    }
    
    void ShootLaser(Transform eye)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(eye.position, ray.direction, out hit, Mathf.Infinity, layerMask))
        {
            GameObject laser = Instantiate(laserPrefab, eye.position, Quaternion.identity);
            LineRenderer lineRenderer = laser.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, eye.position);
            lineRenderer.SetPosition(1, hit.point);
        }
    }
    
    public void PlayRandomFootstepSound()
    {
        if (footstepSounds.Length == 0)
        {
            Debug.LogWarning("Footstep sounds array is empty.");
            return;
        }

        // Randomly select a footstep sound from the array
        AudioClip randomFootstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];

        // Play the selected footstep sound
        stomp.PlayOneShot(randomFootstepSound);
    }
}
