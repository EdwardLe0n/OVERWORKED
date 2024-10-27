using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStation : MonoBehaviour
{

    // 1 == customer interaction, 2 == converstion station, 3 == state station
    // Refilling, cleaning, etc
    public int taskType;

    // Lets humans know of the ways they can walk up to a station
    public int interactOrient;

    //Bool to track if the task is currently available
    public bool taskAvailability = false;

    // Amount of time a task will take
    public float taskTime;
    // Current progress of a given task
    public float taskProgress;

    // Bool to track if a task has been copmpleted or not
    public bool taskCompleted = false;

    public GameObject levelMan;
    public GameObject ReadyIndicator;

    public delegate void TaskComplete();
    public static event TaskComplete done;

    // bool to make see if the task is reuseable
    public bool reuseable = false;

    // public int for the handling of how many times a station can be reused
    public int reuseAmount = -1;

    // bool to see if it even adds to the tasks that need to be done
    public bool forCompletion = true;

    // Resets vars just in case
    private void Awake()
    {
        // Resets vars
        taskProgress = 0f;
        taskCompleted = false;

        // if this work is for sompletion, then it'll be hooked up to the level manager
        if (forCompletion)
        {
            // connects to the level manager check up
            LevelManager.checkTheLevel += sendInfo;
        }

    }

    public void Update(){
        if(taskAvailability){
            ReadyIndicator.SetActive(true);
        } else {
            ReadyIndicator.SetActive(false);
        }
    }

    private void OnDestroy(){
        LevelManager.checkTheLevel -= sendInfo;
    }

    private void Start(){
        if(taskType == 3){
            StartCoroutine(DirtyBathroom());
        } else if (taskType == 1){
            StartCoroutine(Customer());
        }
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
            taskAvailability = false;
            ReadyIndicator.SetActive(false);
            done.Invoke();

            // if  the station is for completion, then it'll let the level manager know
            if (forCompletion)
            {
                // checks if a job is reuseable and by the amount of times it is
                if (reuseable)
                {

                    if (reuseAmount != -1)
                    {
                        reuseAmount--;
                        if (reuseAmount == 0)
                        {
                            taskCompleted = true;
                            // deconnects to the level manager check up
                            LevelManager.checkTheLevel -= sendInfo;
                        }

                    }

                }
                else
                {
                    // deconnects to the level manager check up
                    LevelManager.checkTheLevel -= sendInfo;
                }

                // lets the level manager know that a task been completed
                levelMan.GetComponent<LevelManager>().taskCompleted();
            }
            
        }
    }

    // Dimple getter for task status
    public bool getStatus()
    {
        return taskCompleted;
    }

    // Dimple getter for task status
    public bool getAvailability()
    {
        return taskAvailability;
    }

    public void setAvailability(bool temp)
    {
        taskAvailability = temp;
    }

    // Sends info about task completion over to the level manager
    public void sendInfo()
    {
        levelMan.GetComponent<LevelManager>().recieveTaskInfo();
    }

    public IEnumerator DirtyBathroom(){
        float time = Random.Range(10, 51);
        yield return new WaitForSeconds(time);
        taskAvailability = true;
    }

    public IEnumerator Customer(){
        float time = Random.Range(20, 91);
        yield return new WaitForSeconds(time);
        taskAvailability = true;
    }

}
