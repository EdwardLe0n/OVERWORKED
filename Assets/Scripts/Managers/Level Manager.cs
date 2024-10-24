using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static int CurrentLevelNumber;
    
    public float levelDuration = 3f;
    private float levelTimer = 0f; 
    public TextMeshProUGUI timerText;
    public static ProgressBar progressBar;
    public GameObject pauseUI;

    public GameObject winScreen;
    public GameObject loseScreen;

    public static bool isLevelCompleted;
    public static bool isLevelWon;

    [Header("Tasks Values")]
    public int numberOfTotalTasks;
    public float numberOfCurrentTasks = 0f;

    // Delegate handling
    public delegate void CheckTheLevel();
    public static CheckTheLevel checkTheLevel;

    private void Awake()
    {

        //debugTotalTasks();

        numberOfCurrentTasks = 0;

        checkTheLevel += resetCurrentTasksVal;
        // Adds the check tasks 
        checkTheLevel += checkTasks;

    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("level " + CurrentLevelNumber);

        float dur = levelDuration * 60f; // convert level duration to minutes
        levelTimer = dur;

        // fixes up veriables at the start
        isLevelCompleted = false;
        isLevelWon = false;

        checkTheLevel += debugTotalTasks;

        checkTheLevel();

        progressBar = FindAnyObjectByType<ProgressBar>();
        Debug.Log(progressBar);

        progressBar.SetMaxValue(numberOfTotalTasks);
        progressBar.CheckValue();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLevelCompleted) {
            if(levelTimer > 0f) {
                levelTimer -= Time.deltaTime;
                UpdateTimerText();
            }
            else {
                levelTimer = 0f;
                isLevelCompleted = true; // just to prevent timer from running any further
                UpdateTimerText();
                loseScreen.SetActive(true); // display lose screen
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) {
            TogglePauseMenu();
        }

        if(isLevelCompleted && isLevelWon) {
            winScreen.SetActive(true);
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
        if(pauseUI.activeSelf) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }
    }

    // sets the current tasks val to 0f
    void resetCurrentTasksVal()
    {
        numberOfCurrentTasks = 0f;
    }

    // Notifies all current tsaks to contact the level manager
    void checkTasks()
    {

        numberOfCurrentTasks = 0f;

        Debug.Log("Checking the level for tasks!");
    }

    // The function tasks call to let the level manager know of their status
    public void recieveTaskInfo()
    {
        // Increments the number of tasks in a level
        numberOfCurrentTasks += 1f;

        Debug.Log("Tasks found");

    }

    // Gets called when tasks are completed
    public void taskCompleted()
    {
        // decrements the number of tasks in a level
        numberOfCurrentTasks -= 1f;

        progressBar.IncrementValue(); // increment progress bar on task completion

        // Sanity check
        debugTotalTasks();

        // Checks if there are any more current tasks
        if (numberOfCurrentTasks <= 0)
        {
            Debug.Log("Level Done!");

            LevelComplete(); // level completes when all tasks done
        }
    }

    public void debugTotalTasks()
    {
        Debug.Log("Tasks that still need to be completed: " + numberOfCurrentTasks);
    }

    public void LevelComplete()
    {
        Debug.Log("level completed function");
        isLevelCompleted = true; // stops timer from running any further
        isLevelWon = true; // flag for winning since using isLevelCompleted for both win or lose conditions
        PlayerPrefs.SetInt("levelReached", CurrentLevelNumber+1);
        PlayerPrefs.Save();

        
        // TODO: add more level complete stuff and things
    }
}
