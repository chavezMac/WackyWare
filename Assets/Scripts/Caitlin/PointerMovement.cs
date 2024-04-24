using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMovement : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 leftPosition;
    private Vector3 rightPosition;
    private bool movingRight = true;

    void Start()
    {
        // Define left and right positions relative to the bar's transform
        float halfBarWidth = transform.localScale.x / 2f;
        leftPosition = transform.parent.position - new Vector3(halfBarWidth, 0f, 0f);
        rightPosition = transform.parent.position + new Vector3(halfBarWidth, 0f, 0f);
    }

    void Update()
    {
        // Move the pointer back and forth
        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPosition, speed * Time.deltaTime);
            if (transform.position == rightPosition)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, leftPosition, speed * Time.deltaTime);
            if (transform.position == leftPosition)
            {
                movingRight = true;
            }
        }
    }
}
