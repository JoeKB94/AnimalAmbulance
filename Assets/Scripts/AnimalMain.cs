using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMain : MonoBehaviour
{
    // Reference for the ScriptableObject script.
    public AnimalScriptableObject animalAttributes;

    // Sets variable to access a diferent script.
    private GameManager gameManager;

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
        // Access the speed from the ScriptableObject.
        float currentSpeed = animalAttributes.speed;

        // Transform action.
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
    }

    // On trigger Score is updated and gameobject that has this script will be destroyed.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.UpdateScore(animalAttributes.scoreIncrease);
        }
        else if (other.CompareTag("AidKit"))
        {
            Destroy(gameObject);
            gameManager.UpdateScore(animalAttributes.scoreIncrease * 2);
        }
    }
}
