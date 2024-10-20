using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cat : Interactable
{
    [Tooltip("Time humans are affected by Cat")]
    public float catTime;

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
            // AKA if the object is a human
            if (hitCollider.gameObject.GetComponent<Energy>() != null)
            {
                // the human's states
                HumanStates states = hitCollider.gameObject.GetComponent<HumanStates>();

                // the human's MoodHandler
                MoodHandler catMood = hitCollider.gameObject.GetComponent<MoodHandler>();
                
                // change human mood
                catMood.ChangeMood(emotional);

                // affect productivity
                Debug.Log(hitCollider.name + " has been catted");
                states.isCatted = true;

                
                StartCoroutine(CatDecay(states));
                return;
            }
        }
    }

    IEnumerator CatDecay(HumanStates states){
        // catTime seconds later...
        yield return new WaitForSeconds(catTime);
        // stop the debuff
        Debug.Log(states.name + " has been UNcatted");
        states.isCatted = false;
    }
}
