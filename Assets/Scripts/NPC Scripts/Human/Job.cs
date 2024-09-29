using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(States))]

public class Job : MonoBehaviour
{
    private States states;
    private Transform jobStation;

    public bool IsWorking
    {
        //checks for jobstation being null and if the human is close enough to the job
        get { return jobStation != null && Vector3.Distance(transform.position, jobStation.position) <= 0.1; }
    }

    private void Awake()
    {
        states = GetComponent<States>();
    }

    public void SetStation(Transform station)
    {
        jobStation = station;
    }

    public Transform GetJobStation()
    {
        return jobStation;
    }
}
