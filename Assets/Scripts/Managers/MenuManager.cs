using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string levelSelectScene;
    public string creditsScene;

    public GameObject settingsMenu;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(levelSelectScene);
    }

    public void OpenSettings()
    {
        // now using UI menu as opposed to separate scene
        settingsMenu.SetActive(true);
    }

    public void Credits()
    {
        SceneManager.LoadScene(creditsScene);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("quitting game...");
        Application.Quit();
    }
}
