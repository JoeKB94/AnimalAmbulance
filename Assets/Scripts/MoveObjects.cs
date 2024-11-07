using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    // Sets speed for movement of the objects (= public to allow speeds change per different object).
    public float baseSpeed = 6.0f;
    private float levelSpeed;

    // Sets Z-bound.
    private float zDestroyBound = -26.0f;

    // Set variabel for level component:
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gets current level from the GameManager.
        int currentLevel = gameManager.GetLevel();
        UpdateLevelSpeed(currentLevel);

        // Action to move objects in given direction * speed and time.
        transform.Translate(Vector3.back * levelSpeed * Time.deltaTime);

        // Destroys objects when they exceed given Z-bound.
        if(transform.position.z < zDestroyBound)
        {
            Destroy(gameObject);
        }
    }

    void UpdateLevelSpeed(int level)
    {
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

        levelSpeed = baseSpeed + updateLevel;
    }
}
