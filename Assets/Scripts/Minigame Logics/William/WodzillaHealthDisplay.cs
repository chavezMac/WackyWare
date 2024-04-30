using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WodzillaHealthDisplay : MonoBehaviour
{
    public WodzillaController godzilla;
    public BossMinigameLogic logic;
    public Text buildingsLeft;
    public GameObject[] heartIcons;
    void Start()
    {
        godzilla = FindObjectOfType<WodzillaController>();
        logic = FindObjectOfType<BossMinigameLogic>();
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }
    
    public void UpdateUI()
    {
        buildingsLeft.text = "Buildings left: " + logic.buildingsRemaining.ToString();
        
        // Update heart icons based on Godzilla's hit points
        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (i < godzilla.hp)
            {
                // Heart icon should be filled if hit points are greater than current index
                heartIcons[i].SetActive(true);
            }
            else
            {
                // Heart icon should be empty if hit points are equal to or less than current index
                heartIcons[i].SetActive(false);
            }
        }
    }
}
