using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mood : MonoBehaviour
{
    /*
        Mood affects energy loss from tasks.
        Happy humans lose energy slower.
        Neutral is baseline energy loss.
        Stressed humans lose energy faster.
    */

    [Header("Thresholds")]
    
    [Tooltip("Threshold for a human to be happy while above.")]
    [Range(-1,1)]
    public float happyThreshold;

    [Tooltip("Threshold for a human to be stressed while below.")]
    [Range(-1,1)]
    public float stressedThreshold;

    // -1 <= mood <= 1
    private float mood;

    public bool IsHappy{
        get { return mood >= happyThreshold; }
    }

    public bool IsStressed{
        get { return mood <= stressedThreshold; }
    }

    void Awake(){
        mood = 1;
    }
}
