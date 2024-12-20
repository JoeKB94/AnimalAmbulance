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
    public GameObject StartMenu;

    // Awake is called when the scene is loaded
    void Awake()
    {
        // Pauses the game on awake.
        Time.timeScale = 0;
        StartMenu.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Starts methods that update the health and score count.
        UpdateHealth(100);
        UpdateScore(0);
        // Sets level to 1 on start of the game.
        level = 1;

        // Loads the VSync settings from the menu settings.
        LoadVSyncSettings();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks the level count.
        UpdateLevel(0);

        // Checks if and which button is pressed. 
        PauseButton();
        EscapeKey();
        EnterKey();
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
        health = Mathf.Clamp(health, 0, 100); // Limits health to a range of 0 to 100

        if (health <= 0)
        {
            // Stops game and activates game over.
            Time.timeScale = 0;
            GameOverMenu.SetActive(true);
            // Debug.Log("Game Over");
        }

        // Displays healt count.
        healthText.text = "Health " + health + "%";
    }

    // Method that registers which level the game currently is at.
    public void UpdateLevel(int levelToAdd)
    {
        int stage = Mathf.FloorToInt(score / 100);
        level = Mathf.Max(1, stage + 1);
        levelText.text = "Level: " + level;
    }

    // Returns a level value that can be used by other scripts.
    public int GetLevel()
    {
        return level;
    }

    // Sets action to start the game, also deactivates UI-element.
    public void EnterKey()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !GameOverMenu.activeSelf)
        {
            Time.timeScale = 1;
            StartMenu.SetActive(false);
        }
    }

    // Sets action if Escape button is pressed.
    public void EscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Sets action if P (pause) button is pressed.
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

    // Method that loads the VSync settings.
    public void LoadVSyncSettings()
    {
        if (PlayerPrefs.HasKey("VSync"))
        {
            int vSyncValue = PlayerPrefs.GetInt("VSync");
            QualitySettings.vSyncCount = vSyncValue;
        }
    }

    // Loads the menu, is connected to UI button to trigger action.
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    // Method to restart the game.
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    // Currently unused, but gives and option for the player to go to the Highscore scene.
    public void LoadHighScoreScene()
    {
        SceneManager.LoadScene("HighScore");
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
