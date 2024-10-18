using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Interactable
{
    public override void UseItem(){
        Debug.Log("Cat");
        checkNearby();
    }


    private void checkNearby()
    {

        // Clears the list of possible colliders
        listOfPossibleColliders.Clear();

        // Gets an array of colliders that overlap a new sphere in a specific layer
        Collider[] hitColliders = Physics.OverlapCapsule(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z),
                                                         new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z),
                                                            radiusCheck, layerToLookFor);
        foreach (var hitCollider in hitColliders)
        {

            // Debug.Log("Found Something!");

            // hitCollider.gameObject.GetComponent<Pickup>();
            // Debug.Log(hitCollider.gameObject.name);

            // Checks is a game onbject has the pick up script
            if (hitCollider.gameObject.GetComponent<Energy>() != null)
            {
                EnergyHandler catEnergy = hitCollider.gameObject.GetComponent<EnergyHandler>();

                MoodHandler catMood = hitCollider.gameObject.GetComponent<MoodHandler>();
                
            }
        }
    }
}
