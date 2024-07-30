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
    public TextMeshProUGUI levelText;

    // Sets variable for the score, health and level.
    private int score;
    private int health;
    private int level;

    // Variables to determine if game is active or paused.
    private bool isPaused = false;
   
    // Sets variables for UI objects in engine. 
    public GameObject GameOverMenu;
    public GameObject PauseMenu;

    // Awake is called when the scene is loaded
    void Awake()
    {
        Time.timeScale = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(100);
        UpdateScore(0);
        PauseButton();
        level = 1;
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLevel(0);
        PauseButton();
    }

    // Methode that updates the score vlaue.
    public void UpdateScore (int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Method that updats the health value.
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

    // Method that registers which level the game currently is at.
    public void UpdateLevel(int levelToAdd)
    {
        int stage = Mathf.FloorToInt(score / 100);
        level = Mathf.Max(1, stage + 1);
        levelText.text = "Level: " + level;
    }

    public int GetLevel()
    {
        return level;
    }

    public void PauseButton()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();            
        }
    }
    // Function to pause an active game.
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
        }
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

    // Quits the game both in editor as standalone build, activated by a UI button.
    public void QuitGame()
    {
#if UNITY_STANDALONE
Application.Quit();
#endif

#if UNITY_EDITOR
UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
