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

    public float ChangeMood(float delta){
        return mood.ChangeMood(delta);
    }
}
