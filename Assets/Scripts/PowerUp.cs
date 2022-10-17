using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Health Settings")]
    public bool healthPowerUp = false;
    public int healthAmount = 1;
    [Header("Ammo Settings")]
    public bool ammoPowerUp = false;
    public int ammoAmount = 5;
    [Header("Transform Settings")]
    [SerializeField] private Vector3 turnVector;
    [Header("Scale Settings")]
    [SerializeField] private float period = 2f;
    [SerializeField] private Vector3 scaleVector;
    private float scaleFactor;
    private Vector3 startScale;
    [Header("Audio Settings")]
    [SerializeField] private AudioClip clip;
    private void Awake()
    {
        startScale = transform.localScale;
       
    }
    void Start()
    {


        if (healthPowerUp && ammoPowerUp)
        {
            healthPowerUp = false;
            ammoPowerUp = false;
        }
        else if (healthPowerUp)
        {
            ammoPowerUp = false;
        }
        else if (ammoPowerUp)
        {
            healthPowerUp = false;
        }
    }


    void Update()
    {
        transform.Rotate(turnVector);
        SinusWawe();
    }

    private void SinusWawe()
    {
        if (period <= 0f)
        {
            period = 0.1f;
        }
        float cycles = Time.timeSinceLevelLoad / period;
        const float pix2 = Mathf.PI;
        float sinusWawe = Mathf.Sin(cycles * pix2);
        scaleFactor = sinusWawe / 2 + 0.5f;
        Vector3 offset = scaleFactor * scaleVector;
        transform.localScale = startScale + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }
        AudioSource.PlayClipAtPoint(clip, transform.position);

        if (healthPowerUp)
        {
            other.gameObject.GetComponent<Target>().GetHealth += healthAmount;
           
        }
        else if (ammoPowerUp)
        {
            other.gameObject.GetComponent<Attack>().GetAmmo += ammoAmount;
           
        }
        Destroy(gameObject);
    }
}
