using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class TPSController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField]
    private float normalSensitivity;
    [SerializeField]
    private float aimSensitivity;

    private ThirdPersonController ThirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        ThirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        // If the aim button is pressed change to aim camera
        if (starterAssetsInputs.aim)
        {
            // Set the aim camera and decrease sensitivity
            aimVirtualCamera.gameObject.SetActive(true);
            ThirdPersonController.SetSensitivity(aimSensitivity);
        }
        else
        {
            // Set the normal camera and increase sensitivity
            aimVirtualCamera.gameObject.SetActive(false);
            ThirdPersonController.SetSensitivity(normalSensitivity);
        }
    }
}
