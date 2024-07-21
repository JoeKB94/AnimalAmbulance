using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    //
    public GameObject GameOverMenu;

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
        health = Mathf.Clamp(health, 0, 100); // Limit health to a range of 0 to 100

        if (health <= 0)
        {
            Time.timeScale = 0;
            GameOverMenu.SetActive(true);
            Debug.Log("Game Over");
        }

        healthText.text = "Health " + health + "%";
    }

    // Loads the menu, is connected to UI button to trigger action.
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

}
