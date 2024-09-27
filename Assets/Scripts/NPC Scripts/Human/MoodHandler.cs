using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mood))]
[RequireComponent(typeof(States))]

public class MoodHandler : MonoBehaviour
{
    [Tooltip("Idle mood loss per second.\nSet to 0 to disable idle loss.")]
    public float idleLoss;

    [Header("Working Modifiers")]

    [Tooltip("Mood loss per second while working.\nSet to 0 to disable work affecting mood.")]
    public float workModifier;

    [Tooltip("Multiplier to the working modifier.\nSet to 0 to disable work affecting energy.")]
    public float workEffect;

    private States states;
    private Mood mood;
    private float totalDelta;

    void Awake()
    {
        states = GetComponent<States>();
        mood = GetComponent<Mood>();
    }

    void Update()
    {
        totalDelta = mood.ChangeMood(idleLoss * Time.deltaTime);

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
