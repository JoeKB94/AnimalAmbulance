using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : MonoBehaviour
{
    // Reference for the ScriptableObject script.
    public EnemyScriptableObject enemyAttributes;

    // Sets variable to access a different script.
    private GameManager gameManager;

    // Detection radius for collision checking.
    [SerializeField] 
    private float detectionRadius = 5.0f;

    // Variable to store the level speed.
    private float levelSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the GameManager script.
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // Get the current level and update the level speed.
        int currentLevel = gameManager.GetLevel();
        UpdateLevelSpeed(currentLevel);

        MoveEnemy(); // Activates movement of the enemies after instantiation.
        CheckForCollisions(); // Check for collisions and adjust speed if necessary.
    }

    // Method to move the enemy after instantiation.
    void MoveEnemy()
    {
        // Transform action.
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
                CarEnemy otherEnemy = hitCollider.GetComponent<CarEnemy>();
                if (otherEnemy != null)
                {
                    levelSpeed = otherEnemy.levelSpeed;
                }
                else
                {
                    AnimalMain otherAnimal = hitCollider.GetComponent<AnimalMain>();
                    if (otherAnimal != null)
                    {
                        levelSpeed = otherAnimal.LevelSpeed;
                    }
                }
            }
        }
    }

    // Method to update the level speed based on the current level.
    void UpdateLevelSpeed(int level)
    {
        // Access the speed from the ScriptableObject.
        float currentSpeed = enemyAttributes.speed;

        int updateLevel;

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

        // Gets base speed from the enemyAttributes and adds the level count to get the levelSpeed.
        levelSpeed = currentSpeed + updateLevel;
    }

    // On trigger Health is updated and gameobject that has this script will be destroyed.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.UpdateHealth(enemyAttributes.damageDealt);
        }
    }

    // Optional: Visualize the detection radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}