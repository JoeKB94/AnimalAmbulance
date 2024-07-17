using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Variable to get Text UI elements.
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    // Sets variable for the score.
    private int score;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(100);
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Methode that updates the score in the UI.
    public void UpdateScore (int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateHealth (int healthToAdd)
    {
        health += healthToAdd;
        healthText.text = "Health " + health + "%";
    }
}
