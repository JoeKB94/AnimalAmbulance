using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    // Sets variable to access a diferent script.
    private GameManager gameManager;

    // Set damage amount.
    private int healthUp = 10;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the GameManager script.
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // On trigger Health is updated and gameobject that has this script will be destroyed.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.UpdateHealth(healthUp);
        }
    }
}
