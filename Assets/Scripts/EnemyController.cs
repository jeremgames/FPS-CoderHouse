using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int currentHealth = 5;

    private void Update()
    {
        
    }

    public void DamageEnemy()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
