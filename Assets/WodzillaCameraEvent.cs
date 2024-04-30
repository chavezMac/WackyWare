using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WodzillaCameraEvent : MonoBehaviour
{
    public WodzillaController godzilla;

    public void EndAnimation()
    {
        godzilla.SetInControl(true);
    }
}
