using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : MonoBehaviour
{
    // Sets variable to access a diferent script.
    private GameManager gameManager;

    // Set damage amount.
    private int damageDealt = -10;

    // Set speed of movement.
    private float speed = 7.0f;


    // Start is called before the first frame update
    void Start()
    {
        //Gets the GameManager script.
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    // Method the move the enemy after instantiation.
    void MoveEnemy()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    // On trigger Health is updated and gameobject that has this script will be destroyed.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.UpdateHealth(damageDealt);
        }
    }
}
