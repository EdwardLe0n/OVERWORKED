using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HumanStates))]

public class AgentPickupHandler : MonoBehaviour
{

    // This script handles the interaction between the NavMeshAgent and picking up

    [Tooltip("The origin of the ground check")]
    public Transform groundCheck;
    [Tooltip("The radius of the ground check")]
    public float groundCheckRadius;
    [Tooltip("The layers to check for with the ground check.\nDO NOT CHANGE OFF OF ONLY WALKABLE.")]
    public LayerMask layerMask;

    private NavMeshAgent agent;
    private HumanStates states;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        states = GetComponent<HumanStates>();
    }

    private void Update()
    {
        // if picked up, disable the NavMeshAgent
        if (states.IsPickedUp)
        {
            agent.enabled = false;
            return;
        }

        // if not close enough to floor, do nothing
        if(!Physics.CheckSphere(groundCheck.position, groundCheckRadius, layerMask))
        {
            return;
        }

        //enable NavMeshAgent
        agent.enabled = true;
    }
}
