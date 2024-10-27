using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // public static bool GamePaused; // can be removed if not needed
    public GameObject settingsUI;

    public void ToggleSettingsMenu()
    {
        settingsUI.SetActive(!settingsUI.activeSelf);
    }

    public void ExitLevel()
    {
        Time.timeScale = 1f; // reset time scale since pause menu disappears
        SceneManager.LoadScene("LevelSelect");
    }
}
