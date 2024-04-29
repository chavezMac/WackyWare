using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FishingMinigame : MonoBehaviour
{
    [SerializeField] private Transform topPivot;
    [SerializeField] private Transform bottomPivot;
    [SerializeField] private Transform fish;

    private float fishPosition;

    private float fishDestination;

    private float fishTimer;
    
    [SerializeField] private float timerMultiplicator = 3f;

    private float fishSpeed;
    [SerializeField] private float smoothMotion = 1f;

    [SerializeField] private Transform hook;
    private float hookPosition;
    [SerializeField] private float hookSize = 0.1f;
    [SerializeField] private float hookPower = 5f;
    private float hookProgress;
    private float hookPullvelocity;
    [SerializeField] private float hookPullPower = 0.01f;
    [SerializeField] private float hookGravityPower = 0.005f;
    [SerializeField] private float hookProgressDregradationPower = 0.1f;
    [SerializeField] private SpriteRenderer hookSpriteRenderer;

    [SerializeField] private Transform progressBarContainer;
    
    // Start is called before the first frame update
    void Start()
    {
        Resize();
    }

    void Resize()
    {
        Bounds b = hookSpriteRenderer.bounds;
        float ySize = b.size.y;
        Vector3 ls = hook.localScale;
        float distance = Vector3.Distance(topPivot.position, bottomPivot.position);
        ls.y = (distance / ySize) * hookSize;
        hook.localScale = ls;
    }

    // Update is called once per frame
    void Update()
    {
        Fish();
        Hook();
        ProgressCheck();
    }

    void ProgressCheck()
    {
        Vector3 ls = progressBarContainer.localScale;
        ls.y = hookProgress;
        progressBarContainer.localScale = ls;

        float min = hookPosition - hookSize / 2;
        float max = hookSize + hookSize / 2;

        if (min < fishPosition && fishPosition < max)
        {
            hookProgress += hookPower * Time.deltaTime;
        }
        else
        {

            hookProgress -= hookProgressDregradationPower * Time.deltaTime;
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
        
    }

    void Hook()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hookPullvelocity += hookPullPower * Time.deltaTime;
        }

        hookPullvelocity -= hookGravityPower * Time.deltaTime;

        hookPosition += hookPullvelocity;
        if (hookPosition <= 0f && hookPullvelocity < 0f)
        {
            hookPullvelocity = 0f;
        }

        if (hookPosition >= 1f && hookPullvelocity > 0f)
        {
            hookPullvelocity = 0f;
        }
        hookPosition = Mathf.Clamp(hookPosition, hookSize/2f, 1f-hookSize/2f);
        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPosition);
        
    }
    void Fish()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0f)
        {
            fishTimer = UnityEngine.Random.value * timerMultiplicator;

            fishDestination = UnityEngine.Random.value; 
        }

        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);
    }
}
