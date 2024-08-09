using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMain : MonoBehaviour
{
    // Sets variable to access a diferent script.
    private GameManager gameManager;

    // Sets score increase amount.
    private int scoreIncrease = 10;

    // Sets speed for movement.
    private float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the GameManager script.
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAnimal(); // Activates movement of the animals after instantiation.
    }

    // Method to move the animal after activation/instantiation.
    void MoveAnimal()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    // On trigger Score is updated and gameobject that has this script will be destroyed.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.UpdateScore(scoreIncrease);
        }
        else if (other.CompareTag("AidKit"))
        {
            Destroy(gameObject);
            gameManager.UpdateScore(scoreIncrease * 2);
        }
    }
}
