using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static bool GamePaused; // can be removed if not needed

    public float levelDuration = 3f;
    private float levelTimer = 0f; 
    public TextMeshProUGUI timerText;
    public GameObject pauseUI;

    private bool isLevelCompleted;
    
    // Start is called before the first frame update
    void Start()
    {
        // GamePaused = false;

        float dur = levelDuration * 60f; // convert level duration to minutes (i know it's technically converting to seeconds)
        levelTimer = dur;

        isLevelCompleted = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(levelTimer >= 0) {
            levelTimer -= Time.deltaTime;
            UpdateTimerText();
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) {
            TogglePauseMenu();
        }
    }

    // Updates the timer test every update except for when the pause menu is open
    void UpdateTimerText() 
    {
        float mins = Mathf.FloorToInt(levelTimer/60);
        float secs = Mathf.FloorToInt(levelTimer%60);
        timerText.text = string.Format("{0:0}:{1:00}", mins, secs); // displays text in timer format
    }

    public void TogglePauseMenu()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);

        // if a level has not completed, it'll run the time adjustment scripts
        if (!isLevelCompleted)
        {
            // Deals with turning the timer back on and off
            if(pauseUI.activeSelf) {
                Time.timeScale = 0f;
            }
            else {
                Time.timeScale = 1f;
            }
        }

       
    }
}
