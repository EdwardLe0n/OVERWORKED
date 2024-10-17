using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This component handles the actual changing of a human's mood.

[RequireComponent(typeof(Mood))]
[RequireComponent(typeof(HumanStates))]

public class MoodHandler : MonoBehaviour
{
    [Tooltip("Idle mood loss per second.\nSet to 0 to disable idle loss.")]
    public float idleLoss;

    [Header("Working Modifiers")]

    [Tooltip("Mood loss per second while working.\nSet to 0 to disable work affecting mood.")]
    public float workModifier;

    [Tooltip("Multiplier to the working modifier.\nSet to 0 to disable work affecting energy.")]
    public float workEffect;

    [Header("Recharging")]
    [Tooltip("value per second to recharge mood by.\nRemember mood is [-1,1]\nSet to 0 to disable recharging mood")]
    public float moodRecharge;

    private HumanStates states;
    private Mood mood;
    private float totalDelta;

    void Awake()
    {
        states = GetComponent<HumanStates>();
        mood = GetComponent<Mood>();
    }

    void Update()
    {
        // while human is in a recharging room, do not lose mood to idle loss
        if (states.isRechargingMood)
        {
            totalDelta = mood.ChangeMood(moodRecharge * Time.deltaTime);
            return;
        }

        // idle mood drain
        totalDelta = mood.ChangeMood(idleLoss * Time.deltaTime);

        // drain mood while working
        if (states.IsWorking)
        {
            totalDelta += mood.ChangeMood(workModifier * workEffect * Time.deltaTime);
        }
    }

    public float GetTotalDelta()
    {
        return totalDelta;
    }
}
