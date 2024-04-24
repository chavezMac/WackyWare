using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    public GameObject dustSystem;
    public BossMinigameLogic logic;

    private void Start()
    {
        initialPosition = transform.position;
        logic.UpdateBuildingCount(1);
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

        float x = transform.position.x;
        float z = transform.position.z;
        GameObject dust = Instantiate(dustSystem, new Vector3(x,0f,z), quaternion.identity);
        dust.GetComponent<WodzillaBuildingDust>().building = this.gameObject;
        Destroy(dust,10f);
    }

    IEnumerator WobbleCoroutine()
    {
        float timer = 0f;
        while (!isCrumbling && timer < crumbleDuration)
        {
            float offsetX = Mathf.PerlinNoise(Time.time * wobbleSpeed, 0f) * wobbleAmount;
            float offsetY = Mathf.PerlinNoise(0f, Time.time * wobbleSpeed) * wobbleAmount;
            float offsetZ = Mathf.PerlinNoise(Time.time * wobbleSpeed, Time.time * wobbleSpeed) * wobbleAmount;

            Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);

            transform.position = initialPosition + offset;
            timer += Time.deltaTime;

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
        logic.UpdateBuildingCount(-1);
        Destroy(gameObject);
    }

    IEnumerator CrumbleAnimation()
    {
        float timer = 0f;

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

        Destruct();
    }
}
