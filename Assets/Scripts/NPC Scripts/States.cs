using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Energy))]
[RequireComponent(typeof(Mood))]
public class States : MonoBehaviour
{
    /*
        Use this component to collect all the states from other human scripts.
        If you need a state in code, get this component.
    */

    [Tooltip("FOR TESTING, REMOVE WHEN WORKING IS IMPLEMENTED")]
    public bool isWorking;

    private Energy energy;
    private Mood mood;

    public bool IsHappy{
        get { return mood.IsHappy;}
    }

    public bool IsStressed{
        get { return mood.IsStressed;}
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
        get { return isWorking; }
    }

    void Awake(){
        energy = GetComponent<Energy>();
        mood = GetComponent<Mood>();
    }
}
