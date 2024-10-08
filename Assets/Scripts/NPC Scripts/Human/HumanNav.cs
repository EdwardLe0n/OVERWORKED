using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* This component handles the movement and job setting of the human
 * Use this when setting job station.
 */

[RequireComponent(typeof(Job))]
[RequireComponent(typeof(NavMeshAgent))]

public class HumanNav : MonoBehaviour
{
    [Tooltip("FOR TESTING")]
    public Transform testJob;

    private Job job;
    private NavMeshAgent agent;

    public bool debug = true;

    private void Awake()
    {
        job = GetComponent<Job>();
        agent = GetComponent<NavMeshAgent>();

        // FOR TESTING
        if (debug)
        {
            SetJobTarget(testJob);
        }
    }

    public void SetJobTarget(Transform target)
    {
        job.SetStation(target);
        agent.SetDestination(target.position);
    }
}
