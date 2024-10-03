using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(States))]
[RequireComponent(typeof(Pickup))]
[RequireComponent(typeof(NavMeshAgent))]

public class HumanDie : MonoBehaviour
{
    [Header("Animation")]
    [Tooltip("Animation to play upon death")]
    public AnimationClip deathAnim;
    [Tooltip("Human visual animator")]
    public Animator animator;
    
    [Header("Death")]
    [Tooltip("Prefab to instantiate on death")]
    public GameObject prefab;
    [Tooltip("Position/rotation of prefab instantiation relative to human")]
    public Transform location;

    private States states;
    private Pickup pickup;
    private NavMeshAgent agent;


    // keeps note of if death has already been triggered.
    private bool triggeredDie;

    void Awake(){
        states = GetComponent<States>();
        pickup = GetComponent<Pickup>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update(){
        // if already triggered dying, don't do anything.
        if(triggeredDie){
            return;
        }

        if(states.IsDead){
            Debug.Log(transform.name + " died");
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die(){
        triggeredDie = true;

        animator.SetBool("died", true);

        // prevent human from walking
        agent.enabled = false;

        // if human is currently picked up, will force the player to drop them to play animation.
        if(pickup.IsPickedUp){
            pickup.currentHolder.GetComponent<PlayerController>().BasicDrop();
        }

        Debug.Log("Waiting for anim to end");
        yield return new WaitForSeconds(deathAnim.length);
        Debug.Log("Delete and instantiate dead human prefab");

        // transform references transform this script is on
        Instantiate(prefab, location.position, location.rotation);

        // destroy self
        Destroy(transform.gameObject);
    }
}
