using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WodzillaController : MonoBehaviour
{
    public WodzillaTail tail;
    public float moveSpeed = 10f; // Movement speed
    public float turnSpeed = 2f; // Turning speed
    public Animator anim;

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
    }

    public void EndTailAttack()
    {
        tail.isActive = false;
    }
}
