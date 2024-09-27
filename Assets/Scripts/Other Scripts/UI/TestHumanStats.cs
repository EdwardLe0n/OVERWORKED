using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestHumanStats : MonoBehaviour
{
    public Transform human;
    public TextMeshProUGUI display;

    void Update(){
        string text = "Energy: " + human.GetComponent<Energy>().GetEnergy() +
            "\nMood: " + human.GetComponent<Mood>().GetMood() +
            "\nEnergy Delta: " + human.GetComponent<EnergyHandler>().GetTotalDelta() +
            "\nMood Delta: " + human.GetComponent<MoodHandler>().GetTotalDelta();
        if (human.GetComponent<States>().IsDead)
        {
            text += "\nDead.";
            return;
        }

        if (human.GetComponent<States>().IsHappy)
        {
            text += "\nHappy!";
        }
        else if (human.GetComponent<States>().IsDying)
        {
            text += "\nDying";
        }
        else if (human.GetComponent<States>().IsStressed)
        {
            text += "\nStressed";
        }
        else
        {
            text += "\nContent";
        }

        if (human.GetComponent<States>().IsEnergized)
        {
            text += "\nEnergized!";
        }
        else if (human.GetComponent<States>().IsTired)
        {
            text += "\nTired";
        }
        else
        {
            text += "\nAwake";
        }

        if (human.GetComponent<States>().IsWorking)
        {
            text += "\nWorking";
        }

        display.text = text;
    }
}
