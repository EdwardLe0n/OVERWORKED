using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* This component handles the job of the human.
 * Do not use this to set job station. Use HumanNav
 */

[RequireComponent(typeof(HumanStates))]
[RequireComponent(typeof(HumanNav))]
[RequireComponent(typeof(NavMeshAgent))]

public class Job : MonoBehaviour
{
    [Header("Job Stuff")]
    [Tooltip("How quickly the human completes task at base")]
    public float jobSpeed;
    [Tooltip("Multiplier to jobSpeed when energized.\nSet to 1 to disable happy multiplier.")]
    public float energizedMultiplier;
    [Tooltip("Multiplier to jobSpeed when neutral.\nSet to 1 to disable neutral multiplier.")]
    public float neutralMultiplier;
    [Tooltip("Multiplier to jobSpeed when tired.\nSet to 1 to disable tired multiplier.")]
    public float tiredMultiplier;

    [Header("Wandering")]
    [Tooltip("The maximum distance a human should wander per wander call")]
    public float maxDistance;
    [Tooltip("The minimum distance a human should wander per wander call")]
    public float minDistance;
    [Tooltip("The max time between wander calls")]
    public float maxInterval;
    [Tooltip("The min time between wander calls")]
    public float minInterval;

    private Transform jobStation;

    private HumanStates states;
    private HumanNav humanNav;
    private NavMeshAgent agent;
    private float wanderTimer;

    void Awake(){
        states = GetComponent<HumanStates>();
        humanNav = GetComponent<HumanNav>();
        agent = GetComponent<NavMeshAgent>();

        wanderTimer = Random.Range(minInterval, maxInterval);
    }

    public bool IsWorking()
    {
        //checks for jobstation being null and if the human is close enough to the job
        return jobStation != null && Vector3.Distance(transform.position, jobStation.position) <= 1; 
    }

    private void Update()
    {
        // don't do any job things while picked up
        if(states.IsPickedUp){
            return;
        }

        // if no job station
        if (jobStation == null)
        {
            // decrease timer
            wanderTimer -= Time.deltaTime;

            if(wanderTimer <= 0){
                Wander();
            }
            
            return;
        }

        // if the job station is a test transform, do nothing
        if(jobStation.GetComponent<WorkStation>() == null)
        {
            return;
        }

        // if the human is classified as working, it will let the job station know that it's currently getting completed
        // by a human with energy dependent on their status
        if (IsWorking() && !jobStation.GetComponent<WorkStation>().getStatus() && jobStation.GetComponent<WorkStation>().getAvailability())
        {
            jobStation.GetComponent<WorkStation>().attemptingTask(jobSpeed * EnergyModifier() * Time.deltaTime);
        }
        else if (jobStation.GetComponent<WorkStation>().getStatus())
        {
            humanNav.SetJobTarget(null);
        }
    }

    private void Wander(){
        Vector3 randomPos = RandomPos();

        agent.SetDestination(randomPos);

        wanderTimer = Random.Range(minInterval, maxInterval);
    }

    private Vector3 RandomPos(){
        float x = Random.Range(-1,1);
        float z = Random.Range(-1,1);

        // rand is a direction
        Vector3 rand = new Vector3(x, transform.position.y, z);
        rand.Normalize();

        // multiply by a random distance
        rand *= Random.Range(minDistance, maxDistance);

        return rand;
    }

    // returns the correct energy multiplier based on energy level
    public float EnergyModifier(){
        if(states.isCoffeed){
            return energizedMultiplier;
        }
        
        if(states.isCatted){
            return tiredMultiplier;
        }
        
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
