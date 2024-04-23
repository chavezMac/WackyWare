using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UprightManager : MonoBehaviour
{
    public static UprightManager instance;

    // Counter to track the number of upright objects
    public int uprightCount = 0;

    private void Awake()
    {
        //Check for only one UprightManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
