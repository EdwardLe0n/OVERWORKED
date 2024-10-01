using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingArea : MonoBehaviour
{
    public float type;

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

}
