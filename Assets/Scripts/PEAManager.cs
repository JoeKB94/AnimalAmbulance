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
    private float zSpawnEnemies = 30.0f;
    private float zSpawnAnimals = 50.0f;
    private float zSpawnPowerup = 70.0f;
    private float xSpawnRange = 22.0f;
    private float ySpawn = 0.75f;

    // Set spawn timings
    private float startDelay = 1.0f;
    private float enemySpawnTime = 3.0f;
    private float animalSpawnTime = 2.5f;
    private float powerupSpawnTime = 5.0f;

    // Reference to the GameManager script.
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get the GameManger component
        gameManager = FindObjectOfType<GameManager>();

        // Creates set pools at the start of the game.
        CreatePool("Animal", 35, animals, animalPool);
        CreatePool("Enemy", 35, enemies, enemyPool);
        CreatePool("PowerUp", 20, powerups, powerupPool);

        // repeats the given methodes from the start of the game.
        InvokeRepeating("SpawnEnemy", startDelay, enemySpawnTime);
        InvokeRepeating("SpawnAnimal", startDelay, animalSpawnTime);
        InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnTime);

        // Check and update spawn times periodically
        InvokeRepeating("UpdateSpawnTimesLvl3", startDelay, 1.0f); // Checks every second, adjust if needed.
        InvokeRepeating("UpdateSpawnTimesLvl7", startDelay, 1.0f); // Checks evey second, adjust if needed. 
        InvokeRepeating("UpdateSpawnTimesLvl10", startDelay, 1.0f); // Checks evey second, adjust if needed.
    }

    void CreatePool(string type, int initialSize, GameObject[] prefabs, Dictionary<string, Queue<GameObject>> pool)
    {
        foreach (var prefab in prefabs)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = Instantiate(prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            pool.Add(prefab.name, objectPool);
        }
    }

    bool IsAreaClear(Vector3 position, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("BlockingLayer"))
            {
                Debug.Log($"Blocking collider detected: {hitCollider.name} at {hitCollider.transform.position}");
                return false;
            }
        }
        return true;
    }

    void SpawnFromPool(string tag, float zSpawn, Dictionary<string, Queue<GameObject>> pool)
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn);

        if (IsAreaClear(spawnPos, 5.0f)) // Adjust radius as needed
        {
            if (pool.ContainsKey(tag) && pool[tag].Count > 0)
            {
                GameObject objToSpawn = pool[tag].Dequeue();

                if (objToSpawn != null)
                {
                    objToSpawn.SetActive(true);
                    objToSpawn.transform.position = spawnPos;
                    objToSpawn.transform.rotation = Quaternion.identity; // Reset rotation if needed
                    pool[tag].Enqueue(objToSpawn);

                    // Debug.Log($"{tag} spawned at {spawnPos}");
                }
                else
                {
                    Debug.LogWarning($"Object to spawn is null for {tag}");
                }
            }
            else
            {
                Debug.LogWarning($"No objects available in pool for {tag} or tag not found");
            }
        }
        else
        {
            Debug.LogWarning($"Spawn area not clear for {tag} at {spawnPos}");
        }
    }

    void SpawnEnemy()
    {
        // Randomizes spawned object from array.
        int randomIndex = Random.Range(0, enemies.Length);
        SpawnFromPool(enemies[randomIndex].name, zSpawnEnemies, enemyPool);
    }

    void SpawnAnimal()
    {
        int randomIndex = Random.Range(0, animals.Length);
        SpawnFromPool(animals[randomIndex].name, zSpawnAnimals, animalPool);
    }

    void SpawnPowerup()
    {
        int randomIndex = Random.Range(0, powerups.Length);
        SpawnFromPool(powerups[randomIndex].name, zSpawnPowerup, powerupPool);
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

    void UpdateSpawnTimesLvl3()
    {
        if (gameManager != null)
        {
            int currentLevel = gameManager.GetLevel();
            if (currentLevel >= 3)
            {
                CancelInvoke("SpawnEnemy");
                CancelInvoke("SpawnAnimal");
                enemySpawnTime = 2.5f; // Adjust if needed.
                animalSpawnTime = 2.0f; // Adjust if needed.
                InvokeRepeating("SpawnEnemy", startDelay, enemySpawnTime);
                InvokeRepeating("SpawnAnimal", startDelay, animalSpawnTime);
            }
        }
    }

    void UpdateSpawnTimesLvl7()
    {
        if (gameManager != null)
        {
            int currentLevel = gameManager.GetLevel();
            if (currentLevel >= 7)
            {
                CancelInvoke("SpawnEnemy");
                CancelInvoke("SpawnAnimal");
                enemySpawnTime = 2.0f; // Adjust if needed.
                animalSpawnTime = 1.5f; // Adjust if needed.
                InvokeRepeating("SpawnEnemy", startDelay, enemySpawnTime);
                InvokeRepeating("SpawnAnimal", startDelay, animalSpawnTime);
            }
        }
    }

    void UpdateSpawnTimesLvl10()
    {
        if (gameManager != null)
        {
            int currentLevel = gameManager.GetLevel();
            if (currentLevel >= 10)
            {
                CancelInvoke("SpawnEnemy");
                CancelInvoke("SpawnAnimal");
                enemySpawnTime = 1.5f; // Adjust if needed.
                animalSpawnTime = 1.0f; // Adjust if needed.
                InvokeRepeating("SpawnEnemy", startDelay, enemySpawnTime);
                InvokeRepeating("SpawnAnimal", startDelay, animalSpawnTime);
            }
        }
    }
}
