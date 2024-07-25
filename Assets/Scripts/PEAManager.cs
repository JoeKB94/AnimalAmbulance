using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEAManager : MonoBehaviour
{
    // Arrays for different objects
    public GameObject[] enemies;
    public GameObject[] animals;
    public GameObject[] powerups;

    // Pool dictionaries
    private Dictionary<string, Queue<GameObject>> enemyPool = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, Queue<GameObject>> animalPool = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, Queue<GameObject>> powerupPool = new Dictionary<string, Queue<GameObject>>();

    // Set spawn parameters
    private float zSpawnEnemies = 20.0f;
    private float zSpawnAnimals = 21.0f;
    private float zSpawnPowerup = 22.0f;
    private float xSpawnRange = 22.0f;
    private float ySpawn = 0.75f;

    // Set spawn timings
    private float startDelay = 1.0f;
    private float enemySpawnTime = 3.0f;
    private float animalSpawnTime = 2.5f;
    private float powerupSpawnTime = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Creates set pools at the start of the game.
        CreatePool("Animal", 15, animals);
        CreatePool("Enemy", 10, enemies);
        CreatePool("PowerUp", 8, powerups);

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

    // Create a pool for a specific prefab type
    private void CreatePool(string type, int initialSize, GameObject[] prefabs)
    {
        Queue<GameObject> pool = new Queue<GameObject>();
        for (int i = 0; i < initialSize; i++)
        {
            // Randomly select an animal prefab
            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject prefab = prefabs[randomIndex];

            // Instantiate and parent to the manager
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false); // Deactivate initially
            pool.Enqueue(obj);
        }
        // Add to the appropriate pool dictionary
        if (type == "Enemy")
            enemyPool[type] = pool;
        else if (type == "Animal")
            animalPool[type] = pool;
        else if (type == "PowerUp")
            powerupPool[type] = pool;
    }

    // Spawn an object of a specific prefab type
    public GameObject Spawn(string type, Vector3 position, Quaternion rotation)
    {
        if (enemyPool.ContainsKey(type) || animalPool.ContainsKey(type) || powerupPool.ContainsKey(type))
        {
            Dictionary<string, Queue<GameObject>> selectedPool;

            if (enemyPool.ContainsKey(type))
                selectedPool = enemyPool;
            else if (animalPool.ContainsKey(type))
                selectedPool = animalPool;
            else
                selectedPool = powerupPool;

            GameObject obj = selectedPool[type].Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogError($"Prefab type '{type}' not found in pools.");
            return null;
        }
    }

    // Return an object to its pool
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);

        // Determine the pool based on the object's tag
        if (obj.CompareTag("Enemy"))
            enemyPool[obj.tag].Enqueue(obj);
        else if (obj.CompareTag("Animal"))
            animalPool[obj.tag].Enqueue(obj);
        else if (obj.CompareTag("PowerUp"))
            powerupPool[obj.tag].Enqueue(obj);
        else
            Debug.LogError($"Unknown object tag: {obj.tag}");
    }
}
