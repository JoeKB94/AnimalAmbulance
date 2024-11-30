using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Variables to set player movement speeds.
    private float speed = 25.0f;
    public float normalSpeed = 25.0f;

    // Range that the player can move on the x-axis.
    private float moveRangeX = 22.0f;

    // Variables to limit amount of AidKits being fired.
    private float fireCooldown = 5.0f;
    private float[] lastFireTimes = new float[3];

    // Sets variable for GameManager script.
    private GameManager gameManager;

    // Sets gameobject variables.
    public GameObject FirstAidKit;
    public GameObject AmbulanceLights;

    // Sets a variable for point to be added to score.
    public int pointValue;

    // UI elements for Green and Red lights.
    public GameObject GreenLight1;
    public GameObject RedLight1;
    public GameObject GreenLight2;
    public GameObject RedLight2;
    public GameObject GreenLight3;
    public GameObject RedLight3;

    // AudioSource for ambulance sound.
    private AudioSource ambulanceSounds;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the gameManger.cs.
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        for (int i = 0; i < lastFireTimes.Length; i++)
        {
            lastFireTimes[i] = -fireCooldown; // Initialize to allow immediate firing
        }

        // Methode that checks if the aid-kit lights should be activated or deactivated. 
        UpdateLights();

        // Get the AudioSource component.
        ambulanceSounds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calls upon programmed methods (see further down in code).
        PlayerMovement();
        ConstrainPlayerBound();
        FireFirstAid();
        UpdateLights();
    }

    // Moves the player based on input.
    void PlayerMovement()
    {
        // Links the horizontalInput variable to the GetAxis input method.
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

    // Shoots an Aidkit when SPACE is pressed.
    void FireFirstAid()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < lastFireTimes.Length; i++)
            {
                if (i >= 0 && i < lastFireTimes.Length && Time.time - lastFireTimes[i] >= fireCooldown)
                {
                    Instantiate(FirstAidKit, transform.position, FirstAidKit.transform.rotation);
                    lastFireTimes[i] = Time.time;
                    UpdateLights();
                    break; // Fire only one FirstAidKit per key press
                }
            }
        }
    }

    // Updates the visibility of the Green and Red lights based on cooldown status.
    void UpdateLights()
    {
        for (int i = 0; i < lastFireTimes.Length; i++)
        {
            bool canFire = Time.time - lastFireTimes[i] >= fireCooldown;
            switch (i)
            {
                case 0:
                    GreenLight1.SetActive(canFire);
                    RedLight1.SetActive(!canFire);
                    break;
                case 1:
                    GreenLight2.SetActive(canFire);
                    RedLight2.SetActive(!canFire);
                    break;
                case 2:
                    GreenLight3.SetActive(canFire);
                    RedLight3.SetActive(!canFire);
                    break;
            }
        }
    }

    // Variables to get and set movement speeds.
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
        if (isActive)
        {
            ambulanceSounds.Play(); // Play the ambulance sound
        }
        else
        {
            ambulanceSounds.Stop(); // Stop the ambulance sound
        }
        //Debug.Log("AmbulanceLights set to: " + isActive);
    }

    // Coroutine to handle speed boost
    public IEnumerator SpeedBoostCoroutine(float boostDuration, float boostedSpeed)
    {
        SetSpeed(boostedSpeed); // Set the boosted speed
        SetAmbulanceLights(true); // Activate AmbulanceLights
        //Debug.Log("Speed boost activated");

        yield return new WaitForSecondsRealtime(boostDuration); // Wait for the boost duration
        //Debug.Log("Boost duration ended");

        SetSpeed(normalSpeed); // Return to normal speed
        //Debug.Log("Speed reset to normal");

        SetAmbulanceLights(false); // Deactivate AmbulanceLights
        //Debug.Log("AmbulanceLights deactivated");
    }
}
