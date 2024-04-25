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
    public CameraShaker cam;
    
    public GameObject laserPrefab;
    public Transform leftEye;
    public Transform rightEye;
    public LayerMask layerMask;

    public AudioSource roar;
    public AudioSource laser;
    public AudioSource stomp;
    public AudioSource impact;
    public AudioClip[] impactSounds; // Array of footstep sound effects
    public AudioClip[] footstepSounds; // Array of footstep sound effects
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(PeriodicRoar());
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

    public IEnumerator PeriodicRoar()
    {
        yield return new WaitForSeconds(6 + Random.Range(0,3));
        roar.Play();
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
        cam.ShakeCamera(4f,.5f, true);
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
    
    public void PlayRandomImpactSound()
    {
        cam.ShakeCamera(7f,1f, true);
        if (impactSounds.Length == 0)
        {
            Debug.LogWarning("Impact sounds array is empty.");
            return;
        }

        // Randomly select a footstep sound from the array
        AudioClip randomImpactSound = impactSounds[Random.Range(0, impactSounds.Length)];

        // Play the selected footstep sound
        
        // impact.clip = randomImpactSound;
        // impact.Play();
        impact.PlayOneShot(randomImpactSound);
    }
}
