using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bullet;
    public bool canAutoFire;
    public float fireRate;
    public int maxAmmo, currentAmmo, pickupAmount;
    public string gunName;
    [HideInInspector] public float fireCounter;

    private void Start()
    {
        currentAmmo = maxAmmo;
        UIController.Instance.ammoSlider.maxValue = maxAmmo;
        UIController.Instance.ammoSlider.value = currentAmmo;
    }

    private void Update()
    {
        if(fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }

    public void GetAmmo()
    {
        PlayerController.Instance.activeGun.GetAmmo();
        currentAmmo += pickupAmount;
        UIController.Instance.ammoSlider.value = currentAmmo;
    }
}
