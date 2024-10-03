using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameButton : MonoBehaviour
{

    public int id = -1;
    public float type = -1;

    public string debugType()
    {

        switch (type)
        {
            case 1:
                return "noise-maker";
            case 2:
                return "spawner";
            case 3:
                return "level editor";
            default:
                return "error";
        }
    }

    public void onTouch()
    {

        switch (id) 
        {

            default:
                Debug.Log(gameObject.name + "has a bad id!!!");
                break;
        
        }

    }

}
