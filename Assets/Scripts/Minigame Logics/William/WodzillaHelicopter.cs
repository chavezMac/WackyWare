using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WodzillaHelicopter : MonoBehaviour
{
    public GameObject mainBlades;
    public float mainBladesSpeed = 500f;
    public float hp = 100;
    

    private void Update()
    {
        // Rotate the main blades
        mainBlades.transform.Rotate(Vector3.up, mainBladesSpeed * Time.deltaTime);
    }
}
