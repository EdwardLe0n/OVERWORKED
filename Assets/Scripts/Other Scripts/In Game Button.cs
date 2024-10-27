using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameButton : MonoBehaviour
{

    public int id = -1;
    public float type = -1;

    [Header("Activation")]
    [Tooltip("Effect to occur on press")]
    public Activation activateEffect;
    [Tooltip("Cooldown between activations")]
    public float cooldown;
    [Tooltip("Scalar when on cooldown")]
    public float cdScale;

    [Header("Spawning")]
    [Tooltip("The position to spawn obj1 when activated")]
    public Transform spawnPos;
    public GameObject obj1;
    public GameObject obj2;
    [Tooltip("Limit the number of items to be able to spawn total")]
    public int spawnLimit;

    public bool usesActivation;

    private bool isOffCooldown;

    private void Awake()
    {
        isOffCooldown = true;
    }

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
            case 4:
                return "activator";
            default:
                return "error";
        }
    }

    public void onTouch()
    {
        // if on cooldown, do nothing
        if (!isOffCooldown)
        {
            return;
        }

        if (usesActivation)
        {
            activateEffect.Activate();
        }
        else
        {
            switch (id)
            {
                case 1:
                    // if we reached the spawn limit, don't do anything
                    if(spawnLimit <= 0)
                    {
                        return;
                    }
                    spawnLimit--;
                    spawnPickUpTemp();
                    StartCoroutine("Cooldown");
                    break;
                default:
                    Debug.Log(gameObject.name + " has a bad id!!!");
                    break;

            }
        }

    }

    private IEnumerator Cooldown()
    {
        Debug.Log("Starting cooldown for " + transform.name);
        isOffCooldown = false;
        transform.localScale *= cdScale;
        yield return new WaitForSeconds(cooldown);
        transform.localScale /= cdScale;
        isOffCooldown = true;
    }

    public bool CanActivate()
    {
        if (usesActivation)
        {
            return activateEffect.CanActivate();
        }
        else
        {
            // Hard set to yes for now // solely for times sake
            return true;
        }
    }

    private void spawnPickUpTemp()
    {   
        // instantiate at position
        GameObject clone = Instantiate(obj1, spawnPos.position, Quaternion.identity);
        //clone.transform.position = spawnPos.position;
    }

}
