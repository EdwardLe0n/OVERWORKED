using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RechargeRooms : MonoBehaviour
{
    // This script handles the recharging mechanic for humans

    [Tooltip("% per second to recharge energy by\nSet to 0 to disable recharging energy")]
    public float energyRecharge;
    [Tooltip("value per second to recharge mood by.\nRemember mood is [-1,1]\nSet to 0 to disable recharging mood")]
    public float moodRecharge;
    [Tooltip("Type of recharge room\n0: Energy\n1: Mood")]
    public int type;

    void OnTriggerEnter(Collider other){
        // if the object is not tagged as a human, don't do anything
        if(!other.CompareTag("Human"))
        {
            return;
        }
    }
}
