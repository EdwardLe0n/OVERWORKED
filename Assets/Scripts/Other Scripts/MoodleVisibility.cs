using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodleVisibility : MonoBehaviour
{
    [Header("Sphere Check")]
    [Tooltip("The origin of the sphere.")]
    public Transform origin;

    [Tooltip("Radius of sphere check")]
    public float radius;

    [Tooltip("Layers to check")]
    public LayerMask layerMask;

    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(origin.position, radius, layerMask);

        foreach(Collider col in hits){
            // if collider is not a human, ignore it and move to next
            if (!col.CompareTag("Human")){
                continue;
            }
            
            col.GetComponent<HumanMoodIndicator>().SetVisible(true);
            col.GetComponent<HumanEnergyBar>().SetVisible(true);
        }
    }
}
