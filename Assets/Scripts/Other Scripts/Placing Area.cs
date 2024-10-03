using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingArea : MonoBehaviour
{
    public float type;

    [Header("Current Item Variables")]
    public GameObject currentPick;
    public bool hasItem = false;

    public string debugType()
    {

        switch (type)
        {
            case 1:
                return "table";
            case 2:
                return "station";
            default:
                return "error";
        }

    }

    public void itemGiven(GameObject item)
    {
        currentPick = item;
        hasItem = true;
    }

    public void itemTaken()
    {
        currentPick = null;
        hasItem = false;
    }

}
