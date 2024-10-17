using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// This component handles actually changing the human's energy.

[RequireComponent(typeof(Energy))]
[RequireComponent(typeof(HumanStates))]

public class EnergyHandler : MonoBehaviour
{
    [Tooltip("Idle energy loss per second.\nSet to 0 to disable idle loss.")]
    public float idleLoss;

    [Header("Mood Modifiers")]

    [Tooltip("Energy loss per second while happy.\nSet to 0 to disable happiness affecting energy.")]
    public float happyModifier;

    [Tooltip("Energy loss per second while neutral.\nSet to 0 to disable neutrality affecting energy.")]
    public float neutralModifier;

    [Tooltip("Energy loss per second while stressed.\nSet to 0 to disable stress affecting energy.")]
    public float stressedModifier;

    [Tooltip("Energy loss per second while dying.\nSet to 0 to disable dying affecting energy.")]
    public float dyingModifier;

    [Tooltip("Multiplier to the mood modifiers.\nSet to 0 to disable all moods affecting energy.")]
    public float moodEffect;

    [Header("Recharging")]
    [Tooltip("% per second to recharge energy by.\nSet to 0 to disable recharging energy")]
    public float energyRecharge;

    private HumanStates states;
    private Energy energy;
    private float totalDelta;

    void Awake(){
        states = GetComponent<HumanStates>();
        energy = GetComponent<Energy>();
    }

    void Update(){
        // while human is in a recharging room, do not lose energy to idle loss
        if (states.isRechargingEnergy)
        {
            // energy recharge is relative to max energy
            totalDelta = energy.ChangeEnergy(energy.maxEnergy * energyRecharge * Time.deltaTime);
            return;
        }

        // idle energy loss
        totalDelta = energy.ChangeEnergy(idleLoss * Time.deltaTime);

        // working energy loss
        if(states.IsWorking){
            totalDelta += energy.ChangeEnergy(MoodModifier() * moodEffect * Time.deltaTime);
        }
    }

    //Returns the appropriate MoodModifier based on state
    private float MoodModifier(){
        if(states.IsHappy){
            return happyModifier;
        }

        if (states.IsDying)
        {
            return dyingModifier;
        }

        if (states.IsStressed){
            return stressedModifier;
        }

        return neutralModifier;
    }

    public float GetTotalDelta(){
        return totalDelta;
    }
}
