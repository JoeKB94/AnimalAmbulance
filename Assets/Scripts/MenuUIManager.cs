using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    // Variable to aquire and use the QuitMenu in this script. 
    public GameObject QuitMenu;
    public Toggle vSyncToggle; 
   
    // Start is called before the first frame update
    void Start()
    {
        VsyncSettings();
    }

    // Update is called once per frame
    void Update()
    {
        EscapeKey();
    }

    // Opens the quit menu when ESC is pressed.
    public void EscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitMenu.SetActive(!QuitMenu.activeSelf);
        }
    }

    void VsyncSettings()
    {
        {
            // Load the VSync setting
            if (PlayerPrefs.HasKey("VSync"))
            {
                int vSyncValue = PlayerPrefs.GetInt("VSync");
                QualitySettings.vSyncCount = vSyncValue;
                vSyncToggle.isOn = vSyncValue != 0;
            }
            else
            {
                // Initialize the toggle state based on the current VSync setting
                vSyncToggle.isOn = QualitySettings.vSyncCount != 0;
            }

            // Add a listener to handle the toggle change event
            vSyncToggle.onValueChanged.AddListener(delegate { ToggleVSync(vSyncToggle); });
        }
    }

    public void ToggleVSync(Toggle toggle)
    {
        if (toggle.isOn)
        {
            QualitySettings.vSyncCount = 1; // Enable VSync
            PlayerPrefs.SetInt("VSync", 1); // Save the setting
        }
        else
        {
            QualitySettings.vSyncCount = 0; // Disable VSync
            PlayerPrefs.SetInt("VSync", 0); // Save the setting
        }
        PlayerPrefs.Save(); // Ensure the setting is saved
    }

    // Loads the game, is connected to UI button to trigger action.
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

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
