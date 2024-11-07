using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables to set player movement speeds.
    private float speed = 25.0f;
    public float normalSpeed = 25.0f;

    // Range that the player can move on the x-axis.
    private float moveRangeX = 22.0f;

    // Variables to limit amount of AidKits being fired.
    private float fireCooldown = 2.0f;
    private float lastFireTime = 0.0f;

    // Sets variable for GameManager script.
    private GameManager gameManager;

    // Sets gameobject variables.
    public GameObject FirstAidKit;
    public GameObject AmbulanceLights;

    // Sets a variable for point to be added to score.
    public int pointValue;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calls upon programmed methodes (see futher down in code).
        PlayerMovement();
        ConstrainPlayerBound();
        FireFirstAid();
    }

    // Moves the player based on input.
    void PlayerMovement()
    {
        // links the horizontalInput variable to the GetAxis input methode. 
        float horizontalInput = Input.GetAxis("Horizontal");

        // Moves the player on the x-axis when the corresponding button is pressed.
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
    }

    // Constrains the player from moving out of bounds.
    void ConstrainPlayerBound()
    {
        // Prevents player from moving out of bounds on the right side of the screen.
        if (transform.position.x > moveRangeX)
        {
            transform.position = new Vector3(moveRangeX, transform.position.y, transform.position.z);
        }

        // Prevents player from moving out of bounds on the left side of the screen.
        if (transform.position.x < -moveRangeX)
        {
            transform.position = new Vector3(-moveRangeX, transform.position.y, transform.position.z);
        }
    }

    // Shoots a Aidkit when SPACE is pressed.
    void FireFirstAid()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastFireTime >= fireCooldown)
        {
            Instantiate(FirstAidKit, transform.position, FirstAidKit.transform.rotation);
            lastFireTime = Time.time;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Method to activate and deactivate AmbulanceLights
    public void SetAmbulanceLights(bool isActive)
    {
        AmbulanceLights.SetActive(isActive);
        Debug.Log("AmbulanceLights set to: " + isActive);
    }

    // Coroutine to handle speed boost
    public IEnumerator SpeedBoostCoroutine(float boostDuration, float boostedSpeed)
    {
        SetSpeed(boostedSpeed); // Set the boosted speed
        SetAmbulanceLights(true); // Activate AmbulanceLights
        Debug.Log("Speed boost activated");

        yield return new WaitForSecondsRealtime(boostDuration); // Wait for the boost duration
        Debug.Log("Boost duration ended");

        SetSpeed(normalSpeed); // Return to normal speed
        Debug.Log("Speed reset to normal");

        SetAmbulanceLights(false); // Deactivate AmbulanceLights
        Debug.Log("AmbulanceLights deactivated");
    }
}
