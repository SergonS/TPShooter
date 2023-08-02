using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public Transform crosshairTarget;
    public StarterAssetsInputs starterAssetsInputs;
    RaycastWeapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();

        if (existingWeapon)
        {
            Equip(existingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (starterAssetsInputs.fire)
        {
            weapon.StartFiring();
        }
        else
        {
            weapon.StopFiring();
        }

        if (weapon.isFiring)
        {
            weapon.UpdateFiring(Time.deltaTime);
        }

        weapon.UpdateBullets(Time.deltaTime);
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        weapon = newWeapon;
        weapon.raycastDestination = crosshairTarget;
    }
}
