using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoneSwitch : MonoBehaviour
{
    public string triggerTag;

    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera[] virtualCameras;

    private void Start()
    {
        SwitchToCamera(primaryCamera);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchToCamera(targetCamera);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            SwitchToCamera(primaryCamera);
        }
    }

    private void SwitchToCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach (CinemachineVirtualCamera camera in virtualCameras)
        {
            camera.enabled = camera == targetCamera;
        }
    }   
}
