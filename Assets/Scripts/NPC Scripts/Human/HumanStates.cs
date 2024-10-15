using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/* Use this component to collect all the states from other human scripts.
 * If you need a state in code, get this component.
 */

[RequireComponent(typeof(Energy))]
[RequireComponent(typeof(Mood))]
[RequireComponent(typeof(Job))]
[RequireComponent(typeof(Pickup))]

public class HumanStates : MonoBehaviour
{ 
    public bool isRecharging;

    private Energy energy;
    private Mood mood;
    private Job job;
    private Pickup pickup;

    public bool IsHappy{
        get { return mood.IsHappy; }
    }

    public bool IsStressed{
        get { return mood.IsStressed; }
    }

    public bool IsDying{
        get { return mood.IsDying; }
    }

    public bool IsEnergized{
        get { return energy.IsEnergized; }
    }

    public bool IsTired{
        get { return energy.IsTired; }
    }

    public bool IsDead{
        get { return energy.IsDead; }
    }

    public bool IsWorking{
        get { return job.IsWorking(); }
    }

    public bool IsPickedUp{
        get { return pickup.currentlyHeld; }
    }

    void Awake(){
        energy = GetComponent<Energy>();
        mood = GetComponent<Mood>();
        job = GetComponent<Job>();
        pickup = GetComponent<Pickup>();
    }
}
