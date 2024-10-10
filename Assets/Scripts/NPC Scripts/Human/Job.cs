using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This component handles the job of the human.
 * Do not use this to set job station. Use HumanNav
 */

[RequireComponent(typeof(Energy))]

public class Job : MonoBehaviour
{
    private Transform jobStation;

    public bool IsWorking()
    {
        //checks for jobstation being null and if the human is close enough to the job
        return jobStation != null && Vector3.Distance(transform.position, jobStation.position) <= 1; 
    }

    private void Update()
    {
        if (jobStation != null)
        {
            // if the human is classified as working, it will let the job station know that it's currently getting completed
            // by a human with energy dependent on their status
            if (IsWorking() && !jobStation.GetComponent<WorkStation>().getStatus())
            {
                jobStation.GetComponent<WorkStation>().attemptingTask(this.GetComponent<Energy>().GetEnergy() * 0.00001f);
            }
            else if (jobStation.GetComponent<WorkStation>().getStatus())
            {
                this.GetComponent<HumanNav>().SetJobTarget(null);
            }
        }
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
