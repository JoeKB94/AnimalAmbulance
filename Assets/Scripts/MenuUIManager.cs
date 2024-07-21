using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    // Variable to aquire and use the QuitMenu in this script. 
    public GameObject QuitMenu;
   
    // Start is called before the first frame update
    void Start()
    {
        
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

    // Loads the game, is connected to UI button to trigger action.
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
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
