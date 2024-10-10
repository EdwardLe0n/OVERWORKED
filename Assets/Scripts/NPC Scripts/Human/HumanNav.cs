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

    // Checks for elements in a certain radius listed here
    public float radiusCheck = .5f;

    // Checks for elements in a certain layer listed here
    public LayerMask layerToLookFor;

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
        job.SetJobStation(target);

        // if there is a target, then the code will make sure the navmesh gets a human there
        if (target != null)
        {
            agent.SetDestination(target.position);
            agent.isStopped = false;
        }
        else
        {
            // If null was passed in, the nav mesh will stop
            agent.isStopped = true;
        }
        
    }

    public void Update()
    {
        // will only check if the human is not currently working
        if (!job.IsWorking())
        {

            // Gets an array of colliders that overlap a new sphere in a specific layer
            Collider[] hitColliders = Physics.OverlapCapsule(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z),
                                                             new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z),
                                                                radiusCheck, layerToLookFor);
            
            // loops through all the colliders found on a certain layer in an effort to find a work station
            foreach (var hitCollider in hitColliders)
            {
                // Once a station is found, then it's set as the next target
                if (hitCollider.GetComponent<WorkStation>() != null)
                {
                    // If the job hasn't been completed, then and only then will it be chosen as a target
                    if (!hitCollider.GetComponent<WorkStation>().getStatus())
                    {
                        // this is why the human is walking into the workbench.
                        // set to a separate transform on the workbench prefab where the human stands.
                        SetJobTarget(hitCollider.transform);
                        return;
                    }
                }
            }

        }
    }

}
