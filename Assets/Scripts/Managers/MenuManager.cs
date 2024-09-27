using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string levelSelectScene;
    public string settingsScene;
    public string creditsScene;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(levelSelectScene);
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene(settingsScene);
    }

    public void Credits()
    {
        SceneManager.LoadScene(creditsScene);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
