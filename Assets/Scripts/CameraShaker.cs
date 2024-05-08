using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    // Adjust these values to control the intensity and duration of the shake
    private Vector3 originalLocalPosition;
    public Camera cam;

    private void Start()
    {
        // Save the original local position of the camera
        originalLocalPosition = cam.transform.localPosition;
    }

    public void ShakeCamera(float intensity, float duration, bool diminishIntensity = false)
    {
        StartCoroutine(Shake(intensity, duration, diminishIntensity));
    }

    private IEnumerator Shake(float intensity, float duration, bool diminishIntensity)
    {
        float elapsed = 0f;
        float currentIntensity = intensity;

        while (elapsed < duration)
        {
            // Calculate the shake amount using Perlin noise
            float x = Mathf.PerlinNoise(Time.time * currentIntensity, 0f) * 2f - 1f;
            float y = Mathf.PerlinNoise(0f, Time.time * currentIntensity) * 2f - 1f;

            // Apply the shake to the camera's local position
            Vector3 shakeAmount = new Vector3(x, y, 0f) * currentIntensity;
            cam.transform.localPosition = originalLocalPosition + shakeAmount;

            // Increment the elapsed time
            elapsed += Time.deltaTime;

            // Diminish intensity over time if specified
            if (diminishIntensity)
            {
                float diminishingFactor = 1f - (elapsed / duration);
                currentIntensity = intensity * diminishingFactor;
            }

            yield return null;
        }

        // Smoothly transition back to the original position
        float smoothness = 5f; // Adjust this value to control the smoothness of the transition
        while (Vector3.Distance(transform.localPosition, originalLocalPosition) > 0.001f)
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, originalLocalPosition, smoothness * Time.deltaTime);
            yield return null;
        }

        // Ensure the camera's local position is set to the original position after the shake is complete
        cam.transform.localPosition = originalLocalPosition;
    }
}