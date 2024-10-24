using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupSpawner : Activation
{
    [Tooltip("PlacingArea script of the cup spawner")]
    public PlacingArea cupTablePlacingArea;

    [Tooltip("Object to spawn")]
    public GameObject cup;

    public override void Activate()
    {

        // Creates a new cup
        GameObject newCup = Instantiate(cup);

        // lets this game object now that it'll be holding the game object
        cupTablePlacingArea.gameObject.GetComponent<PlacingArea>().itemGiven(newCup);

        // lets the spawned object know that it was picked up
        newCup.GetComponent<Pickup>().ItemGrabbed(cupTablePlacingArea.gameObject);

    }

    // If there is something on the table, then a new item cannot be spawned in
    public override bool CanActivate()
    {

        if (cupTablePlacingArea.gameObject.GetComponent<PlacingArea>().hasItem)
        {
            return false;
        }

        return true;

    }

}