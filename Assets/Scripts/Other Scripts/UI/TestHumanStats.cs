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
        if (human.GetComponent<HumanStates>().IsDead)
        {
            text += "\nDead.";
            display.text = text;
            return;
        }

        if (human.GetComponent<HumanStates>().IsHappy)
        {
            text += "\nHappy!";
        }
        else if (human.GetComponent<HumanStates>().IsDying)
        {
            text += "\nDying";
        }
        else if (human.GetComponent<HumanStates>().IsStressed)
        {
            text += "\nStressed";
        }
        else
        {
            text += "\nContent";
        }

        if (human.GetComponent<HumanStates>().IsEnergized)
        {
            text += "\nEnergized!";
        }
        else if (human.GetComponent<HumanStates>().IsTired)
        {
            text += "\nTired";
        }
        else
        {
            text += "\nAwake";
        }

        if (human.GetComponent<HumanStates>().IsWorking)
        {
            text += "\nWorking";
        }

        display.text = text;
    }
}
