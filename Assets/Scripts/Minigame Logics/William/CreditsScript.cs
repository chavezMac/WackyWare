using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    public Button skipButton;
    public Text buttonText;
    public GameObject obstacleContainer;
    public GameObject fadeBlack;

    private bool skip = false;
    // Start is called before the first frame update
    void Start()
    {
        fadeBlack.SetActive(true);
        obstacleContainer.SetActive(false);
        var controller = FindObjectOfType<MainGameController>().gameObject;
        if (controller != null)
        {
            Destroy(controller);
        }
    }

    public void DropObstacles()
    {
        obstacleContainer.SetActive(true);
    }

    public void Skip()
    {
        if (skip)
        {
            ReturnToMenu();
        }

        buttonText.fontSize = 32;
        buttonText.text = "Wait, no, don't skip! I worked so hard on these credits!\n(Skip anyways)";
        skip = true;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu Scene");
    }
}
