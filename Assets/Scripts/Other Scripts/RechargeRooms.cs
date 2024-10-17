using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RechargeRooms : MonoBehaviour
{
    // This script handles activating/deactivating recharging mechanic for humans

    [Tooltip("Type of recharge room\n0: Energy\n1: Mood")]
    public int type;

    void OnTriggerEnter(Collider other){
        // if the object is not tagged as a human, don't do anything
        if(!other.CompareTag("Human"))
        {
            return;
        }

        HumanStates states = other.GetComponent<HumanStates>();

        // checks the type and starts the appropriate recharging action
        switch(type){
            case 0:
                states.isRechargingEnergy = true;
                break;
            case 1:
                states.isRechargingMood = true;
                break;
            default:
                Debug.Log("Invalid Type");
                break;
        }
    }

    void OnTriggerExit(Collider other){
        // if the object is not tagged as human, don't do anything
        if(!other.CompareTag("Human"))
        {
            return;
        }

        HumanStates states = other.GetComponent<HumanStates>();

        // disables all recharging
        states.isRechargingEnergy = false;
        states.isRechargingMood = false;
    }
}
