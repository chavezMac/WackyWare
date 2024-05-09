using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public Button startButton;
    public Button howtoplayButton;
    public Button nextButton1;
    public Button nextButton2;
    public Text qualityButton;
    public Button fullscreenButton;
    public GameObject titleText;
    public GameObject mainMenuSelection;
    public GameObject tutorialCard1;
    public GameObject tutorialCard2;
    public GameObject GameControler;
    public GameObject readthisfirst;
    
    // Start is called before the first frame update
    void Start()
    {
        tutorialCard1.SetActive(false);
        tutorialCard2.SetActive(false);
        startButton.onClick.AddListener(StartGame);
        howtoplayButton.onClick.AddListener(HowToPlay);
        nextButton1.onClick.AddListener(Next1);
        nextButton2.onClick.AddListener(Next2);
        int currentQualityLevel = QualitySettings.GetQualityLevel();
        Debug.Log("Initial Quality Level: " + currentQualityLevel);
        qualityButton.text = "Current quality level: " + QualitySettings.GetQualityLevel();
    }

    public void ToggleFullScreenMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    
    public void CycleQualitySettings()
    {
        // Get the total number of quality levels
        int totalQualityLevels = QualitySettings.names.Length;

        // Get the current quality level
        int currentQualityLevel = QualitySettings.GetQualityLevel();

        // Calculate the next quality level
        int nextQualityLevel = (currentQualityLevel + 1) % totalQualityLevels;

        // Set the next quality level
        QualitySettings.SetQualityLevel(nextQualityLevel, true);

        Debug.Log("New Quality Level: " + nextQualityLevel);
        qualityButton.text = "Current quality level: " + QualitySettings.GetQualityLevel();
    }

    private void StartGame()
    {
        // Instantiate(GameControler);
        GameControler.SetActive(true);
        mainMenuSelection.SetActive(false);
        var music = FindObjectOfType<MinigameMusic>();
        if (music != null)
        {
            music.FadeOutMusic();
        }
    }
    
    private void HowToPlay()
    {
        Destroy(readthisfirst);
        mainMenuSelection.SetActive(false);
        tutorialCard1.SetActive(true);
    }
    
    private void Next1()
    {
        tutorialCard1.SetActive(false);
        tutorialCard2.SetActive(true);
    }
    
    private void Next2()
    {
        startButton.interactable = true;
        tutorialCard2.SetActive(false);
        mainMenuSelection.SetActive(true);
    }
}
