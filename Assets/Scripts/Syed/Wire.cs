using System;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    private Vector3 startPoint;
    private Vector3 startPosition;
    public GameObject lightOn;
    private static int wireNo;
    private void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;
        wireNo = 0;

    }

    private void OnMouseDrag()
    {
        // Calculate the new position based on the mouse position and the offset
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Make sure the object stays on the same z-axis position
        newPosition.z = 0;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                updateWire(collider.transform.position);
                if (transform.parent.name.Equals(collider.transform.parent.name))
                {
                    collider.GetComponent<Wire>()?.Done();
                    Done();
                    wireNo += 1;
                    if (wireNo == 4)
                    {
                        MinigameBroadcaster.MinigameCompleted();
                    }
                }
               
                return;
            }
        }
        // MinigameBroadcaster.MinigameCompleted();
        // Update the object's position
        updateWire(newPosition);
    }

    void Done()
    {
        lightOn.SetActive(true);
        Destroy(this);
    }

    private void OnMouseUp()
    {
        updateWire(startPosition);
    }

    void updateWire(Vector3 newPosition)
    {
        transform.position = newPosition;

        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        float dist = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }

    private void Update()
    {
        if (MainGameController.timeRemaining <= 0)
        {
            MinigameBroadcaster.MinigameFailed();
        }
    }
}
