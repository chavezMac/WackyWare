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
    public bool inControl = false;
    public Animator anim;
    public CameraShaker cam;

    public float scaleModifier = 1.5f;
    public float growDuration = 0.5f;
    private bool isGrowing = false;
    public AnimationCurve growthCurve;
    
    public GameObject laserPrefab;
    public Transform leftEye;
    public Transform rightEye;
    public LayerMask layerMask;
    public LineRenderer lineRendererL;
    public LineRenderer lineRendererR;
    public GameObject LaserHitL;
    public GameObject LaserHitR;
    // public GameObject debugSphere;

    public AudioSource roar;
    public AudioSource lasersfx;
    public AudioSource stomp;
    public AudioSource impact;
    public AudioSource miscsfx;
    public AudioClip[] impactSounds; // Array of footstep sound effects
    public AudioClip[] footstepSounds; // Array of footstep sound effects
    public bool audioEnabled = true; //Debug feature
    
    public int hp = 3;
    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(PeriodicRoar());
        hp += MainGameController.minigamesCompletedSuccessfully / 3;//dynamic difficulty HP
        scaleModifier += MainGameController.minigamesCompletedSuccessfully / 10f;
        transform.localScale = new Vector3(scaleModifier, scaleModifier, scaleModifier);
        moveSpeed += MainGameController.minigamesCompletedSuccessfully / 2f;

        //Audio debugging
        roar.mute = !audioEnabled;
        lasersfx.mute = !audioEnabled;
        stomp.mute = !audioEnabled;
        impact.mute = !audioEnabled;
        miscsfx.mute = !audioEnabled;
    }

    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && inControl)
        {
            anim.Play("WodzillaSpin");
            tail.isActive = true;
        }
        
        if (Input.GetMouseButton(0) && !tail.isActive && inControl) // Check for left mouse button click
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

    public void SetInControl(bool cancontrol)
    {
        inControl = cancontrol;
    }

    private void PlayerMovement()
    {
        if (!inControl)
        {
            return;
        }
        // Get input for movement
        float verticalInput = -Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate movement and rotation
        Vector3 moveDirection = transform.forward * verticalInput * moveSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, horizontalInput * turnSpeed * Time.deltaTime, 0f);

        // Apply movement and rotation
        transform.Translate(moveDirection, Space.World);
        transform.rotation *= turnRotation;
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

        var mouse = Input.mousePosition;

        if (!lasersfx.isPlaying)
        {
            lasersfx.Play();
        }

        if (Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, Mathf.Infinity, layerMask))
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
        // debugSphere.transform.position = hit.transform.position;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WodzillaPowerup"))
        {
            StartCoroutine(Grow());
            hp++;
            Destroy(other.gameObject);
        }
    }

    private IEnumerator Grow()
    {
        miscsfx.Play();
        if (!isGrowing)
        {
            isGrowing = true;
            inControl = false;
            anim.speed = 0f;

            // Calculate target scale
            float targetScale = scaleModifier + 0.5f;

            // Gradually increase scale over time using animation curve
            float elapsedTime = 0;
            while (elapsedTime < growDuration)
            {
                float curveValue = growthCurve.Evaluate(elapsedTime / growDuration);
                float newScale = Mathf.Lerp(scaleModifier, targetScale, curveValue);
                transform.localScale = new Vector3(newScale, newScale, newScale);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure final scale matches the target scale exactly
            transform.localScale = new Vector3(targetScale, targetScale, targetScale);

            // Update scale modifier
            scaleModifier = targetScale;

            isGrowing = false;
            anim.speed = 1;
            inControl = true;
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

        AudioClip randomImpactSound = impactSounds[Random.Range(0, impactSounds.Length)];
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
