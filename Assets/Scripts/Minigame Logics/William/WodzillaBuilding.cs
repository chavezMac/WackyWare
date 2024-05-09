using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class WodzillaBuilding : MonoBehaviour
{
    public float hp = 100;
    public float wobbleAmount = 3f; // Adjust the intensity of the wobble
    public float wobbleSpeed = 15f; // Adjust the speed of the wobble
    private Vector3 initialPosition;

    public bool isCrumbling = false;
    public float crumbleDuration = 3f;
    public float sinkSpeed = 35f;

    public GameObject dustSystem;
    public GameObject dustSystemLow;
    public BossMinigameLogic logic;
    private AudioSource sfx;
    
    private void Start()
    {
        initialPosition = transform.position;
        logic.UpdateBuildingCount(1);
        sfx = GetComponent<AudioSource>();
        BoxCollider boxC = GetComponent<BoxCollider>();
        NavMeshObstacle nav = GetComponent<NavMeshObstacle>();
        nav.size = boxC.size;
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
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
        
        if (damage > 10f)
        {
            GameObject dustQual = dustSystem;
            if (QualitySettings.GetQualityLevel() == 0)
            {
                dustQual = dustSystemLow;
            }
            GameObject dust = Instantiate(dustQual, new Vector3(x,0f,z), quaternion.identity);
            dust.GetComponent<WodzillaBuildingDust>().building = this.gameObject;
            Destroy(dust,6f);
        }
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
            sfx.Play();
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
