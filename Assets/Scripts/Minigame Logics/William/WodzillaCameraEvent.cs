using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WodzillaCameraEvent : MonoBehaviour
{
    public WodzillaController godzilla;

    private void Start()
    {
        MinigameBroadcaster.SetGameTimerPauseState(true);
    }

    public void EndAnimation()
    {
        godzilla.SetInControl(true);
        MinigameBroadcaster.SetGameTimerPauseState(false);
    }
}
