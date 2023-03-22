using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !collected)
        {
            PlayerController.Instance.activeGun.GetAmmo();
            collected= true;
            Destroy(gameObject);
        }
    }
}
