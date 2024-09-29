using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Job))]
[RequireComponent(typeof(NavMeshAgent))]


public class HumanNav : MonoBehaviour
{
    [Tooltip("FOR TESTING")]
    public Transform testJob;

    private Job job;
    private NavMeshAgent agent;

    private void Awake()
    {
        job = GetComponent<Job>();
        agent = GetComponent<NavMeshAgent>();

        // FOR TESTING
        SetJobTarget(testJob);
    }

    public void SetJobTarget(Transform target)
    {
        job.SetStation(target);
        agent.SetDestination(target.position);
    }
}
