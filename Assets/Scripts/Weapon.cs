using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Attack attack;
    [SerializeField] Transform fireTransfom;
    [SerializeField] float fireRate;
    [SerializeField] int clipSize;
    [SerializeField] AudioClip clip;
    private int currentAmmoCount;
    public int GetCurrentWeaponAmmoCount
    {
        get
        {
            return currentAmmoCount;

        }
        set
        {
            currentAmmoCount = value;
        }
    }

    private void Awake()
    {
        currentAmmoCount = clipSize;
    }

    private void OnEnable()
    {
        if (attack != null)
        {
            attack.GetFireTransform = fireTransfom;
            attack.GetFireRate = fireRate;
            attack.GetClipSize=clipSize;
            attack.GetAmmo = currentAmmoCount;
            attack.GetClipToPlay = clip;
        }
    }
}
