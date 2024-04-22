using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WodzillaBuilding : MonoBehaviour
{
    public int hp = 100;
    public float wobbleAmount = 3f; // Adjust the intensity of the wobble
    public float wobbleSpeed = 15f; // Adjust the speed of the wobble
    private Vector3 initialPosition;

    public bool isCrumbling = false;
    public float crumbleDuration = 3f;
    public float sinkSpeed = 35f;

    private void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Crumble();
        }
        else
        {
            //play wobble animation
            StartCoroutine(WobbleCoroutine());
        }
    }

    IEnumerator WobbleCoroutine()
    {
        while (!isCrumbling)
        {
            float offsetX = Mathf.PerlinNoise(Time.time * wobbleSpeed, 0f) * wobbleAmount;
            float offsetY = Mathf.PerlinNoise(0f, Time.time * wobbleSpeed) * wobbleAmount;
            float offsetZ = Mathf.PerlinNoise(Time.time * wobbleSpeed, Time.time * wobbleSpeed) * wobbleAmount;

            Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);

            transform.position = initialPosition + offset;

            yield return null; // Wait for the next frame
        }
    }
    
    void Crumble()
    {
        if (!isCrumbling)
        {
            isCrumbling = true;
            wobbleAmount *= 1.5f;
            StartCoroutine(CrumbleAnimation());
        }
    }

    public void Destruct()
    {
        Destroy(gameObject);
    }

    IEnumerator CrumbleAnimation()
    {
        float timer = 0f;
        Vector3 targetPosition = initialPosition - Vector3.up * 1500f; // Adjust the sink depth as needed

        while (timer < crumbleDuration)
        {
            // Apply wobbling effect during crumbling
            float offsetX = Mathf.PerlinNoise(Time.time * wobbleSpeed, 0f) * wobbleAmount;
            float offsetY = Mathf.PerlinNoise(0f, Time.time * wobbleSpeed) * wobbleAmount;
            float offsetZ = Mathf.PerlinNoise(Time.time * wobbleSpeed, Time.time * wobbleSpeed) * wobbleAmount;

            Vector3 offset = new Vector3(offsetX, offsetY- (timer*sinkSpeed), offsetZ);

            transform.position = initialPosition + offset;

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // Destroy the building after crumble animation
    }
}
