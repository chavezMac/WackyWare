using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
     private int itemsLeft;

    void Start()
    {
        //Set the number of items left to collect
        GameObject[] items = GameObject.FindGameObjectsWithTag("PickUp");
        itemsLeft = items.Length;
    }
    //This minigame simply has the win trigger in the onClick event of the button
    void Update()
    {
        //Check if time has run out, and if so, we fail the minigame
        if (MainGameController.timeRemaining <= 0)
        {
            MinigameBroadcaster.MinigameFailed();
        }

        int itemsLeft = GetItemsLeft();
        if (itemsLeft <= 0)
        {
            //change color of the finish line to green
            MinigameBroadcaster.MinigameCompleted();
        }
        //Check if player has collected all the items

    }

    public int GetItemsLeft()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("PickUp");
        int itemsLeft = items.Length;
        return itemsLeft;
    }
}
