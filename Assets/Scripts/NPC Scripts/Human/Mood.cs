using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Mood affects energy loss from DOING TASKS.
 * Happy humans lose energy slower.
 * Neutral is baseline energy loss.
 * Stressed humans lose energy faster.
 * Dying humans lose energy very rapidly.
 */

[RequireComponent(typeof(HumanStates))]

public class Mood : MonoBehaviour
{
    [Header("Mood Value")]

    [Tooltip("The starting value for the human's mood.")]
    [Range(-1, 1)]
    public float moodStart;

    [Header("Thresholds")]
    
    [Tooltip("Threshold for a human to be happy while above.")]
    [Range(-1,1)]
    public float happyThreshold;

    [Tooltip("Threshold for a human to be stressed while below.")]
    [Range(-1,1)]
    public float stressedThreshold;

    // -1 <= mood <= 1
    private float mood;
    private HumanStates states;

    public bool IsHappy{
        get { return mood >= happyThreshold; }
    }

    public bool IsStressed{
        get { return mood <= stressedThreshold; }
    }

    public bool IsDying{
        get { return mood <= -1; }
    }

    void Awake(){
        mood = moodStart;
        states = GetComponent<HumanStates>();
    }

    public float GetMood()
    {
        return mood;
    }

    // returns delta
    public float ChangeMood(float delta)
    {
        // if the human is dead, don't change mood.
        if (states.IsDead)
        {
            return 0;
        }

        mood += delta;
        mood = Mathf.Clamp(mood, -1, 1);
        return delta;
    }
}
