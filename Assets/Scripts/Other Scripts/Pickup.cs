using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float type;

    public string debugType ()
    {

        switch (type) {
            case 1:
                return "human";
            case 2:
                return "object";
            default:
                return "error";
        }
            

    }

}
