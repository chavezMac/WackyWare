using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    public Button skipButton;
    public Text buttonText;

    private bool skip = false;
    // Start is called before the first frame update
    void Start()
    {
        var controller = FindObjectOfType<MainGameController>().gameObject;
        if (controller != null)
        {
            Destroy(controller);
        }
    }

    public void Skip()
    {
        if (skip)
        {
            ReturnToMenu();
        }

        buttonText.fontSize = 32;
        buttonText.text = "Wait, no, don't skip! I worked so hard on these credits!";
        skip = true;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu Scene");
    }
}
