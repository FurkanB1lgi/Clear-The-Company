using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Mathematics;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private GameObject ammo;

    [SerializeField] private bool isPlayer = false;
    private int ammoCount = 0;
    private Transform fireTransform;
    private AudioClip clipToplay;
    private AudioSource audioSource;
    private float fireRate = 0.5f;
    private int maxAmmoCount = 5;
    private GameManager gameManager;
    public bool silahDegistimi;

    private float currentFireRate = 0;
    public AudioClip GetClipToPlay
    {
        get
        {
            return clipToplay;
        }
        set
        {
            clipToplay = value;
        }
    }
    public int GetAmmo
    {
        get
        {
            return ammoCount;
        }
        set
        {
            ammoCount = value;
            if (ammoCount > maxAmmoCount)
            {
                ammoCount = maxAmmoCount;
            }
        }
    }
    public float GetCurrentFireRate
    {
        get
        {
            return currentFireRate;
        }
        set
        {
            currentFireRate = value;
        }
    }
    public int GetClipSize
    {
        get
        {
            return maxAmmoCount;
        }
        set
        {
            maxAmmoCount = value;
        }
    }
    public float GetFireRate
    {
        get
        {
            return fireRate;
        }
        set
        {
            fireRate = value;
        }
    }
    public Transform GetFireTransform
    {
        get
        {
            return fireTransform;
        }
        set
        {
            fireTransform = value;
        }
    }



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        silahDegistimi = false;
    }

    void Update()
    {
        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }

        if (!gameManager.GetLevelFinished)
        {
            PlayerInput();
        }
        

    }

    public void PlayerInput()
    {
        if (isPlayer)
        {
           /* if (Input.GetMouseButtonDown(0))
            {
                if (currentFireRate <= 0 && ammoCount > 0)
                {

                    Fire();
                }

            }*/
            switch (Input.inputString)
            {
                case "1":
                    weapons[1].GetComponent<Weapon>().GetCurrentWeaponAmmoCount = ammoCount;
                    weapons[0].SetActive(true);
                    weapons[1].SetActive(false);
                    break;
                case "2":
                    weapons[0].GetComponent<Weapon>().GetCurrentWeaponAmmoCount = ammoCount;
                    weapons[1].SetActive(true);
                    weapons[0].SetActive(false);
                    break;


            }

        }

    }
    public void MobileFire()
    {
        if (isPlayer)
        {
            if (currentFireRate <= 0 && ammoCount > 0)
            {

                Fire();
            }
        }
    }
    public void MobileSilahDegistir()
    {
        silahDegistimi = !silahDegistimi;
        switch (silahDegistimi)
        {
            case false:
                weapons[1].GetComponent<Weapon>().GetCurrentWeaponAmmoCount = ammoCount;
                weapons[0].SetActive(true);
                weapons[1].SetActive(false);
                break;
            case true:
                weapons[0].GetComponent<Weapon>().GetCurrentWeaponAmmoCount = ammoCount;
                weapons[1].SetActive(true);
                weapons[0].SetActive(false);
                break;


        }

    }

    public void Fire()
    {
        float difference = 180f - transform.eulerAngles.y;
        float targetRotation = -90f;
        if (difference >= 90f)
        {
            targetRotation = 90f;
        }
        else if (difference < 90f)
        {
            targetRotation = -90f;
        }
        currentFireRate = fireRate;
        ammoCount--;
        audioSource.PlayOneShot(clipToplay);
        ammo.transform.position += transform.forward;
        GameObject bullet = Instantiate(ammo, fireTransform.position, Quaternion.Euler(0f, 0f, targetRotation));
        bullet.GetComponent<Bullet>().owner = gameObject;
    }
}
