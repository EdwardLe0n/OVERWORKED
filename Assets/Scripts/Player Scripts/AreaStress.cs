using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaStress : MonoBehaviour
{
    [Tooltip("The collider to check within.")]
    public SphereCollider area;

    [Tooltip("Stress per second maximum")]
    public float rate;

    // this is as a decimal 0-1
    private float distance;

    void Update(){
        
    }
}
