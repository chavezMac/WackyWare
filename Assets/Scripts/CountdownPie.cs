using UnityEngine;
using UnityEngine.UI;

public class PieTimer : MonoBehaviour
{
    public Image pieImage;
    private float maxTime = 10f;
    public float currentTime;
    private bool timeUp = false;
    public MainGameController gameController;

    void Start()
    {
        
    }
    
    public void StartTimer()
    {
        currentTime = MainGameController.timeRemaining;
        timeUp = false;
    }

    void Update()
    {
        if (timeUp)
        {
            return;
        }
        
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            // Handle timer expiration
            Debug.Log("Time up!");
            timeUp = true;
        }

        float fillAmount = currentTime / maxTime;
        pieImage.fillAmount = fillAmount;
    }
}
