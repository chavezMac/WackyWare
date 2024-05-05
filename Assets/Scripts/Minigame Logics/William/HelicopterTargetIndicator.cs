using System;
using UnityEditor.Media;
using UnityEngine;
using UnityEngine.UI;

public class HelicopterTargetIndicator : MonoBehaviour
{
    public Transform helicopterTransform; // Reference to the helicopter's transform
    public Text shootText; // Reference to the "shoot it!" text
    public Camera mainCamera; // Reference to the main camera

    void Update()
    {
        SetScreenPosition();
    }

    public void SetScreenPosition()
    {
        if (helicopterTransform == null)
        {
            shootText.gameObject.SetActive(false);
            Destroy(this);
            return;
        }
        // Calculate the position of the helicopter in screen space
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(helicopterTransform.position);

        // Set the position of the "shoot it!" text
        shootText.rectTransform.position = screenPoint;
    }
}