using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Interactable
{
    public float coffeeTime;

    public override void UseItem(){
        Debug.Log("Coffee");
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
            // AKA if the object is a human
            if (hitCollider.gameObject.GetComponent<Energy>() != null)
            {
                // the human's states
                HumanStates states = hitCollider.gameObject.GetComponent<HumanStates>();

                // the human's MoodHandler
                MoodHandler coffeeMood = hitCollider.gameObject.GetComponent<MoodHandler>();
                
                // change human mood
                coffeeMood.ChangeMood(emotional);

                // affect productivity
                Debug.Log(hitCollider.name + " has been coffeed");
                states.isCoffeed = true;

                StartCoroutine(CoffeeDecay(states));
                return;
            }
        }
    }

    IEnumerator CoffeeDecay(HumanStates states){
        // coffeeTime seconds later...
        yield return new WaitForSeconds(coffeeTime);
        // stop the buff
        Debug.Log(states.name + " has been UNcoffeed");
        states.isCoffeed = false;
    }
}
