using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMain : MonoBehaviour
{
    // Sest variable for the levelspeed.
    private float levelSpeed;

    // Variable to aquire the return value from GameManager.cs.
    public float LevelSpeed 
    {
        get { return levelSpeed; }
        set { levelSpeed = value; }
    }

    // Reference for the ScriptableObject script.
    public AnimalScriptableObject animalAttributes;

    // Sets variable to access GameManager.cs.
    private GameManager gameManager;

    // Detection radius for collision checking.
    [SerializeField] 
    private float detectionRadius = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the GameManager script.
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gets current level from the GameManager.
        int currentLevel = gameManager.GetLevel();
        UpdateLevelSpeed(currentLevel);

        MoveAnimal(); // Activates movement of the animals after instantiation.
        CheckForCollisions(); // Checks for collisions and adjusts speed if necessary.
    }

    // Method to move the animal after activation/instantiation.
    void MoveAnimal()
    {
        // Move action.
        transform.Translate(Vector3.back * levelSpeed * Time.deltaTime);
    }

    // Method to check for collisions and adjust speed.
    void CheckForCollisions()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != gameObject &&
                (hitCollider.CompareTag("Animal") || hitCollider.CompareTag("Enemy")))
            {
                // Adjust speed to match the other object
                AnimalMain otherAnimal = hitCollider.GetComponent<AnimalMain>();
                if (otherAnimal != null)
                {
                    levelSpeed = otherAnimal.levelSpeed;
                }
                else
                {
                    CarEnemy otherEnemy = hitCollider.GetComponent<CarEnemy>();
                    if (otherEnemy != null)
                    {
                        levelSpeed = otherEnemy.LevelSpeed; 
                    }
                }
            }
        }
    }

    void UpdateLevelSpeed(int level)
    {
        // Access the speed from the ScriptableObject.
        float currentSpeed = animalAttributes.speed;

        int updateLevel;

        // Sets the speed depending on the current level.
        if (level >= 1 && level <= 3)
        {
            updateLevel = 1;
        }
        else if (level >= 4 && level <= 6)
        {
            updateLevel = 4;
        }
        else if (level >= 7 && level <= 9)
        {
            updateLevel = 7;
        }
        else
        {
            updateLevel = 9; // Default value if level is outside the specified ranges.
        }

        // Gets base speed from the animalAttributes and adds the level count to get the levelSpeed.
        levelSpeed = currentSpeed  + updateLevel;
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

    // Optional: Visualize the detection radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
