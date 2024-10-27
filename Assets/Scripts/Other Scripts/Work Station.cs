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

    // Resets vars just in case
    private void Awake()
    {
        // Resets vars
        taskProgress = 0f;
        taskCompleted = false;

        // connects to the level manager check up
        LevelManager.checkTheLevel += sendInfo;

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

            // Sanity Check
            Debug.Log("Task has been completed!");

            // deconnects to the level manager check up
            LevelManager.checkTheLevel -= sendInfo;
            levelMan.GetComponent<LevelManager>().taskCompleted();
            //LevelManager.checkTheLevel();

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
