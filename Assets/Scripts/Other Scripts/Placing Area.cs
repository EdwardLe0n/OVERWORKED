using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacingArea : MonoBehaviour
{
    public float type;

    [Header("Current Item Variables")]
    public GameObject currentPick;
    public bool hasItem = false;

    [Header("Station Specific Timers")]
    public bool inProgress = false;
    public float timeRunning = 0.0f;
    public float targetTime = 3f;

    public Material tempMaterial;

    public string debugType()
    {

        switch (type)
        {
            case 1:
                return "table";
            case 2:
                return "station";
            default:
                return "error";
        }

    }

    public void Update()
    {
        // If there is a job to be done
        if (inProgress)
        {
            // The update function will increment the run time of a project
            timeRunning += Time.deltaTime;

            // And once the times up, the code will do something!!!
            if (timeRunning >= targetTime)
            {

                // temp line, make this a general do something function later
                currentPick.gameObject.GetComponent<MeshRenderer>().material = tempMaterial;
                ResetTimer();

            }

        }
    }

    public void itemGiven(GameObject item)
    {
        currentPick = item;
        hasItem = true;
        if (type == 2)
        {
            StartTimer();
        }
    }

    public void itemTaken()
    {
        currentPick = null;
        hasItem = false;

        if (type == 2)
        {
            ResetTimer();
        }

    }

    public void StartTimer()
    {
        inProgress = true;
        timeRunning = 0.0f;
    }

    public void ResetTimer()
    {
        inProgress = false;
    }

}
