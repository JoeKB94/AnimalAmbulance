using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables to set player movement speeds.
    private float speed = 25.0f;
    private float normalSpeed = 25.0f;
    private float boostedSpeed = 50.0f;

    // Variable to set the duration on the speedboost.
    private float boostDuration = 10.0f;

    // Range that the player can move on the x-axis.
    private float moveRangeX = 22.0f;

    // Sets variable for GameManager script.
    private GameManager gameManager;

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
         
        // Gets spacebar input from the player.  
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Action to throw a net will be coded here.
        }
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

    // Action to do on collision.
    private void OnTriggerEnter(Collider other)
    {
        // On collision checks tag and if correct, does an action.
        if (other.CompareTag("PowerUp")) 
        {
            Destroy(other.gameObject);
            StartCoroutine(SpeedBoostCoroutine());
        }
        if (other.CompareTag("Animal"))
        {
            Destroy (other.gameObject);
            gameManager.UpdateScore(pointValue);
        }
    }

    // Coroutine for a speedboost to the player.
    private IEnumerator SpeedBoostCoroutine()
    {
        speed = boostedSpeed; // Set the boosted speed

        yield return new WaitForSeconds(boostDuration); // Wait for the boost duration

        speed = normalSpeed; // Return to normal speed
    }
}
