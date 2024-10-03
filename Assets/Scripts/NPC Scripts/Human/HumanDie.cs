using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(State))]

public class HumanDie : MonoBehaviour
{
    public AnimationClip deathAnim;

    private States states;

    // keeps note of if death has already been triggered.
    private bool triggeredDie;

    void Awake(){
        states = GetComponent<States>();
    }

    void Update(){
        // if already triggered dying, don't do anything.
        if(triggeredDie){
            return;
        }

        if(states.IsDead){
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die(){
        Debug.Log("Waiting for anim to end");
        yield return new WaitForSeconds(deathAnim.length);
        Debug.Log("Delete and instantiate dead human prefab");
    }
}
