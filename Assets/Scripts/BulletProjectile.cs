using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{

    [SerializeField]
    private Transform vfxHitGreen;
    [SerializeField]
    private Transform vfxHitRed;

    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Set a speed for the projectile
        float speed = 40f;

        // Move the bullet forward
        bulletRigidbody.velocity = transform.forward * speed;
    }

    // Testing for collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletTarget>() != null)
        {
            // Target Hit
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        }
        else
        {
            // Hit something else
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);

        }
        Destroy(gameObject);
    }
}
