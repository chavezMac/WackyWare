using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermanCredits : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim.Play("PlayerStill");
    }

    public void castLine()
    {
        anim.Play("playerSwingBack");
    }

    public void reelIn()
    {
        anim.Play("PlayerFishing");
    }
}
