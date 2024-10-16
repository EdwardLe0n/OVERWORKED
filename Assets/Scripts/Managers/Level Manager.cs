using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static bool GamePaused; // can be removed if not needed

    public float levelDuration = 3f;
    private float levelTimer = 0f; 
    public TextMeshProUGUI timerText;
    public GameObject pauseUI;

    private bool isLevelCompleted;

    [Header("Tasks Values")]
    public float numberOfTotalTasks;
    public float numberOfCurrentTasks;

    // Delegate handling
    public delegate void CheckTheLevel();
    public static CheckTheLevel checkTheLevel;

    private void Awake()
    {
        // Adds the check tasks 
        checkTheLevel += checkTasks;

    }

    // Start is called before the first frame update
    void Start()
    {
        // GamePaused = false;

        float dur = levelDuration * 60f; // convert level duration to minutes (i know it's technically converting to seeconds)
        levelTimer = dur;

        // fixes up veriables at the start
        isLevelCompleted = false;

        checkTheLevel();

        // Sets the total number of tasks to the number of tasks that was found in the first call
        numberOfTotalTasks = numberOfCurrentTasks;

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
        numberOfCurrentTasks += 1;

        Debug.Log("Tasks found");

    }

    // Gets called when tasks are completed
    public void taskCompleted()
    {
        // Increments the number of tasks in a level
        numberOfCurrentTasks -= 1;

        // Sanity check
        debugTotalTasks();

        // Checks if there are any more current tasks
        if (numberOfCurrentTasks <= 0)
        {
            Debug.Log("Level Done!");
        }
    }

    public void debugTotalTasks()
    {
        Debug.Log("Tasks that still need to be completed: " + numberOfCurrentTasks);
    }


}
