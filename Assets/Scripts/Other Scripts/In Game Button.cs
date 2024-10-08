using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameButton : MonoBehaviour
{

    public int id = -1;
    public float type = -1;

    [Tooltip("The position to spawn obj1 when activated")]
    public Transform spawnPos;
    public GameObject obj1;
    public GameObject obj2;

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
            case 1:
                spawnPickUpTemp();
                break;
            default:
                Debug.Log(gameObject.name + " has a bad id!!!");
                break;
        
        }

    }

    private void spawnPickUpTemp()
    {
        GameObject clone = Instantiate(obj1);
        clone.transform.position = spawnPos.position;

    }

}
