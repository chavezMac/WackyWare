using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class BalloonMovement : MonoBehaviour
{
    public delegate void BalloonDied();
    public static event BalloonDied onBalloonDied;

    [Header("Dissolve Balloon")] [SerializeField]
    private float _dissolveTime = .75f;
    private SpriteRenderer _spriteRenderers;
    private Material _materials;
    public GameObject script;
    private int _dissolveAmount = Shader.PropertyToID("_DissolveAmount");
    private Rigidbody2D rb;
    public float upMovement = 3.5f;
    private GameObject  logigObject;

    private UnityEngine.UI.Image image;
    private Material material;

    private void Start()
    {
        
        logigObject = GameObject.FindGameObjectWithTag("MiniGameLogic");
        image = GetComponent<Image>();
        if (image == null)
        {
           // Debug.LogError("Image component not found on the GameObject.");
            return;
        }

        material = new Material(image.material);
        GetComponent<Image>().material = material;
        if (material == null)
        {
           // Debug.LogError("Material not found on the Image component.");
            return;
        }

        material.SetFloat("_DissolveAmount",0f);
       // Debug.Log("Material initialized: " + material.name); // Debug log to check material initialization

        rb = GetComponent<Rigidbody2D>();
    }


       
    



    void Update()
    {
        Vector2 upDirection = Vector2.up;
        rb.velocity =  upDirection * upMovement;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Border"))
        {
            //script.balloonEscaped = true;
            Debug.Log("Balloon hit border and destroyed");
            Destroy(gameObject);
            MinigameBroadcaster.MinigameFailed();


        }
    }

    void animationCompleteDestroy()
    {
        onBalloonDied.Invoke();
        Destroy(gameObject);
    }

    public IEnumerator Vanish()
    {
        if (material == null)
        {
           // Debug.LogError("Material is null in the Vanish coroutine.");
            yield break;
        }

        float timeElapsed = 0f;
        while (timeElapsed < _dissolveTime)
        {
            timeElapsed += Time.deltaTime;
            float lerpedDissolve = Mathf.Lerp(0f, 1.1f, timeElapsed / _dissolveTime);
            //Debug.Log("Dissolve amount before: " + material.GetFloat("_DissolveAmount"));
            material.SetFloat("_DissolveAmount", lerpedDissolve);
           // Debug.Log("Dissolve amount after: " + material.GetFloat("_DissolveAmount"));
            yield return null;
        }

        material.SetFloat("_DissolveAmount", 1.1f);
        Destroy(gameObject);

    }


}