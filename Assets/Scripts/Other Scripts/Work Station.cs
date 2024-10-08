using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStation : MonoBehaviour
{

    // Refilling, cleaning, etc
    public int taskType;

    // Lets humans know of the ways they can walk up to a station
    public int interactOrient;

    // Amount of time a task will take
    public float taskTime;
    // Current progress of a given task
    public float taskProgress;

    // Bool to track if a task has been copmpleted or not
    public bool taskCompleted = false;

    // Resets vars just in case
    private void Awake()
    {
        taskProgress = 0f;
        taskCompleted = false;
    }

    // Increments the task dependent on the given float
    public void attemptingTask(float someVal)
    {

        taskProgress += someVal;

        updateStatus();

    }

    // updates the status if a task has completed, then will tell the level manager as well!
    public void updateStatus()
    {
        if (taskProgress >= taskTime)
        {
            taskCompleted = true;

            // Sanity Check
            Debug.Log("Task has been completed!");

        }
    }

    // Dimple getter for task status
    public bool getStatus()
    {
        return taskCompleted;
    }

}
