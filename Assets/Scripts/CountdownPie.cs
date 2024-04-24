using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PieTimer : MonoBehaviour
{
    public Image pieImage;
    private float maxTimeLimit;
    private float currentTime;
    private bool timeUp = true;
    public MainGameController gameController;
    public GameObject ClockHand;
    public Animator anim;
    public Color[] colors;
    private void Start()
    {
        gameController = FindObjectOfType<MainGameController>();
        // anim = GetComponent<Animator>();
        anim.speed = 0;
    }
    
    public void StartTimer(float maxTime)
    {
        maxTimeLimit = maxTime;
        currentTime = maxTime;
        timeUp = false;
        anim.Play("ClockAnimation1");
    }

    void Update()
    {
        if (timeUp)
        {
            return;
        }
        
        currentTime = MainGameController.timeRemaining;

        if (currentTime <= 0)
        {
            currentTime = 0;
            // Handle timer expiration
            Debug.Log("Time up!");
            timeUp = true;
            anim.Play("ClockAnimation2");
            // gameController.MinigameDone(false);
        }

        float fillAmount = Mathf.Clamp(currentTime / maxTimeLimit,0f,1f);
        float ratioDone = 1 - fillAmount;
        pieImage.fillAmount = fillAmount;
        Quaternion rotation = Quaternion.Euler(0f, 0f, (1 - fillAmount) * 360f);
        ClockHand.transform.rotation = rotation;
        if (fillAmount > 0)
        {
            anim.speed = 1f / fillAmount;
            pieImage.color = colors[(int)Mathf.Floor(ratioDone*colors.Length)];
        }
        else
        {
            anim.speed = 10f;
        }
    }
}
