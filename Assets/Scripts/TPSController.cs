using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class TPSController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField]
    private Rig aimLayer;
    [SerializeField]
    private float normalSensitivity;
    [SerializeField]
    private float aimSensitivity;
    [SerializeField]
    private LayerMask aimColliderLayerMask;
    [SerializeField]
    private Transform debugTransform;
    [SerializeField]
    private Transform pfBulletProjectile;
    [SerializeField]
    private Transform spawnBulletPosition;
    [SerializeField]
    private float aimDuration = 0.3f;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("InputX", thirdPersonController.getMovementX());
        animator.SetFloat("InputY", thirdPersonController.getMovementY());

        Vector3 mouseWorldPosition = Vector3.zero;

        // Getting the center position of the screen for aiming

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        Transform hitTransform = null;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        // If the aim button is pressed change to aim camera
        if (starterAssetsInputs.aim)
        {
            animator.SetBool("Aiming", true);
            // Set the aim camera and decrease sensitivity
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);

            // Activating animation: Pistol Idle
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            // Increasing weight of rig
            aimLayer.weight += Time.deltaTime / aimDuration;

            // Make the player turn towards where it's aiming
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            
        }
        else
        {
            animator.SetBool("Aiming", false);
            // Set the normal camera and increase sensitivity
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);

            // Dectivating animation: Pistol Idle
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));

            // Decreasing weight of rig
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }

        if (starterAssetsInputs.fire)
        {
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            starterAssetsInputs.fire = false;
        }

        
    }
}
