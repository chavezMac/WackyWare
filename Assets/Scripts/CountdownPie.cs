using UnityEngine;
using UnityEngine.UI;

public class PieTimer : MonoBehaviour
{
    public Image pieImage;
    private float maxTime = 10f;
    public float currentTime;
    private bool timeUp = false;
    public MainGameController gameController;
    private void Start()
    {
        gameController = FindObjectOfType<MainGameController>();
    }
    
    public void StartTimer()
    {
        maxTime = MainGameController.timeRemaining;
        currentTime = maxTime;
        timeUp = false;
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
            // gameController.MinigameDone(false);
        }

        float fillAmount = currentTime / maxTime;
        pieImage.fillAmount = fillAmount;
    }
}
