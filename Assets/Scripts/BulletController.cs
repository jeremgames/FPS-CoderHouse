using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLifeTime;
    public Rigidbody rb;
    public GameObject impactEffect;
    public int damage = 1;
    [SerializeField]private bool damageEnemy, damagePlayer;

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
        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }
        if (other.gameObject.tag == "HeadShot" && damageEnemy)
        {
            other.transform.parent.GetComponent<EnemyHealthController>().DamageEnemy(damage * 4);
            Debug.Log("HEADSHOT!");
        }
        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            PlayerHealthController.instance.DamagePlayer(damage);
            //opcion2= other.gameObject.GetComponent<PlayerHealthController>().DamagePlayer(damage);
        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
