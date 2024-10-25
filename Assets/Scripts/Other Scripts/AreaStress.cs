using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AreaStress : MonoBehaviour
{
    [Header("Sphere Check")]
    [Tooltip("The origin of the sphere.")]
    public Transform origin;

    [Tooltip("Radius of sphere check")]
    public float radius;

    [Tooltip("Layers to check")]
    public LayerMask layerMask;

    [Header("Stress")]
    [Tooltip("Mood per second loss")]
    public float rate;

    // this is as a decimal 0-1
    private float factor;

    void Update(){
        Collider[] hits = Physics.OverlapSphere(origin.position, radius, layerMask);

        foreach(Collider col in hits){
            // if collider is not a human, ignore it and move to next
            if (!col.CompareTag("Human")){
                continue;
            }

            factor = Vector3.Distance(origin.position, col.transform.position) / radius;
            factor = 1 - factor;
            Debug.Log(col.name + ": " + factor);

            MoodHandler mood = col.GetComponent<MoodHandler>();
            mood.ChangeMood(rate * factor * Time.deltaTime);
        }
    }
}
