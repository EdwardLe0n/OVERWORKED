using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class Energy : MonoBehaviour
{
    /*
        Energy is the lifeblood of a human. Also affects task speed.
        Energized humans complete tasks faster.
        Neutral is baseline task completion.
        Tired humans complete tasks slower.
        Energy also slowly drains naturally over time.
        If energy ever gets to 0, the human dies.
    */

    [Header("Energy values")]
    [Tooltip("The maximum value for the energy bar.")]
    public float maxEnergy;

    [Header("Thresholds")]

    [Tooltip("% Threshold for a human to be energized while above.")]
    [Range(0,1)]
    public float energizedThreshold;

    [Tooltip("% Threshold for a human to be tired while below.")]
    [Range(0,1)]
    public float tiredThreshold;

    // 0 <= energy <= maxEnergy
    private float energy;

    public bool IsEnergized{
        get { return energy/maxEnergy >= energizedThreshold; }
    }

    public bool IsTired{
        get { return energy/maxEnergy <= tiredThreshold; }
    }

    public bool IsDead{
        get { return energy <= 0;}
    }

    void Awake(){
        energy = maxEnergy;
    }

    public float GetEnergy(){
        return energy;
    }

    public void ChangeEnergy(float delta){
        // if the human is dead, don't change energy.
        if(IsDead){
            return;
        }
        
        energy += delta;
        Mathf.Clamp(energy, 0, maxEnergy);
    }
}
