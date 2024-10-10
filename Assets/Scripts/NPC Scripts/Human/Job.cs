using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This component handles the job of the human.
 * Do not use this to set job station. Use HumanNav
 */

public class Job : MonoBehaviour
{
    private Transform jobStation;

    public bool IsWorking
    {
        //checks for jobstation being null and if the human is close enough to the job
        get { return jobStation != null && Vector3.Distance(transform.position, jobStation.position) <= 0.1; }
    }

    public void SetJobStation(Transform station)
    {
        jobStation = station;
    }

    public Transform GetJobStation()
    {
        return jobStation;
    }
}
