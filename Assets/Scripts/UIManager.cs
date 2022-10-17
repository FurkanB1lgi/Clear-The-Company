using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image healthFill;
    public Image ammoFill;


    private Target playerHealth;
    private Attack playerAmmo;
    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
        playerAmmo=playerHealth.GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTheAmmo();
        UpdateTheHealth();
    }

    private void UpdateTheHealth()
    {
        healthFill.fillAmount =(float)playerHealth.GetHealth / (float)playerHealth.GetMaxHealth;
    }

    private void UpdateTheAmmo()
    {
        ammoFill.fillAmount = (float)playerAmmo.GetAmmo / (float)playerAmmo.GetClipSize;
    }
}
