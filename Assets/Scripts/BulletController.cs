using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLifeTime;
    public Rigidbody rb;
    public GameObject impactEffect;

    private void Update()
    {
        rb.velocity = transform.forward * bulletSpeed;
        bulletLifeTime = Time.deltaTime;
        if(bulletLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
