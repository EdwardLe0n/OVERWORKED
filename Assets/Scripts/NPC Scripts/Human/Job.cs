using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This component handles the job of the human.
 * Do not use this to set job station. Use HumanNav
 */

[RequireComponent(typeof(HumanStates))]
[RequireComponent(typeof(HumanNav))]

public class Job : MonoBehaviour
{
    [Tooltip("How quickly the human completes task at base")]
    public float jobSpeed;
    [Tooltip("Multiplier to jobSpeed when energized.\nSet to 1 to disable happy multiplier.")]
    public float energizedMultiplier;
    [Tooltip("Multiplier to jobSpeed when neutral.\nSet to 1 to disable neutral multiplier.")]
    public float neutralMultiplier;
    [Tooltip("Multiplier to jobSpeed when tired.\nSet to 1 to disable tired multiplier.")]
    public float tiredMultiplier;

    private Transform jobStation;

    private HumanStates states;
    private HumanNav humanNav;

    void Awake(){
        states = GetComponent<HumanStates>();
        humanNav = GetComponent<HumanNav>();
    }

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
                jobStation.GetComponent<WorkStation>().attemptingTask(jobSpeed * EnergyModifier() * Time.deltaTime);
            }
            else if (jobStation.GetComponent<WorkStation>().getStatus())
            {
                humanNav.SetJobTarget(null);
            }
        }
    }

    // returns the correct energy multiplier based on energy level
    private float EnergyModifier(){
        if(states.IsEnergized){
            return energizedMultiplier;
        }

        if (states.IsTired){
            return tiredMultiplier;
        }

        return neutralMultiplier;
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
