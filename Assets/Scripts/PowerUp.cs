using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Variable for the duration and the speed of the powerup.
    public float boostDuration = 10.0f;
    public float boostedSpeed = 50.0f;

    // Variable to acces PlayerController.cs.
    private PlayerController playerController;

    void Start()
    {
        // Gets the PlayerController. 
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Checks trigger and activets the SpeedBoostCoroutine from the PlayerController.cs.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.StartCoroutine(playerController.SpeedBoostCoroutine(boostDuration, boostedSpeed));
            Destroy(gameObject);
        }
    }
}
