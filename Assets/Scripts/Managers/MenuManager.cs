using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string levelSelectScene;
    public string creditsScene;

    public GameObject settingsMenu;

    void Start()
    {
        // check for language on Main Menu start so it updates immediately
        // (otherwise won't update until opening settings menu)
        int savedLangIndex = PlayerPrefs.GetInt("langKey", -1);
        if (savedLangIndex >= 0 && savedLangIndex < LocalizationSettings.AvailableLocales.Locales.Count)
        {
            // If a valid language index is found in PlayerPrefs, set it as the current locale
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[savedLangIndex];
        }
    }

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

    public void RetryLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
