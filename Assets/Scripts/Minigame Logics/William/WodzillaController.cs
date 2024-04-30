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
    public LineRenderer lineRendererL;
    public LineRenderer lineRendererR;
    public GameObject LaserHitL;
    public GameObject LaserHitR;

    public AudioSource roar;
    public AudioSource lasersfx;
    public AudioSource stomp;
    public AudioSource impact;
    public AudioClip[] impactSounds; // Array of footstep sound effects
    public AudioClip[] footstepSounds; // Array of footstep sound effects

    public int hp = 3;
    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(PeriodicRoar());
        hp += MainGameController.minigamesCompletedSuccessfully;//dynamic difficulty HP
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
        
        if (Input.GetMouseButton(0) && !tail.isActive) // Check for left mouse button click
        {
            ShootLaser(leftEye,lineRendererL,LaserHitL);
            ShootLaser(rightEye,lineRendererR,LaserHitR);
        }
        else
        {
            lineRendererL.gameObject.SetActive(false);
            lineRendererR.gameObject.SetActive(false);
            lasersfx.Stop();
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
    
    void ShootLaser(Transform eye, LineRenderer laser, GameObject laserHit)
    {
        laser.gameObject.SetActive(true);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!lasersfx.isPlaying)
        {
            lasersfx.Play();
        }

        if (Physics.Raycast(eye.position, ray.direction, out hit, Mathf.Infinity, layerMask))
        {
            laser.SetPosition(0, eye.position);
            laser.SetPosition(1, hit.point);

            // Offset the hit effect position by 0.1 away from where it hit
            Vector3 hitEffectPosition = hit.point + ray.direction * 0.1f;

            // Set the position and rotation of the hit effect
            laserHit.transform.position = hitEffectPosition;

            // Calculate the rotation to make the hit effect face the laser's origin
            Quaternion rotation = Quaternion.LookRotation((eye.position - hitEffectPosition).normalized);
            laserHit.transform.rotation = rotation;

            if (hit.collider.CompareTag("WodzillaDestructible"))
            {
                // Trigger the TakeDamage function on the hit object
                hit.collider.GetComponent<WodzillaBuilding>().TakeDamage(0.5f);
            }
            
            if (hit.collider.CompareTag("WodzillaHelicopter"))
            {
                // Trigger the TakeDamage function on the hit object
                hit.collider.GetComponent<WodzillaHelicopter>().TakeDamage(0.5f);
            }
            
        }
        // Debug.Log(hit + " at " + hit.point.ToString());
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

    public void TakeDamage(int damage)
    {
        Debug.Log("Wodzilla took damage! Wodzilla has " + hp + " hit points left!");
        hp -= damage;
        if (hp <= 0)
        {
            MinigameBroadcaster.MinigameFailed();
            MinigameMusic music = FindObjectOfType<MinigameMusic>();
            music.FadeOutMusicFailure();
        }
    }
}
