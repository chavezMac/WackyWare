using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public Button startButton;
    public Button howtoplayButton;
    public Button nextButton1;
    public Button nextButton2;
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
