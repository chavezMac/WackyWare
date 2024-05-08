using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPostion;
    // Start is called before the first frame update
    void Start()
    {
        startingPostion = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        Transform siblingTransform = GetComponent<Transform>();
        float offset = Mathf.Sin(Time.time);
        siblingTransform.position = startingPostion + Vector3.right * offset;

    }
}
