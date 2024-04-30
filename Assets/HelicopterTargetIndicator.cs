using UnityEngine;
using UnityEngine.UI;

public class HelicopterTargetIndicator : MonoBehaviour
{
    public Transform helicopterTransform; // Reference to the helicopter's transform
    public HorizontalLayoutGroup shootText; // Reference to the "shoot it!" text
    public Image arrowImage; // Reference to the arrow image
    public Camera mainCamera; // Reference to the main camera

    void Update()
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
        shootText.transform.position = screenPoint;

        // Set the position of the arrow image
        // arrowImage.rectTransform.position = screenPoint;

        // Adjust the position of the arrow image to point towards the helicopter
        Vector3 direction = helicopterTransform.position - mainCamera.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // arrowImage.rectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}