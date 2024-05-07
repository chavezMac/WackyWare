using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WodzillaCameraEvent : MonoBehaviour
{
    public WodzillaController godzilla;
    public Text bonusNotification;

    private void Start()
    {
        MinigameBroadcaster.SetGameTimerPauseState(true);
    }

    public void EndAnimation()
    {
        godzilla.SetInControl(true);
        MinigameBroadcaster.SetGameTimerPauseState(false);
        if (MainGameController.minigamesCompletedSuccessfully >= 1)
        {
            godzilla.StartCoroutine(godzilla.Grow(MainGameController.minigamesCompletedSuccessfully / 10f , true) );
        }

        var anim = GetComponent<Animator>();
        FindObjectOfType<CameraShaker>().isShaking = false;
        anim.enabled = false;
    }

    public void spawnFristHelicopter()
    {
        var logic = FindObjectOfType<BossMinigameLogic>();
        logic.SpawnHelicopertAtSpot(new Vector3(-190, 42.54f, 0));
    }
}
