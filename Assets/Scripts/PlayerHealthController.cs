using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int maxHealth, currentHealth;
    private float invincibleCounter;
    public float invincibleLenght;

    private void Awake()
    {
        instance= this;
    }
    private void Start()
    {
        currentHealth = maxHealth;
        UIController.Instance.healthSlider.maxValue= maxHealth;
        UIController.Instance.healthSlider.value= currentHealth;
    }
    private void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if(invincibleCounter <= 0)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                currentHealth = 0;
                GameManager.Instance.PlayerDied();
            }
            invincibleCounter = invincibleLenght;
            UIController.Instance.healthSlider.value = currentHealth;
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.Instance.healthSlider.value = currentHealth;
    }
}
