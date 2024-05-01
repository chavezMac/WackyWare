using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMovement : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 leftPosition;
    private Vector3 rightPosition;
    private bool movingRight = true;
    public GameObject bar;

    void Start()
    {
        bar = GetComponent<GameObject>();
        // Define left and right positions relative to the bar's transform
        float halfBarWidth = bar.transform.localScale.x / 2f - 1;
        Debug.Log(halfBarWidth);
        leftPosition = transform.parent.position - new Vector3(halfBarWidth, 0f, 0f);
        rightPosition = transform.parent.position + new Vector3(halfBarWidth, 0f, 0f);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
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
                Debug.Log("Changed");
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
