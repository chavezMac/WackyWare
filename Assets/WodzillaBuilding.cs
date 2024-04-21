using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WodzillaBuilding : MonoBehaviour
{
    public int hp = 100;
    public float wobbleAmount = 0.5f; // Adjust the intensity of the wobble
    public float wobbleSpeed = 1f; // Adjust the speed of the wobble
    private Vector3 initialPosition;
    
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
        }
    }

    IEnumerator WobbleCoroutine()
    {
        while (true)
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
        
    }
}
