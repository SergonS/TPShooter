using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Set a speed for the projectile
        float speed = 10f;

        // Move the bullet forward
        bulletRigidbody.velocity = transform.forward * speed;
    }

    // Testing for collisions
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
