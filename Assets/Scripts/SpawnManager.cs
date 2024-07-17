using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Creates different arrays for all the objects.
    public GameObject[] enemies;
    public GameObject[] animals;
    public GameObject[] powerups;

    // Sets Z-bound for spawning of different objects.
    private float zSpawnEnemies = 20.0f;
    private float zSpawnAnimals = 21.0f;
    private float zSpawnPowerup = 22.0f;

    // Sets X-range where objects can spawn.
    private float xSpawnRange = 22.0f;

    // Sets Y-location of the spawning objects.
    private float ySpawn = 0.75f;

    // Spawntimings:
    private float startDelay = 1.0f;
    private float enemySpawnTime = 3.0f;
    private float animalSpawnTime = 1.0f;
    private float powerupSpawnTime = 5.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        // repeats the given methodes from the start of the game.
        InvokeRepeating("SpawnEnemy", startDelay, enemySpawnTime);
        InvokeRepeating("SpawnAnimal", startDelay, animalSpawnTime);
        InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Methode to spawn enemies within an array, in the given range. 
    void SpawnEnemy()
    {
        // Randomizes spawn location on the X-Axis. 
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        // Randomizes spawned object from array.
        int randomIndex = Random.Range(0, enemies.Length);

        // Determines the spawn location within given parameters.
        Vector3 enemySpawnPos = new Vector3(randomX, ySpawn, zSpawnEnemies);

        // Actually creates object from the chosen array in above mentioned location range.
        Instantiate(enemies[randomIndex], enemySpawnPos, enemies[randomIndex].gameObject.transform.rotation);
    }

    // Methode to spawn animals within an array, in the given range. 
    void SpawnAnimal()
    {
         // Randomizes spawn location on the X-Axis. 
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        // Randomizes spawned object from array.
        int randomIndex = Random.Range(0, animals.Length);

        // Determines the spawn location within given parameters.
        Vector3 animalSpawnPos = new Vector3(randomX, ySpawn, zSpawnAnimals);

        // Actually creates object from the chosen array in above mentioned location range.
        Instantiate(animals[randomIndex], animalSpawnPos, animals[randomIndex].gameObject.transform.rotation);
    }

    // Methode to spawn powerups within an array, in the given range. 
    void SpawnPowerup()
    {
         // Randomizes spawn location on the X-Axis. 
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        // Randomizes spawned object from array.
        int randomIndex = Random.Range(0, powerups.Length);

        // Determines the spawn location within given parameters.
        Vector3 powerupSpawnPos = new Vector3(randomX, ySpawn, zSpawnPowerup);

        // Actually creates object from the chosen array in above mentioned location range.
        Instantiate(powerups[randomIndex], powerupSpawnPos, powerups[randomIndex].gameObject.transform.rotation);
    }

}
