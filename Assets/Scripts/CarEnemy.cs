using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : MonoBehaviour
{
    // Reference for the ScriptableObject script.
    public EnemyScriptableObject enemyAttributes;
    
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
        MoveEnemy(); // Activates movement of the enemies after instantiation.
    }

    // Method the move the enemy after instantiation.
    void MoveEnemy()
    {
        // Acces the speed from the ScriptableObject. 
        float currentSpeed = enemyAttributes.speed;

        // Transform action.
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
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
}
