using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleMinigame1Logic : MonoBehaviour
{
    void Update()
    {
        if (MainGameController.timeRemaining <= 0)
        {
            MinigameBroadcaster.MinigameFailed();
        }
    }
}
