using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    // Variable to aquire and use the QuitMenu in this script. 
    public GameObject QuitMenu;
    public Toggle vSyncToggle;

    // Variables for the game audio.
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sFXSlider;
   
    // Start is called before the first frame update
    void Start()
    {
        VsyncSettings();

        float masterVolume; 
        float musicVolume; 
        float sFXVolume; 
        
        audioMixer.GetFloat("MasterVolume", out masterVolume); 
        audioMixer.GetFloat("MusicVolume", out musicVolume); 
        audioMixer.GetFloat("SFXVolume", out sFXVolume); 
        
        masterSlider.value = Mathf.Pow(10, masterVolume / 20); 
        musicSlider.value = Mathf.Pow(10, musicVolume / 20); 
        sFXSlider.value = Mathf.Pow(10, sFXVolume / 20); 
        
        masterSlider.onValueChanged.AddListener(SetMasterVolume); 
        musicSlider.onValueChanged.AddListener(SetMusicVolume); 
        sFXSlider.onValueChanged.AddListener(SetSFXVolume);
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

    public void SetMasterVolume(float volume) 
    { 
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20); 
    }

    public void SetMusicVolume(float volume) 
    { 
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); 
    }

    public void SetSFXVolume(float volume) 
    { 
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20); 
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
