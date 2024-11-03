using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMain : MonoBehaviour
{
    private float levelSpeed;

    public float LevelSpeed // public property
    {
        get { return levelSpeed; }
        set { levelSpeed = value; }
    }

    // Reference for the ScriptableObject script.
    public AnimalScriptableObject animalAttributes;

    // Sets variable to access a different script.
    private GameManager gameManager;

    // Detection radius for collision checking.
    [SerializeField] 
    private float detectionRadius = 4.0f;

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
        CheckForCollisions(); // Check for collisions and adjust speed if necessary.
    }

    // Method to move the animal after activation/instantiation.
    void MoveAnimal()
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
                AnimalMain otherAnimal = hitCollider.GetComponent<AnimalMain>();
                if (otherAnimal != null)
                {
                    levelSpeed = otherAnimal.levelSpeed;
                }
            }
        }
    }

    void UpdateLevelSpeed(int updateLevel)
    {
        // Access the speed from the ScriptableObject.
        float currentSpeed = animalAttributes.speed;

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
