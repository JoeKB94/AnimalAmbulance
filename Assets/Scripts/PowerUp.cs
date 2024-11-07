using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float boostDuration = 10.0f;
    public float boostedSpeed = 50.0f;

    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.StartCoroutine(playerController.SpeedBoostCoroutine(boostDuration, boostedSpeed));
            Destroy(gameObject);
        }
    }
}
